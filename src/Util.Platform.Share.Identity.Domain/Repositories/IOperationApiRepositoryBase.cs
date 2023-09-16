namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 操作Api仓储
/// </summary>
/// <typeparam name="TOperationApi">操作Api类型</typeparam>
/// <typeparam name="TOperationApiId">操作Api标识类型</typeparam>
/// <typeparam name="TOperationId">操作标识类型</typeparam>
/// <typeparam name="TResourceId">Api资源标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
public interface IOperationApiRepositoryBase<TOperationApi, in TOperationApiId, in TOperationId, TResourceId, in TApplicationId> : IRepository<TOperationApi, TOperationApiId> 
    where TOperationApi : class, IAggregateRoot<TOperationApiId> {
    /// <summary>
    /// 通过操作资源标识获取Api资源标识列表
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    Task<List<TResourceId>> GetApiIdsByOperationIdAsync( TOperationId operationId );
    /// <summary>
    /// 通过操作资源标识获取Api资源标识列表
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    /// <param name="apiApplicationId">Api资源所属应用标识</param>
    Task<List<TResourceId>> GetApiIdsByOperationIdAsync( TOperationId operationId, TApplicationId apiApplicationId );
    /// <summary>
    /// 移除操作Api
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    Task RemoveAsync( TOperationId operationId );
    /// <summary>
    /// 移除操作Api
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    /// <param name="apiResourceIds">Api资源标识列表</param>
    Task RemoveAsync( TOperationId operationId, List<TResourceId> apiResourceIds );
}