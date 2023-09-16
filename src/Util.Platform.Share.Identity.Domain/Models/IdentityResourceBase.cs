namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 身份资源
/// </summary>
/// <typeparam name="TIdentityResource">身份资源类型</typeparam>
/// <typeparam name="TResourceId">身份资源标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class IdentityResourceBase<TIdentityResource, TResourceId, TAuditUserId> : AggregateRoot<TIdentityResource, TResourceId> 
    where TIdentityResource : IdentityResourceBase<TIdentityResource, TResourceId, TAuditUserId> {
    /// <summary>
    /// 初始化身份资源
    /// </summary>
    /// <param name="id">身份资源标识</param>
    protected IdentityResourceBase( TResourceId id ) : base( id ) {
    }

    /// <summary>
    /// 资源标识
    /// </summary>
    [DisplayName( "资源标识" )]
    [Required]
    [StringLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// 显示名称
    /// </summary>
    [DisplayName( "显示名称" )]
    [Required]
    [StringLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [DisplayName( "描述" )]
    [StringLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [DisplayName( "启用" )]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    [DisplayName( "排序号" )]
    public int? SortId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [DisplayName( "创建时间" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    /// </summary>
    [DisplayName( "创建人标识" )]
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    /// </summary>
    [DisplayName( "最后修改时间" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    /// </summary>
    [DisplayName( "最后修改人标识" )]
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    public List<string> Claims { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TIdentityResource other ) {
        AddChange( t => t.Id, other.Id );
        AddChange( t => t.Uri, other.Uri );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.Claims, other.Claims );
        AddChange( t => t.SortId, other.SortId );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init() {
        base.Init();
        InitName();
    }

    /// <summary>
    /// 初始化显示名称
    /// </summary>
    public virtual void InitName() {
        if ( Name.IsEmpty() )
            Name = Uri;
    }
}