namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 声明参数
/// </summary>
public abstract class ClaimDtoBase : ClaimDtoBase<Guid?> {
}

/// <summary>
/// 声明参数
/// </summary>
public abstract class ClaimDtoBase<TAuditUserId> : DtoBase {
    /// <summary>
    /// 声明名称
    ///</summary>
    [Display( Name = "identity.claim.name" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Display( Name = "identity.claim.enabled" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序号
    ///</summary>
    [Display( Name = "identity.claim.sortId" )]
    [Required]
    public int? SortId { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.claim.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
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
}