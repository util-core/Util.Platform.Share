namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 创建操作资源参数
/// </summary>
public abstract class CreateOperationRequestBase : CreateOperationRequestBase<Guid?, Guid?> {
}

/// <summary>
/// 创建操作资源参数
/// </summary>
public abstract class CreateOperationRequestBase<TApplicationId, TResourceParentId> : RequestBase {
    /// <summary>
    /// 应用程序标识
    /// </summary>
    [Display( Name = "identity.application.name" )]
    [Required]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 模块标识
    /// </summary>
    [Required]
    [Display( Name = "identity.module.name" )]
    public TResourceParentId ModuleId { get; set; }
    /// <summary>
    /// 操作名称
    /// </summary>
    [Required]
    [StringLength( 200 )]
    [Display( Name = "identity.operation.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 资源标识
    /// </summary>
    [StringLength( 300 )]
    [Required]
    [Display( Name = "identity.operation.uri" )]
    public string Uri { get; set; }
    /// <summary>
    /// 是否基础资源
    /// </summary>
    [Display( Name = "identity.operation.isBase" )]
    public bool? IsBase { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.operation.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.operation.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    [Display( Name = "identity.operation.sortId" )]
    public int? SortId { get; set; }
}