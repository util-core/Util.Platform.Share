namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 角色查询参数
/// </summary>
public abstract class RoleQueryBase : TreeQueryParameter {
    /// <summary>
    /// 角色编码
    ///</summary>
    [Display( Name = "identity.role.code" )]
    public string Code { get; set; }
    /// <summary>
    /// 角色名称
    ///</summary>
    [Display( Name = "identity.role.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 角色类型
    ///</summary>
    [Display( Name = "identity.role.type" )]
    public string Type { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.role.remark" )]
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