namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 角色参数
/// </summary>
public abstract class RoleDtoBase<TRoleDto> : RoleDtoBase<TRoleDto, Guid?>
    where TRoleDto : RoleDtoBase<TRoleDto> {
}

/// <summary>
/// 角色参数
/// </summary>
public abstract class RoleDtoBase<TRoleDto, TAuditUserId> : TreeDtoBase<TRoleDto> 
    where TRoleDto : RoleDtoBase<TRoleDto, TAuditUserId> {
    /// <summary>
    /// 角色编码
    ///</summary>
    [Display( Name = "identity.role.code" )]
    [Required]
    [MaxLength( 256 )]
    public string Code { get; set; }
    /// <summary>
    /// 角色名称
    ///</summary>
    [Display( Name = "identity.role.name" )]
    [Required]
    [MaxLength( 256 )]
    public string Name { get; set; }
    /// <summary>
    /// 角色类型
    ///</summary>
    [Display( Name = "identity.role.type" )]
    [Required]
    [MaxLength( 80 )]
    public string Type { get; set; }
    /// <summary>
    /// 管理员
    ///</summary>
    [Display( Name = "identity.role.isAdmin" )]
    public bool IsAdmin { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.role.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 拼音简码
    ///</summary>
    [Display( Name = "util.pinYin" )]
    [MaxLength( 200 )]
    public string PinYin { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "util.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "util.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    ///</summary>
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public override string GetText() {
        return Name;
    }
}