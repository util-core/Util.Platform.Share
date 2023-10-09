import { ViewModel } from "util-angular";

/**
 * 应用程序参数
 */
export class ApplicationModel extends ViewModel {
    /**
     * 应用程序编码
     */
    code;
    /**
     * 应用程序名称
     */
    name;
    /**
    * 是否Api
    */
    isApi;
}