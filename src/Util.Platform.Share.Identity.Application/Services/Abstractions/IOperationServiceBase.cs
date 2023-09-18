namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 操作资源服务
/// </summary>
public interface IOperationServiceBase<TOperationDto, in TResourceQuery, in TCreateOperationRequest, TApiResourceDto> 
    : IOperationServiceBase<TOperationDto, TResourceQuery, TCreateOperationRequest, TApiResourceDto, Guid, Guid>
    where TOperationDto : new()
    where TResourceQuery : IPage
    where TCreateOperationRequest : class
    where TApiResourceDto : class {
}

/// <summary>
/// 操作资源服务
/// </summary>
public interface IOperationServiceBase<TOperationDto, in TResourceQuery, in TCreateOperationRequest, TApiResourceDto, in TOperationId, TResourceId> 
    : IQueryService<TOperationDto, TResourceQuery> 
    where TOperationDto : new() 
    where TResourceQuery : IPage 
    where TCreateOperationRequest : class 
    where TApiResourceDto : class {
    /// <summary>
    /// 创建操作资源
    /// </summary>
    /// <param name="request">创建操作资源参数</param>
    Task<string> CreateAsync( [NotNull][Valid] TCreateOperationRequest request );
    /// <summary>
    /// 更新操作资源
    /// </summary>
    /// <param name="request">操作资源参数</param>
    Task UpdateAsync( [NotNull][Valid] TOperationDto request );
    /// <summary>
    /// 删除操作资源
    /// </summary>
    /// <param name="ids">标识列表</param>
    Task DeleteAsync( string ids );
    /// <summary>
    /// 获取操作关联的Api资源标识列表
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    Task<List<TResourceId>> GetApiResourceIdsAsync( TOperationId operationId );
    /// <summary>
    /// 获取操作关联的Api资源列表
    /// </summary>
    /// <param name="operationId">操作资源标识</param>
    Task<List<TApiResourceDto>> GetApiResourcesAsync( TOperationId operationId );
}