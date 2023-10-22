namespace Util.Platform.Share.CacheKeys;

/// <summary>
/// 是否加载访问控制列表缓存键
/// </summary>
public class IsLoadAclCacheKey : CacheKey {
    /// <summary>
    /// 初始化是否加载访问控制列表缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="applicationId">应用程序标识</param>
    public IsLoadAclCacheKey( string userId, string applicationId ) {
        Prefix = string.Format( CacheKeyConst.UserPrefix, userId );
        Key = $"Is-Load-Acl-{applicationId}";
    }
}