namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 权限服务
/// </summary>
public interface IPermissionServiceBase<TAppResources> : IPermissionServiceBase<TAppResources, Guid, Guid>
    where TAppResources : class {
}

/// <summary>
/// 权限服务
/// </summary>
public interface IPermissionServiceBase<TAppResources, in TUserId, in TApplicationId> : IService 
    where TAppResources : class {
    /// <summary>
    /// 是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    bool IsAdmin( TUserId userId );
    /// <summary>
    /// 是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> IsAdminAsync( TUserId userId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 获取授予用户的前端应用资源
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TAppResources> GetAppResourcesAsync( TApplicationId applicationId, TUserId userId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 获取用户的访问控制列表
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<string>> GetAclAsync( TUserId userId, CancellationToken cancellationToken = default );
}