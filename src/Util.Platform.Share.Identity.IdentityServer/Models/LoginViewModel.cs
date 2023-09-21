namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// 登录参数
/// </summary>
public class LoginViewModel : LoginInputModel  {
    /// <summary>
    /// 外部认证提供器列表
    /// </summary>
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
    /// <summary>
    /// 可显示的外部认证提供器列表
    /// </summary>
    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where( t => t.DisplayName.IsEmpty() == false );
    /// <summary>
    /// 外部认证方案
    /// </summary>
    public string ExternalLoginScheme => ExternalProviders?.FirstOrDefault()?.AuthenticationScheme;
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }
}