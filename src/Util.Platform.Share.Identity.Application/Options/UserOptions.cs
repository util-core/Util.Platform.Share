namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 用户配置
/// </summary>
public class UserOptions {
    /// <summary>
    /// 用户名允许使用的字符. 默认值为 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
    /// </summary>
    public string AllowedUserNameCharacters { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    /// <summary>
    /// 用户的电子邮件是否必须唯一，默认不启用
    /// </summary>
    public bool RequireUniqueEmail { get; set; }
}