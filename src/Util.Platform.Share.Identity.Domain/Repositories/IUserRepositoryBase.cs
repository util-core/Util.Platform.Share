namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 用户仓储
/// </summary>
public interface IUserRepositoryBase<TUser> : IUserRepositoryBase<TUser, Guid, Guid>
    where TUser : class, IAggregateRoot<Guid> {
}

/// <summary>
/// 用户仓储
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
/// <typeparam name="TRoleId">角色标识类型</typeparam>
public interface IUserRepositoryBase<TUser, in TUserId, in TRoleId> : IRepository<TUser, TUserId> where TUser : class, IAggregateRoot<TUserId> {
    /// <summary>
    /// 用户是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    bool IsAdmin( TUserId userId );
    /// <summary>
    /// 用户是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> IsAdminAsync( TUserId userId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 获取未关联角色的用户列表,排除已关联用户
    /// </summary>
    /// <param name="roleId">关联角色标识</param>
    /// <param name="userIds">用户列表</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TUser>> GetUnassociatedUsers( TRoleId roleId, IEnumerable<TUserId> userIds, CancellationToken cancellationToken = default );
}