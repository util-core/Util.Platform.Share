using System.Security.Claims;

namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// ������������
/// </summary>
public class ClaimsIdentityOptions {
    /// <summary>
    /// ��ɫ��������,Ĭ��ֵ: ClaimTypes.Role
    /// </summary>
    public string RoleClaimType { get; set; } = ClaimTypes.Role;

    /// <summary>
    /// �û�����������,Ĭ��ֵ: ClaimTypes.Name
    /// </summary>
    public string UserNameClaimType { get; set; } = ClaimTypes.Name;

    /// <summary>
    /// �û���ʶ��������,Ĭ��ֵ: ClaimTypes.NameIdentifier
    /// </summary>
    public string UserIdClaimType { get; set; } = ClaimTypes.NameIdentifier;

    /// <summary>
    /// �����ʼ���������,Ĭ��ֵ: ClaimTypes.Email
    /// </summary>
    public string EmailClaimType { get; set; } = ClaimTypes.Email;

    /// <summary>
    /// ��ȫ��־��������,Ĭ��ֵ: AspNet.Identity.SecurityStamp
    /// </summary>
    public string SecurityStampClaimType { get; set; } = "AspNet.Identity.SecurityStamp";
}