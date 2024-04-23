namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 常用操作资源查询参数
/// </summary>
public abstract class CommonOperationQueryBase : QueryParameter {
    /// <summary>
    /// 操作名称
    ///</summary>
    [Display( Name = "identity.commonOperation.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Display( Name = "identity.commonOperation.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.commonOperation.remark" )]
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