using Microsoft.AspNetCore.Razor.TagHelpers;
using Util.Platform.Share.Ui.Components.ApplicationSelect.Renders;
using Util.Ui.Angular.TagHelpers;
using Util.Ui.Configs;
using Util.Ui.Renders;

namespace Util.Platform.Share.Ui.Components.ApplicationSelect;

/// <summary>
/// 应用程序选择组件
/// </summary>
[HtmlTargetElement( "biz-application-select" )]
public class ApplicationSelectTagHelper : AngularTagHelperBase {
    /// <summary>
    /// [loadApiOnly],是否仅加载Api应用程序,默认值: false
    /// </summary>
    public bool LoadApiOnly { get; set; }
    /// <summary>
    /// [loadNonApiOnly],是否仅加载非Api应用程序,默认值: false
    /// </summary>
    public bool LoadNonApiOnly { get; set; }
    /// <summary>
    /// (onClick),单击事件
    /// </summary>
    public string OnClick { get; set; }

    /// <inheritdoc />
    protected override IRender GetRender( TagHelperContext context, TagHelperOutput output, TagHelperContent content ) {
        var config = new Config( context, output , content );
        return new ApplicationSelectRender( config );
    }
}