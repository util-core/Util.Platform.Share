namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 操作
/// </summary>
public abstract class OperationBase<TOperation> : OperationBase<TOperation, Guid, Guid?, Guid?> 
    where TOperation : OperationBase<TOperation, Guid, Guid?, Guid?> {
    /// <summary>
    /// 初始化操作
    /// </summary>
    /// <param name="id">操作标识</param>
    protected OperationBase( Guid id ) : base( id ) {
    }
}

/// <summary>
/// 操作
/// </summary>
/// <typeparam name="TOperation">操作类型</typeparam>
/// <typeparam name="TResourceId">资源标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class OperationBase<TOperation, TResourceId, TApplicationId, TAuditUserId> : AggregateRoot<TOperation, TResourceId> 
    where TOperation : OperationBase<TOperation, TResourceId, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化操作
    /// </summary>
    /// <param name="id">资源标识</param>
    protected OperationBase( TResourceId id ) : base( id ) {
    }

    /// <summary>
    /// 应用程序标识
    ///</summary>
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 模块标识
    /// </summary>
    public TResourceId ModuleId { get; set; }
    /// <summary>
    /// 操作名称
    ///</summary>
    [DisplayName( "操作名称" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 资源标识符
    ///</summary>
    [DisplayName( "资源标识符" )]
    [MaxLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [DisplayName( "备注" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortId { get; set; }
    /// <summary>
    /// 拼音简码
    ///</summary>
    [DisplayName( "拼音简码" )]
    [MaxLength( 200 )]
    public string PinYin { get; set; }
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
    /// 是否绑定Api资源
    /// </summary>
    [Display( Name = "是否绑定Api资源" )]
    public bool? IsBindApi { get; set; }
    /// <summary>
    /// 是否基础资源
    /// </summary>
    [Display( Name = "是否基础资源" )]
    public bool? IsBase { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TOperation other ) {
        AddChange( t => t.Uri, other.Uri );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.PinYin, other.PinYin );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.SortId, other.SortId );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
        AddChange( t => t.IsBindApi, other.IsBindApi );
        AddChange( t => t.IsBase, other.IsBase );
    }
}