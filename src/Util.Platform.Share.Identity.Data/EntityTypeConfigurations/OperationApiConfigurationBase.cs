namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 操作Api类型配置
/// </summary>
public abstract class OperationApiConfigurationBase<TOperationApi> : OperationApiConfigurationBase<TOperationApi, Guid, Guid, Guid, Guid?>
    where TOperationApi : OperationApiBase<TOperationApi> {
}

/// <summary>
/// 操作Api类型配置
/// </summary>
public abstract class OperationApiConfigurationBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TAuditUserId> : IEntityTypeConfiguration<TOperationApi> 
    where TOperationApi : OperationApiBase<TOperationApi, TOperationApiId, TOperationId, TResourceId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TOperationApi> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TOperationApi> builder ) {
        builder.ToTable( "OperationApi", "Permissions",t => t.HasComment( "操作Api" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TOperationApi> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "OperationApiId" )
            .HasComment( "操作Api标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TOperationApi> builder ) {
        builder.Property( t => t.OperationId )
            .HasColumnName( "OperationId" )
            .HasComment( "操作资源标识" );
        builder.Property( t => t.ApiId )
            .HasColumnName( "ApiId" )
            .HasComment( "Api资源标识" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TOperationApi> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TOperationApi> builder ) {
    }
}