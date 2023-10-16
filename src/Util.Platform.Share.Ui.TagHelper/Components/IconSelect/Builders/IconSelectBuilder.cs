using Util.Ui.Angular.Builders;
using Util.Ui.Configs;

namespace Util.Platform.Share.Ui.Components.IconSelect.Builders;

/// <summary>
/// 图标选择标签生成器
/// </summary>
public class IconSelectBuilder : AngularTagBuilder {
    /// <summary>
    /// 配置
    /// </summary>
    private readonly Config _config;
    /// <summary>
    /// 初始化图标选择标签生成器
    /// </summary>
    /// <param name="config">配置</param>
    public IconSelectBuilder( Config config ) : base( config, "platform-icon-select" ) {
        _config = config;
    }

    /// <summary>
    /// 配置显示几列
    /// </summary>
    public IconSelectBuilder Columns() {
        AttributeIfNotEmpty( "columns", _config.GetValue( "columns" ) );
        return this;
    }

    /// <summary>
    /// 配置显示几行
    /// </summary>
    public IconSelectBuilder Rows() {
        AttributeIfNotEmpty( "rows", _config.GetValue( "rows" ) );
        return this;
    }

    /// <summary>
    /// 配置分页大小
    /// </summary>
    public IconSelectBuilder PageSize() {
        AttributeIfNotEmpty( "pageSize", _config.GetValue( "page-size" ) );
        return this;
    }

    /// <summary>
    /// 配置分页选项
    /// </summary>
    public IconSelectBuilder PageSizeOptions() {
        AttributeIfNotEmpty( "[pageSizeOptions]", _config.GetValue( "page-size-options" ) );
        return this;
    }

    /// <summary>
    /// 配置选中的图标
    /// </summary>
    public IconSelectBuilder SelectedIcon() {
        AttributeIfNotEmpty( "[selectedIcon]", _config.GetValue( "selected-icon" ) );
        return this;
    }

    /// <summary>
    /// 配置图标选中事件
    /// </summary>
    public IconSelectBuilder OnSelectIcon() {
        AttributeIfNotEmpty( "(onSelectIcon)", _config.GetValue( "on-select-icon" ) );
        return this;
    }

    /// <summary>
    /// 配置
    /// </summary>
    public override void Config() {
        base.Config();
        Columns().Rows().PageSize().PageSizeOptions().SelectedIcon().OnSelectIcon();
    }
}