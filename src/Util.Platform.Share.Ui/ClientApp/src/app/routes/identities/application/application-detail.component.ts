import { Component,ChangeDetectionStrategy, Injector } from '@angular/core';
import { environment } from "@env/environment";
import { EditComponentBase } from "util-angular";
import { ApplicationViewModel } from './model/application-view-model';

/**
* 应用程序详情页
*/
@Component( {
    selector: 'application-detail',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './html/application-detail.component.html'
} )
export class ApplicationDetailComponent extends EditComponentBase<ApplicationViewModel> {
    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "application";
    }
}