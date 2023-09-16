using Util.Platform.Share.Identity.Domain.Identity;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;
using SignInResult = Util.Platform.Share.Identity.Domain.Results.SignInResult;

namespace Util.Platform.Share.Identity.Domain.Services.Implements;

/// <summary>
/// 登录服务
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class SignInManagerBase<TUser, TUserId, TRole, TAuditUserId> : ISignInManagerBase<TUser, TUserId, TRole, TAuditUserId>
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId>
    where TRole : class {
    /// <summary>
    /// 初始化登录服务
    /// </summary>
    /// <param name="signInManager">Identity登录服务</param>
    /// <param name="userManager">用户服务</param>
    /// <param name="localizer">本地化查找器</param>
    protected SignInManagerBase( IdentitySignInManagerBase<TUser, TUserId, TRole, TAuditUserId> signInManager, IUserManagerBase<TUser, TUserId, TRole, TAuditUserId> userManager, IStringLocalizer localizer ) {
        IdentitySignInManager = signInManager ?? throw new ArgumentNullException( nameof( signInManager ) );
        UserManager = userManager ?? throw new ArgumentNullException( nameof( userManager ) );
        Localizer = localizer ?? throw new ArgumentNullException( nameof( localizer ) );
    }

    /// <summary>
    /// Identity登录服务
    /// </summary>
    protected IdentitySignInManagerBase<TUser, TUserId, TRole, TAuditUserId> IdentitySignInManager { get; }
    /// <summary>
    /// 用户服务
    /// </summary>
    protected IUserManagerBase<TUser, TUserId, TRole, TAuditUserId> UserManager { get; }
    /// <summary>
    /// 本地化查找器
    /// </summary>
    protected IStringLocalizer Localizer { get; }

    /// <inheritdoc />
    public async Task<SignInResult> SignInAsync( TUser user, string password, bool isPersistent, bool lockoutOnFailure ) {
        if ( user == null )
            return new SignInResult( SignInState.Failed, null, Localizer["InvalidAccountOrPassword"] );
        var signInResult = await IdentitySignInManager.PasswordSignInAsync( user, password, isPersistent, lockoutOnFailure );
        return GetSignInResult( user, signInResult );
    }

    /// <summary>
    /// 获取登录结果
    /// </summary>
    private SignInResult GetSignInResult( TUser user, Microsoft.AspNetCore.Identity.SignInResult signInResult ) {
        if ( signInResult.IsNotAllowed )
            return new SignInResult( SignInState.Failed, null, Localizer["UserIsDisabled"] );
        if ( signInResult.IsLockedOut )
            return new SignInResult( SignInState.Failed, null, Localizer["UserLockedOut"] );
        if ( signInResult.Succeeded )
            return new SignInResult( SignInState.Succeeded, user.Id.SafeString() );
        if ( signInResult.RequiresTwoFactor )
            return new SignInResult( SignInState.TwoFactor, user.Id.SafeString() );
        return new SignInResult( SignInState.Failed, null, Localizer["InvalidAccountOrPassword"] );
    }

    /// <inheritdoc />
    public async Task SignOutAsync() {
        await IdentitySignInManager.SignOutAsync();
    }
}