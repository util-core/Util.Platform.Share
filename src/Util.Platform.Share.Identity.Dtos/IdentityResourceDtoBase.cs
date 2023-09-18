namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 身份资源参数
/// </summary>
public abstract class IdentityResourceDtoBase : IdentityResourceDtoBase<Guid?> {
}

/// <summary>
/// 身份资源参数
/// </summary>
public abstract class IdentityResourceDtoBase<TAuditUserId> : DtoBase {
    /// <summary>
    /// 资源标识
    /// </summary>
    [StringLength( 300 )]
    [Display( Name = "identity.identityResource.uri" )]
    [Required]
    public string Uri { get; set; }
    /// <summary>
    /// 显示名称
    /// </summary>
    [StringLength( 200 )]
    [Display( Name = "identity.identityResource.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.identityResource.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.identityResource.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    [Display( Name = "identity.identityResource.claims" )]
    public List<string> Claims { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    [Display( Name = "identity.identityResource.claims" )]
    public string ClaimsDisplay => Claims.Join();
    /// <summary>
    /// 排序号
    /// </summary>
    [Display( Name = "identity.identityResource.sortId" )]
    public int? SortId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Display( Name = "identity.identityResource.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    /// </summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    /// </summary>
    [Display( Name = "identity.identityResource.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    /// </summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    /// </summary>
    public byte[] Version { get; set; }
}