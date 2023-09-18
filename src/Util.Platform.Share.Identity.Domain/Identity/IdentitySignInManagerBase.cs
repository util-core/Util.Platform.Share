using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Identity;

/// <summary>
/// Identity登录服务
/// </summary>
public abstract class IdentitySignInManagerBase<TUser, TRole> : IdentitySignInManagerBase<TUser, Guid, TRole, Guid?>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser> {
    /// <summary>
    /// 初始化Identity登录服务
    /// </summary>
    /// <param name="userManager">Identity用户服务</param>
    /// <param name="contextAccessor">HttpContext访问器</param>
    /// <param name="claimsFactory">用户声明工厂</param>
    /// <param name="optionsAccessor">Identity配置</param>
    /// <param name="logger">日志</param>
    /// <param name="schemes">认证架构提供程序</param>
    /// <param name="confirmation">用户确认信息</param>
    protected IdentitySignInManagerBase( UserManager<TUser> userManager, IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<TUser> confirmation )
        : base( userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation ) {
    }
}

/// <summary>
/// Identity登录服务
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public abstract class IdentitySignInManagerBase<TUser, TUserId, TRole, TAuditUserId> : SignInManager<TUser> 
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId> 
    where TRole: class {
    /// <summary>
    /// 初始化Identity登录服务
    /// </summary>
    /// <param name="userManager">Identity用户服务</param>
    /// <param name="contextAccessor">HttpContext访问器</param>
    /// <param name="claimsFactory">用户声明工厂</param>
    /// <param name="optionsAccessor">Identity配置</param>
    /// <param name="logger">日志</param>
    /// <param name="schemes">认证架构提供程序</param>
    /// <param name="confirmation">用户确认信息</param>
    protected IdentitySignInManagerBase( UserManager<TUser> userManager, IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<TUser> confirmation )
        : base( userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation ) {
    }

    /// <summary>
    /// 是否允许登录
    /// </summary>
    /// <param name="user">用户</param>
    public override async Task<bool> CanSignInAsync( TUser user ) {
        if ( user.Enabled == false )
            return false;
        return await base.CanSignInAsync( user );
    }
}