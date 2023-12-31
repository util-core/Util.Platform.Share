using Microsoft.Extensions.Localization;
using Util.Platform.Share.Identity.Dtos;
using Util.Platform.Share.Identity.IdentityServer.Models;
using Util.Platform.Share.Identity.IdentityServer.Options;

namespace Util.Platform.Share.Identity.IdentityServer.Controllers;

/// <summary>
/// 用户认证控制器
/// </summary>
public abstract class AccountControllerBase<TSystemService, TLoginRequest> : AccountControllerBase<TSystemService, TLoginRequest, Guid, Guid>
    where TSystemService : ISystemServiceBase<TLoginRequest>
    where TLoginRequest : LoginRequestBase, new() {
    /// <summary>
    /// 初始化用户认证控制器
    /// </summary>
    /// <param name="interaction">交互服务</param>
    /// <param name="schemeProvider">认证方案提供器</param>
    /// <param name="events">事件服务</param>
    /// <param name="systemService">系统服务</param>
    /// <param name="localizer">字符串本地化</param>
    protected AccountControllerBase( IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IEventService events, TSystemService systemService, IStringLocalizer localizer )
        : base( interaction, schemeProvider, events, systemService, localizer ) {
    }
}

/// <summary>
/// 用户认证控制器
/// </summary>
public abstract class AccountControllerBase<TSystemService, TLoginRequest, TUserId, TApplicationId> : Controller
    where TSystemService : ISystemServiceBase<TLoginRequest, TUserId, TApplicationId>
    where TLoginRequest : LoginRequestBase, new() {
    /// <summary>
    /// 初始化用户认证控制器
    /// </summary>
    /// <param name="interaction">交互服务</param>
    /// <param name="schemeProvider">认证方案提供器</param>
    /// <param name="events">事件服务</param>
    /// <param name="systemService">系统服务</param>
    /// <param name="localizer">字符串本地化</param>
    protected AccountControllerBase( IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IEventService events, 
        TSystemService systemService, IStringLocalizer localizer ) {
        InteractionService = interaction ?? throw new ArgumentNullException( nameof( interaction ) );
        AuthenticationSchemeProvider = schemeProvider ?? throw new ArgumentNullException( nameof( schemeProvider ) );
        EventService = events ?? throw new ArgumentNullException( nameof( events ) );
        SystemService = systemService ?? throw new ArgumentNullException( nameof( systemService ) );
        Localizer = localizer ?? throw new ArgumentNullException( nameof( localizer ) );
    }

    /// <summary>
    /// 交互服务
    /// </summary>
    protected IIdentityServerInteractionService InteractionService { get; }
    /// <summary>
    /// 认证方案提供器
    /// </summary>
    protected IAuthenticationSchemeProvider AuthenticationSchemeProvider { get; }
    /// <summary>
    /// 事件服务
    /// </summary>
    protected IEventService EventService { get; }
    /// <summary>
    /// 系统服务
    /// </summary>
    protected TSystemService SystemService { get; }
    /// <summary>
    /// 字符串本地化
    /// </summary>
    protected IStringLocalizer Localizer { get; }

    /// <summary>
    /// 打开登录页面
    /// </summary>
    /// <param name="returnUrl">返回地址</param>
    [HttpGet]
    public virtual async Task<IActionResult> Login( string returnUrl ) {
        var model = await CreateLoginViewModel( returnUrl );
        return View( model );
    }

    /// <summary>
    /// 创建登录视图模型
    /// </summary>
    protected virtual async Task<LoginViewModel> CreateLoginViewModel( string returnUrl ) {
        var context = await InteractionService.GetAuthorizationContextAsync( returnUrl );
        if ( context?.IdP != null && await AuthenticationSchemeProvider.GetSchemeAsync( context.IdP ) != null ) {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;
            var result = new LoginViewModel {
                ReturnUrl = returnUrl,
                UserName = context.LoginHint,
            };
            if ( !local )
                result.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
            return result;
        }
        var schemes = await AuthenticationSchemeProvider.GetAllSchemesAsync();
        var providers = schemes
            .Where( t => t.DisplayName != null )
            .Select( t => new ExternalProvider { DisplayName = t.DisplayName ?? t.Name, AuthenticationScheme = t.Name } )
            .ToList();
        return new LoginViewModel {
            ReturnUrl = returnUrl,
            UserName = context?.LoginHint,
            ExternalProviders = providers.ToArray()
        };
    }

    /// <summary>
    /// 登录
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Login( LoginInputModel model ) {
        if ( model == null || model.UserName.IsEmpty() ) {
            var viewModel = new LoginViewModel {
                Message = Localizer["UserNamePrompt"]
            };
            return View( viewModel );
        }
        if ( model.Password.IsEmpty() ) {
            var viewModel = new LoginViewModel {
                Message = Localizer["PasswordPrompt"]
            };
            return View( viewModel );
        }
        var context = await InteractionService.GetAuthorizationContextAsync( model.ReturnUrl );
        if ( context == null )
            return Redirect( "~/" );
        if ( ModelState.IsValid == false ) {
            var viewModel = await CreateLoginViewModel( model );
            return View( viewModel );
        }
        var result = await SystemService.SignInAsync( ToLoginRequest( model ) );
        if ( result.State == SignInState.Failed ) {
            var viewModel = await CreateLoginViewModel( model );
            viewModel.Message = result.Message;
            return View( viewModel );
        }
        return Redirect( model.ReturnUrl );
    }

    /// <summary>
    /// 转换为登录参数
    /// </summary>
    protected virtual TLoginRequest ToLoginRequest( LoginInputModel model ) {
        return new TLoginRequest {
            UserName = model.UserName,
            Password = model.Password,
            Remember = model.Remember
        };
    }

    /// <summary>
    /// 创建登录视图模型
    /// </summary>
    protected virtual async Task<LoginViewModel> CreateLoginViewModel( LoginInputModel model ) {
        var result = await CreateLoginViewModel( model.ReturnUrl );
        result.UserName = model.UserName;
        result.Remember = model.Remember;
        return result;
    }

    /// <summary>
    /// 打开注销页面
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> Logout( string logoutId ) {
        var model = await CreateLogoutViewModel( logoutId );
        return await Logout( model );
    }

    /// <summary>
    /// 创建注销参数
    /// </summary>
    protected virtual async Task<LogoutViewModel> CreateLogoutViewModel( string logoutId ) {
        var context = await InteractionService.GetLogoutContextAsync( logoutId );
        var result = new LogoutViewModel {
            AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = context?.PostLogoutRedirectUri,
            ClientName = string.IsNullOrEmpty( context?.ClientName ) ? context?.ClientId : context?.ClientName,
            SignOutIframeUrl = context?.SignOutIFrameUrl,
            LogoutId = logoutId
        };
        if ( User?.Identity?.IsAuthenticated == true ) {
            var idp = User.FindFirst( JwtClaimTypes.IdentityProvider )?.Value;
            if ( idp != null && idp != IdentityServerConstants.LocalIdentityProvider ) {
                var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync( idp );
                if ( providerSupportsSignout == false )
                    return result;
                result.LogoutId ??= await InteractionService.CreateLogoutContextAsync();
                result.ExternalAuthenticationScheme = idp;
            }
        }
        return result;
    }

    /// <summary>
    /// 注销
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Logout( LogoutViewModel model ) {
        if ( User?.Identity?.IsAuthenticated == true ) {
            await SystemService.SignOutAsync();
            await EventService.RaiseAsync( new UserLogoutSuccessEvent( User.GetSubjectId(), User.GetDisplayName() ) );
        }
        if ( model.AutomaticRedirectAfterSignOut )
            return Redirect( model.PostLogoutRedirectUri );
        if ( model.TriggerExternalSignout ) {
            string url = Url.Action( "Logout", new { logoutId = model.LogoutId } );
            return SignOut( new AuthenticationProperties { RedirectUri = url }, model.ExternalAuthenticationScheme );
        }
        return View( model );
    }
}