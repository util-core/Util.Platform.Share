namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 创建角色参数
/// </summary>
public abstract class CreateRoleRequestBase : CreateRoleRequestBase<Guid?> {
}

/// <summary>
/// 创建角色参数
/// </summary>
public abstract class CreateRoleRequestBase<TRoleParentId> : RequestBase {
    /// <summary>
    /// 角色编码
    /// </summary>
    [Required]
    [StringLength( 256 )]
    [Display( Name = "identity.role.code" )]
    public string Code { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [Required]
    [StringLength( 256 )]
    [Display( Name = "identity.role.name" )]
    public string Name { get; set; }

    /// <summary>
    /// 父角色标识
    /// </summary>
    public TRoleParentId ParentId { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    [Display( Name = "identity.role.enabled" )]
    public bool? Enabled { get; set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [StringLength( 80 )]
    [Display( Name = "identity.role.type" )]
    public string Type { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength( 500 )]
    [Display( Name = "identity.role.remark" )]
    public string Remark { get; set; }
}