namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// ��¼����
/// </summary>
public class LoginViewModel : LoginInputModel  {
    /// <summary>
    /// �ⲿ��֤�ṩ���б�
    /// </summary>
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
    /// <summary>
    /// ����ʾ���ⲿ��֤�ṩ���б�
    /// </summary>
    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where( t => t.DisplayName.IsEmpty() == false );
    /// <summary>
    /// �ⲿ��֤����
    /// </summary>
    public string ExternalLoginScheme => ExternalProviders?.FirstOrDefault()?.AuthenticationScheme;
    /// <summary>
    /// ��Ϣ
    /// </summary>
    public string Message { get; set; }
}