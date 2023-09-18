using Util.Data.Queries;
using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Services;

/// <summary>
/// 跨域策略服务
/// </summary>
public class CorsPolicyServiceBase<TApplicationService, TApplicationDto, TApplicationQuery>
    : CorsPolicyServiceBase<TApplicationService, TApplicationDto, TApplicationQuery, Guid?>
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化跨域策略服务
    /// </summary>
    /// <param name="applicationService">应用程序服务</param>
    public CorsPolicyServiceBase( TApplicationService applicationService ) : base( applicationService ) {
    }
}

/// <summary>
/// 跨域策略服务
/// </summary>
public class CorsPolicyServiceBase<TApplicationService, TApplicationDto, TApplicationQuery, TAuditUserId> : ICorsPolicyService
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase<TAuditUserId>, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化跨域策略服务
    /// </summary>
    /// <param name="applicationService">应用程序服务</param>
    public CorsPolicyServiceBase( TApplicationService applicationService ) {
        Service = applicationService ?? throw new ArgumentNullException( nameof( applicationService ) );
    }

    /// <summary>
    /// 应用程序服务
    /// </summary>
    protected TApplicationService Service { get; set; }

    /// <summary>
    /// 是否允许跨域访问
    /// </summary>
    /// <param name="origin">来源</param>
    public async Task<bool> IsOriginAllowedAsync( string origin ) {
        return await Service.IsOriginAllowedAsync( origin );
    }
}