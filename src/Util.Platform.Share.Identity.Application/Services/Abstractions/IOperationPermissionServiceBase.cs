namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 操作权限服务
/// </summary>
public interface IOperationPermissionServiceBase<TOperationPermissionDto, in TPermissionRequest> : IOperationPermissionServiceBase<TOperationPermissionDto, TPermissionRequest, Guid, Guid>
    where TOperationPermissionDto : class
    where TPermissionRequest : class {
}

/// <summary>
/// 操作权限服务
/// </summary>
public interface IOperationPermissionServiceBase<TOperationPermissionDto, in TPermissionRequest, in TApplicationId, in TRoleId> : IService 
    where TOperationPermissionDto : class
    where TPermissionRequest : class {
    /// <summary>
    /// 获取指定角色的操作权限,同时获取所有已启用的模块资源和操作资源
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    /// <param name="isDeny">是否拒绝</param>
    Task<List<TOperationPermissionDto>> GetOperationsAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny );
    /// <summary>
    /// 授予操作权限
    /// </summary>
    /// <param name="request">操作权限参数</param>
    Task GrantOperationPermissionsAsync( [NotNull][Valid] TPermissionRequest request );
    /// <summary>
    /// 拒绝操作权限
    /// </summary>
    /// <param name="request">操作权限参数</param>
    Task DenyOperationPermissionsAsync( [NotNull][Valid] TPermissionRequest request );
    /// <summary>
    /// 清除指定角色全部操作权限
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    Task ClearOperationPermissionsAsync( TApplicationId applicationId, TRoleId roleId );
}