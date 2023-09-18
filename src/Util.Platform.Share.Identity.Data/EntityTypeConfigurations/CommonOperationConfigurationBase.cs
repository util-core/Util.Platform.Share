namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 常用操作资源类型配置
/// </summary>
public abstract class CommonOperationConfigurationBase<TCommonOperation> : CommonOperationConfigurationBase<TCommonOperation, Guid, Guid?>
    where TCommonOperation : CommonOperationBase<TCommonOperation> {
}

/// <summary>
/// 常用操作资源类型配置
/// </summary>
public abstract class CommonOperationConfigurationBase<TCommonOperation, TCommonOperationId, TAuditUserId> : IEntityTypeConfiguration<TCommonOperation> 
    where TCommonOperation : CommonOperationBase<TCommonOperation, TCommonOperationId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TCommonOperation> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TCommonOperation> builder ) {
        builder.ToTable( "CommonOperation", "Permissions", t => t.HasComment( "常用操作资源" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TCommonOperation> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "OperationId" )
            .HasComment( "操作标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TCommonOperation> builder ) {
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "操作名称" );
        builder.Property( t => t.Enabled )
            .HasColumnName( "Enabled" )
            .HasComment( "启用" );
        builder.Property( t => t.SortId )
            .HasColumnName( "SortId" )
            .HasComment( "排序号" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TCommonOperation> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TCommonOperation> builder ) {
    }
}