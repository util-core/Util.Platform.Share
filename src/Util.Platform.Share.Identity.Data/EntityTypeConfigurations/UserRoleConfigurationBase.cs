namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 用户角色类型配置
/// </summary>
public abstract class UserRoleConfigurationBase<TUserRole> : UserRoleConfigurationBase<TUserRole, Guid, Guid>
    where TUserRole : UserRoleBase {
}

/// <summary>
/// 用户角色类型配置
/// </summary>
public abstract class UserRoleConfigurationBase<TUserRole, TUserId, TRoleId> : IEntityTypeConfiguration<TUserRole> 
    where TUserRole : UserRoleBase<TUserId, TRoleId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TUserRole> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TUserRole> builder ) {
        builder.ToTable( "UserRole", "Permissions", t => t.HasComment( "用户角色" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TUserRole> builder ) {
        builder.HasKey( t => new { t.UserId, t.RoleId } );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TUserRole> builder ) {
        builder.Property( t => t.UserId )
            .HasColumnName( "UserId" )
            .HasComment( "用户标识" );
        builder.Property( t => t.RoleId )
            .HasColumnName( "RoleId" )
            .HasComment( "角色标识" );
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TUserRole> builder ) {
    }
}