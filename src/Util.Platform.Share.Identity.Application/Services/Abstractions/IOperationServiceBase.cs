namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 操作资源服务
/// </summary>
public interface IOperationServiceBase<TOperationDto, in TResourceQuery, in TCreateOperationRequest> 
    : IQueryService<TOperationDto, TResourceQuery> 
    where TOperationDto : new() 
    where TResourceQuery : IPage 
    where TCreateOperationRequest : class {
    /// <summary>
    /// 创建操作资源
    /// </summary>
    /// <param name="request">创建操作资源参数</param>
    Task<string> CreateAsync( TCreateOperationRequest request );
    /// <summary>
    /// 更新操作资源
    /// </summary>
    /// <param name="request">操作资源参数</param>
    Task UpdateAsync( TOperationDto request );
    /// <summary>
    /// 删除操作资源
    /// </summary>
    /// <param name="ids">标识列表</param>
    Task DeleteAsync( string ids );
}