import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared';
import { IdentityRoutingModule } from './identity-routing.module';

import { ApplicationListComponent } from './application/application-list.component';
import { ApplicationEditComponent } from './application/application-edit.component';
import { ApplicationDetailComponent } from './application/application-detail.component';
    
/**
* identity模块
*/
@NgModule( {
    declarations: [
      ApplicationListComponent,ApplicationEditComponent,ApplicationDetailComponent
    ],
    imports: [
        SharedModule,IdentityRoutingModule
    ],
    providers: [
    ]
} )
export class IdentityModule {
}
