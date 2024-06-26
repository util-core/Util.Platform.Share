//============== 图标选择组件 ==========================
// 本组件由杨柳(https://github.com/willowlau)从 ng-ant-admin(https://github.com/huajian123/ng-ant-admin) 整理,特此感谢
//Licensed under the MIT license
//======================================================
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, Output, EventEmitter, AfterViewInit, inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzPopoverModule } from "ng-zorro-antd/popover";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzCardModule } from "ng-zorro-antd/card";
import { NgFor, NgIf, NgStyle } from "@angular/common";
import { NzEmptyModule } from "ng-zorro-antd/empty";
import { NzPaginationModule } from "ng-zorro-antd/pagination";
import { NzToolTipModule } from "ng-zorro-antd/tooltip";
import { FormsModule } from "@angular/forms";
import { zorroIcons } from "./zorro-icons";

interface IconItem {
    icon: string;
    isChecked: boolean;
}

/**
 * 图标选择组件
 */
@Component({
    selector: 'platform-icon-select',
    templateUrl: './html/icon-select.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [NzIconModule, NzButtonModule, NzPopoverModule, NzInputModule, NzCardModule, NgIf, NgFor, NgStyle,
        NzEmptyModule, NzPaginationModule, NzToolTipModule, FormsModule]
})
export class IconSelectComponent implements OnInit, AfterViewInit {    
    @Input() columns = 10;
    @Input() rows = 10;
    @Input() pageSize = 100;
    @Input() pageSizeOptions = [100, 200, 500];
    visible = false;
    pageIndex = 1;
    cardWidth = 350;
    cardHeight = 270;
    iconSpan = 20;
    searchValue: any;
    private searchText$ = new Subject<string>();
    @Input() selectedIcon = '';
    @Output() readonly onSelectIcon = new EventEmitter<string>();
    iconsStrAllArray: IconItem[] = [];
    sourceIconsArray: IconItem[] = [];
    iconsStrShowArray: IconItem[] = [];
    gridStyle = {
        width: this.iconSpan + '%'
    };
    destroyRef = inject(DestroyRef);

    fnKebabCase(value: string): string {
        return value
            .replace(/([a-z])([A-Z])/g, '$1-$2')
            .replace(/([0-9])([a-zA-Z]+)$/g, '-$1-$2')
            .replace(/[\s_]+/g, '-')
            .toLowerCase();
    };

    constructor(private cdr: ChangeDetectorRef) {
        zorroIcons.forEach(item => {
            this.sourceIconsArray.push({ icon: this.fnKebabCase(item), isChecked: false });
        });
        this.iconsStrAllArray = JSON.parse(JSON.stringify(this.sourceIconsArray));
    }

    camelCase(value: string): string {
        return value.replace(/-\w/g, (_r, i) => value.charAt(i + 1).toUpperCase());
    }

    upperCamelCase(value: string): string {
        const camelCased = this.camelCase(value);
        return camelCased.charAt(0).toUpperCase() + camelCased.slice(1);
    }   

    searchIcon(value) {
        this.searchText$.next(value);
    }

    selectIcon(item: IconItem): void {
        this.selectedIcon = item.icon;
        this.sourceIconsArray.forEach(icon => (icon.isChecked = false));
        this.iconsStrShowArray.forEach(icon => (icon.isChecked = false));
        this.iconsStrAllArray.forEach(icon => (icon.isChecked = false));
        item.isChecked = true;
        this.visible = false;
        this.onSelectIcon.emit(item.icon);
    }

    pageSizeChange(event: number): void {
        this.pageSize = event ;
        this.getData(1);
    }

    getData(event: number = this.pageIndex): void {
        this.pageIndex = event;
        this.iconsStrShowArray = [...this.iconsStrAllArray.slice((this.pageIndex - 1) * this.pageSize, this.pageIndex * this.pageSize)];
        this.cdr.markForCheck();
    }

    ngOnInit(): void {
        this.cardWidth = 66.8 * this.columns;
        this.cardHeight = 72 * this.rows;
        this.iconSpan = (100 / this.columns);
        this.getData();
    }

    ngAfterViewInit(): void {
        this.searchText$.pipe(
            debounceTime(200),
            distinctUntilChanged(),
            takeUntilDestroyed(this.destroyRef)
        ).subscribe(res => {
            if (!res) {
                this.iconsStrAllArray = [...this.sourceIconsArray];
            }
            else {
                this.iconsStrAllArray = this.sourceIconsArray.filter(item => item.icon.includes(res));
            }            
            this.getData();
            this.cdr.markForCheck();
        });
    }
}
