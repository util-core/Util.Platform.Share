import { provideHttpClient,withInterceptorsFromDi,withInterceptors } from '@angular/common/http';
import { default as ngLang } from '@angular/common/locales/zh';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter, withComponentInputBinding, withInMemoryScrolling, withHashLocation, RouterFeatures, withViewTransitions } from '@angular/router';
import { I18NService, provideStartup } from '@core';
import { AlainProvideLang, provideAlain, zh_CN as delonLang } from '@delon/theme';
import { AlainConfig } from '@delon/util/config';
import { provideReuseTabConfig } from '@delon/abc/reuse-tab';
import { environment } from '@env/environment';
import { zhCN as dateLang } from 'date-fns/locale';
import { NzConfig, provideNzConfig } from 'ng-zorro-antd/core/config';
import { zh_CN as zorroLang } from 'ng-zorro-antd/i18n';
import { IconDefinition } from '@ant-design/icons-angular';
import * as AllIcons from '@ant-design/icons-angular/icons';
import { AppConfig, languageInterceptorFn } from 'util-angular';
import { utilConfig } from './util.config';
import { routes } from '../routes/routes';
import { authConfig, authModuleConfig } from './auth.config';
import { AuthConfig, provideOAuthClient } from 'angular-oauth2-oidc';

//默认语言设置
const defaultLang: AlainProvideLang = {
    abbr: 'zh-CN',
    ng: ngLang,
    zorro: zorroLang,
    date: dateLang,
    delon: delonLang
};

//ng alain设置
const alainConfig: AlainConfig = {
    pageHeader: { homeI18n: 'home' }
};

//ng zorro设置
const ngZorroConfig: NzConfig = {};

//路由设置
const routerFeatures: RouterFeatures[] = [
    withComponentInputBinding(),
    withViewTransitions(),
    withInMemoryScrolling({ scrollPositionRestoration: 'top' })
];
if (environment.useHash)
    routerFeatures.push(withHashLocation());

//图标设置
const antDesignIcons = AllIcons as {
    [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key]);

/**
 * 应用配置
 */
export const appConfig: ApplicationConfig = {
    providers: [
        provideHttpClient(withInterceptorsFromDi(), withInterceptors([languageInterceptorFn])),
        provideAnimations(),
        provideRouter(routes, ...routerFeatures),
        provideAlain({ config: alainConfig, defaultLang, i18nClass: I18NService, icons: icons }),
        provideNzConfig(ngZorroConfig),        
        provideReuseTabConfig(),
        provideOAuthClient(authModuleConfig),
        { provide: AuthConfig, useValue: authConfig },
        { provide: AppConfig, useValue: utilConfig },
        provideStartup(),
        ...(environment.providers || [])
    ]
};
