namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 常用操作资源
/// </summary>
/// <typeparam name="TCommonOperation">常用操作资源类型</typeparam>
/// <typeparam name="TCommonOperationId">常用操作资源标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class CommonOperationBase<TCommonOperation, TCommonOperationId, TAuditUserId> : AggregateRoot<TCommonOperation, TCommonOperationId>,IDelete,IAudited<TAuditUserId> 
    where TCommonOperation : CommonOperationBase<TCommonOperation, TCommonOperationId, TAuditUserId> {
    /// <summary>
    /// 初始化常用操作资源
    /// </summary>
    /// <param name="id">常用操作资源标识</param>
    protected CommonOperationBase( TCommonOperationId id ) : base( id ) {
    }

    /// <summary>
    /// 操作名称
    ///</summary>
    [DisplayName( "操作名称" )]
    [Required]
    [MaxLength( 50 )]
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
    protected override void AddChanges( TCommonOperation other ) {
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