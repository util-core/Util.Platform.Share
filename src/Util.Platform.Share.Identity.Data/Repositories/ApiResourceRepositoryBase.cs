namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// Api资源仓储
/// </summary>
public abstract class ApiResourceRepositoryBase<TApiResource, TResource, TApplication> : ApiResourceRepositoryBase<TApiResource, TResource, Guid, Guid?, TApplication, Guid?, Guid?>
    where TApiResource : ApiResourceBase<TApiResource>
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication> {
    /// <summary>
    /// 初始化Api资源仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected ApiResourceRepositoryBase( IResourceRepositoryBase<TResource> resourceRepository ) : base( resourceRepository ) {
    }
}

/// <summary>
/// Api资源仓储
/// </summary>
public abstract class ApiResourceRepositoryBase<TApiResource, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    : IApiResourceRepositoryBase<TApiResource, TApplicationId, TResourceParentId>
    where TApiResource : ApiResourceBase<TApiResource, TResourceId, TResourceParentId, TApplicationId, TAuditUserId>
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 资源仓储
    /// </summary>
    private readonly IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> _resourceRepository;

    /// <summary>
    /// 初始化Api资源仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected ApiResourceRepositoryBase( IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> resourceRepository ) {
        _resourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
    }

    /// <inheritdoc />
    public virtual async Task<TApiResource> FindByIdAsync( object id ) {
        var resource = await _resourceRepository.FindByIdAsync( id );
        return resource.MapTo<TApiResource>();
    }

    /// <inheritdoc />
    public virtual async Task<List<TApiResource>> GetEnabledResources() {
        var resources = await _resourceRepository.FindAllAsync( t => t.Type == ResourceType.Api && t.Enabled && t.Level == 1 && t.Uri != null );
        return resources.Select( t => t.MapTo<TApiResource>() ).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<int> GenerateSortIdAsync( TApplicationId applicationId, TResourceParentId parentId ) {
        var maxSortId = await _resourceRepository.Find( t => t.ApplicationId.Equals( applicationId ) && t.ParentId.Equals( parentId ) ).MaxAsync( t => t.SortId );
        return maxSortId.SafeValue() + 1;
    }

    /// <inheritdoc />
    public virtual async Task AddAsync( TApiResource entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Api;
        await _resourceRepository.AddAsync( resource );
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync( TApiResource entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Api;
        await _resourceRepository.UpdatePathAsync( resource );
        await _resourceRepository.UpdateAsync( resource );
    }
}