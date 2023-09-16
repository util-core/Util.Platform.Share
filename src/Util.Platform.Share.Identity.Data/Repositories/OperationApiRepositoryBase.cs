namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 操作Api仓储
/// </summary>
public abstract class OperationApiRepositoryBase<TUnitOfWork, TOperationApi, TOperationApiId, TOperationId, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    : RepositoryBase<TOperationApi, TOperationApiId>, IOperationApiRepositoryBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TApplicationId>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    where TOperationApi : OperationApiBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TAuditUserId> {
    /// <summary>
    /// 初始化操作Api仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected OperationApiRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    /// <inheritdoc />
    public virtual async Task<List<TResourceId>> GetApiIdsByOperationIdAsync( TOperationId operationId ) {
        return await Find( t => t.OperationId.Equals( operationId ) ).Select( t => t.ApiId ).ToListAsync();
    }

    /// <inheritdoc />
    public virtual async Task<List<TResourceId>> GetApiIdsByOperationIdAsync( TOperationId operationId, TApplicationId apiApplicationId ) {
        var query = from operationApi in Find()
                    join resource in UnitOfWork.Set<TResource>() on operationApi.ApiId equals resource.Id
                    where resource.ApplicationId.Equals( apiApplicationId ) &&
                          operationApi.OperationId.Equals( operationId )
                    select operationApi.ApiId;
        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public virtual async Task RemoveAsync( TOperationId operationId ) {
        var result = await GetOperationApisAsync( operationId );
        await RemoveAsync( result );
    }

    /// <summary>
    /// 获取操作Api列表
    /// </summary>
    protected virtual async Task<List<TOperationApi>> GetOperationApisAsync( TOperationId operationId ) {
        return await Find( t => t.OperationId.Equals( operationId ) ).ToListAsync();
    }

    /// <inheritdoc />
    public virtual async Task RemoveAsync( TOperationId operationId, List<TResourceId> apiResourceIds ) {
        var result = await GetOperationApisAsync( operationId, apiResourceIds );
        await RemoveAsync( result );
    }

    /// <summary>
    /// 获取操作Api列表
    /// </summary>
    protected virtual async Task<List<TOperationApi>> GetOperationApisAsync( TOperationId operationId, List<TResourceId> apiResourceIds ) {
        return await Find( t => t.OperationId.Equals( operationId ) && apiResourceIds.Contains( t.ApiId ) ).ToListAsync();
    }
}