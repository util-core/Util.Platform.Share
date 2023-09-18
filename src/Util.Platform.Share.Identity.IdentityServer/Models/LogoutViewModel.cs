namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// 注销参数
/// </summary>
public class LogoutViewModel {
    /// <summary>
    /// 注销标识
    /// </summary>
    public string LogoutId { get; set; }
    /// <summary>
    /// 客户端名称
    /// </summary>
    public string ClientName { get; set; }
    /// <summary>
    /// 注销后是否自动重定向
    /// </summary>
    public bool AutomaticRedirectAfterSignOut { get; set; }
    /// <summary>
    /// 注销后重定向地址
    /// </summary>
    public string PostLogoutRedirectUri { get; set; }
    /// <summary>
    /// 注销iframe地址
    /// </summary>
    public string SignOutIframeUrl { get; set; }
    /// <summary>
    /// 是否触发外部注销
    /// </summary>
    public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;
    /// <summary>
    /// 外部认证方案
    /// </summary>
    public string ExternalAuthenticationScheme { get; set; }
}