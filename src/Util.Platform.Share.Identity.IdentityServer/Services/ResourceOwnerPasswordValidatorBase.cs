using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Services; 

/// <summary>
/// 密码验证器
/// </summary>
public abstract class ResourceOwnerPasswordValidatorBase<TSystemService, TLoginRequest> : IResourceOwnerPasswordValidator
    where TSystemService : ISystemServiceBase<TLoginRequest>
    where TLoginRequest : LoginRequestBase, new() {

    /// <summary>
    /// 初始化密码验证器
    /// </summary>
    /// <param name="service">系统服务</param>
    protected ResourceOwnerPasswordValidatorBase( TSystemService service ) {
        SystemService = service ?? throw new ArgumentNullException( nameof( service ) );
    }

    /// <summary>
    /// 系统服务
    /// </summary>
    protected TSystemService SystemService { get; }

    /// <summary>
    /// 验证
    /// </summary>
    public async Task ValidateAsync( ResourceOwnerPasswordValidationContext context ) {
        var request = new TLoginRequest {
            Account = context.UserName,
            Password = context.Password
        };
        try {
            var result = await SystemService.SignInAsync( request );
            if ( result.State == SignInState.Succeeded ) {
                context.Result = new GrantValidationResult( result.UserId, OidcConstants.AuthenticationMethods.Password );
                return;
            }
            if ( result.State == SignInState.Failed )
                context.Result = new GrantValidationResult( TokenRequestErrors.InvalidGrant );
        }
        catch ( Exception ex ) {
            context.Result = new GrantValidationResult( TokenRequestErrors.InvalidGrant, ex.GetPrompt() );
        }
    }
}