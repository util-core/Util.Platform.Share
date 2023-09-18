namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 声明查询参数
/// </summary>
public abstract class ClaimQueryBase : QueryParameter {
    /// <summary>
    /// 声明名称
    ///</summary>
    [Description( "identity.claim.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Description( "identity.claim.enabled" )]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Description( "identity.claim.remark" )]
    public string Remark { get; set; }
}