namespace Util.Platform.Share.Tools.Dtos;

/// <summary>
/// 头像参数
/// </summary>
public class AvatarDto : RequestBase {
    /// <summary>
    /// 文本
    ///</summary>
    public string Text { get; set; }
    /// <summary>
    /// 显示文本长度
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// 头像大小
    /// </summary>
    public int? Size { get; set; }
    /// <summary>
    /// 文字大小
    /// </summary>
    public double? FontSize { get; set; }
    /// <summary>
    /// 字体
    /// </summary>
    public string Font { get; set; }
}