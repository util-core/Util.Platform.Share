using Util.Helpers;

namespace Util.Platform.Share; 

/// <summary>
/// 用户会话扩展
/// </summary>
public static class SessionExtensions {
    /// <summary>
    /// 获取当前应用程序标识
    /// </summary>
    /// <param name="session">用户会话</param>
    public static Guid GetApplicationId( this Util.Sessions.ISession session ) {
        return Web.Identity.GetValue( ClaimTypes.ApplicationId ).ToGuid();
    }
}