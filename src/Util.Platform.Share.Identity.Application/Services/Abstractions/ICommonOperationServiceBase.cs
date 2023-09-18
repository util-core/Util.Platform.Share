namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 常用操作资源服务
/// </summary>
public interface ICommonOperationServiceBase<TCommonOperationDto, in TCommonOperationQuery> : ICrudService<TCommonOperationDto, TCommonOperationQuery>
    where TCommonOperationDto : IDto, new()
    where TCommonOperationQuery : IPage {
    /// <summary>
    /// 获取已启用的常用操作名称列表
    /// </summary>
    Task<List<TCommonOperationDto>> GetEnabledNamesAsync();
}