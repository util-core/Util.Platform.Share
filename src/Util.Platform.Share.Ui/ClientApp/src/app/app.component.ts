import { Component, OnInit, inject,Injector } from '@angular/core';
import { NavigationEnd, NavigationError, RouteConfigLoadStart, Router, RouterOutlet } from '@angular/router';
import { TitleService, stepPreloader } from '@delon/theme';
import { environment } from '@env/environment';
import { Util,ComponentBase } from 'util-angular';

/**
 * 应用根组件
 */
@Component({
    selector: 'app-root',
    template: `<router-outlet />`,
    standalone: true,
    imports: [RouterOutlet]
})
export class AppComponent extends ComponentBase implements OnInit {
    private readonly router = inject(Router);
    private readonly titleService = inject(TitleService);
    private donePreloader = stepPreloader();

    /**
     * 初始化应用根组件
     */
    constructor() {
        super();
        Util.init();
    }

    /**
     * 初始化
     */
    ngOnInit() {
        let configLoad = false;
        this.router.events.subscribe(event => {
            if (event instanceof RouteConfigLoadStart)
                configLoad = true;
            if (configLoad && event instanceof NavigationError)
                this.util.message.error("启动失败");
            if (event instanceof NavigationEnd) {
                this.donePreloader();
                this.titleService.setTitle();
            }
        });
    }
}