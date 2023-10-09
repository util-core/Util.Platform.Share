using Microsoft.AspNetCore.Html;
using Util.Platform.Share.Ui.Components.ApplicationSelect.Builders;
using Util.Ui.Builders;
using Util.Ui.Configs;
using Util.Ui.Renders;

namespace Util.Platform.Share.Ui.Components.ApplicationSelect.Renders;

/// <summary>
/// 应用程序选择渲染器
/// </summary>
public class ApplicationSelectRender : RenderBase {
    /// <summary>
    /// 配置
    /// </summary>
    private readonly Config _config;

    /// <summary>
    /// 初始化应用程序选择渲染器
    /// </summary>
    /// <param name="config">配置</param>
    public ApplicationSelectRender( Config config ) {
        _config = config;
    }

    /// <summary>
    /// 获取标签生成器
    /// </summary>
    protected override TagBuilder GetTagBuilder() {
        var builder = new ApplicationSelectBuilder( _config );
        builder.Config();
        return builder;
    }

    /// <inheritdoc />
    public override IHtmlContent Clone() {
        return new ApplicationSelectBuilder( _config.Copy() );
    }
}