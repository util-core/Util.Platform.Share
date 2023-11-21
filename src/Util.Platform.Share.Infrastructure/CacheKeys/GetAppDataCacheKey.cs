namespace Util.Platform.Share.CacheKeys;

/// <summary>
/// 获取应用数据缓存键
/// </summary>
public class GetAppDataCacheKey : CacheKey {
    /// <summary>
    /// 初始化获取应用数据缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="applicationId">应用程序标识</param>
    public GetAppDataCacheKey( string userId, string applicationId ) {
        Prefix = string.Format( CacheKeyConst.UserPrefix, userId );
        Key = $"GetAppData-{applicationId}";
    }
}