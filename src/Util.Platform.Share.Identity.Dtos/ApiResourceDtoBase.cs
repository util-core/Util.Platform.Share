namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// Api资源参数
/// </summary>
public abstract class ApiResourceDtoBase<TApiResourceDto> : ApiResourceDtoBase<TApiResourceDto, Guid?, Guid?>
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto> {
}

/// <summary>
/// Api资源参数
/// </summary>
public abstract class ApiResourceDtoBase<TApiResourceDto, TApplicationId, TAuditUserId> : TreeDtoBase<TApiResourceDto> 
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 应用程序标识
    ///</summary>
    [Display( Name = "identity.apiResource.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 应用程序
    /// </summary>
    [Display( Name = "identity.apiResource.applicationName" )]
    public string ApplicationName { get; set; }
    /// <summary>
    /// Api资源标识
    ///</summary>
    [Display( Name = "identity.apiResource.uri" )]
    [MaxLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// Api资源名称
    ///</summary>
    [Display( Name = "identity.apiResource.name" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// Api地址
    /// </summary>
    [DisplayName( "identity.apiResource.url" )]
    public string Url { get; set; }
    /// <summary>
    /// Http方法
    /// </summary>
    [Display( Name = "identity.apiResource.httpMethod" )]
    public HttpMethod? HttpMethod { get; set; }
    /// <summary>
    /// Http方法
    /// </summary>
    [Display( Name = "identity.apiResource.httpMethod" )]
    public string HttpMethodDisplay => HttpMethod.Description();
    /// <summary>
    /// 用户声明
    /// </summary>
    [Display( Name = "identity.apiResource.claims" )]
    public List<string> Claims { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    [Display( Name = "identity.apiResource.claims" )]
    public string ClaimsDisplay => Claims.Join();
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.apiResource.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "identity.apiResource.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "identity.apiResource.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    ///</summary>
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public override string GetText() {
        return Name;
    }
}