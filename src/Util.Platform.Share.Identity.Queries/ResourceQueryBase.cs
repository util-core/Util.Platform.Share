namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 资源查询参数
/// </summary>
public abstract class ResourceQueryBase : ResourceQueryBase<Guid?> {
}

/// <summary>
/// 资源查询参数
/// </summary>
public abstract class ResourceQueryBase<TApplicationId> : TreeQueryParameter {
    /// <summary>
    /// 应用程序标识
    ///</summary>
    [Description( "identity.resource.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 资源标识符
    ///</summary>
    [Description( "identity.resource.uri" )]
    public string Uri { get; set; }
    /// <summary>
    /// 资源名称
    ///</summary>
    [Description( "identity.resource.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Description( "identity.resource.remark" )]
    public string Remark { get; set; }
    /// <summary>
    /// 起始创建时间
    /// </summary>
    [Display( Name = "util.beginCreationTime" )]
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// 结束创建时间
    /// </summary>
    [Display( Name = "util.endCreationTime" )]
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// 起始最后修改时间
    /// </summary>
    [Display( Name = "util.beginLastModificationTime" )]
    public DateTime? BeginLastModificationTime { get; set; }
    /// <summary>
    /// 结束最后修改时间
    /// </summary>
    [Display( Name = "util.endLastModificationTime" )]
    public DateTime? EndLastModificationTime { get; set; }
}