using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 模块仓储
/// </summary>
public interface IModuleRepositoryBase<TModule> : IModuleRepositoryBase<TModule, Guid, Guid?, Guid?>
    where TModule:ModuleBase<TModule> {
}

/// <summary>
/// 模块仓储
/// </summary>
/// <typeparam name="TModule">模块类型</typeparam>
/// <typeparam name="TModuleId">模块标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TModuleParentId">模块父标识类型</typeparam>
public interface IModuleRepositoryBase<TModule, in TModuleId, in TApplicationId, in TModuleParentId> : IScopeDependency where TModule : class {
    /// <summary>
    /// 通过标识查找模块
    /// </summary>
    /// <param name="id">模块标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TModule> FindByIdAsync( object id, CancellationToken cancellationToken = default );
    /// <summary>
    /// 通过标识列表查找模块列表
    /// </summary>
    /// <param name="ids">标识列表</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TModule>> FindByIdsAsync( IEnumerable<TModuleId> ids, CancellationToken cancellationToken = default );
    /// <summary>
    /// 通过标识列表查找模块列表
    /// </summary>
    /// <param name="ids">标识列表</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TModule>> FindByIdsNoTrackingAsync( IEnumerable<TModuleId> ids, CancellationToken cancellationToken = default );
    /// <summary>
    /// 生成排序号
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="parentId">父标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<int> GenerateSortIdAsync( TApplicationId applicationId, TModuleParentId parentId, CancellationToken cancellationToken = default );
    /// <summary>
    /// 判断模块是否存在
    /// </summary>
    /// <param name="entity">模块</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> ExistsAsync( TModule entity, CancellationToken cancellationToken = default );
    /// <summary>
    /// 添加模块
    /// </summary>
    /// <param name="entity">模块</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task AddAsync( [Valid] TModule entity, CancellationToken cancellationToken = default );
    /// <summary>
    /// 修改模块
    /// </summary>
    /// <param name="entity">模块</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task UpdateAsync( [Valid] TModule entity, CancellationToken cancellationToken = default );
    /// <summary>
    /// 获取已启用的模块集合
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TModule>> GetEnabledModulesAsync( TApplicationId applicationId, CancellationToken cancellationToken = default );
}