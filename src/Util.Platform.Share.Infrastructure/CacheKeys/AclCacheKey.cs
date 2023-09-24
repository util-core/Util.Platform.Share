namespace Util.Platform.Share.CacheKeys;

/// <summary>
/// 访问控制缓存键
/// </summary>
public class AclCacheKey : CacheKey {
    /// <summary>
    /// 初始化访问控制缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="resourceId">资源标识</param>
    public AclCacheKey( string userId, string resourceId ) {
        Prefix = string.Format( CacheKeyConst.UserPrefix, userId );
        Key = $"Acl-{resourceId}";
    }
}