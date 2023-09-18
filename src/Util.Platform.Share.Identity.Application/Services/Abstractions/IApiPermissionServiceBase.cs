namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// Api权限服务
/// </summary>
public interface IApiPermissionServiceBase<TApiResourceDto, in TPermissionRequest> : IApiPermissionServiceBase<TApiResourceDto, TPermissionRequest, Guid, Guid>
    where TApiResourceDto : class
    where TPermissionRequest : class {
}

/// <summary>
/// Api权限服务
/// </summary>
public interface IApiPermissionServiceBase<TApiResourceDto, in TPermissionRequest, in TApplicationId, in TRoleId> : IService
    where TApiResourceDto : class
    where TPermissionRequest : class {
    /// <summary>
    /// 获取指定角色的Api权限,同时获取所有已启用的Api资源
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    /// <param name="isDeny">是否拒绝</param>
    Task<List<TApiResourceDto>> GetApisAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny );
    /// <summary>
    /// 清除指定角色全部Api权限
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    Task ClearApiPermissionsAsync( TApplicationId applicationId, TRoleId roleId );
    /// <summary>
    /// 授予Api权限
    /// </summary>
    /// <param name="request">权限参数</param>
    Task GrantApiPermissionsAsync( [NotNull][Valid] TPermissionRequest request );
    /// <summary>
    /// 拒绝Api权限
    /// </summary>
    /// <param name="request">权限参数</param>
    Task DenyApiPermissionsAsync( [NotNull][Valid] TPermissionRequest request );
}