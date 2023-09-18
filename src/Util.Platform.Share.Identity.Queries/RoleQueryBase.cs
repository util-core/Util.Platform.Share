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
}