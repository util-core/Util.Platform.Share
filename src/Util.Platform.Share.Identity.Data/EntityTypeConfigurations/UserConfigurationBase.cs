namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 用户类型配置
/// </summary>
public abstract class UserConfigurationBase<TUser, TRole, TUserRole> : UserConfigurationBase<TUser, Guid, TRole, Guid, Guid?, TUserRole, Guid?>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TUserRole : UserRoleBase {
}

/// <summary>
/// 用户类型配置
/// </summary>
public abstract class UserConfigurationBase<TUser, TUserId, TRole, TRoleId, TRoleParentId, TUserRole, TAuditUserId> : IEntityTypeConfiguration<TUser> 
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId> 
    where TRole: RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId> 
    where TUserRole: UserRoleBase<TUserId,TRoleId> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TUser> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigAssociations( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TUser> builder ) {
        builder.ToTable( "User", "Permissions", t => t.HasComment( "用户" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TUser> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "UserId" )
            .HasComment( "用户标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TUser> builder ) {
        builder.Property( t => t.UserName )
            .HasColumnName( "UserName" )
            .HasComment( "用户名" );
        builder.Property( t => t.NormalizedUserName )
            .HasColumnName( "NormalizedUserName" )
            .HasComment( "标准化用户名" );
        builder.Property( t => t.Email )
            .HasColumnName( "Email" )
            .HasComment( "安全邮箱" );
        builder.Property( t => t.NormalizedEmail )
            .HasColumnName( "NormalizedEmail" )
            .HasComment( "标准化邮箱" );
        builder.Property( t => t.EmailConfirmed )
            .HasColumnName( "EmailConfirmed" )
            .HasComment( "邮箱是否已确认" );
        builder.Property( t => t.PhoneNumber )
            .HasColumnName( "PhoneNumber" )
            .HasComment( "安全手机号" );
        builder.Property( t => t.PhoneNumberConfirmed )
            .HasColumnName( "PhoneNumberConfirmed" )
            .HasComment( "手机号是否已确认" );
        builder.Property( t => t.PasswordHash )
            .HasColumnName( "PasswordHash" )
            .HasComment( "密码散列" );
        builder.Property( t => t.TwoFactorEnabled )
            .HasColumnName( "TwoFactorEnabled" )
            .HasComment( "是否启用双因素认证" );
        builder.Property( t => t.Enabled )
            .HasColumnName( "Enabled" )
            .HasComment( "是否启用" );
        builder.Property( t => t.DisabledTime )
            .HasColumnName( "DisabledTime" )
            .HasComment( "冻结时间" );
        builder.Property( t => t.LockoutEnabled )
            .HasColumnName( "LockoutEnabled" )
            .HasComment( "是否启用锁定" );
        builder.Property( t => t.LockoutEnd )
            .HasColumnName( "LockoutEnd" )
            .HasComment( "锁定截止" );
        builder.Property( t => t.AccessFailedCount )
            .HasColumnName( "AccessFailedCount" )
            .HasComment( "登录失败次数" );
        builder.Property( t => t.LoginCount )
            .HasColumnName( "LoginCount" )
            .HasComment( "登录次数" );
        builder.Property( t => t.RegisterIp )
            .HasColumnName( "RegisterIp" )
            .HasComment( "注册Ip" );
        builder.Property( t => t.LastLoginTime )
            .HasColumnName( "LastLoginTime" )
            .HasComment( "上次登陆时间" );
        builder.Property( t => t.LastLoginIp )
            .HasColumnName( "LastLoginIp" )
            .HasComment( "上次登陆Ip" );
        builder.Property( t => t.CurrentLoginTime )
            .HasColumnName( "CurrentLoginTime" )
            .HasComment( "本次登陆时间" );
        builder.Property( t => t.CurrentLoginIp )
            .HasColumnName( "CurrentLoginIp" )
            .HasComment( "本次登陆Ip" );
        builder.Property( t => t.SecurityStamp )
            .HasColumnName( "SecurityStamp" )
            .HasComment( "安全戳" );
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
    /// 配置关联
    /// </summary>
    protected virtual void ConfigAssociations( EntityTypeBuilder<TUser> builder ) {
        builder.HasMany( t => t.Roles )
            .WithMany( t => t.Users )
            .UsingEntity<TUserRole>();
    }

    /// <summary>
    /// 配置索引
    /// </summary>
    protected virtual void ConfigIndex( EntityTypeBuilder<TUser> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TUser> builder ) {
    }
}