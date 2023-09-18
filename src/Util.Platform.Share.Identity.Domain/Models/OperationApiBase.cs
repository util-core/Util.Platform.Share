namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 操作Api
/// </summary>
public abstract class OperationApiBase<TOperationApi> : OperationApiBase<TOperationApi, Guid, Guid, Guid, Guid?> 
    where TOperationApi : OperationApiBase<TOperationApi, Guid, Guid, Guid, Guid?> {
    /// <summary>
    /// 初始化操作Api
    /// </summary>
    /// <param name="id">操作Api标识</param>
    protected OperationApiBase( Guid id ) : base( id ) {
    }
}

/// <summary>
/// 操作Api
/// </summary>
/// <typeparam name="TOperationApi">操作Api类型</typeparam>
/// <typeparam name="TOperationApiId">操作Api标识类型</typeparam>
/// <typeparam name="TOperationId">操作标识类型</typeparam>
/// <typeparam name="TResourceId">Api资源标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class OperationApiBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TAuditUserId> : AggregateRoot<TOperationApi, TOperationApiId>,IDelete,IAudited<TAuditUserId> 
    where TOperationApi : OperationApiBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TAuditUserId> {
    /// <summary>
    /// 初始化操作Api
    /// </summary>
    /// <param name="id">操作Api</param>
    protected OperationApiBase( TOperationApiId id ) : base( id ) {
    }

    /// <summary>
    /// 操作资源标识
    ///</summary>
    [DisplayName( "操作资源标识" )]
    [Required]
    public TOperationId OperationId { get; set; }
    /// <summary>
    /// Api资源标识
    ///</summary>
    [DisplayName( "Api资源标识" )]
    [Required]
    public TResourceId ApiId { get; set; }
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
    protected override void AddChanges( TOperationApi other ) {
        AddChange( t => t.OperationId, other.OperationId );
        AddChange( t => t.ApiId, other.ApiId );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
    }
}