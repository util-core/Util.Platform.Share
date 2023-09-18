using Util.Data.Queries;
using Util.Platform.Share.Identity.Dtos;
using GrantType = Util.Platform.Share.Identity.Enums.GrantType;

namespace Util.Platform.Share.Identity.IdentityServer.Services;

/// <summary>
/// 客户端存储
/// </summary>
public abstract class ClientStoreBase<TApplicationService, TApplicationDto, TApplicationQuery> 
    : ClientStoreBase<TApplicationService, TApplicationDto, TApplicationQuery,Guid?>
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化客户端存储
    /// </summary>
    /// <param name="service">应用程序服务</param>
    protected ClientStoreBase( TApplicationService service ) : base( service ) {
    }
}

/// <summary>
    /// 客户端存储
    /// </summary>
    public abstract class ClientStoreBase<TApplicationService,TApplicationDto, TApplicationQuery, TAuditUserId> : IClientStore 
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase<TAuditUserId>, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化客户端存储
    /// </summary>
    /// <param name="service">应用程序服务</param>
    protected ClientStoreBase( TApplicationService service ) {
        Service = service ?? throw new ArgumentNullException( nameof( service ) );
    }

    /// <summary>
    /// 应用程序服务
    /// </summary>
    protected TApplicationService Service { get; set; }

    /// <summary>
    /// 查找客户端
    /// </summary>
    /// <param name="clientId">客户端标识</param>
    public virtual async Task<Client> FindClientByIdAsync( string clientId ) {
        var application = await Service.GetByCodeAsync( clientId );
        if ( application == null )
            return null;
        if ( application.RedirectUri.IsEmpty() )
            throw new ArgumentNullException( application.RedirectUri );
        var result = new Client {
            ClientId = application.Code,
            ClientName = application.Name,
            Enabled = application.Enabled,
            RequireClientSecret = false,
            AllowOfflineAccess = application.AllowOfflineAccess,
            AccessTokenLifetime = application.AccessTokenLifetime.SafeValue() == 0 ? 3600: application.AccessTokenLifetime.SafeValue(),
            AllowedCorsOrigins = Util.Helpers.Convert.ToList<string>( application.AllowedCorsOrigins ),
            AllowAccessTokensViaBrowser = true
        };
        AddScopes( result, application );
        AddGrantTypes( result, application );
        AddRedirectUris( result, application );
        AddLogoutRedirectUris( result, application );
        return result;
    }

    /// <summary>
    /// 添加权限范围
    /// </summary>
    protected virtual void AddScopes( Client client, TApplicationDto application ) {
        application.AllowedScopes.ForEach( client.AllowedScopes.Add );
        if ( client.AllowedScopes.Contains( "openid" ) == false )
            client.AllowedScopes.Add( "openid" );
        if ( client.AllowedScopes.Contains( "profile" ) == false )
            client.AllowedScopes.Add( "profile" );
    }

    /// <summary>
    /// 添加授权类型
    /// </summary>
    protected virtual void AddGrantTypes( Client client, TApplicationDto application ) {
        client.AllowedGrantTypes = GetGrantTypes( application.AllowedGrantType );
    }

    /// <summary>
    /// 获取授权类型
    /// </summary>
    protected virtual ICollection<string> GetGrantTypes( GrantType? type ) {
        switch ( type ) {
            case GrantType.Implicit:
                return GrantTypes.Implicit;
            case GrantType.AuthorizationCode:
                return GrantTypes.Code;
            case GrantType.ClientCredentials:
                return GrantTypes.ClientCredentials;
            case GrantType.Hybrid:
                return GrantTypes.Hybrid;
            case GrantType.ResourceOwnerPassword:
                return GrantTypes.ResourceOwnerPassword;
            default:
                return GrantTypes.Code;
        }
    }

    /// <summary>
    /// 添加登录成功重定向地址
    /// </summary>
    protected virtual void AddRedirectUris( Client client,TApplicationDto application ) {
        var uris = Util.Helpers.Convert.ToList<string>( application.RedirectUri );
        uris.ForEach( client.RedirectUris.Add );
    }

    /// <summary>
    /// 添加退出登录重定向地址
    /// </summary>
    protected virtual void AddLogoutRedirectUris( Client client, TApplicationDto application ) {
        var logoutRedirectUri = application.PostLogoutRedirectUri;
        if ( logoutRedirectUri.IsEmpty() )
            logoutRedirectUri = application.RedirectUri;
        var uris = Util.Helpers.Convert.ToList<string>( logoutRedirectUri );
        uris.ForEach( client.PostLogoutRedirectUris.Add );
    }
}