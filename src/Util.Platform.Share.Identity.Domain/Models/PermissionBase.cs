namespace Util.Platform.Share.Identity.Domain.Models;

/// <summary>
/// 权限
/// </summary>
/// <typeparam name="TPermission">权限类型</typeparam>
/// <typeparam name="TPermissionId">权限标识类型</typeparam>
/// <typeparam name="TRoleId">角色标识类型</typeparam>
/// <typeparam name="TResource">资源类型</typeparam>
/// <typeparam name="TResourceId">资源标识类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
[Description( "权限" )]
public abstract class PermissionBase<TPermission, TPermissionId, TRoleId, TResource, TResourceId, TAuditUserId> : AggregateRoot<TPermission, TPermissionId>,IDelete,IAudited<TAuditUserId>, ITenant
    where TPermission : PermissionBase<TPermission, TPermissionId, TRoleId, TResource, TResourceId, TAuditUserId> {
    /// <summary>
    /// 初始化权限
    /// </summary>
    /// <param name="id">权限标识</param>
    protected PermissionBase( TPermissionId id ) : base( id ) {
    }

    /// <summary>
    /// 角色标识
    ///</summary>
    [DisplayName( "角色标识" )]
    [Required]
    public TRoleId RoleId { get; set; }
    /// <summary>
    /// 资源标识
    ///</summary>
    [DisplayName( "资源标识" )]
    [Required]
    public TResourceId ResourceId { get; set; }
    /// <summary>
    /// 拒绝
    ///</summary>
    [DisplayName( "拒绝" )]
    public bool IsDeny { get; set; }
    /// <summary>
    /// 临时
    ///</summary>
    [DisplayName( "临时" )]
    public bool IsTemporary { get; set; }
    /// <summary>
    /// 到期时间
    ///</summary>
    [DisplayName( "到期时间" )]
    public DateTime? ExpirationTime { get; set; }
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
    /// 资源
    /// </summary>
    [ForeignKey( "ResourceId" )]
    public TResource Resource { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( TPermission other ) {
        AddChange( t => t.RoleId, other.RoleId );
        AddChange( t => t.ResourceId, other.ResourceId );
        AddChange( t => t.IsDeny, other.IsDeny );
        AddChange( t => t.IsTemporary, other.IsTemporary );
        AddChange( t => t.ExpirationTime, other.ExpirationTime );
        AddChange( t => t.CreationTime, other.CreationTime );
        AddChange( t => t.CreatorId, other.CreatorId );
        AddChange( t => t.LastModificationTime, other.LastModificationTime );
        AddChange( t => t.LastModifierId, other.LastModifierId );
    }
}