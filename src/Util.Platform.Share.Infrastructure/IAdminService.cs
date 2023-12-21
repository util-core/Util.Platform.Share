namespace Util.Platform.Share;

/// <summary>
/// 管理员服务
/// </summary>
public interface IAdminService : IAdminService<Guid> {
}

/// <summary>
/// 管理员服务
/// </summary>
public interface IAdminService<in TUserId> : IScopeDependency {
    /// <summary>
    /// 是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="token">取消令牌</param>
    Task<bool> IsAdminAsync( TUserId userId, CancellationToken token = default );
}