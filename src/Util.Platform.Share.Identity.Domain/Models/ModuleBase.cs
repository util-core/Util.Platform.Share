namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 模块
/// </summary>
public abstract class ModuleBase<TModule> : ModuleBase<TModule, Guid, Guid?, Guid?, Guid?>
    where TModule : ModuleBase<TModule, Guid, Guid?, Guid?, Guid?> {
    /// <summary>
    /// 初始化模块
    /// </summary>
    /// <param name="id">模块标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ModuleBase( Guid id, string path, int level ) : base( id, path, level ) {
    }
}

/// <summary>
/// 模块
/// </summary>
/// <typeparam name="TModule">模块类型</typeparam>
/// <typeparam name="TResourceId">模块标识类型</typeparam>
/// <typeparam name="TResourceParentId">模块父标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class ModuleBase<TModule, TResourceId, TResourceParentId, TApplicationId, TAuditUserId> : TreeEntityBase<TModule, TResourceId, TResourceParentId> 
    where TModule : ModuleBase<TModule, TResourceId, TResourceParentId, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化模块
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected ModuleBase( TResourceId id, string path, int level ) : base( id, path, level ) {
    }

    /// <summary>
    /// 应用程序标识
    ///</summary>
    [DisplayName( "应用程序标识" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 模块地址
    ///</summary>
    [DisplayName( "模块地址" )]
    [MaxLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// 模块名称
    ///</summary>
    [DisplayName( "模块名称" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 多语言键
    ///</summary>
    [DisplayName( "多语言键" )]
    public string I18n { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [DisplayName( "图标" )]
    public string Icon { get; set; }
    /// <summary>
    /// 是否展开
    /// </summary>
    [DisplayName( "是否展开" )]
    public bool? Expanded { get; set; }
    /// <summary>
    /// 是否隐藏
    /// </summary>
    [DisplayName( "是否隐藏" )]
    public bool? IsHide { get; set; }
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
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TModule other ) {
        AddChange( t => t.Uri, other.Uri );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.ParentId, other.ParentId );
        AddChange( t => t.Path, other.Path );
        AddChange( t => t.Level, other.Level );
        AddChange( t => t.Icon, other.Icon );
        AddChange( t => t.Expanded, other.Expanded );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.PinYin, other.PinYin );
        AddChange( t => t.Enabled, other.Enabled );
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
        InitPinYin();
    }

    /// <summary>
    /// 初始化拼音简码
    /// </summary>
    public virtual void InitPinYin() {
        PinYin = Util.Helpers.String.PinYin( Name );
    }
}