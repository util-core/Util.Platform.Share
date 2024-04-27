namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 身份资源服务
/// </summary>
public interface IIdentityResourceServiceBase<TIdentityResourceDto, in TResourceQuery> : IQueryService<TIdentityResourceDto, TResourceQuery> 
    where TIdentityResourceDto : new()
    where TResourceQuery : IPage {
    /// <summary>
    /// 获取身份资源列表
    /// </summary>
    /// <param name="uri">资源标识列表</param>
    Task<List<TIdentityResourceDto>> GetResourcesAsync( List<string> uri );
    /// <summary>
    /// 获取已启用的身份资源列表
    /// </summary>
    Task<List<TIdentityResourceDto>> GetEnabledResourcesAsync();
    /// <summary>
    /// 创建身份资源
    /// </summary>
    /// <param name="request">身份资源参数</param>
    Task<string> CreateAsync( TIdentityResourceDto request );
    /// <summary>
    /// 更新身份资源
    /// </summary>
    /// <param name="request">身份资源参数</param>
    Task UpdateAsync( TIdentityResourceDto request );
    /// <summary>
    /// 删除身份资源
    /// </summary>
    /// <param name="ids">标识列表</param>
    Task DeleteAsync( string ids );
}