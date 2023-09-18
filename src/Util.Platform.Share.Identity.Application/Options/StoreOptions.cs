namespace Util.Platform.Share.Identity.Applications.Options; 

/// <summary>
/// 存储配置
/// </summary>
public class StoreOptions {
    /// <summary>
    /// 键最大长度
    /// </summary>
    public int MaxLengthForKeys { get; set; }

    /// <summary>
    /// 加密存储用户数据，默认不启用
    /// </summary>
    public bool ProtectPersonalData { get; set; }
}