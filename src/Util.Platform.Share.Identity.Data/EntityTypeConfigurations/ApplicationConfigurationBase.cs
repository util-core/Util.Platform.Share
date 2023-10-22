namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 应用程序类型配置
/// </summary>
public abstract class ApplicationConfigurationBase<TApplication> : ApplicationConfigurationBase<TApplication, Guid, Guid?>
    where TApplication : ApplicationBase<TApplication> {
}

/// <summary>
/// 应用程序类型配置
/// </summary>
public abstract class ApplicationConfigurationBase<TApplication, TApplicationId, TAuditUserId> : IEntityTypeConfiguration<TApplication> 
    where TApplication : ApplicationBase<TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TApplication> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TApplication> builder ) {
        builder.ToTable( "Application", "Permissions", t => t.HasComment( "应用程序" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TApplication> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "ApplicationId" )
            .HasComment( "应用程序标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TApplication> builder ) {
        builder.Property( t => t.Code )
            .HasColumnName( "Code" )
            .HasComment( "应用程序编码" );
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "应用程序名称" );
        builder.Property( t => t.Enabled )
            .HasColumnName( "Enabled" )
            .HasComment( "启用" );
        builder.Property( t => t.AllowedCorsOrigins )
            .HasColumnName( "AllowedCorsOrigins" )
            .HasComment( "允许跨域来源" );
        builder.Property( t => t.Remark )
            .HasColumnName( "Remark" )
            .HasComment( "备注" );
        builder.Property( t => t.CreationTime )
            .HasColumnName( "CreationTime" )
            .HasComment( "创建时间" );
        builder.Property( t => t.CreatorId )
            .HasColumnName( "CreatorId" )
            .HasComment( "创建人标识" );
        builder.Property( t => t.LastModificationTime )
            .HasColumnName( "LastModificationTime" )
            .HasComment( "最后修改时间" );
        builder.Property( t => t.LastModifierId )
            .HasColumnName( "LastModifierId" )
            .HasComment( "最后修改人标识" );
    }

    /// <summary>
    /// 配置索引
    /// </summary>
    protected virtual void ConfigIndex( EntityTypeBuilder<TApplication> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TApplication> builder ) {
    }
}