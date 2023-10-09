import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { AlainThemeModule } from '@delon/theme';
import { ApplicationSelectComponent } from './application-select.component';

/**
 * 应用程序选择模块
 */
@NgModule({
    declarations: [ApplicationSelectComponent],
    exports: [ApplicationSelectComponent],
    imports: [
        CommonModule,
        NzCardModule,
        NzMenuModule,
        AlainThemeModule.forChild()
    ]
})
export class ApplicationSelectModule {
}