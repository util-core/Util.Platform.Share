import { NgModule, Injector } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { zh_CN } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import zh from '@angular/common/locales/zh';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ShareModule } from "./share.module";
import { AlainThemeModule } from '@delon/theme';

registerLocaleData(zh);

// #region 语言设置

import { default as ngLang } from '@angular/common/locales/zh';
import { zhCN as dateLang } from 'date-fns/locale';
import { NZ_DATE_LOCALE, zh_CN as zorroLang } from 'ng-zorro-antd/i18n';
import { DELON_LOCALE, zh_CN as delonLang } from '@delon/theme';
const LANG = {
    abbr: 'zh',
    ng: ngLang,
    zorro: zorroLang,
    date: dateLang,
    delon: delonLang,
};
registerLocaleData(LANG.ng, LANG.abbr);
const LANG_PROVIDES = [
    { provide: NZ_I18N, useValue: LANG.zorro },
    { provide: NZ_DATE_LOCALE, useValue: LANG.date },
    { provide: DELON_LOCALE, useValue: LANG.delon }
];
// #endregion

// #region 图标设置
import { IconDefinition } from '@ant-design/icons-angular';
import { NzIconModule } from 'ng-zorro-antd/icon';
import * as AllIcons from '@ant-design/icons-angular/icons';

const antDesignIcons = AllIcons as {
    [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key])
// #endregion

import { Util, AppConfig } from 'util-angular';
import { appConfig } from './app-config';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        NzIconModule.forRoot(icons),
        AlainThemeModule.forChild(),
        ShareModule
    ],
    providers: [
        ...LANG_PROVIDES,
        { provide: NZ_I18N, useValue: zh_CN },
        { provide: AppConfig, useValue: appConfig }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    /**
     * 初始化应用根模块
     * @param injector 注入器
     */
    constructor(injector: Injector) {
        Util.init(injector);
    }
}