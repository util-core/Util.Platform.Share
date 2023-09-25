namespace Util.Platform.Share.Apps.Dtos;

/// <summary>
/// 菜单图标信息
/// </summary>
public class MenuIconInfo {
    /// <summary>
    /// 初始化菜单图标信息
    /// </summary>
    public MenuIconInfo() {
        Type = MenuIconType.Icon;
        Theme = IconTheme.Outline;
    }

    /// <summary>
    /// 图标类型
    /// </summary>
    [JsonIgnore]
    public MenuIconType Type { get; set; }

    /// <summary>
    /// 图标类型
    /// </summary>
    [JsonPropertyName( "type" )]
    public string TypeText => Type.Description();

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 图标主题风格
    /// </summary>
    [JsonIgnore]
    public IconTheme Theme { get; set; }

    /// <summary>
    /// 图标主题风格
    /// </summary>
    [JsonPropertyName( "theme" )]
    public string ThemeText => Theme.Description();

    /// <summary>
    /// 是否旋转
    /// </summary>
    public string Spin { get; set; }

    /// <summary>
    /// 双色图标的主要颜色
    /// </summary>
    public string TwoToneColor { get; set; }

    /// <summary>
    /// 指定来自 IconFont 的图标类型
    /// </summary>
    public string Iconfont { get; set; }

    /// <summary>
    /// 图标旋转角度
    /// </summary>
    public double? Rotate { get; set; }
}