import { TreeQueryParameter } from 'util-angular';

/**
 * 资源查询参数
 */
export class ResourceQueryBase extends TreeQueryParameter {
    /**
     * 应用程序标识
     */
    applicationId;
    /**
     * 资源标识符
     */
    uri;
    /**
     * 资源名称
     */
    name;
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