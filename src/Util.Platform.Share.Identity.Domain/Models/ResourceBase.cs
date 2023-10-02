namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 资源
/// </summary>
public abstract class ResourceBase<TResource, TApplication> : ResourceBase<TResource, Guid, Guid?, TApplication, Guid?, Guid?> 
    where TResource : ResourceBase<TResource, Guid, Guid?, TApplication, Guid?, Guid?> 
    where TApplication : ApplicationBase<TApplication> {
    /// <summary>
    /// 初始化资源
    /// </summary>
    /// <param name="id">资源标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ResourceBase( Guid id, string path, int level ) : base( id, path, level ) {
    }
}

/// <summary>
/// 资源
/// </summary>
/// <typeparam name="TResource">资源类型</typeparam>
/// <typeparam name="TResourceId">资源标识类型</typeparam>
/// <typeparam name="TResourceParentId">资源父标识类型</typeparam>
/// <typeparam name="TApplication">应用程序类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> 
    : TreeEntityBase<TResource, TResourceId, TResourceParentId>, IDelete, IAudited<TAuditUserId>, IExtraProperties
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化资源
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ResourceBase( TResourceId id, string path, int level ) : base( id, path, level ) {
        Claims = new List<string>();
    }

    /// <summary>
    /// 应用程序标识
    ///</summary>
    [DisplayName( "应用程序标识" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 资源标识符
    ///</summary>
    [DisplayName( "资源标识符" )]
    [MaxLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// 资源名称
    ///</summary>
    [DisplayName( "资源名称" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 资源类型
    ///</summary>
    [DisplayName( "资源类型" )]
    [Required]
    public ResourceType? Type { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [DisplayName( "备注" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
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
    /// 是否删除
    ///</summary>
    [DisplayName( "是否删除" )]
    public bool IsDeleted { get; set; }
    /// <summary>
    /// 应用程序
    /// </summary>
    [ForeignKey( "ApplicationId" )]
    public TApplication Application { get; set; }
    /// <summary>
    /// 父资源
    /// </summary>
    [ForeignKey( "ParentId" )]
    public TResource Parent { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [NotMapped]
    public string Icon {
        get => ExtraProperties.GetProperty<string>( nameof( Icon ) );
        set => ExtraProperties.SetProperty( nameof( Icon ), value );
    }
    /// <summary>
    /// 是否展开
    /// </summary>
    [NotMapped]
    public bool? Expanded {
        get => ExtraProperties.GetProperty<bool?>( nameof( Expanded ) );
        set => ExtraProperties.SetProperty( nameof( Expanded ), value );
    }

    private readonly ExtraProperty<List<string>> _claims = new( nameof( Claims ) );
    /// <summary>
    /// 用户声明
    /// </summary>
    [NotMapped]
    public List<string> Claims {
        get => _claims.GetProperty( ExtraProperties );
        set => _claims.SetProperty( ExtraProperties, value );
    }

    /// <summary>
    /// 是否绑定Api资源
    /// </summary>
    [NotMapped]
    public bool IsBindApi {
        get => ExtraProperties.GetProperty<bool>( nameof( IsBindApi ) );
        set => ExtraProperties.SetProperty( nameof( IsBindApi ), value );
    }

    /// <summary>
    /// Api地址
    /// </summary>
    [NotMapped]
    public string Url {
        get => ExtraProperties.GetProperty<string>( nameof( Url ) );
        set => ExtraProperties.SetProperty( nameof( Url ), value );
    }

    /// <summary>
    /// Http方法
    /// </summary>
    [NotMapped]
    public HttpMethod? HttpMethod {
        get => ExtraProperties.GetProperty<HttpMethod?>( nameof( HttpMethod ) );
        set => ExtraProperties.SetProperty( nameof( HttpMethod ), value );
    }

    /// <summary>
    /// 多语言键
    ///</summary>
    [NotMapped]
    public string I18n {
        get => ExtraProperties.GetProperty<string>( nameof( I18n ) );
        set => ExtraProperties.SetProperty( nameof( I18n ), value );
    }
}