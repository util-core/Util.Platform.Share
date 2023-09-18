namespace Util.Platform.Share.Identity.Data.EntityTypeConfigurations;

/// <summary>
/// 角色类型配置
/// </summary>
public abstract class RoleConfigurationBase<TRole, TUser> : RoleConfigurationBase<TRole, Guid, Guid?, TUser, Guid?>
    where TRole : RoleBase<TRole,TUser>
    where TUser : UserBase<TUser, TRole> {
}

/// <summary>
/// 角色类型配置
/// </summary>
public abstract class RoleConfigurationBase<TRole, TRoleId, TParentId, TUser, TAuditUserId> : IEntityTypeConfiguration<TRole> 
    where TRole : RoleBase<TRole, TRoleId, TParentId, TUser, TAuditUserId> 
    where TUser: class {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<TRole> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected virtual void ConfigTable( EntityTypeBuilder<TRole> builder ) {
        builder.ToTable( "Role", "Permissions", t => t.HasComment( "角色" ) );
    }

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<TRole> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "RoleId" )
            .HasComment( "角色标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<TRole> builder ) {
        builder.Property( t => t.Code )
            .HasColumnName( "Code" )
            .HasComment( "角色编码" );
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "角色名称" );
        builder.Property( t => t.NormalizedName )
            .HasColumnName( "NormalizedName" )
            .HasComment( "标准化角色名称" );
        builder.Property( t => t.Type )
            .HasColumnName( "Type" )
            .HasComment( "角色类型" );
        builder.Property( t => t.IsAdmin )
            .HasColumnName( "IsAdmin" )
            .HasComment( "管理员" );
        builder.Property( t => t.ParentId )
            .HasColumnName( "ParentId" )
            .HasComment( "父标识" );
        builder.Property( t => t.Path )
            .HasColumnName( "Path" )
            .HasComment( "路径" );
        builder.Property( t => t.Level )
            .HasColumnName( "Level" )
            .HasComment( "层级" );
        builder.Property( t => t.SortId )
            .HasColumnName( "SortId" )
            .HasComment( "排序号" );
        builder.Property( t => t.Enabled )
            .HasColumnName( "Enabled" )
            .HasComment( "启用" );
        builder.Property( t => t.Remark )
            .HasColumnName( "Remark" )
            .HasComment( "备注" );
        builder.Property( t => t.PinYin )
            .HasColumnName( "PinYin" )
            .HasComment( "拼音简码" );
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
    protected virtual void ConfigIndex( EntityTypeBuilder<TRole> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<TRole> builder ) {
    }
}