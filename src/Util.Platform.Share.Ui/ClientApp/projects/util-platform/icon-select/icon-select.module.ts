//============== 图标选择模块 ==========================
// 本组件由杨柳(https://github.com/willowlau)贡献,特此感谢
//Licensed under the MIT license
//======================================================

import { NgModule } from '@angular/core';
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzPopoverModule } from "ng-zorro-antd/popover";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzCardModule } from "ng-zorro-antd/card";
import { NgFor, NgIf, NgStyle } from "@angular/common";
import { NzEmptyModule } from "ng-zorro-antd/empty";
import { NzPaginationModule } from "ng-zorro-antd/pagination";
import { NzToolTipModule } from "ng-zorro-antd/tooltip";
import { IconSelectComponent } from "./icon-select.component";

/**
 * 图标选择模块
 */
@NgModule({
    imports: [NzIconModule, NzButtonModule, NzPopoverModule, NzInputModule, NzCardModule, NgIf, NgFor, NgStyle,
        NzEmptyModule, NzPaginationModule, NzToolTipModule],
    declarations: [IconSelectComponent],
    exports: [IconSelectComponent]
})
export class IconSelectModule {
}
