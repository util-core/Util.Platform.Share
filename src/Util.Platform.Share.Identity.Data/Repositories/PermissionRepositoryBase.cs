namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 权限仓储
/// </summary>
public abstract class PermissionRepositoryBase<TUnitOfWork, TPermission, TApplication, TResource> 
    : PermissionRepositoryBase<TUnitOfWork, TPermission, Guid, TApplication, Guid?, Guid, TResource, Guid, Guid?, Guid?>, IPermissionRepositoryBase<TPermission>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>
    where TResource : ResourceBase<TResource, TApplication> 
    where TApplication : ApplicationBase<TApplication> {
    /// <summary>
    /// 初始化权限仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected PermissionRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}

/// <summary>
/// 权限仓储
/// </summary>
public abstract class PermissionRepositoryBase<TUnitOfWork, TPermission, TPermissionId, TApplication, TApplicationId, TRoleId, TResource, TResourceId, TResourceParentId, TAuditUserId>
    : RepositoryBase<TPermission, TPermissionId>, IPermissionRepositoryBase<TPermission, TPermissionId, TApplicationId, TRoleId, TResourceId>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TPermissionId, TRoleId, TResource, TResourceId, TAuditUserId>
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {

    #region 构造方法

    /// <summary>
    /// 初始化权限仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected PermissionRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    #endregion

    #region ClearOperationPermissionsAsync

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TPermissionId>> ClearOperationPermissionsAsync( TApplicationId applicationId, TRoleId roleId ) {
        var permissions = await FindAllAsync( t => t.Resource.ApplicationId.Equals( applicationId ) && t.RoleId.Equals( roleId ) &&
                                                  ( t.Resource.Type == ResourceType.Module || t.Resource.Type == ResourceType.Operation ) );
        await RemoveAsync( permissions );
        return permissions.Select( t => t.Id );
    }

    #endregion

    #region ClearApiPermissionsAsync

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TPermissionId>> ClearApiPermissionsAsync( TApplicationId applicationId, TRoleId roleId ) {
        var permissions = await FindAllAsync( t => t.Resource.ApplicationId.Equals( applicationId ) && t.RoleId.Equals( roleId ) && t.Resource.Type == ResourceType.Api );
        await RemoveAsync( permissions );
        return permissions.Select( t => t.Id );
    }

    #endregion

    #region GetOperationPermissionResourceIdsAsync

    /// <inheritdoc />
    public virtual async Task<List<TResourceId>> GetOperationPermissionResourceIdsAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny ) {
        return await Find( t => t.Resource.ApplicationId.Equals( applicationId ) && t.RoleId.Equals( roleId ) && t.IsDeny == isDeny &&
                               ( t.Resource.Type == ResourceType.Module || t.Resource.Type == ResourceType.Operation ) )
            .Select( t => t.ResourceId )
            .ToListAsync();
    }

    #endregion

    #region GetApiPermissionResourceIdsAsync

    /// <inheritdoc />
    public virtual async Task<List<TResourceId>> GetApiPermissionResourceIdsAsync( TApplicationId applicationId, TRoleId roleId, bool isDeny ) {
        return await Find( t => t.Resource.ApplicationId.Equals( applicationId ) && t.RoleId.Equals( roleId ) && t.IsDeny == isDeny && t.Resource.Type == ResourceType.Api )
            .Select( t => t.ResourceId )
            .ToListAsync();
    }

    #endregion

    #region RemoveAsync

    /// <inheritdoc />
    public virtual async Task RemoveAsync( TRoleId roleId, List<TResourceId> resourceIds, bool isDeny ) {
        var result = await GetPermissionsAsync( roleId, resourceIds, isDeny );
        await RemoveAsync( result );
    }

    /// <summary>
    ///获取权限列表
    /// </summary>
    protected virtual async Task<List<TPermission>> GetPermissionsAsync( TRoleId roleId, List<TResourceId> resourceIds, bool isDeny ) {
        return await Find().Where( t => t.RoleId.Equals( roleId ) && t.IsDeny == isDeny && resourceIds.Contains( t.ResourceId ) ).ToListAsync();
    }

    #endregion
}