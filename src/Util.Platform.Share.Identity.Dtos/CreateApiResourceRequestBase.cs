namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 创建Api资源参数
/// </summary>
public abstract class CreateApiResourceRequestBase : CreateApiResourceRequestBase<Guid?, Guid?> {
}

/// <summary>
/// 创建Api资源参数
/// </summary>
public abstract class CreateApiResourceRequestBase<TApplicationId, TResourceParentId> : RequestBase {
    /// <summary>
    /// 应用程序标识
    /// </summary>
    [Display( Name = "identity.apiResource.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 父标识
    /// </summary>
    [Display( Name = "identity.apiResource.parentId" )]
    public TResourceParentId ParentId { get; set; }
    /// <summary>
    /// Api资源名称
    /// </summary>
    [Required]
    [StringLength( 200 )]
    [Display( Name = "identity.apiResource.name" )]
    public string Name { get; set; }
    /// <summary>
    /// Api资源标识
    /// </summary>
    [StringLength( 300 )]
    [Display( Name = "identity.apiResource.uri" )]
    public string Uri { get; set; }
    /// <summary>
    /// Api地址
    /// </summary>
    [Display( Name = "identity.apiResource.url" )]
    public string Url { get; set; }
    /// <summary>
    /// Http方法
    /// </summary>
    [Display( Name = "identity.apiResource.httpMethod" )]
    public string HttpMethod { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    [Display( Name = "identity.apiResource.claims" )]
    public List<string> Claims { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.apiResource.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.apiResource.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    [Display( Name = "identity.apiResource.sortId" )]
    public int? SortId { get; set; }
}