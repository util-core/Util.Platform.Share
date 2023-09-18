namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 声明
/// </summary>
public abstract class ClaimBase<TClaim> : ClaimBase<TClaim, Guid, Guid?> 
    where TClaim : ClaimBase<TClaim, Guid, Guid?> {
    /// <summary>
    /// 初始化声明
    /// </summary>
    /// <param name="id">声明标识</param>
    protected ClaimBase( Guid id ) : base( id ) {
    }
}

/// <summary>
/// 声明
/// </summary>
/// <typeparam name="TClaim">声明类型</typeparam>
/// <typeparam name="TClaimId">声明标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class ClaimBase<TClaim, TClaimId, TAuditUserId> : AggregateRoot<TClaim, TClaimId>, IDelete, IAudited<TAuditUserId> 
    where TClaim : ClaimBase<TClaim, TClaimId, TAuditUserId> {
    /// <summary>
    /// 初始化声明
    /// </summary>
    /// <param name="id">声明标识</param>
    protected ClaimBase( TClaimId id ) : base( id ) {
    }

    /// <summary>
    /// 声明名称
    ///</summary>
    [DisplayName( "声明名称" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 启用
    ///</summary>
    [DisplayName( "启用" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序号
    ///</summary>
    [DisplayName( "排序号" )]
    public int? SortId { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [DisplayName( "备注" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [DisplayName( "创建时间" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    [DisplayName( "创建人标识" )]
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [DisplayName( "最后修改时间" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    [DisplayName( "最后修改人标识" )]
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 是否删除
    ///</summary>
    [DisplayName( "是否删除" )]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TClaim other ) {
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.SortId, other.SortId );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
    }
}