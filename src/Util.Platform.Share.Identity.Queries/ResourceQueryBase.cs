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
}