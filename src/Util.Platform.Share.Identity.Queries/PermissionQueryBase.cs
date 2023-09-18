namespace Util.Platform.Share.Identity.Queries;

/// <summary>
/// 权限查询参数
/// </summary>
public abstract class PermissionQueryBase : PermissionQueryBase<Guid?, Guid?, Guid?> {
}

/// <summary>
/// 权限查询参数
/// </summary>
public abstract class PermissionQueryBase<TApplicationId, TRoleId, TResourceId> : QueryParameter {
    /// <summary>
    /// 应用程序标识
    ///</summary>
    [Display( Name = "identity.permission.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 角色标识
    ///</summary>
    [Display( Name = "identity.permission.roleId" )]
    public TRoleId RoleId { get; set; }
    /// <summary>
    /// 资源标识
    ///</summary>
    [Display( Name = "identity.permission.resourceId" )]
    public TResourceId ResourceId { get; set; }
    /// <summary>
    /// 拒绝
    ///</summary>
    [Display( Name = "identity.permission.isDeny" )]
    public bool? IsDeny { get; set; }
    /// <summary>
    /// 临时
    ///</summary>
    [Display( Name = "identity.permission.isTemporary" )]
    public bool? IsTemporary { get; set; }
    /// <summary>
    /// 起始到期时间
    /// </summary>
    [Display( Name = "identity.permission.beginExpirationTime" )]
    public DateTime? BeginExpirationTime { get; set; }
    /// <summary>
    /// 结束到期时间
    /// </summary>
    [Display( Name = "identity.permission.endExpirationTime" )]
    public DateTime? EndExpirationTime { get; set; }
    /// <summary>
    /// 起始创建时间
    /// </summary>
    [Display( Name = "identity.permission.beginCreationTime" )]
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// 结束创建时间
    /// </summary>
    [Display( Name = "identity.permission.endCreationTime" )]
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// 起始最后修改时间
    /// </summary>
    [Display( Name = "identity.permission.beginLastModificationTime" )]
    public DateTime? BeginLastModificationTime { get; set; }
    /// <summary>
    /// 结束最后修改时间
    /// </summary>
    [Display( Name = "identity.permission.endLastModificationTime" )]
    public DateTime? EndLastModificationTime { get; set; }
}