import { Component, Injector, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { ComponentBase } from "util-angular";
import { ApplicationModel } from './application-model';

/**
 * 选择应用程序组件
 */
@Component({
    selector: 'platform-application-select',
    templateUrl: './html/index.html'
})
export class ApplicationSelectComponent extends ComponentBase implements OnInit {
    /**
     * 应用程序列表
     */
    @Input() list: ApplicationModel[];
    /**
     * 当前应用程序
     */
    selected: ApplicationModel;
    /**
     * 单击事件
     */
    @Output() onClick = new EventEmitter<ApplicationModel>();

    /**
     * 初始化选择应用程序
     * @param injector 注入器
     */
    constructor(injector: Injector) {
        super(injector);
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
        this.selected = item;
        this.onClick.emit(item);
    }
}