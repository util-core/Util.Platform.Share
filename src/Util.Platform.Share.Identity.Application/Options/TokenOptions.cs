using Microsoft.AspNetCore.Identity;

namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// ��������
/// </summary>
public class TokenOptions {
    /// <summary>
    /// �����ʼ�ȷ�ϡ��������ú͸��ĵ����ʼ�ʹ�õ�Ĭ�������ṩ��������,Ĭ��ֵ: Default
    /// </summary>
    public static readonly string DefaultProvider = "Default";

    /// <summary>
    /// �����ʼ�Ĭ�������ṩ��������,Ĭ��ֵ: Email
    /// </summary>
    public static readonly string DefaultEmailProvider = "Email";

    /// <summary>
    /// �ֻ�Ĭ�������ṩ��������,Ĭ��ֵ: Phone
    /// </summary>
    public static readonly string DefaultPhoneProvider = "Phone";

    /// <summary>
    /// AuthenticatorTokenProviderĬ�������ṩ��������,Ĭ��ֵ: Authenticator
    /// </summary>
    public static readonly string DefaultAuthenticatorProvider = "Authenticator";

    /// <summary>
    /// �����ṩ�����ֵ�
    /// </summary>
    public Dictionary<string, TokenProviderDescriptor> ProviderMap { get; set; } = new();

    /// <summary>
    /// �ʻ�ȷ�ϵ����ʼ�ʹ�õ������ṩ����
    /// </summary>
    public string EmailConfirmationTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// ��������ʹ�õ������ṩ����
    /// </summary>
    public string PasswordResetTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// �����ʼ�����ȷ��ʹ�õ������ṩ����
    /// </summary>
    public string ChangeEmailTokenProvider { get; set; } = DefaultProvider;

    /// <summary>
    /// �ֻ��Ÿ���ȷ��ʹ�õ������ṩ����
    /// </summary>
    public string ChangePhoneNumberTokenProvider { get; set; } = DefaultPhoneProvider;

    /// <summary>
    /// ˫������֤��¼ʹ�õ������ṩ����
    /// </summary>
    public string AuthenticatorTokenProvider { get; set; } = DefaultAuthenticatorProvider;

    /// <summary>
    /// ��ݰ䷢��ʹ�õ������ṩ����,Ĭ��ֵ: Microsoft.AspNetCore.Identity.UI
    /// </summary>
    public string AuthenticatorIssuer { get; set; } = "Microsoft.AspNetCore.Identity.UI";
}