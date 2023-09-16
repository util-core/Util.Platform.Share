namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 模块仓储
/// </summary>
public abstract class ModuleRepositoryBase<TModule, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    : IModuleRepositoryBase<TModule, TResourceId, TApplicationId, TResourceParentId>
    where TModule : ModuleBase<TModule, TResourceId, TResourceParentId, TApplicationId, TAuditUserId>
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 资源仓储
    /// </summary>
    private readonly IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> _resourceRepository;

    /// <summary>
    /// 初始化模块仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected ModuleRepositoryBase( IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> resourceRepository ) {
        _resourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
    }

    /// <inheritdoc />
    public async Task<TModule> FindByIdAsync( object id, CancellationToken cancellationToken = default ) {
        var resource = await _resourceRepository.FindByIdAsync( id, cancellationToken );
        return resource.MapTo<TModule>();
    }

    /// <inheritdoc />
    public async Task<List<TModule>> FindByIdsAsync( IEnumerable<TResourceId> ids, CancellationToken cancellationToken = default ) {
        var resources = await _resourceRepository.FindByIdsAsync( ids, cancellationToken );
        return resources.Select( t => t.MapTo<TModule>() ).ToList();
    }

    /// <inheritdoc />
    public async Task<int> GenerateSortIdAsync( TApplicationId applicationId, TResourceParentId parentId, CancellationToken cancellationToken = default ) {
        var maxSortId = await _resourceRepository.Find( t => t.ApplicationId.Equals( applicationId ) && t.ParentId.Equals( parentId ) ).MaxAsync( t => t.SortId, cancellationToken );
        return maxSortId.SafeValue() + 1;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync( TModule entity, CancellationToken cancellationToken = default ) {
        return await _resourceRepository.ExistsAsync( t => !t.Id.Equals( entity.Id ) && t.ApplicationId.Equals( entity.ApplicationId ) && t.ParentId.Equals( entity.ParentId ) && t.Name == entity.Name, cancellationToken );
    }

    /// <inheritdoc />
    public async Task AddAsync( TModule entity, CancellationToken cancellationToken = default ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Module;
        await _resourceRepository.AddAsync( resource, cancellationToken );
    }

    /// <inheritdoc />
    public async Task UpdateAsync( TModule entity, CancellationToken cancellationToken = default ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Module;
        await _resourceRepository.UpdatePathAsync( resource );
        await _resourceRepository.UpdateAsync( resource, cancellationToken );
    }

    /// <inheritdoc />
    public async Task<List<TModule>> GetEnabledModulesAsync( TApplicationId applicationId, CancellationToken cancellationToken = default ) {
        var resources = await _resourceRepository.GetEnabledResourcesAsync( applicationId, ResourceType.Module, cancellationToken );
        return resources.Select( t => t.MapTo<TModule>() ).ToList();
    }
}