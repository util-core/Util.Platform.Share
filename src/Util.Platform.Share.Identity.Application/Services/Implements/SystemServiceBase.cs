using Util.Platform.Share.Identity.Applications.CacheKeys;
using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements; 

/// <summary>
/// 系统服务
/// </summary>
public abstract class SystemServiceBase<TUnitOfWork, TPermission, TResource, TApplication, TUser, TRole, TAppResources, TModuleDto, TApplicationDto, TApplicationQuery, TLoginRequest> 
    : ServiceBase, ISystemServiceBase<TLoginRequest>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>, new()
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TAppResources : AppResourcesBase<TModuleDto>, new()
    where TModuleDto : ModuleDtoBase<TModuleDto> 
    where TApplicationDto : ApplicationDtoBase,new()
    where TApplicationQuery : ApplicationQueryBase
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
    /// <param name="applicationService">应用程序服务</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="signInManager">登录服务</param>
    /// <param name="userManager">用户服务</param>
    protected SystemServiceBase( IServiceProvider serviceProvider, IEventBus eventBus, ICache cache, TUnitOfWork unitOfWork, IPermissionServiceBase<TAppResources> permissionService,
        IApplicationServiceBase<TApplicationDto, TApplicationQuery> applicationService, IUserRepositoryBase<TUser> userRepository, IPermissionRepositoryBase<TPermission> permissionRepository,
        ISignInManagerBase<TUser, TRole> signInManager, IUserManagerBase<TUser, TRole> userManager ) : base( serviceProvider ) {
        EventBus = eventBus ?? throw new ArgumentNullException( nameof( eventBus ) );
        CacheService = cache ?? throw new ArgumentNullException( nameof( cache ) );
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        PermissionService = permissionService ?? throw new ArgumentNullException( nameof( permissionService ) );
        ApplicationService = applicationService ?? throw new ArgumentNullException( nameof( applicationService ) );
        UserRepository = userRepository ?? throw new ArgumentNullException( nameof( userRepository ) );
        PermissionRepository = permissionRepository ?? throw new ArgumentNullException( nameof( permissionRepository ) );
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
    /// 用户仓储
    /// </summary>
    public IUserRepositoryBase<TUser> UserRepository { get; set; }
    /// <summary>
    /// 权限仓储
    /// </summary>
    protected IPermissionRepositoryBase<TPermission> PermissionRepository { get; }
    /// <summary>
    /// 权限服务
    /// </summary>
    protected IPermissionServiceBase<TAppResources> PermissionService { get; }
    /// <summary>
    /// 应用程序服务
    /// </summary>
    protected IApplicationServiceBase<TApplicationDto, TApplicationQuery> ApplicationService { get; }
    /// <summary>
    /// 登录服务
    /// </summary>
    protected ISignInManagerBase<TUser,TRole> SignInManager { get; set; }
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

    #region IsAdmin

    /// <inheritdoc />
    public virtual bool IsAdmin( Guid userId ) {
        if ( userId.IsEmpty() )
            return false;
        return PermissionService.IsAdmin( userId );
    }

    #endregion

    #region IsAdminAsync

    /// <inheritdoc />
    public virtual async Task<bool> IsAdminAsync( Guid userId, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() )
            return false;
        return await PermissionService.IsAdminAsync( userId, cancellationToken );
    }

    #endregion

    #region IsAdminByCacheAsync

    /// <inheritdoc />
    public virtual async Task<bool> IsAdminByCacheAsync( Guid userId, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() )
            return false;
        var cacheKey = new IsAdminCacheKey( userId );
        return await CacheService.GetAsync( cacheKey, async () => await IsAdminAsync( userId, cancellationToken ),cancellationToken: cancellationToken );
    }

    #endregion

    #region SetAclCacheAsync

    /// <inheritdoc />
    public virtual async Task SetAclCacheAsync( Guid userId, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() )
            return;
        var isAdmin = await IsAdminAsync( userId, cancellationToken );
        if ( isAdmin )
            return;
        var acl = await PermissionService.GetAclAsync( userId, cancellationToken );
        if ( acl == null || acl.Count == 0 )
            return;
        var items = acl.Select( resourceUri => new KeyValuePair<CacheKey,int>( new AclCacheKey( userId, resourceUri ),1 ) ).ToDictionary(t => t.Key,t => t.Value);
        await CacheService.SetAsync( items, cancellationToken:cancellationToken );
    }

    #endregion

    #region HasPermissionByCacheAsync

    /// <inheritdoc />
    public virtual async Task<bool> HasPermissionByCacheAsync( Guid userId, string resourceUri, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() || resourceUri.IsEmpty() )
            return false;
        var cacheKey = new AclCacheKey( userId, resourceUri );
        return await CacheService.ExistsAsync( cacheKey, cancellationToken );
    }

    #endregion
}