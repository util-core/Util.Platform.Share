namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// Api资源服务
/// </summary>
public interface IApiResourceServiceBase<TApiResourceDto, in TCreateApiResourceRequest, in TResourceQuery>
    : ITreeService<TApiResourceDto, TCreateApiResourceRequest, TApiResourceDto, TResourceQuery> 
    where TApiResourceDto: ITreeNode, new()
    where TCreateApiResourceRequest: IRequest, new()
    where TResourceQuery : ITreeQueryParameter {
    /// <summary>
    /// 获取Api资源列表
    /// </summary>
    /// <param name="uri">资源标识列表</param>
    Task<List<TApiResourceDto>> GetResourcesAsync( List<string> uri );
    /// <summary>
    /// 获取已启用的Api资源列表,仅返回第一层级
    /// </summary>
    Task<List<TApiResourceDto>> GetEnabledResourcesAsync();
}