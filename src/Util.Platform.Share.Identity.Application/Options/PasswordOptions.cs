namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 密码配置
/// </summary>
public class PasswordOptions {
    /// <summary>
    /// 密码最小长度，默认值: 1
    /// </summary>
    public int RequiredLength { get; set; } = 1;

    /// <summary>
    /// 密码必须包含的最小唯一字符数，默认值: 1
    /// </summary>
    public int RequiredUniqueChars { get; set; } = 1;

    /// <summary>
    /// 密码是否必须包含非字母和数字的特殊字符，比如 #，默认不启用
    /// </summary>
    public bool RequireNonAlphanumeric { get; set; }

    /// <summary>
    /// 密码是否必须包含小写字母，默认不启用
    /// </summary>
    public bool RequireLowercase { get; set; }

    /// <summary>
    /// 密码是否必须包含大写字母，默认不启用
    /// </summary>
    public bool RequireUppercase { get; set; }

    /// <summary>
    /// 密码是否必须包含数字，默认不启用
    /// </summary>
    public bool RequireDigit { get; set; }
}