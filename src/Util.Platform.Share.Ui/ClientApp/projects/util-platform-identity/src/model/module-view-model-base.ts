import { TreeViewModel } from 'util-angular';

/**
 * 模块参数
 */
export class ModuleViewModelBase extends TreeViewModel {
    /**
     * 应用程序标识
     */
    applicationId;
    /**
     * 应用程序名称
     */
    applicationName;
    /**
     * 模块名称
     */
    name;
    /**
     * 多语言键名
     */
    i18n;
    /**
     * 是否展开
     */
    isExpanded: boolean;
    /**
     * 是否显示分组
     */
    group: boolean;
    /**
     * 是否在面包屑导航中隐藏
     */
    hideInBreadcrumb: boolean;
    /**
     * 模块地址
     */
    uri;
    /**
     * 图标
     */
    icon;
    /**
     * 是否隐藏
     */
    isHide;
    /**
     * 备注
     */
    remark;
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