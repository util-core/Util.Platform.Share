namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations; 

/// <summary>
/// 权限类型配置
/// </summary>
public abstract class PermissionConfigurationBase<TPermission, TPermissionId, TRoleId, TResource, TResourceId, TAuditUserId> : IEntityTypeConfiguration<TPermission> 
    where TPermission : PermissionBase<TPermission, TPermissionId, TRoleId, TResource, TResourceId, TAuditUserId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TPermission> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    private void ConfigTable( EntityTypeBuilder<TPermission> builder ) {
        builder.ToTable( "Permission", "Permissions", t => t.HasComment( "权限" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    private void ConfigId( EntityTypeBuilder<TPermission> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "PermissionId" )
            .HasComment( "权限标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    private void ConfigProperties( EntityTypeBuilder<TPermission> builder ) {
        builder.Property( t => t.RoleId )
            .HasColumnName( "RoleId" )
            .HasComment( "角色标识" );
        builder.Property( t => t.ResourceId )
            .HasColumnName( "ResourceId" )
            .HasComment( "资源标识" );
        builder.Property( t => t.IsDeny )
            .HasColumnName( "IsDeny" )
            .HasComment( "拒绝" );
        builder.Property( t => t.IsTemporary )
            .HasColumnName( "IsTemporary" )
            .HasComment( "临时" );
        builder.Property( t => t.ExpirationTime )
            .HasColumnName( "ExpirationTime" )
            .HasComment( "到期时间" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TPermission> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TPermission> builder ) {
    }
}