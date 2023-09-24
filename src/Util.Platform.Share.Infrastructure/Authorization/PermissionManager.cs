namespace Util.Platform.Share.Authorization;

/// <summary>
/// 权限管理器
/// </summary>
public class PermissionManager : IPermissionManager {
    /// <summary>
    /// 初始化权限管理器
    /// </summary>
    /// <param name="session">用户会话</param>
    /// <param name="cache">系统服务</param>
    public PermissionManager( ISession session, ICache cache ) {
        Session = session ?? throw new ArgumentNullException( nameof(session) );
        CacheService = cache ?? throw new ArgumentNullException( nameof( cache ) );
    }

    /// <summary>
    /// 用户会话
    /// </summary>
    protected readonly ISession Session;
    /// <summary>
    /// 缓存服务
    /// </summary>
    protected ICache CacheService { get; }

    /// <inheritdoc />
    public virtual bool HasPermission( string resourceUri ) {
        var userId = Session.UserId;
        if ( userId.IsEmpty() )
            return false;
        var isAdmin = IsAdminByCache( userId );
        if ( isAdmin )
            return true;
        if ( resourceUri.IsEmpty() )
            return false;
        return HasPermissionByCache( userId, resourceUri );
    }

    /// <summary>
    /// 通过缓存检测是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    protected virtual bool IsAdminByCache( string userId ) {
        var cacheKey = new IsAdminCacheKey( userId );
        return CacheService.Exists( cacheKey );
    }

    /// <summary>
    /// 通过缓存检测用户是否有权限访问资源
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="resourceUri">资源标识</param>
    public virtual bool HasPermissionByCache( string userId, string resourceUri ) {
        var cacheKey = new AclCacheKey( userId, resourceUri );
        return CacheService.Exists( cacheKey );
    }

    /// <inheritdoc />
    public virtual async Task<bool> HasPermissionAsync( string resourceUri,CancellationToken cancellationToken ) {
        var userId = Session.UserId;
        if ( userId.IsEmpty() )
            return false;
        var isAdmin = await IsAdminByCacheAsync( userId, cancellationToken );
        if ( isAdmin )
            return true;
        if ( resourceUri.IsEmpty() )
            return false;
        return await HasPermissionByCacheAsync( userId, resourceUri, cancellationToken );
    }

    /// <summary>
    /// 通过缓存检测是否管理员
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    protected virtual async Task<bool> IsAdminByCacheAsync( string userId, CancellationToken cancellationToken ) {
        var cacheKey = new IsAdminCacheKey( userId );
        return await CacheService.ExistsAsync( cacheKey, cancellationToken );
    }

    /// <summary>
    /// 通过缓存检测用户是否有权限访问资源
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="resourceUri">资源标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    protected virtual async Task<bool> HasPermissionByCacheAsync( string userId, string resourceUri, CancellationToken cancellationToken ) {
        var cacheKey = new AclCacheKey( userId, resourceUri );
        return await CacheService.ExistsAsync( cacheKey, cancellationToken );
    }
}