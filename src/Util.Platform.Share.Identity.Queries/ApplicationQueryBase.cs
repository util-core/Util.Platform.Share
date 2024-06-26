namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 应用程序查询参数
/// </summary>
public abstract class ApplicationQueryBase : QueryParameter {
    /// <summary>
    /// 应用程序编码
    ///</summary>
    [Description( "identity.application.code" )]
    public string Code { get; set; }
    /// <summary>
    /// 应用程序名称
    ///</summary>
    [Description( "identity.application.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Description( "identity.application.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Description( "identity.application.remark" )]
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