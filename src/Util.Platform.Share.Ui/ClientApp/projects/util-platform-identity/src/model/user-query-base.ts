import { QueryParameter } from "util-angular";

/**
* 用户查询参数
*/
export class UserQueryBase extends QueryParameter {
    /**
    * 角色标识
    */
    roleId;
    /**
     * 排除的角色标识
     */
    exceptRoleId;
    /**
     * 用户名
     */
    userName;
    /**
    * 安全邮箱
    */
    email;
    /**
    * 安全手机号
    */
    phoneNumber;
    /**
    * 是否启用
    */
    enabled;
    /**
    * 起始冻结时间
    */
    beginDisabledTime;
    /**
    * 结束冻结时间
    */
    endDisabledTime;
    /**
     * 是否启用锁定
     */
    lockoutEnabled;
    /**
    * 起始锁定截止
    */
    beginLockoutEnd;
    /**
    * 结束锁定截止
    */
    endLockoutEnd;
    /**
    * 注册Ip
    */
    registerIp;
    /**
    * 起始上次登录时间
    */
    beginLastLoginTime;
    /**
    * 结束上次登录时间
    */
    endLastLoginTime;
    /**
     * 上次登录Ip
     */
    lastLoginIp;
    /**
    * 起始本次登录时间
    */
    beginCurrentLoginTime;
    /**
    * 结束本次登录时间
    */
    endCurrentLoginTime;
    /**
     * 本次登录Ip
     */
    currentLoginIp;
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
