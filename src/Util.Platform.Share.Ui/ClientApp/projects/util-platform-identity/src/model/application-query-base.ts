import { QueryParameter } from "util-angular";

/**
 * 应用程序查询参数
 */
export class ApplicationQueryBase extends QueryParameter {
    /**
     * 应用程序编码
     */
    code;
    /**
     * 应用程序名称
     */
    name;
    /**
     * 启用
     */
    enabled;
    /**
    * 是否Api
    */
    isApi;
    /**
     * 备注
     */
    remark;
    /**
     * 起始创建时间
     */
    beginCreationTime;
    /**
     * 结束创建时间
     */
    endCreationTime;
    /**
     * 起始最后修改时间
     */
    beginLastModificationTime;
    /**
     * 结束最后修改时间
     */
    endLastModificationTime;
}
