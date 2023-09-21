namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// 登录输入参数
/// </summary>
public class LoginInputModel {
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string UserName { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; }
    /// <summary>
    /// 是否记住密码
    /// </summary>
    public bool Remember { get; set; }
    /// <summary>
    /// 返回地址
    /// </summary>
    public string ReturnUrl { get; set; }
}