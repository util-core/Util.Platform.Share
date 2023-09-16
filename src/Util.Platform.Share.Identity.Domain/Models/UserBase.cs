namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 用户
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
[Description( "用户" )]
public abstract class UserBase<TUser, TUserId, TRole, TAuditUserId>
    : AggregateRoot<TUser, TUserId>,IDelete,IAudited<TAuditUserId>,IExtraProperties, ITenant
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId> 
    where TRole: class {
    /// <summary>
    /// 初始化用户
    /// </summary>
    /// <param name="id">用户标识</param>
    protected UserBase( TUserId id ) : base( id ) {
        Roles = new List<TRole>();
    }

    /// <summary>
    /// 用户名
    ///</summary>
    [DisplayName( "用户名" )]
    [MaxLength( 256 )]
    public string UserName { get; set; }
    /// <summary>
    /// 标准化用户名
    ///</summary>
    [DisplayName( "标准化用户名" )]
    [MaxLength( 256 )]
    public string NormalizedUserName { get; set; }
    /// <summary>
    /// 安全邮箱
    ///</summary>
    [DisplayName( "安全邮箱" )]
    [MaxLength( 256 )]
    public string Email { get; set; }
    /// <summary>
    /// 标准化邮箱
    ///</summary>
    [DisplayName( "标准化邮箱" )]
    [MaxLength( 256 )]
    public string NormalizedEmail { get; set; }
    /// <summary>
    /// 邮箱是否已确认
    ///</summary>
    [DisplayName( "邮箱是否已确认" )]
    public bool EmailConfirmed { get; set; }
    /// <summary>
    /// 安全手机号
    ///</summary>
    [DisplayName( "安全手机号" )]
    [MaxLength( 64 )]
    public string PhoneNumber { get; set; }
    /// <summary>
    /// 手机号是否已确认
    ///</summary>
    [DisplayName( "手机号是否已确认" )]
    public bool PhoneNumberConfirmed { get; set; }
    /// <summary>
    /// 密码散列
    ///</summary>
    [DisplayName( "密码散列" )]
    [MaxLength( 1024 )]
    public string PasswordHash { get; set; }
    /// <summary>
    /// 是否启用双因素认证
    ///</summary>
    [DisplayName( "是否启用双因素认证" )]
    public bool TwoFactorEnabled { get; set; }
    /// <summary>
    /// 是否启用
    ///</summary>
    [DisplayName( "是否启用" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 冻结时间
    ///</summary>
    [DisplayName( "冻结时间" )]
    public DateTime? DisabledTime { get; set; }
    /// <summary>
    /// 是否启用锁定
    ///</summary>
    [DisplayName( "是否启用锁定" )]
    public bool LockoutEnabled { get; set; }
    /// <summary>
    /// 锁定截止
    ///</summary>
    [DisplayName( "锁定截止" )]
    public DateTimeOffset? LockoutEnd { get; set; }
    /// <summary>
    /// 登录失败次数
    ///</summary>
    [DisplayName( "登录失败次数" )]
    public int? AccessFailedCount { get; set; }
    /// <summary>
    /// 登录次数
    ///</summary>
    [DisplayName( "登录次数" )]
    public int? LoginCount { get; set; }
    /// <summary>
    /// 注册Ip
    ///</summary>
    [DisplayName( "注册Ip" )]
    [MaxLength( 30 )]
    public string RegisterIp { get; set; }
    /// <summary>
    /// 上次登录时间
    ///</summary>
    [DisplayName( "上次登录时间" )]
    public DateTime? LastLoginTime { get; set; }
    /// <summary>
    /// 上次登录Ip
    ///</summary>
    [DisplayName( "上次登录Ip" )]
    [MaxLength( 30 )]
    public string LastLoginIp { get; set; }
    /// <summary>
    /// 本次登录时间
    ///</summary>
    [DisplayName( "本次登录时间" )]
    public DateTime? CurrentLoginTime { get; set; }
    /// <summary>
    /// 本次登录Ip
    ///</summary>
    [DisplayName( "本次登录Ip" )]
    [MaxLength( 30 )]
    public string CurrentLoginIp { get; set; }
    /// <summary>
    /// 安全戳
    ///</summary>
    [DisplayName( "安全戳" )]
    [MaxLength( 1024 )]
    public string SecurityStamp { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [DisplayName( "备注" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [DisplayName( "创建时间" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    [DisplayName( "创建人标识" )]
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [DisplayName( "最后修改时间" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    [DisplayName( "最后修改人标识" )]
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 是否删除
    ///</summary>
    [DisplayName( "是否删除" )]
    public bool IsDeleted { get; set; }
    /// <summary>
    /// 租户标识
    /// </summary>
    public string TenantId { get; set; }
    /// <summary>
    /// 角色列表
    /// </summary>
    public ICollection<TRole> Roles { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TUser other ) {
        AddChange( t => t.UserName, other.UserName );
        AddChange( t => t.NormalizedUserName, other.NormalizedUserName );
        AddChange( t => t.Email, other.Email );
        AddChange( t => t.NormalizedEmail, other.NormalizedEmail );
        AddChange( t => t.EmailConfirmed, other.EmailConfirmed );
        AddChange( t => t.PhoneNumber, other.PhoneNumber );
        AddChange( t => t.PhoneNumberConfirmed, other.PhoneNumberConfirmed );
        AddChange( t => t.PasswordHash, other.PasswordHash );
        AddChange( t => t.TwoFactorEnabled, other.TwoFactorEnabled );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.DisabledTime, other.DisabledTime );
        AddChange( t => t.LockoutEnabled, other.LockoutEnabled );
        AddChange( t => t.LockoutEnd, other.LockoutEnd );
        AddChange( t => t.AccessFailedCount, other.AccessFailedCount );
        AddChange( t => t.LoginCount, other.LoginCount );
        AddChange( t => t.RegisterIp, other.RegisterIp );
        AddChange( t => t.LastLoginTime, other.LastLoginTime );
        AddChange( t => t.LastLoginIp, other.LastLoginIp );
        AddChange( t => t.CurrentLoginTime, other.CurrentLoginTime );
        AddChange( t => t.CurrentLoginIp, other.CurrentLoginIp );
        AddChange( t => t.SecurityStamp, other.SecurityStamp );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init() {
        base.Init();
        InitUserName();
        InitEnabled();
    }

    /// <summary>
    /// 初始化用户名
    /// </summary>
    protected virtual void InitUserName() {
        if ( UserName.IsEmpty() == false )
            return;
        if ( PhoneNumber.IsEmpty() == false ) {
            UserName = PhoneNumber;
            return;
        }
        if ( Email.IsEmpty() == false )
            UserName = Email;
    }

    /// <summary>
    /// 初始化启用
    /// </summary>
    protected virtual void InitEnabled() {
        Enabled ??= true;
    }
}