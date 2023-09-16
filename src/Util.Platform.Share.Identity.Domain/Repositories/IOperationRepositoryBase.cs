namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 操作仓储
/// </summary>
/// <typeparam name="TOperation">操作类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TResourceId">模块标识类型</typeparam>
public interface IOperationRepositoryBase<TOperation, in TApplicationId, in TResourceId> : IScopeDependency where TOperation : class {
    /// <summary>
    /// 通过标识查找操作
    /// </summary>
    /// <param name="id">操作标识</param>
    Task<TOperation> FindByIdAsync( object id );
    /// <summary>
    /// 获取已启用的操作资源列表
    /// </summary>
    Task<List<TOperation>> GetEnabledResources();
    /// <summary>
    /// 生成排序号
    /// </summary>
    /// <param name="applicationId">应用程序标识</param>
    /// <param name="moduleId">模块标识</param>
    Task<int> GenerateSortIdAsync( TApplicationId applicationId, TResourceId moduleId );
    /// <summary>
    /// 添加操作
    /// </summary>
    /// <param name="entity">操作</param>
    Task AddAsync( [Valid] TOperation entity );
    /// <summary>
    /// 修改操作
    /// </summary>
    /// <param name="entity">操作</param>
    Task UpdateAsync( [Valid] TOperation entity );
}