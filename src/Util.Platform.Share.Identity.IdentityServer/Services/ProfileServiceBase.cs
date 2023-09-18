using Util.Data.Queries;
using Util.Platform.Share.Identity.Domain.Identity;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Services;

/// <summary>
/// 用户配置服务
/// </summary>
public abstract class ProfileServiceBase<TUser, TRole, TIdentityUserManager, TApplicationService, TApplicationDto, TApplicationQuery> 
    : ProfileServiceBase<TUser, Guid, TRole, Guid, Guid?, TIdentityUserManager, TApplicationService, TApplicationDto, TApplicationQuery, Guid?>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TIdentityUserManager : IdentityUserManagerBase<TUser>
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化用户身份配置服务
    /// </summary>
    /// <param name="userManager">用户服务</param>
    /// <param name="claimsFactory">用户声明工厂</param>
    /// <param name="applicationService">应用程序服务</param>
    protected ProfileServiceBase( TIdentityUserManager userManager, IUserClaimsPrincipalFactory<TUser> claimsFactory, TApplicationService applicationService ) 
        : base( userManager, claimsFactory, applicationService ) {
    }
}

/// <summary>
/// 用户配置服务
/// </summary>
public abstract class ProfileServiceBase<TUser, TUserId, TRole, TRoleId, TRoleParentId, TIdentityUserManager, TApplicationService, TApplicationDto, TApplicationQuery, TAuditUserId> : ProfileService<TUser>
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId>
    where TRole : RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId>
    where TIdentityUserManager : IdentityUserManagerBase<TUser>
    where TApplicationService : IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TApplicationDto : ApplicationDtoBase, new()
    where TApplicationQuery : IPage {
    /// <summary>
    /// 初始化用户身份配置服务
    /// </summary>
    /// <param name="userManager">用户服务</param>
    /// <param name="claimsFactory">用户声明工厂</param>
    /// <param name="applicationService">应用程序服务</param>
    protected ProfileServiceBase( TIdentityUserManager userManager, IUserClaimsPrincipalFactory<TUser> claimsFactory, TApplicationService applicationService ) : base( userManager, claimsFactory ) {
        ApplicationService = applicationService ?? throw new ArgumentNullException( nameof( applicationService ) );
    }

    /// <summary>
    /// 应用程序服务
    /// </summary>
    protected TApplicationService ApplicationService { get; set; }

    /// <summary>
    /// 获取用户配置
    /// </summary>
    public override async Task GetProfileDataAsync( ProfileDataRequestContext context ) {
        if ( context == null )
            return;
        var user = await GetUser( context );
        if ( user == null )
            return;
        var principal = await ClaimsFactory.CreateAsync( user );
        var identity = principal.Identities.FirstOrDefault();
        await AddApplicationClaims( context, identity );
        context.IssuedClaims.AddRange( principal.Claims );
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    private async Task<TUser> GetUser( ProfileDataRequestContext context ) {
        var userId = context.Subject?.GetSubjectId();
        if ( userId.IsEmpty() )
            return null;
        return await UserManager.FindByIdAsync( userId );
    }

    /// <summary>
    /// 添加应用程序声明
    /// </summary>
    protected virtual async Task AddApplicationClaims( ProfileDataRequestContext context, ClaimsIdentity identity ) {
        if ( identity == null )
            return;
        var application = await GetApplication( context );
        if ( application == null )
            return;
        identity.AddClaim( new Claim( Util.Security.ClaimTypes.ApplicationId, application.Id ) );
        identity.AddClaim( new Claim( Util.Security.ClaimTypes.ApplicationCode, application.Code ) );
        identity.AddClaim( new Claim( Util.Security.ClaimTypes.ApplicationName, application.Name ) );
    }

    /// <summary>
    /// 获取应用程序
    /// </summary>
    private async Task<TApplicationDto> GetApplication( ProfileDataRequestContext context ) {
        var applicationCode = context.Client?.ClientId;
        if ( applicationCode.IsEmpty() )
            return null;
        return await ApplicationService.GetByCodeAsync( applicationCode );
    }
}