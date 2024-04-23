import { Component,ChangeDetectionStrategy, Injector } from '@angular/core';
import { environment } from "@env/environment";
import { EditComponentBase } from "util-angular";
import { ApplicationViewModel } from './model/application-view-model';

/**
 * 应用程序编辑页
 */
@Component( {
    selector: 'application-edit',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './html/application-edit.component.html'
} )
export class ApplicationEditComponent extends EditComponentBase<ApplicationViewModel> {
    /**
     * 创建模型
     */
    protected createModel() {
        let result = new ApplicationViewModel();
        return result;
    }

    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "application";
    }
}