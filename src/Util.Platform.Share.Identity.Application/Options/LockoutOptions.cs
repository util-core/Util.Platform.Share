namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// ��¼��������
/// </summary>
public class LockoutOptions {
    /// <summary>
    /// �Ƿ������´������û���Ĭ������
    /// </summary>
    public bool AllowedForNewUsers { get; set; } = true;

    /// <summary>
    /// ���������ĵ�¼ʧ����������Ĭ��5��
    /// </summary>
    public int MaxFailedAccessAttempts { get; set; } = 5;

    /// <summary>
    /// ����ʱ������Ĭ��5����
    /// </summary>
    public TimeSpan DefaultLockoutTimeSpan { get; set; } = TimeSpan.FromMinutes( 5 );
}