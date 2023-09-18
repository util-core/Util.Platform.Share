namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 权限仓储
/// </summary>
public interface IPermissionRepositoryBase<TPermission> : IPermissionRepositoryBase<TPermission, Guid, Guid?, Guid, Guid> 
    where TPermission : class, IAggregateRoot<Guid> {
}

/// <summary>
/// 权限仓储
/// </summary>
/// <typeparam name="TPermission">权限类型</typeparam>
/// <typeparam name="TPermissionId">权限标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TRoleId">角色标识类型</typeparam>
/// <typeparam name="TResourceId">资源标识类型</typeparam>
public interface IPermissionRepositoryBase<TPermission, TPermissionId, in TApplicationId, in TRoleId, TResourceId> : IRepository<TPermission, TPermissionId> where TPermission : class, IAggregateRoot<TPermissionId> {
    /// <summary>
    /// 清除指定角色全部操作权限
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    Task<IEnumerable<TPermissionId>> ClearOperationPermissionsAsync( TApplicationId applicationId, TRoleId roleId );
    /// <summary>
    /// 清除指定角色全部Api权限
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    Task<IEnumerable<TPermissionId>> ClearApiPermissionsAsync( TApplicationId applicationId, TRoleId roleId );
    /// <summary>
    /// 获取指定角色授予或拒绝的操作权限资源标识列表
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    /// <param name="isDeny">是否拒绝</param>
    Task<List<TResourceId>> GetOperationPermissionResourceIdsAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny );
    /// <summary>
    /// 获取指定角色授予或拒绝的Api权限资源标识列表
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="roleId">角色标识</param>
    /// <param name="isDeny">是否拒绝</param>
    Task<List<TResourceId>> GetApiPermissionResourceIdsAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny );
    /// <summary>
    /// 移除权限
    /// </summary>
    /// <param name="roleId">角色标识</param>
    /// <param name="resourceIds">资源标识列表</param>
    /// <param name="isDeny">是否拒绝</param>
    Task RemoveAsync( TRoleId roleId, List<TResourceId> resourceIds, bool isDeny );
}