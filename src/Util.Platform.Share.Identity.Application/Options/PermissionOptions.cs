namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// Ȩ������
/// </summary>
public class PermissionOptions {
    /// <summary>
    /// ������������
    /// </summary>
    public ClaimsIdentityOptions ClaimsIdentity { get; set; } = new();

    /// <summary>
    /// �û�����
    /// </summary>
    public UserOptions User { get; set; } = new();

    /// <summary>
    /// ��������
    /// </summary>
    public PasswordOptions Password { get; set; } = new();

    /// <summary>
    /// ��¼��������
    /// </summary>
    public LockoutOptions Lockout { get; set; } = new();

    /// <summary>
    /// ��¼����
    /// </summary>
    public SignInOptions SignIn { get; set; } = new();

    /// <summary>
    /// ��������
    /// </summary>
    public TokenOptions Tokens { get; set; } = new();

    /// <summary>
    /// �洢����
    /// </summary>
    public StoreOptions Stores { get; set; } = new();
}