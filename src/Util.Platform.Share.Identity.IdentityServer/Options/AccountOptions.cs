namespace Util.Platform.Share.Identity.IdentityServer.Options; 

/// <summary>
/// 用户认证配置
/// </summary>
public class AccountOptions {
    /// <summary>
    /// 注销后是否自动重定向
    /// </summary>
    public static bool AutomaticRedirectAfterSignOut = true;
    /// <summary>
    /// 凭据无效消息
    /// </summary>
    public static string InvalidCredentialsErrorMessage = "Invalid username or password";
}