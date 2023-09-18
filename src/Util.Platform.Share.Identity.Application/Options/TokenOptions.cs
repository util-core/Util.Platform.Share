using Microsoft.AspNetCore.Identity;

namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 令牌配置
/// </summary>
public class TokenOptions {
    /// <summary>
    /// 电子邮件确认、密码重置和更改电子邮件使用的默认令牌提供程序名称,默认值: Default
    /// </summary>
    public static readonly string DefaultProvider = "Default";

    /// <summary>
    /// 电子邮件默认令牌提供程序名称,默认值: Email
    /// </summary>
    public static readonly string DefaultEmailProvider = "Email";

    /// <summary>
    /// 手机默认令牌提供程序名称,默认值: Phone
    /// </summary>
    public static readonly string DefaultPhoneProvider = "Phone";

    /// <summary>
    /// AuthenticatorTokenProvider默认令牌提供程序名称,默认值: Authenticator
    /// </summary>
    public static readonly string DefaultAuthenticatorProvider = "Authenticator";

    /// <summary>
    /// 令牌提供程序字典
    /// </summary>
    public Dictionary<string, TokenProviderDescriptor> ProviderMap { get; set; } = new();

    /// <summary>
    /// 帐户确认电子邮件使用的令牌提供程序
    /// </summary>
    public string EmailConfirmationTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// 密码重置使用的令牌提供程序
    /// </summary>
    public string PasswordResetTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// 电子邮件更改确认使用的令牌提供程序
    /// </summary>
    public string ChangeEmailTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// 手机号更改确认使用的令牌提供程序
    /// </summary>
    public string ChangePhoneNumberTokenProvider { get; set; } = DefaultPhoneProvider;

    /// <summary>
    /// 双因素验证登录使用的令牌提供程序
    /// </summary>
    public string AuthenticatorTokenProvider { get; set; } = DefaultAuthenticatorProvider;

    /// <summary>
    /// 身份颁发者使用的令牌提供程序,默认值: Microsoft.AspNetCore.Identity.UI
    /// </summary>
    public string AuthenticatorIssuer { get; set; } = "Microsoft.AspNetCore.Identity.UI";
}