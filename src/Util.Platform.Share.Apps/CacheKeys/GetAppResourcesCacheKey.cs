using Util.Caching;

namespace Util.Platform.Share.Apps.CacheKeys;

/// <summary>
/// 获取应用数据缓存键
/// </summary>
public class GetAppDataCacheKey : GetAppDataCacheKeyBase<Guid, Guid> {
    /// <summary>
    /// 初始化获取应用数据缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="applicationId">应用程序标识</param>
    public GetAppDataCacheKey( Guid userId, Guid applicationId ) : base( userId, applicationId ) {
    }
}

/// <summary>
/// 获取应用数据缓存键
/// </summary>
public abstract class GetAppDataCacheKeyBase<TUserId, TApplicationId> : CacheKey {
    /// <summary>
    /// 初始化获取应用数据缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="applicationId">应用程序标识</param>
    protected GetAppDataCacheKeyBase( TUserId userId, TApplicationId applicationId ) {
        Prefix = string.Format( CacheKeyConst.UserPrefix, userId );
        Key = $"GetAppData-{applicationId}";
    }
}