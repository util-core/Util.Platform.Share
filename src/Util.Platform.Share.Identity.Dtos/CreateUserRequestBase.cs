namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 创建用户参数
/// </summary>
public abstract class CreateUserRequestBase : RequestBase {
    /// <summary>
    /// 用户名
    /// </summary>
    [StringLength( 256 )]
    [Display( Name = "identity.user.userName" )]
    public string UserName { get; set; }
    /// <summary>
    /// 安全邮箱
    /// </summary>
    [StringLength( 256 )]
    [EmailAddress]
    [Display( Name = "identity.user.email" )]
    public string Email { get; set; }
    /// <summary>
    /// 安全手机号
    /// </summary>
    [StringLength( 64 )]
    [Phone]
    [Display( Name = "identity.user.phoneNumber" )]
    public string PhoneNumber { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    [StringLength( 256 )]
    [DataType( DataType.Password )]
    [Display( Name = "identity.user.password" )]
    public string Password { get; set; }
}