import { ViewModel } from 'util-angular';

/**
 * 操作资源参数
 */
export class OperationViewModelBase extends ViewModel {
    /**
     * 应用程序标识
     */
    applicationId;    
    /**
     * 应用程序名称
     */
    applicationName;
    /**
     * 模块标识
     */
    moduleId;
    /**
     * 模块名称
     */
    moduleName;
    /**
     * 操作资源标识
     */
    uri;
    /**
     * 操作资源名称
     */
    name;
    /**
     * 是否基础资源
     */
    isBase;
    /**
     * 备注
     */
    remark;
    /**
     * 启用
     */
    enabled;
    /**
     * 排序号
     */
    sortId;
    /**
     * 创建时间
     */
    creationTime;
    /**
     * 创建人标识
     */
    creatorId;
    /**
     * 最后修改时间
     */
    lastModificationTime;
    /**
     * 最后修改人标识
     */
    lastModifierId;
    /**
     * 版本号
     */
    version;
}