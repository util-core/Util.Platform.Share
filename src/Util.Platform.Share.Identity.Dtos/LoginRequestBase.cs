namespace Util.Platform.Share.Identity.Dtos; 

/// <summary>
/// 登录参数
/// </summary>
public abstract class LoginRequestBase : RequestBase {
    /// <summary>
    /// 账号，可以是用户名，手机号或电子邮件
    /// </summary>
    [Display( Name = "account" )]
    public string Account { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Display( Name = "userName" )]
    public string UserName { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    [Display( Name = "email" )]
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [Display( Name = "phoneNumber" )]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    [DataType( DataType.Password )]
    [Display( Name = "password" )]
    public string Password { get; set; }

    /// <summary>
    /// 记住密码
    /// </summary>
    [Display( Name = "remember" )]
    public bool? Remember { get; set; }

    /// <summary>
    /// 验证
    /// </summary>
    public override ValidationResultCollection Validate() {
        if( Account.IsEmpty() && UserName.IsEmpty() && Email.IsEmpty() && PhoneNumber.IsEmpty() )
            throw new Warning( "账号不能为空." );
        return base.Validate();
    }
}