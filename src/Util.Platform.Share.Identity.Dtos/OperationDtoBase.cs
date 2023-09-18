namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 操作资源参数
/// </summary>
public abstract class OperationDtoBase : OperationDtoBase<Guid?, Guid?, Guid?> {
}

/// <summary>
/// 操作资源参数
/// </summary>
public abstract class OperationDtoBase<TApplicationId, TResourceParentId, TAuditUserId> : DtoBase {
    /// <summary>
    /// 应用程序标识
    ///</summary>
    [Display( Name = "identity.application.name" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 应用程序名称
    ///</summary>
    [Display( Name = "identity.application.name" )]
    public string ApplicationName { get; set; }
    /// <summary>
    /// Api资源应用程序标识
    /// </summary>
    [Required]
    public TApplicationId ApiApplicationId { get; set; }
    /// <summary>
    /// 模块标识
    /// </summary>
    [Display( Name = "identity.module.name" )]
    public TResourceParentId ModuleId { get; set; }
    /// <summary>
    /// 模块名称
    /// </summary>
    [Display( Name = "identity.module.name" )]
    public string ModuleName { get; set; }
    /// <summary>
    /// 资源标识
    /// </summary>
    [StringLength( 300 )]
    [Display( Name = "identity.operation.uri" )]
    [Required]
    public string Uri { get; set; }
    /// <summary>
    /// 操作名称
    /// </summary>
    [StringLength( 200 )]
    [Required]
    [Display( Name = "identity.operation.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.operation.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 是否绑定Api资源
    /// </summary>
    [Display( Name = "identity.operation.isBindApi" )]
    public bool? IsBindApi { get; set; }
    /// <summary>
    /// 选中的Api资源标识列表
    /// </summary>
    public List<string> ApiRourceIds { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.operation.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    [Display( Name = "identity.operation.sortId" )]
    public int? SortId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Display( Name = "identity.operation.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    /// </summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    /// </summary>
    [Display( Name = "identity.operation.lastModificationTime" )]
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