namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 应用程序
/// </summary>
/// <typeparam name="TApplication">应用程序类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class ApplicationBase<TApplication, TApplicationId, TAuditUserId> : AggregateRoot<TApplication, TApplicationId>,IDelete,IAudited<TAuditUserId>,IExtraProperties 
    where TApplication : ApplicationBase<TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化应用程序
    /// </summary>
    /// <param name="id">应用程序标识</param>
    protected ApplicationBase( TApplicationId id ) : base( id ) {
    }

    /// <summary>
    /// 应用程序编码
    ///</summary>
    [DisplayName( "应用程序编码" )]
    [Required]
    [MaxLength( 60 )]
    public string Code { get; set; }
    /// <summary>
    /// 应用程序名称
    ///</summary>
    [DisplayName( "应用程序名称" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 是否Api
    ///</summary>
    [DisplayName( "是否Api" )]
    public bool IsApi { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [DisplayName( "启用" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 允许跨域来源
    /// </summary>
    public string AllowedCorsOrigins { get; set; }
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
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 是否客户端
    /// </summary>
    [NotMapped]
    public bool IsClient {
        get => ExtraProperties.GetProperty<bool>( nameof( IsClient ) );
        set => ExtraProperties.SetProperty( nameof( IsClient ), value );
    }

    /// <summary>
    /// 允许的授权类型
    /// </summary>
    [NotMapped]
    public GrantType? AllowedGrantType {
        get => ExtraProperties.GetProperty<GrantType?>( nameof( AllowedGrantType ) );
        set => ExtraProperties.SetProperty( nameof( AllowedGrantType ), value );
    }

    /// <summary>
    /// 认证重定向地址
    /// </summary>
    [NotMapped]
    public string RedirectUri {
        get => ExtraProperties.GetProperty<string>( nameof( RedirectUri ) );
        set => ExtraProperties.SetProperty( nameof( RedirectUri ), value );
    }

    /// <summary>
    /// 注销重定向地址
    /// </summary>
    [NotMapped]
    public string PostLogoutRedirectUri {
        get => ExtraProperties.GetProperty<string>( nameof( PostLogoutRedirectUri ) );
        set => ExtraProperties.SetProperty( nameof( PostLogoutRedirectUri ), value );
    }

    /// <summary>
    /// 访问令牌生存期,单位:秒
    /// </summary>
    [NotMapped]
    public int AccessTokenLifetime {
        get => ExtraProperties.GetProperty<int>( nameof( AccessTokenLifetime ) );
        set => ExtraProperties.SetProperty( nameof( AccessTokenLifetime ), value );
    }

    private readonly ExtraProperty<List<string>> _allowedScopes = new( nameof( AllowedScopes ) );
    /// <summary>
    /// 允许的权限范围
    /// </summary>
    [NotMapped]
    public List<string> AllowedScopes {
        get => _allowedScopes.GetProperty( ExtraProperties );
        set => _allowedScopes.SetProperty( ExtraProperties, value );
    }

    /// <summary>
    /// 允许获取刷新令牌
    /// </summary>
    [NotMapped]
    public bool AllowOfflineAccess {
        get => ExtraProperties.GetProperty<bool>( nameof( AllowOfflineAccess ) );
        set => ExtraProperties.SetProperty( nameof( AllowOfflineAccess ), value );
    }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TApplication other ) {
        AddChange( t => t.Code, other.Code );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.IsApi, other.IsApi );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
        AddChange( t => t.IsClient, other.IsClient );
        AddChange( t => t.RedirectUri, other.RedirectUri );
        AddChange( t => t.PostLogoutRedirectUri, other.PostLogoutRedirectUri );
        AddChange( t => t.AllowedCorsOrigins, other.AllowedCorsOrigins );
        AddChange( t => t.AccessTokenLifetime, other.AccessTokenLifetime );
        AddChange( t => t.AllowedScopes, other.AllowedScopes );
    }
}