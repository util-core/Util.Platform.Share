namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 系统服务
/// </summary>
public interface ISystemServiceBase<in TLoginRequest> : ISystemServiceBase<TLoginRequest, Guid, Guid>
    where TLoginRequest : class {
}

/// <summary>
/// 系统服务
/// </summary>
public interface ISystemServiceBase<in TLoginRequest, in TUserId, in TApplicationId> : IService
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
    /// 设置用户访问控制列表缓存
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task SetAclCacheAsync( TApplicationId applicationId, TUserId userId, CancellationToken cancellationToken = default );
}