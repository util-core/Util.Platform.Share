using Util.Platform.Share.CacheKeys;

namespace Util.Platform.Share.Middlewares;

/// <summary>
/// 访问控制列表加载中间件
/// </summary>
public class LoadAclMiddleware {
    /// <summary>
    /// 中间件管道
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 初始化访问控制列表加载中间件
    /// </summary>
    /// <param name="next">中间件管道</param>
    public LoadAclMiddleware( RequestDelegate next ) {
        _next = next;
    }

    /// <summary>
    /// 执行管道
    /// </summary>
    /// <param name="httpContext">Http上下文</param>
    public async Task InvokeAsync( HttpContext httpContext ) {
        if ( _next == null )
            return;
        if ( httpContext == null ) {
            await _next( httpContext );
            return;
        }
        await LoadAcl( httpContext );
        await _next( httpContext );
    }

    /// <summary>
    /// 加载访问控制列表
    /// </summary>
    private async Task LoadAcl( HttpContext httpContext ) {
        var session = httpContext.RequestServices.GetRequiredService<Util.Sessions.ISession>();
        if ( session.IsAuthenticated == false )
            return;
        var userId = session.UserId;
        if( userId.IsEmpty() )
            return;
        var applicationId = session.GetApplicationId().SafeString();
        if ( applicationId.IsEmpty() )
            return;
        var cache = httpContext.RequestServices.GetRequiredService<ICache>();
        var key = new IsLoadAclCacheKey( userId, applicationId );
        var exists = await cache.ExistsAsync( key );
        if ( exists )
            return;
        await cache.SetAsync( key, 1 );
        var service = httpContext.RequestServices.GetRequiredService<IAclService>();
        await service.SetAclAsync( userId, applicationId );
    }
}