namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 角色
/// </summary>
public abstract class RoleBase<TRole, TUser> : RoleBase<TRole, Guid, Guid?, TUser, Guid?> 
    where TRole : RoleBase<TRole, Guid, Guid?, TUser, Guid?>
    where TUser : UserBase<TUser, Guid, TRole, Guid?> {
    /// <summary>
    /// 初始化角色
    /// </summary>
    /// <param name="id">角色标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected RoleBase( Guid id, string path, int level ) : base( id, path, level ) {
    }
}

/// <summary>
/// 角色
/// </summary>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TRoleId">角色标识类型</typeparam>
/// <typeparam name="TRoleParentId">角色父标识类型</typeparam>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId> 
    : TreeEntityBase<TRole, TRoleId, TRoleParentId>, IDelete, IAudited<TAuditUserId>, IExtraProperties,ITenant
    where TRole : RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId> 
    where TUser: class {
    /// <summary>
    /// 初始化角色
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="path">路径</param>
    /// <param name="level">层级</param>
    protected RoleBase( TRoleId id, string path, int level ) : base( id, path, level ) {
        Users = new List<TUser>();
    }

    /// <summary>
    /// 角色编码
    ///</summary>
    [DisplayName( "角色编码" )]
    [Required]
    [MaxLength( 256 )]
    public string Code { get; set; }
    /// <summary>
    /// 角色名称
    ///</summary>
    [DisplayName( "角色名称" )]
    [Required]
    [MaxLength( 256 )]
    public string Name { get; set; }
    /// <summary>
    /// 标准化角色名称
    ///</summary>
    [DisplayName( "标准化角色名称" )]
    [Required]
    [MaxLength( 256 )]
    public string NormalizedName { get; set; }
    /// <summary>
    /// 角色类型
    ///</summary>
    [DisplayName( "角色类型" )]
    [Required]
    [MaxLength( 80 )]
    public string Type { get; set; }
    /// <summary>
    /// 管理员
    ///</summary>
    [DisplayName( "管理员" )]
    public bool IsAdmin { get; private set; }
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
    /// 租户标识
    /// </summary>
    public string TenantId { get; set; }
    /// <summary>
    /// 用户列表
    /// </summary>
    public ICollection<TUser> Users { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TRole other ) {
        AddChange( t => t.Code, other.Code );
        AddChange( t => t.Name, other.Name );
        AddChange( t => t.NormalizedName, other.NormalizedName );
        AddChange( t => t.Type, other.Type );
        AddChange( t => t.IsAdmin, other.IsAdmin );
        AddChange( t => t.ParentId, other.ParentId );
        AddChange( t => t.Path, other.Path );
        AddChange( t => t.Level, other.Level );
        AddChange( t => t.Enabled, other.Enabled );
        AddChange( t => t.Remark, other.Remark );
        AddChange( t => t.PinYin, other.PinYin );
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
        InitType();
        InitPinYin();
    }

    /// <summary>
    /// 初始化类型
    /// </summary>
    protected virtual void InitType() {
        if ( Type.IsEmpty() )
            Type = "Role";
    }

    /// <summary>
    /// 初始化拼音简码
    /// </summary>
    public virtual void InitPinYin() {
        PinYin = Util.Helpers.String.PinYin( Name );
    }

    /// <summary>
    /// 设置为超级管理员
    /// </summary>
    public virtual void SetAdmin() {
        IsAdmin = true;
    }
}