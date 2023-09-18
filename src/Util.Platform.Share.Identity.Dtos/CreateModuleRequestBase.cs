namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 创建模块参数
/// </summary>
public abstract class CreateModuleRequestBase : CreateModuleRequestBase<Guid?, Guid?> {
}

/// <summary>
/// 创建模块参数
/// </summary>
public abstract class CreateModuleRequestBase<TApplicationId, TResourceParentId> : RequestBase {
    /// <summary>
    /// 应用程序标识
    /// </summary>
    [Display( Name = "identity.module.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 父标识
    /// </summary>
    [Display( Name = "identity.module.parentId" )]
    public TResourceParentId ParentId { get; set; }
    /// <summary>
    /// 模块名称
    /// </summary>
    [Required]
    [StringLength( 200 )]
    [Display( Name = "identity.module.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 模块地址
    /// </summary>
    [StringLength( 300 )]
    [Display( Name = "identity.module.uri" )]
    public string Uri { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [Display( Name = "identity.module.icon" )]
    public string Icon { get; set; }
    /// <summary>
    /// 展开
    /// </summary>
    [Display( Name = "identity.module.expanded" )]
    public bool? Expanded { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.module.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.module.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    [Display( Name = "identity.module.sortId" )]
    public int? SortId { get; set; }
}