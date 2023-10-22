namespace Util.Platform.Share.Identity.Events; 

/// <summary>
/// 退出登录事件
/// </summary>
/// <param name="UserId">用户标识</param>
public record SignOutEvent(
    string UserId
) : IntegrationEvent;