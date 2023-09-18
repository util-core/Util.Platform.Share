namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 操作仓储
/// </summary>
public abstract class OperationRepositoryBase<TOperation, TResource, TApplication> 
    : OperationRepositoryBase<TOperation, TResource, Guid, Guid?, TApplication, Guid?, Guid?>, IOperationRepositoryBase<TOperation>
    where TOperation : OperationBase<TOperation>
    where TResource : ResourceBase<TResource, TApplication> 
    where TApplication : ApplicationBase<TApplication> {
    /// <summary>
    /// 初始化操作仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected OperationRepositoryBase( IResourceRepositoryBase<TResource> resourceRepository ) : base( resourceRepository ) {
    }
}

/// <summary>
/// 操作仓储
/// </summary>
public abstract class OperationRepositoryBase<TOperation, TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId>
    : IOperationRepositoryBase<TOperation, TApplicationId, TResourceId>
    where TOperation : OperationBase<TOperation, TResourceId, TApplicationId, TAuditUserId>
    where TResource : ResourceBase<TResource, TResourceId, TResourceParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 资源仓储
    /// </summary>
    private readonly IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> _resourceRepository;

    /// <summary>
    /// 初始化操作仓储
    /// </summary>
    /// <param name="resourceRepository">资源仓储</param>
    protected OperationRepositoryBase( IResourceRepositoryBase<TResource, TResourceId, TResourceParentId, TApplicationId> resourceRepository ) {
        _resourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
    }

    /// <inheritdoc />
    public virtual async Task<TOperation> FindByIdAsync( object id ) {
        var resource = await _resourceRepository.FindByIdAsync( id );
        return resource.MapTo<TOperation>();
    }

    /// <inheritdoc />
    public virtual async Task<List<TOperation>> GetEnabledResources() {
        var resources = await _resourceRepository.FindAllAsync( t => t.Type == ResourceType.Operation && t.Enabled );
        return resources.Select( t => t.MapTo<TOperation>() ).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<int> GenerateSortIdAsync( TApplicationId applicationId, TResourceId moduleId ) {
        var maxSortId = await _resourceRepository.Find( t => t.ApplicationId.Equals( applicationId ) && t.ParentId.Equals( moduleId ) ).MaxAsync( t => t.SortId );
        return maxSortId.SafeValue() + 1;
    }

    /// <inheritdoc />
    public virtual async Task AddAsync( TOperation entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Operation;
        resource.ParentId = Util.Helpers.Convert.To<TResourceParentId>( entity.ModuleId );
        resource.SortId ??= await GenerateSortIdAsync( entity.ApplicationId, entity.ModuleId );
        await _resourceRepository.AddAsync( resource );
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync( TOperation entity ) {
        var resource = entity.MapTo<TResource>();
        resource.Type = ResourceType.Operation;
        resource.ParentId = Util.Helpers.Convert.To<TResourceParentId>( entity.ModuleId );
        await _resourceRepository.UpdateAsync( resource );
    }
}