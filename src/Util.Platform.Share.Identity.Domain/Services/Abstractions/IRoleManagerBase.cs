namespace Util.Platform.Share.Identity.Domain.Services.Abstractions;

/// <summary>
/// 角色服务
/// </summary>
public interface IRoleManagerBase<in TRole> : IRoleManagerBase<TRole, Guid, Guid> 
    where TRole : class {
}

/// <summary>
/// 角色服务
/// </summary>
public interface IRoleManagerBase<in TRole, in TRoleId, in TUserId> : IDomainService where TRole : class {
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="entity">角色</param>
    Task CreateAsync( TRole entity );
    /// <summary>
    /// 修改角色
    /// </summary>
    /// <param name="entity">角色</param>
    Task UpdateAsync( TRole entity );
    /// <summary>
    /// 添加用户集合到角色
    /// </summary>
    /// <param name="roleId">角色标识</param>
    /// <param name="userIds">用户标识列表</param>
    Task AddUsersToRoleAsync( TRoleId roleId, IEnumerable<TUserId> userIds );
    /// <summary>
    /// 从角色移除用户集合
    /// </summary>
    /// <param name="roleId">角色标识</param>
    /// <param name="userIds">用户标识列表</param>
    Task RemoveUsersFromRoleAsync( TRoleId roleId, IEnumerable<TUserId> userIds );
}