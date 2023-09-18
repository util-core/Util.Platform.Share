namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 声明类型配置
/// </summary>
public abstract class ClaimConfigurationBase<TClaim> : ClaimConfigurationBase<TClaim, Guid, Guid?>
    where TClaim : ClaimBase<TClaim> {
}

/// <summary>
/// 声明类型配置
/// </summary>
public abstract class ClaimConfigurationBase<TClaim, TClaimId, TAuditUserId> : IEntityTypeConfiguration<TClaim> 
    where TClaim : ClaimBase<TClaim, TClaimId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TClaim> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TClaim> builder ) {
        builder.ToTable( "Claim", "Permissions", t => t.HasComment( "声明" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TClaim> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "ClaimId" )
            .HasComment( "声明标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TClaim> builder ) {
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "声明名称" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TClaim> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TClaim> builder ) {
    }
}