namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// Api资源
/// </summary>
public abstract class ApiResourceBase<TApiResource> : ApiResourceBase<TApiResource, Guid, Guid?, Guid?, Guid?> 
    where TApiResource : ApiResourceBase<TApiResource, Guid, Guid?, Guid?, Guid?> {
    /// <summary>
    /// 初始化Api资源
    /// </summary>
    /// <param name="id">模块标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ApiResourceBase( Guid id, string path, int level ) : base( id, path, level ) {
    }
}

/// <summary>
/// Api资源
/// </summary>
/// <typeparam name="TApiResource">Api资源类型</typeparam>
/// <typeparam name="TResourceId">Api资源标识类型</typeparam>
/// <typeparam name="TResourceParentId">Api资源父标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class ApiResourceBase<TApiResource, TResourceId, TResourceParentId, TApplicationId, TAuditUserId> : TreeEntityBase<TApiResource, TResourceId, TResourceParentId> 
    where TApiResource : ApiResourceBase<TApiResource, TResourceId, TResourceParentId, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化Api资源
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ApiResourceBase( TResourceId id, string path, int level ) : base( id, path, level ) {
    }

    /// <summary>
    /// 应用程序标识
    ///</summary>
    [DisplayName( "应用程序标识" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 资源标识
    /// </summary>
    [DisplayName( "资源标识" )]
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
    /// 拼音简码
    ///</summary>
    [DisplayName( "拼音简码" )]
    [MaxLength( 200 )]
    public string PinYin { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    /// </summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    /// </summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// Api地址
    /// </summary>
    [DisplayName( "Api地址" )]
    public string Url { get; set; }
    /// <summary>
    /// Http方法
    /// </summary>
    [DisplayName( "Http方法" )]
    public HttpMethod? HttpMethod { get; set; }
    /// <summary>
    /// 用户声明
    /// </summary>
    public List<string> Claims { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TApiResource other ) {
        AddChange( t => t.Id, other.Id );
        AddChange( t => t.Uri, other.Uri );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.SortId, other.SortId );
        AddChange( t => t.Claims, other.Claims );
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
        InitUri();
        InitPinYin();
    }

    /// <summary>
    /// 初始化显示名称
    /// </summary>
    public virtual void InitName() {
        if ( Name.IsEmpty() )
            Name = Uri;
    }

    /// <summary>
    /// 初始化资源标识
    /// </summary>
    public virtual void InitUri() {
        Uri = GetResourceUri( Url, HttpMethod );
    }

    /// <summary>
    /// 获取资源标识
    /// </summary>
    protected virtual string GetResourceUri( string path, HttpMethod? httpMethod ) {
        if ( path.IsEmpty() )
            return null;
        if( httpMethod == null )
            return path.ToLower( CultureInfo.InvariantCulture );
        return $"{path}#{httpMethod}".ToLower( CultureInfo.InvariantCulture );
    }

    /// <summary>
    /// 初始化拼音简码
    /// </summary>
    public virtual void InitPinYin() {
        PinYin = Util.Helpers.String.PinYin( Name );
    }
}