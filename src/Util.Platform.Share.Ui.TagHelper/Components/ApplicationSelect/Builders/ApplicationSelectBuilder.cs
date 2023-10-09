using Util.Ui.Angular.Builders;
using Util.Ui.Configs;

namespace Util.Platform.Share.Ui.Components.ApplicationSelect.Builders;

/// <summary>
/// 应用程序选择标签生成器
/// </summary>
public class ApplicationSelectBuilder : AngularTagBuilder {
    /// <summary>
    /// 配置
    /// </summary>
    private readonly Config _config;
    /// <summary>
    /// 初始化应用程序选择标签生成器
    /// </summary>
    /// <param name="config">配置</param>
    public ApplicationSelectBuilder( Config config ) : base( config, "platform-application-select" ) {
        _config = config;
    }

    /// <summary>
    /// 配置仅加载Api应用程序
    /// </summary>
    public ApplicationSelectBuilder LoadApiOnly() {
        AttributeIfNotEmpty( "[loadApiOnly]", _config.GetBoolValue( "load-api-only" ) );
        return this;
    }

    /// <summary>
    /// 配置仅加载非Api应用程序
    /// </summary>
    public ApplicationSelectBuilder LoadNonApiOnly() {
        AttributeIfNotEmpty( "[loadNonApiOnly]", _config.GetBoolValue( "load-non-api-only" ) );
        return this;
    }

    /// <summary>
    /// 配置单击事件
    /// </summary>
    public ApplicationSelectBuilder OnClick() {
        AttributeIfNotEmpty( "(onClick)", _config.GetValue( "on-click" ) );
        return this;
    }

    /// <summary>
    /// 配置
    /// </summary>
    public override void Config() {
        base.Config();
        LoadApiOnly().LoadNonApiOnly().OnClick();
    }
}