namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 资源仓储
/// </summary>
public interface IResourceRepositoryBase<TResource> : IResourceRepositoryBase<TResource, Guid, Guid?, Guid?>
    where TResource : class, ITreeEntity<TResource, Guid, Guid?> {
}

/// <summary>
/// 资源仓储
/// </summary>
/// <typeparam name="TResource">资源类型</typeparam>
/// <typeparam name="TResourceId">资源标识类型</typeparam>
/// <typeparam name="TResourceParentId">资源父标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
public interface IResourceRepositoryBase<TResource, in TResourceId, in TResourceParentId, in TApplicationId> : ITreeRepository<TResource, TResourceId, TResourceParentId> 
    where TResource : class, ITreeEntity<TResource, TResourceId, TResourceParentId> {
    /// <summary>
    /// 获取已启用的资源集合
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="type">资源类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TResource>> GetEnabledResourcesAsync( TApplicationId applicationId, ResourceType type, CancellationToken cancellationToken = default );
}