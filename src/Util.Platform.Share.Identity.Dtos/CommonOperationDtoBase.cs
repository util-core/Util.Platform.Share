namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 常用操作资源参数
/// </summary>
public abstract class CommonOperationDtoBase : CommonOperationDtoBase<Guid?> {
}

/// <summary>
/// 常用操作资源参数
/// </summary>
public abstract class CommonOperationDtoBase<TAuditUserId> : DtoBase {
    /// <summary>
    /// 操作名称
    ///</summary>
    [Display( Name = "identity.commonOperation.name" )]
    [Required]
    [MaxLength( 50 )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [Display( Name = "identity.commonOperation.enabled" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序号
    ///</summary>
    [Display( Name = "identity.commonOperation.sortId" )]
    public int? SortId { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.commonOperation.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "identity.commonOperation.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "identity.commonOperation.lastModificationTime" )]
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