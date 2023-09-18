namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// ע������
/// </summary>
public class LogoutViewModel {
    /// <summary>
    /// ע����ʶ
    /// </summary>
    public string LogoutId { get; set; }
    /// <summary>
    /// �ͻ�������
    /// </summary>
    public string ClientName { get; set; }
    /// <summary>
    /// ע�����Ƿ��Զ��ض���
    /// </summary>
    public bool AutomaticRedirectAfterSignOut { get; set; }
    /// <summary>
    /// ע�����ض����ַ
    /// </summary>
    public string PostLogoutRedirectUri { get; set; }
    /// <summary>
    /// ע��iframe��ַ
    /// </summary>
    public string SignOutIframeUrl { get; set; }
    /// <summary>
    /// �Ƿ񴥷��ⲿע��
    /// </summary>
    public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;
    /// <summary>
    /// �ⲿ��֤����
    /// </summary>
    public string ExternalAuthenticationScheme { get; set; }
}