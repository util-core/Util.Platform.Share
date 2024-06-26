﻿import { TreeViewModel } from 'util-angular';

/**
 * Api资源参数
 */
export class ApiResourceViewModelBase extends TreeViewModel {
    /**
     * 应用程序标识
     */
    applicationId;
    /**
     * 应用程序名称
     */
    applicationName;
    /**
     * 资源名称
     */
    name;
    /**
     * 资源标识
     */
    uri;
    /**
     * Api地址
     */
    url;
    /**
     * Http方法
     */
    httpMethod;
    /**
     * Http方法,用于显示
     */
    httpMethodDisplay;    
    /**
     * 用户声明
     */
    claims;
    /**
     * 用户声明
     */
    claimsDisplay;
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