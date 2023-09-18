using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// ��¼�������
/// </summary>
public class LoginInputModel<TLoginRequest> where TLoginRequest: LoginRequestBase,new() {
    /// <summary>
    /// �û���
    /// </summary>
    [Required]
    public string UserName { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    [Required]
    public string Password { get; set; }
    /// <summary>
    /// �Ƿ��ס����
    /// </summary>
    public bool Remember { get; set; }
    /// <summary>
    /// ���ص�ַ
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    /// ת��Ϊ��¼����
    /// </summary>
    public TLoginRequest ToLoginRequest() {
        return new TLoginRequest {
            UserName = UserName,
            Password = Password,
            Remember = Remember
        };
    }
}