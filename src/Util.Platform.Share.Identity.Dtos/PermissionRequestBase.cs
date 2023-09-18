namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 保存权限参数
/// </summary>
public abstract class PermissionRequestBase : PermissionRequestBase<Guid?, Guid?> {
}

/// <summary>
/// 保存权限参数
/// </summary>
public abstract class PermissionRequestBase<TApplicationId, TRoleId> : RequestBase {
    /// <summary>
    /// 应用程序标识
    /// </summary>
    [Required]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 角色标识
    /// </summary>
    [Required]
    public TRoleId RoleId { get; set; }
    /// <summary>
    /// 资源标识列表
    /// </summary>
    public string ResourceIds { get; set; }
}