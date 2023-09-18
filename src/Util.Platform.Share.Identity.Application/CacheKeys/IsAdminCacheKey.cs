namespace Util.Platform.Share.Identity.Applications.CacheKeys; 

/// <summary>
/// 是否管理员缓存键
/// </summary>
public class IsAdminCacheKey : CacheKey {
    /// <summary>
    /// 初始化是否管理员缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    public IsAdminCacheKey( Guid userId ) {
        Prefix = string.Format( CacheKeyConst.UserPrefix, userId );
        Key = "IsAdmin";
    }
}