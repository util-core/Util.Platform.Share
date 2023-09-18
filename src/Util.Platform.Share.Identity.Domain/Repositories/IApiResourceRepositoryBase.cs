using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// Api资源仓储
/// </summary>
public interface IApiResourceRepositoryBase<TApiResource> : IApiResourceRepositoryBase<TApiResource, Guid, Guid?> 
    where TApiResource : ApiResourceBase<TApiResource> {
}

/// <summary>
/// Api资源仓储
/// </summary>
/// <typeparam name="TApiResource">Api资源类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TResourceParentId">Api资源父标识类型</typeparam>
public interface IApiResourceRepositoryBase<TApiResource, in TApplicationId, in TResourceParentId> : IScopeDependency where TApiResource : class {
    /// <summary>
    /// 通过标识查找Api资源
    /// </summary>
    /// <param name="id">Api资源标识</param>
    Task<TApiResource> FindByIdAsync( object id );
    /// <summary>
    /// 获取已启用的Api资源列表,仅返回第一层级
    /// </summary>
    Task<List<TApiResource>> GetEnabledResources();
    /// <summary>
    /// 生成排序号
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="parentId">父标识</param>
    Task<int> GenerateSortIdAsync( TApplicationId applicationId, TResourceParentId parentId );
    /// <summary>
    /// 添加Api资源
    /// </summary>
    /// <param name="entity">Api资源</param>
    Task AddAsync( [Valid] TApiResource entity );
    /// <summary>
    /// 修改Api资源
    /// </summary>
    /// <param name="entity">Api资源</param>
    Task UpdateAsync( [Valid] TApiResource entity );
}