namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// ��¼�������
/// </summary>
public class LoginInputModel {
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
}