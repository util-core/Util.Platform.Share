namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// »®œﬁ≈‰÷√
/// </summary>
public class PermissionOptions {
    /// <summary>
    /// …˘√˜¿‡–Õ≈‰÷√
    /// </summary>
    public ClaimsIdentityOptions ClaimsIdentity { get; set; } = new();

    /// <summary>
    /// ”√ªß≈‰÷√
    /// </summary>
    public UserOptions User { get; set; } = new();

    /// <summary>
    /// √‹¬Î≈‰÷√
    /// </summary>
    public PasswordOptions Password { get; set; } = new();

    /// <summary>
    /// µ«¬ºÀ¯∂®≈‰÷√
    /// </summary>
    public LockoutOptions Lockout { get; set; } = new();

    /// <summary>
    /// µ«¬º≈‰÷√
    /// </summary>
    public SignInOptions SignIn { get; set; } = new();

    /// <summary>
    /// ¡Ó≈∆≈‰÷√
    /// </summary>
    public TokenOptions Tokens { get; set; } = new();

    /// <summary>
    /// ¥Ê¥¢≈‰÷√
    /// </summary>
    public StoreOptions Stores { get; set; } = new();
}