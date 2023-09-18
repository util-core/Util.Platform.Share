namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 声明服务
/// </summary>
public interface IClaimServiceBase<TClaimDto, in TClaimQuery> : ICrudService<TClaimDto, TClaimQuery>
    where TClaimDto : IDto, new()
    where TClaimQuery : IPage {
    /// <summary>
    /// 获取已启用的声明列表
    /// </summary>
    Task<List<TClaimDto>> GetEnabledClaimsAsync();
}