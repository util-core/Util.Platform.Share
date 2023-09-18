namespace Util.Platform.Share.Identity.IdentityServer.TenantResolvers;

/// <summary>
/// 身份认证租户解析器
/// </summary>
public class IdentityTenantResolver : TenantResolverBase {
    /// <summary>
    /// 解析租户标识
    /// </summary>
    protected override Task<string> Resolve( HttpContext context ) {
        var key = GetTenantKey( context );
        var returnUrl = context.Request.Query["ReturnUrl"].FirstOrDefault();
        if ( returnUrl.IsEmpty() )
            return null;
        var items = Util.Helpers.Web.ParseQueryString( returnUrl );
        var values = items.GetValues( key );
        if ( values == null )
            return null;
        var tenantId = values.FirstOrDefault();
        GetLog( context ).LogTrace( $"执行身份认证租户解析器,{key}={tenantId}" );
        return Task.FromResult( tenantId );
    }
}