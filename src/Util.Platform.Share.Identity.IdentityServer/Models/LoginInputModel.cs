using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// 登录输入参数
/// </summary>
public class LoginInputModel<TLoginRequest> where TLoginRequest: LoginRequestBase,new() {
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

    /// <summary>
    /// 转换为登录参数
    /// </summary>
    public TLoginRequest ToLoginRequest() {
        return new TLoginRequest {
            UserName = UserName,
            Password = Password,
            Remember = Remember
        };
    }
}