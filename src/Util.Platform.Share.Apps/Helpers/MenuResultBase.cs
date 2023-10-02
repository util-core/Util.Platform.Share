using Util.Platform.Share.Apps.Dtos;

namespace Util.Platform.Share.Apps.Helpers;

/// <summary>
/// 菜单结果
/// </summary>
public class MenuResultBase<TModuleDto> : MenuResultBase<TModuleDto, Guid?, Guid?>
    where TModuleDto : ModuleDtoBase<TModuleDto> {
    /// <summary>
    /// 初始化菜单结果
    /// </summary>
    /// <param name="data">模块参数列表</param>
    /// <param name="async">是否异步加载</param>
    /// <param name="allExpand">所有节点是否全部展开</param>
    protected MenuResultBase( IEnumerable<TModuleDto> data, bool async = false, bool allExpand = false ) : base( data, async, allExpand ) {
    }
}

/// <summary>
/// 菜单结果
/// </summary>
public abstract class MenuResultBase<TModuleDto, TApplicationId, TAuditUserId> : TreeResultBase<TModuleDto, MenuInfo, List<MenuInfo>> 
    where TModuleDto : ModuleDtoBase<TModuleDto, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化菜单结果
    /// </summary>
    /// <param name="data">模块参数列表</param>
    /// <param name="async">是否异步加载</param>
    /// <param name="allExpand">所有节点是否全部展开</param>
    protected MenuResultBase( IEnumerable<TModuleDto> data, bool async = false, bool allExpand = false ) : base( data, async, allExpand ) {
    }

    /// <summary>
    /// 转换为菜单
    /// </summary>
    protected override MenuInfo ToDestinationNode( TModuleDto dto ) {
        var result = new MenuInfo {
            Key = dto.Id,
            Text = dto.GetText(),
            I18n = dto.I18n,
            Icon = new MenuIconInfo {
                Type = MenuIconType.Icon,
                Value = dto.Icon
            },
            Disabled = !dto.Enabled.SafeValue(),
            Children = dto.Children.Select( ToDestinationNode ).ToList(),
            Reuse = true
        };
        SetLink( result, dto );
        return result;
    }

    /// <summary>
    /// 设置链接
    /// </summary>
    protected virtual MenuInfo SetLink( MenuInfo menu, TModuleDto dto ) {
        if ( dto.Uri.IsEmpty() )
            return menu;
        if ( dto.Uri.StartsWith( "http" ) ) {
            menu.ExternalLink = dto.Uri;
            return menu;
        }
        menu.Link = dto.Uri;
        return menu;
    }

    /// <summary>
    /// 转换为结果
    /// </summary>
    protected override List<MenuInfo> ToResult( List<MenuInfo> nodes ) {
        return nodes;
    }
}