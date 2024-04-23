import { Component, Injector, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy,importProvidersFrom } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { AlainThemeModule } from '@delon/theme';
import { ComponentBase, SelectExtendDirective, SelectItem } from "util-angular";
import { ApplicationModel } from './application-model';

/**
 * 选择应用程序组件
 */
@Component({
    selector: 'platform-application-select',
    templateUrl: './html/application-select.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        CommonModule, FormsModule, NzFormModule, NzCardModule, NzMenuModule,
        NzSelectModule, AlainThemeModule, SelectExtendDirective
    ]
})
export class ApplicationSelectComponent extends ComponentBase implements OnInit {    
    /**
     * 列表项集合
     */
    items: SelectItem[];
    /**
     * 当前应用程序
     */
    selected: ApplicationModel;
    /**
     * 选择值
     */
    selectValue;
    /**
     * 应用程序列表
     */
    @Input() list: ApplicationModel[];
    /**
     * 是否隐藏
     */
    @Input() hidden: boolean;
    /**
     * 单击事件
     */
    @Output() onClick = new EventEmitter<ApplicationModel>();

    /**
     * 初始化选择应用程序
     */
    constructor() {
        super();
        this.list = new Array<ApplicationModel>();
    }

    /**
     * 初始化
     */
    ngOnInit() {
        if (this.list && this.list.length > 0)
            return;
        this.loadApplications();
    }

    /**
     * 加载应用程序列表
     */
    loadApplications() {
        let url = "application/enabled";
        this.util.webapi.get<ApplicationModel[]>(url).loading().handle({
            ok: result => {
                this.list = result;
                this.items = this.list.map(item => new SelectItem(item.name, item.id));
                this.selectApplication();
            }
        });
    }

    /**
     * 选择当前应用程序
     */
    private selectApplication() {
        if (!this.list || this.list.length === 0)
            return;
        this.clickItem(this.list[0]);
    }

    /**
     * 单击
     */
    clickItem(item) {
        this.selected = this.getItem(item);
        this.onClick.emit(this.selected);
        this.util.changeDetector.markForCheck();
    }

    /**
     * 获取项
     */
    private getItem(item) {
        if (!item)
            return null;
        if (item.id)
            return item;
        return this.list.find(t => t.id === item);
    }
}