namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 用户参数
/// </summary>
public abstract class UserDtoBase : UserDtoBase<Guid?> {
}

/// <summary>
/// 用户参数
/// </summary>
public abstract class UserDtoBase<TAuditUserId> : DtoBase {
    /// <summary>
    /// 用户名
    ///</summary>
    [Display( Name = "identity.user.userName" )]
    [MaxLength( 256 )]
    public string UserName { get; set; }
    /// <summary>
    /// 安全邮箱
    ///</summary>
    [Display( Name = "identity.user.email" )]
    [MaxLength( 256 )]
    public string Email { get; set; }
    /// <summary>
    /// 邮箱是否已确认
    ///</summary>
    [Display( Name = "identity.user.emailConfirmed" )]
    public bool EmailConfirmed { get; set; }
    /// <summary>
    /// 安全手机号
    ///</summary>
    [Display( Name = "identity.user.phoneNumber" )]
    [MaxLength( 64 )]
    public string PhoneNumber { get; set; }
    /// <summary>
    /// 手机号是否已确认
    ///</summary>
    [Display( Name = "identity.user.phoneNumberConfirmed" )]
    public bool PhoneNumberConfirmed { get; set; }
    /// <summary>
    /// 是否启用双因素认证
    ///</summary>
    [Display( Name = "identity.user.twoFactorEnabled" )]
    public bool TwoFactorEnabled { get; set; }
    /// <summary>
    /// 是否启用
    ///</summary>
    [Display( Name = "identity.user.enabled" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 冻结时间
    ///</summary>
    [Display( Name = "identity.user.disabledTime" )]
    public DateTime? DisabledTime { get; set; }
    /// <summary>
    /// 是否启用锁定
    ///</summary>
    [Display( Name = "identity.user.lockoutEnabled" )]
    public bool LockoutEnabled { get; set; }
    /// <summary>
    /// 锁定截止
    ///</summary>
    [Display( Name = "identity.user.lockoutEnd" )]
    public DateTime? LockoutEnd { get; set; }
    /// <summary>
    /// 登录失败次数
    ///</summary>
    [Display( Name = "identity.user.accessFailedCount" )]
    public int? AccessFailedCount { get; set; }
    /// <summary>
    /// 登录次数
    ///</summary>
    [Display( Name = "identity.user.loginCount" )]
    public int? LoginCount { get; set; }
    /// <summary>
    /// 注册Ip
    ///</summary>
    [Display( Name = "identity.user.registerIp" )]
    [MaxLength( 30 )]
    public string RegisterIp { get; set; }
    /// <summary>
    /// 上次登录时间
    ///</summary>
    [Display( Name = "identity.user.lastLoginTime" )]
    public DateTime? LastLoginTime { get; set; }
    /// <summary>
    /// 上次登录Ip
    ///</summary>
    [Display( Name = "identity.user.lastLoginIp" )]
    [MaxLength( 30 )]
    public string LastLoginIp { get; set; }
    /// <summary>
    /// 本次登录时间
    ///</summary>
    [Display( Name = "identity.user.currentLoginTime" )]
    public DateTime? CurrentLoginTime { get; set; }
    /// <summary>
    /// 本次登录Ip
    ///</summary>
    [Display( Name = "identity.user.currentLoginIp" )]
    [MaxLength( 30 )]
    public string CurrentLoginIp { get; set; }
    /// <summary>
    /// 安全戳
    ///</summary>
    [Display( Name = "identity.user.securityStamp" )]
    [MaxLength( 1024 )]
    public string SecurityStamp { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.user.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "identity.user.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "identity.user.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    ///</summary>
    public byte[] Version { get; set; }
}