namespace Util.Platform.Share.Identity.IdentityServer.Options; 

/// <summary>
/// 安全头过滤器
/// </summary>
public class SecurityHeadersAttribute : ActionFilterAttribute {
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context">执行上下文</param>
    public override void OnResultExecuting( ResultExecutingContext context ) {
        if ( context.Result is not ViewResult )
            return;
        if ( !context.HttpContext.Response.Headers.ContainsKey( "X-Content-Type-Options" ) ) {
            context.HttpContext.Response.Headers.Add( "X-Content-Type-Options", "nosniff" );
        }
        if ( !context.HttpContext.Response.Headers.ContainsKey( "X-Frame-Options" ) ) {
            context.HttpContext.Response.Headers.Add( "X-Frame-Options", "SAMEORIGIN" );
        }
        var csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";
        if ( !context.HttpContext.Response.Headers.ContainsKey( "Content-Security-Policy" ) ) {
            context.HttpContext.Response.Headers.Add( "Content-Security-Policy", csp );
        }
        if ( !context.HttpContext.Response.Headers.ContainsKey( "X-Content-Security-Policy" ) ) {
            context.HttpContext.Response.Headers.Add( "X-Content-Security-Policy", csp );
        }
        if ( !context.HttpContext.Response.Headers.ContainsKey( "Referrer-Policy" ) ) {
            context.HttpContext.Response.Headers.Add( "Referrer-Policy", "no-referrer" );
        }
    }
}