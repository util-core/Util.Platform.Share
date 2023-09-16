namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations; 

/// <summary>
/// 资源类型配置
/// </summary>
public abstract class ResourceConfigurationBase<TResource, TResourceId, TParentId, TApplication, TApplicationId, TAuditUserId> : IEntityTypeConfiguration<TResource> 
    where TResource : ResourceBase<TResource, TResourceId, TParentId, TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TResource> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TResource> builder ) {
        builder.ToTable( "Resource", "Permissions", t => t.HasComment( "资源" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TResource> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "ResourceId" )
            .HasComment( "资源标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TResource> builder ) {
        builder.Property( t => t.ApplicationId )
            .HasColumnName( "ApplicationId" )
            .HasComment( "应用程序标识" );
        builder.Property( t => t.Uri )
            .HasColumnName( "Uri" )
            .HasComment( "资源标识符" );
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "资源名称" );
        builder.Property( t => t.Type )
            .HasColumnName( "Type" )
            .HasComment( "资源类型" );
        builder.Property( t => t.ParentId )
            .HasColumnName( "ParentId" )
            .HasComment( "父标识" );
        builder.Property( t => t.Path )
            .HasColumnName( "Path" )
            .HasComment( "路径" );
        builder.Property( t => t.Level )
            .HasColumnName( "Level" )
            .HasComment( "层级" );
        builder.Property( t => t.Remark )
            .HasColumnName( "Remark" )
            .HasComment( "备注" );
        builder.Property( t => t.PinYin )
            .HasColumnName( "PinYin" )
            .HasComment( "拼音简码" );
        builder.Property( t => t.Enabled )
            .HasColumnName( "Enabled" )
            .HasComment( "启用" );
        builder.Property( t => t.SortId )
            .HasColumnName( "SortId" )
            .HasComment( "排序号" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TResource> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TResource> builder ) {
    }
}