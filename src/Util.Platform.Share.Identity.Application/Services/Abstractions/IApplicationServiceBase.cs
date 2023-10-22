namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 应用程序服务
/// </summary>
public interface IApplicationServiceBase<TApplicationDto, in TApplicationQuery> : ICrudService<TApplicationDto, TApplicationQuery> 
    where TApplicationDto : IDto, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 获取已启用的应用程序列表
    /// </summary>
    Task<List<TApplicationDto>> GetEnabledApplicationsAsync();
    /// <summary>
    /// 通过应用程序编码查找
    /// </summary>
    /// <param name="code">应用程序编码</param>
    Task<TApplicationDto> GetByCodeAsync( string code );
    /// <summary>
    /// 是否允许跨域访问
    /// </summary>
    /// <param name="origin">域名</param>
    Task<bool> IsOriginAllowedAsync( string origin );
    /// <summary>
    /// 获取权限范围
    /// </summary>
    Task<List<Item>> GetScopesAsync();
}