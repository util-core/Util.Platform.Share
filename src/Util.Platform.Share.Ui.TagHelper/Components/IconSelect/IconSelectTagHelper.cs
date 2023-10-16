using Microsoft.AspNetCore.Razor.TagHelpers;
using Util.Platform.Share.Ui.Components.IconSelect.Renders;
using Util.Ui.Angular.TagHelpers;
using Util.Ui.Configs;
using Util.Ui.Renders;

namespace Util.Platform.Share.Ui.Components.IconSelect;

/// <summary>
/// 图标选择组件
/// </summary>
[HtmlTargetElement( "biz-icon-select" )]
public class IconSelectTagHelper : AngularTagHelperBase {
    /// <summary>
    /// columns,显示几列,默认值: 10
    /// </summary>
    public int Columns { get; set; }
    /// <summary>
    /// rows,显示几行,默认值: 10
    /// </summary>
    public int Rows { get; set; }
    /// <summary>
    /// pageSize,分页大小,默认值: 100
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// [pageSizeOptions],分页选项,默认值: [100, 200, 500]
    /// </summary>
    public string PageSizeOptions { get; set; }
    /// <summary>
    /// [selectedIcon],选中的图标
    /// </summary>
    public string SelectedIcon { get; set; }
    /// <summary>
    /// (onSelectIcon),图标选中事件
    /// </summary>
    public string OnSelectIcon { get; set; }

    /// <inheritdoc />
    protected override IRender GetRender( TagHelperContext context, TagHelperOutput output, TagHelperContent content ) {
        var config = new Config( context, output , content );
        return new IconSelectRender( config );
    }
}