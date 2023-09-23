namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 应用程序参数
/// </summary>
public abstract class ApplicationDtoBase : ApplicationDtoBase<Guid?> {
}

/// <summary>
/// 应用程序参数
/// </summary>
public abstract class ApplicationDtoBase<TAuditUserId> : DtoBase {
    /// <summary>
    /// 应用程序编码
    ///</summary>
    [Display( Name = "identity.application.code" )]
    [Required]
    [MaxLength( 60 )]
    public string Code { get; set; }
    /// <summary>
    /// 应用程序名称
    ///</summary>
    [Display( Name = "identity.application.name" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Display( Name = "identity.application.enabled" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 是否Api
    ///</summary>
    [Display( Name = "identity.application.isApi" )]
    public bool IsApi { get; set; }
    /// <summary>
    /// 是否客户端
    ///</summary>
    [Display( Name = "identity.application.isClient" )]
    public bool IsClient { get; set; }
    /// <summary>
    /// 允许的授权类型
    /// </summary>
    [Display( Name = "identity.application.allowedGrantType" )]
    public GrantType? AllowedGrantType { get; set; }
    /// <summary>
    /// 允许的授权类型
    /// </summary>
    [Display( Name = "identity.application.allowedGrantType" )]
    public string AllowedGrantTypeDescription => AllowedGrantType.Description();
    /// <summary>
    /// 认证重定向地址
    /// </summary>
    [Display( Name = "identity.application.redirectUri" )]
    public string RedirectUri { get; set; }
    /// <summary>
    /// 注销重定向地址
    /// </summary>
    [Display( Name = "identity.application.postLogoutRedirectUri" )]
    public string PostLogoutRedirectUri { get; set; }
    /// <summary>
    /// 允许的跨域来源
    /// </summary>
    [Display( Name = "identity.application.allowedCorsOrigins" )]
    public string AllowedCorsOrigins { get; set; }
    /// <summary>
    /// 访问令牌生存期
    /// </summary>
    [Display( Name = "identity.application.accessTokenLifetime" )]
    public int? AccessTokenLifetime { get; set; }
    /// <summary>
    /// 允许的权限范围
    /// </summary>
    [Display( Name = "identity.application.allowedScopes" )]
    public List<string> AllowedScopes { get; set; }
    /// <summary>
    /// 允许获取刷新令牌
    /// </summary>
    [Display( Name = "identity.application.allowOfflineAccess" )]
    public bool AllowOfflineAccess { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.application.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "util.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "util.lastModificationTime" )]
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