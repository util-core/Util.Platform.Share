using System.Security.Claims;

namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 声明类型配置
/// </summary>
public class ClaimsIdentityOptions {
    /// <summary>
    /// 角色声明类型,默认值: ClaimTypes.Role
    /// </summary>
    public string RoleClaimType { get; set; } = ClaimTypes.Role;

    /// <summary>
    /// 用户名声明类型,默认值: ClaimTypes.Name
    /// </summary>
    public string UserNameClaimType { get; set; } = ClaimTypes.Name;

    /// <summary>
    /// 用户标识声明类型,默认值: ClaimTypes.NameIdentifier
    /// </summary>
    public string UserIdClaimType { get; set; } = ClaimTypes.NameIdentifier;

    /// <summary>
    /// 电子邮件声明类型,默认值: ClaimTypes.Email
    /// </summary>
    public string EmailClaimType { get; set; } = ClaimTypes.Email;

    /// <summary>
    /// 安全标志声明类型,默认值: AspNet.Identity.SecurityStamp
    /// </summary>
    public string SecurityStampClaimType { get; set; } = "AspNet.Identity.SecurityStamp";
}