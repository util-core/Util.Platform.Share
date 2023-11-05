using Util.Platform.Share.Identity.Enums;

namespace Util.Platform.Share.Identity.Events;

/// <summary>
/// 登录事件
/// </summary>
/// <param name="State">登录状态</param>
/// <param name="UserId">用户标识</param>
/// <param name="Account">用户账号</param>
/// <param name="Message">消息</param>
public record SignInEvent (
    SignInState State,
    string UserId,
    string Account,
    string Message
) : IntegrationEvent;