import { QueryParameter } from "util-angular";

/**
* 权限查询参数
*/
export class PermissionQueryBase extends QueryParameter {
    /**
     * 应用程序标识
     */
    applicationId;
   /**
    * 角色标识
    */
    roleId;
    /**
    * 资源标识
    */
    resourceId;
    /**
    * 拒绝
    */
    isDeny;
    /**
    * 临时
    */
    isTemporary;
    /**
    * 起始到期时间
    */
    beginExpirationTime;
    /**
    * 结束到期时间
    */
    endExpirationTime;
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
