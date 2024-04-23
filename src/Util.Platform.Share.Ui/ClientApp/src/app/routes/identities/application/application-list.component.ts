import { Component,ChangeDetectionStrategy, Injector } from '@angular/core';
import { environment } from "@env/environment";
import { TableQueryComponentBase } from "util-angular";
import { ApplicationQuery } from './model/application-query';
import { ApplicationViewModel } from './model/application-view-model';
import { ApplicationEditComponent } from './application-edit.component';
import { ApplicationDetailComponent } from './application-detail.component';

/**
* 应用程序列表页
*/
@Component( {
    selector: 'application-list',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './html/application-list.component.html'
} )
export class ApplicationListComponent extends TableQueryComponentBase<ApplicationViewModel, ApplicationQuery> {
    /**
     * 获取创建组件
     */
    getCreateComponent() {
        return ApplicationEditComponent;
    }

    /**
     * 获取详情组件
     */
    getDetailComponent() {
        return ApplicationDetailComponent;
    }
    
    /**
     * 获取创建标题
     */
    getCreateTitle() {
        return 'permissions.application.create';
    }

    /**
     * 获取更新标题
     */
    getEditTitle() {
        return 'permissions.application.update';
    }
        
    /**
     * 获取详情标题
     */
    getDetailTitle() {
        return 'permissions.application.detail';
    }
}