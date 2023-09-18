namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// ��¼����
/// </summary>
public class SignInOptions {
    /// <summary>
    /// ����ȷ�ϵ����ʼ����ܵ�¼,Ĭ��ֵ: false
    /// </summary>
    public bool RequireConfirmedEmail { get; set; }

    /// <summary>
    /// ����ȷ���ֻ��Ų��ܵ�¼,Ĭ��ֵ: false
    /// </summary>
    public bool RequireConfirmedPhoneNumber { get; set; }

    /// <summary>
    /// ����ȷ���û��ʻ����ܵ�¼,�ʻ�ȷ���� IUserConfirmation �ӿ��ṩ,Ĭ��ֵ: false
    /// </summary>
    /// <value>True if a user must have a confirmed account before they can sign in, otherwise false.</value>
    public bool RequireConfirmedAccount { get; set; }
}