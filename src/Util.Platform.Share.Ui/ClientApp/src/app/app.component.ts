import { Component,Injector } from '@angular/core';
import { environment } from 'environment';
import { ComponentBase } from 'util-angular';

@Component({
    selector: 'app-root',
    templateUrl: environment.production ? './index.html' : '/view/src/app/index',
    styleUrls: ['./app.component.less']
})
export class AppComponent extends ComponentBase{

    constructor(injector: Injector) {
        super(injector);
    }

    click(application) {
        this.util.message.success(application.name);
    }
}
