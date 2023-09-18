namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// ��������
/// </summary>
public class PasswordOptions {
    /// <summary>
    /// ������С���ȣ�Ĭ��ֵ: 1
    /// </summary>
    public int RequiredLength { get; set; } = 1;

    /// <summary>
    /// ��������������СΨһ�ַ�����Ĭ��ֵ: 1
    /// </summary>
    public int RequiredUniqueChars { get; set; } = 1;

    /// <summary>
    /// �����Ƿ�����������ĸ�����ֵ������ַ������� #��Ĭ�ϲ�����
    /// </summary>
    public bool RequireNonAlphanumeric { get; set; }

    /// <summary>
    /// �����Ƿ�������Сд��ĸ��Ĭ�ϲ�����
    /// </summary>
    public bool RequireLowercase { get; set; }

    /// <summary>
    /// �����Ƿ���������д��ĸ��Ĭ�ϲ�����
    /// </summary>
    public bool RequireUppercase { get; set; }

    /// <summary>
    /// �����Ƿ����������֣�Ĭ�ϲ�����
    /// </summary>
    public bool RequireDigit { get; set; }
}