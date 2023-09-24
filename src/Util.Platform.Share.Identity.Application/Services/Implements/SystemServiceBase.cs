using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 系统服务
/// </summary>
public abstract class SystemServiceBase<TUnitOfWork, TPermission, TResource, TApplication, TUser, TRole, TAppResources, TModuleDto,TLoginRequest>
    : ServiceBase, ISystemServiceBase<TLoginRequest>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>, new()
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TAppResources : AppResourcesBase<TModuleDto>, new()
    where TModuleDto : ModuleDtoBase<TModuleDto>
    where TLoginRequest : LoginRequestBase {

    #region 构造方法

    /// <summary>
    /// 初始化系统服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="eventBus">事件总线</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="permissionService">权限服务</param>
    /// <param name="signInManager">登录服务</param>
    /// <param name="userManager">用户服务</param>
    protected SystemServiceBase( IServiceProvider serviceProvider, IEventBus eventBus, ICache cache, TUnitOfWork unitOfWork,
        IPermissionServiceBase<TAppResources> permissionService, ISignInManagerBase<TUser, TRole> signInManager, IUserManagerBase<TUser, TRole> userManager ) : base( serviceProvider ) {
        EventBus = eventBus ?? throw new ArgumentNullException( nameof( eventBus ) );
        CacheService = cache ?? throw new ArgumentNullException( nameof( cache ) );
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        PermissionService = permissionService ?? throw new ArgumentNullException( nameof( permissionService ) );
        SignInManager = signInManager ?? throw new ArgumentNullException( nameof( signInManager ) );
        UserManager = userManager ?? throw new ArgumentNullException( nameof( userManager ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 事件总线
    /// </summary>
    protected IEventBus EventBus { get; }
    /// <summary>
    /// 缓存服务
    /// </summary>
    protected ICache CacheService { get; }
    /// <summary>
    /// 工作单元
    /// </summary>
    protected TUnitOfWork UnitOfWork { get; }
    /// <summary>
    /// 权限服务
    /// </summary>
    protected IPermissionServiceBase<TAppResources> PermissionService { get; }
    /// <summary>
    /// 登录服务
    /// </summary>
    protected ISignInManagerBase<TUser, TRole> SignInManager { get; set; }
    /// <summary>
    /// 用户服务
    /// </summary>
    protected IUserManagerBase<TUser, TRole> UserManager { get; set; }

    #endregion

    #region SignInAsync

    /// <inheritdoc />
    public virtual async Task<SignInResult> SignInAsync( TLoginRequest request, CancellationToken cancellationToken = default ) {
        var user = await GetUser( request );
        var result = await SignInManager.SignInAsync( user, request.Password, request.Remember.SafeValue() );
        await UnitOfWork.CommitAsync();
        await EventBus.PublishAsync( new SignInEvent( result.State, result.UserId, result.Message ), cancellationToken );
        return result;
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    protected virtual async Task<TUser> GetUser( TLoginRequest request ) {
        if ( request.UserName.IsEmpty() == false )
            return await UserManager.FindByNameAsync( request.UserName );
        if ( request.PhoneNumber.IsEmpty() == false )
            return await UserManager.FindByPhoneAsync( request.PhoneNumber );
        if ( request.Email.IsEmpty() == false )
            return await UserManager.FindByEmailAsync( request.Email );
        if ( request.Account.IsEmpty() )
            return null;
        var user = await UserManager.FindByNameAsync( request.Account );
        if ( user == null )
            user = await UserManager.FindByPhoneAsync( request.Account );
        if ( user == null )
            user = await UserManager.FindByEmailAsync( request.Account );
        return user;
    }

    #endregion

    #region SignOutAsync

    /// <inheritdoc />
    public virtual async Task SignOutAsync( CancellationToken cancellationToken = default ) {
        await SignInManager.SignOutAsync();
        await EventBus.PublishAsync( new SignOutEvent(), cancellationToken );
    }

    #endregion

    #region SetAclCacheAsync

    /// <inheritdoc />
    public virtual async Task SetAclCacheAsync( Guid userId, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() )
            return;
        var isAdmin = await PermissionService.IsAdminAsync( userId, cancellationToken );
        if ( isAdmin ) {
            await CacheService.SetAsync( new IsAdminCacheKey( userId.ToString() ), 1, cancellationToken: cancellationToken );
            return;
        }
        var acl = await PermissionService.GetAclAsync( userId, cancellationToken );
        if ( acl == null || acl.Count == 0 )
            return;
        var items = acl.Select( resourceUri => new KeyValuePair<CacheKey, int>( new AclCacheKey( userId.ToString(), resourceUri ), 1 ) ).ToDictionary( t => t.Key, t => t.Value );
        await CacheService.SetAsync( items, cancellationToken: cancellationToken );
    }

    #endregion
}