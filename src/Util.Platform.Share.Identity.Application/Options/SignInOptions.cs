namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 登录配置
/// </summary>
public class SignInOptions {
    /// <summary>
    /// 必须确认电子邮件才能登录,默认值: false
    /// </summary>
    public bool RequireConfirmedEmail { get; set; }

    /// <summary>
    /// 必须确认手机号才能登录,默认值: false
    /// </summary>
    public bool RequireConfirmedPhoneNumber { get; set; }

    /// <summary>
    /// 必须确认用户帐户才能登录,帐户确认由 IUserConfirmation 接口提供,默认值: false
    /// </summary>
    /// <value>True if a user must have a confirmed account before they can sign in, otherwise false.</value>
    public bool RequireConfirmedAccount { get; set; }
}