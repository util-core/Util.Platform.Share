namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// �û�����
/// </summary>
public class UserOptions {
    /// <summary>
    /// �û�������ʹ�õ��ַ�. Ĭ��ֵΪ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
    /// </summary>
    public string AllowedUserNameCharacters { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    /// <summary>
    /// �û��ĵ����ʼ��Ƿ����Ψһ��Ĭ�ϲ�����
    /// </summary>
    public bool RequireUniqueEmail { get; set; }
}