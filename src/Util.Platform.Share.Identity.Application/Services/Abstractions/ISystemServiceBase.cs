namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 系统服务
/// </summary>
public interface ISystemServiceBase<in TLoginRequest> : ISystemServiceBase<TLoginRequest, Guid>
    where TLoginRequest : class {
}

/// <summary>
/// 系统服务
/// </summary>
public interface ISystemServiceBase<in TLoginRequest, in TUserId> : IService 
    where TLoginRequest : class {
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request">登录参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<SignInResult> SignInAsync( [NotNull][Valid] TLoginRequest request, CancellationToken cancellationToken = default );
    /// <summary>
    /// 退出登录
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    Task SignOutAsync( CancellationToken cancellationToken = default );
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
    /// 通过缓存检测是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> IsAdminByCacheAsync( TUserId userId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 设置用户访问控制列表缓存
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task SetAclCacheAsync( TUserId userId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 通过缓存检测用户是否有权限访问资源
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="resourceUri">资源标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> HasPermissionByCacheAsync( TUserId userId, string resourceUri, CancellationToken cancellationToken = default );
}