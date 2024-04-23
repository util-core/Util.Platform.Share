namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 用户查询参数
/// </summary>
public abstract class UserQueryBase : UserQueryBase<Guid?> {
}

/// <summary>
/// 用户查询参数
/// </summary>
public abstract class UserQueryBase<TRoleId> : QueryParameter {
    /// <summary>
    /// 角色标识
    /// </summary>
    public TRoleId RoleId { get; set; }
    /// <summary>
    /// 排除的角色标识
    /// </summary>
    public TRoleId ExceptRoleId { get; set; }
    /// <summary>
    /// 用户名
    ///</summary>
    [Display( Name = "identity.user.userName" )]
    public string UserName { get; set; }
    /// <summary>
    /// 安全邮箱
    ///</summary>
    [Display( Name = "identity.user.email" )]
    public string Email { get; set; }
    /// <summary>
    /// 安全手机号
    ///</summary>
    [Display( Name = "identity.user.phoneNumber" )]
    public string PhoneNumber { get; set; }
    /// <summary>
    /// 是否启用
    ///</summary>
    [Display( Name = "identity.user.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 起始冻结时间
    /// </summary>
    [Display( Name = "identity.user.beginDisabledTime" )]
    public DateTime? BeginDisabledTime { get; set; }
    /// <summary>
    /// 结束冻结时间
    /// </summary>
    [Display( Name = "identity.user.endDisabledTime" )]
    public DateTime? EndDisabledTime { get; set; }
    /// <summary>
    /// 是否启用锁定
    ///</summary>
    [Display( Name = "identity.user.lockoutEnabled" )]
    public bool? LockoutEnabled { get; set; }
    /// <summary>
    /// 起始锁定截止
    /// </summary>
    [Display( Name = "identity.user.beginLockoutEnd" )]
    public DateTime? BeginLockoutEnd { get; set; }
    /// <summary>
    /// 结束锁定截止
    /// </summary>
    [Display( Name = "identity.user.endLockoutEnd" )]
    public DateTime? EndLockoutEnd { get; set; }
    /// <summary>
    /// 注册Ip
    ///</summary>
    [Display( Name = "identity.user.registerIp" )]
    public string RegisterIp { get; set; }
    /// <summary>
    /// 上次登录起始时间
    /// </summary>
    [Display( Name = "identity.user.beginLastLoginTime" )]
    public DateTime? BeginLastLoginTime { get; set; }
    /// <summary>
    /// 上次登录结束时间
    /// </summary>
    [Display( Name = "identity.user.endLastLoginTime" )]
    public DateTime? EndLastLoginTime { get; set; }
    /// <summary>
    /// 上次登录Ip
    ///</summary>
    [Display( Name = "identity.user.lastLoginIp" )]
    public string LastLoginIp { get; set; }
    /// <summary>
    /// 本次登录起始时间
    /// </summary>
    [Display( Name = "identity.user.beginCurrentLoginTime" )]
    public DateTime? BeginCurrentLoginTime { get; set; }
    /// <summary>
    /// 本次登录结束时间
    /// </summary>
    [Display( Name = "identity.user.endCurrentLoginTime" )]
    public DateTime? EndCurrentLoginTime { get; set; }
    /// <summary>
    /// 本次登录Ip
    ///</summary>
    [Display( Name = "identity.user.currentLoginIp" )]
    public string CurrentLoginIp { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.user.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 起始创建时间
    /// </summary>
    [Display( Name = "util.beginCreationTime" )]
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// 结束创建时间
    /// </summary>
    [Display( Name = "util.endCreationTime" )]
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// 起始最后修改时间
    /// </summary>
    [Display( Name = "util.beginLastModificationTime" )]
    public DateTime? BeginLastModificationTime { get; set; }
    /// <summary>
    /// 结束最后修改时间
    /// </summary>
    [Display( Name = "util.endLastModificationTime" )]
    public DateTime? EndLastModificationTime { get; set; }
}