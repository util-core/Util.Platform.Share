namespace Util.Platform.Share.Apps.Dtos;

/// <summary>
/// 应用数据
/// </summary>
public class AppData {
    /// <summary>
    /// 初始化应用数据
    /// </summary>
    public AppData() {
        App = new AppInfo();
        User = new UserInfo();
        Menu = new List<MenuInfo>();
        Acl = new List<string>();
    }

    /// <summary>
    /// 应用程序信息
    /// </summary>
    public AppInfo App { get; set; }
    /// <summary>
    /// 用户信息
    /// </summary>
    public UserInfo User { get; set; }
    /// <summary>
    /// 菜单信息
    /// </summary>
    public List<MenuInfo> Menu { get; set; }
    /// <summary>
    /// 访问控制列表,包含操作资源标识和列集资源标识
    /// </summary>
    public List<string> Acl { get; set; }
    /// <summary>
    /// 是否管理员
    /// </summary>
    public bool IsAdmin { get; set; }
}