namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 资源仓储
/// </summary>
public abstract class ResourceRepositoryBase<TUnitOfWork, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    : TreeRepositoryBase<TResource, TResourceId, TResourceParentId>, IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化资源仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected ResourceRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    /// <inheritdoc />
    public virtual async Task<List<TResource>> GetEnabledResourcesAsync( TApplicationId applicationId, ResourceType type, CancellationToken cancellationToken = default ) {
        return await Find( t => t.ApplicationId.Equals( applicationId ) && t.Type == type && t.Enabled ).ToListAsync( cancellationToken );
    }
}