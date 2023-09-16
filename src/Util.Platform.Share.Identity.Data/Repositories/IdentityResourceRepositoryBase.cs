namespace Util.Platform.Share.Identity.Data.Repositories; 

/// <summary>
/// 身份资源仓储
/// </summary>
public abstract class IdentityResourceRepositoryBase<TIdentityResource, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> 
    : IIdentityResourceRepositoryBase<TIdentityResource>
    where TIdentityResource : IdentityResourceBase<TIdentityResource, TResourceId, TAuditUserId>
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 资源仓储
    /// </summary>
    private readonly IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> _resourceRepository;

    /// <summary>
    /// 初始化身份资源仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected IdentityResourceRepositoryBase( IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> resourceRepository ) {
        _resourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
    }

    /// <inheritdoc />
    public virtual async Task<TIdentityResource> FindByIdAsync( object id ) {
        var resource = await _resourceRepository.FindByIdAsync( id );
        return resource.MapTo<TIdentityResource>();
    }

    /// <inheritdoc />
    public virtual async Task<List<TIdentityResource>> GetEnabledResources() {
        var list = await _resourceRepository.FindAllAsync( t => t.Type == ResourceType.Identity && t.Enabled );
        return list.Select( resource => resource.MapTo<TIdentityResource>() ).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<bool> ExistsAsync( TIdentityResource entity ) {
        return await _resourceRepository.ExistsAsync( t => t.Type == ResourceType.Identity && !t.Id.Equals(entity.Id) && t.Uri == entity.Uri );
    }

    /// <inheritdoc />
    public virtual async Task AddAsync( TIdentityResource entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Identity;
        await _resourceRepository.AddAsync( resource );
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync( [Valid] TIdentityResource entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Identity;
        await _resourceRepository.UpdateAsync( resource );
    }
}