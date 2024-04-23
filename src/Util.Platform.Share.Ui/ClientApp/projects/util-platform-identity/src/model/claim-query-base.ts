import { QueryParameter } from 'util-angular';

/**
 * 声明查询参数
 */
export class ClaimQueryBase extends QueryParameter {
    /**
     * 声明名称
     */
    name;
    /**
     * 启用
     */
    enabled;
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