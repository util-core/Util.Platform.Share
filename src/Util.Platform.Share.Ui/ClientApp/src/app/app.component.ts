import { Component,Injector } from '@angular/core';
import { environment } from 'environment';
import { ComponentBase } from 'util-angular';

/**
 * 模块参数
 */
export class ModuleViewModel {
    /**
     * 应用程序标识
     */
    applicationId;
    /**
     * 应用程序名称
     */
    applicationName;
    /**
     * 模块名称
     */
    name;
    /**
     * 多语言键名
     */
    i18n;
    /**
     * 模块地址
     */
    uri;
    /**
     * 图标
     */
    icon;
    /**
     * 是否隐藏
     */
    isHide;
    /**
     * 备注
     */
    remark;
    /**
     * 创建时间
     */
    creationTime;
}

@Component({
    selector: 'app-root',
    templateUrl: environment.production ? './index.html' : '/view/src/app/index',
    styleUrls: ['./app.component.less']
})
export class AppComponent extends ComponentBase{

    model: ModuleViewModel;

    /**
     * 选择图标
     */
    selectedIcon(icon) {
        this.model.icon = icon;
    }

    constructor(injector: Injector) {
        super(injector);
        this.model = new ModuleViewModel();
    }
}
