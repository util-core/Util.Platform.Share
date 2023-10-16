using Microsoft.AspNetCore.Html;
using Util.Platform.Share.Ui.Components.IconSelect.Builders;
using Util.Ui.Builders;
using Util.Ui.Configs;
using Util.Ui.Renders;

namespace Util.Platform.Share.Ui.Components.IconSelect.Renders;

/// <summary>
/// 图标选择渲染器
/// </summary>
public class IconSelectRender : RenderBase {
    /// <summary>
    /// 配置
    /// </summary>
    private readonly Config _config;

    /// <summary>
    /// 初始化图标选择渲染器
    /// </summary>
    /// <param name="config">配置</param>
    public IconSelectRender( Config config ) {
        _config = config;
    }

    /// <summary>
    /// 获取标签生成器
    /// </summary>
    protected override TagBuilder GetTagBuilder() {
        var builder = new IconSelectBuilder( _config );
        builder.Config();
        return builder;
    }

    /// <inheritdoc />
    public override IHtmlContent Clone() {
        return new IconSelectBuilder( _config.Copy() );
    }
}