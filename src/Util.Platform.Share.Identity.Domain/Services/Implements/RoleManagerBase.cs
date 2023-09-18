using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;

namespace Util.Platform.Share.Identity.Domain.Services.Implements;

/// <summary>
/// 角色服务
/// </summary>
public abstract class RoleManagerBase<TRole, TUser> : RoleManagerBase<TRole, Guid, Guid?, TUser, Guid, Guid, Guid?>, IRoleManagerBase<TRole>
    where TRole : RoleBase<TRole, TUser>
    where TUser : UserBase<TUser, TRole> {
    /// <summary>
    /// 初始化角色服务
    /// </summary>
    /// <param name="roleManager">Identity角色服务</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="localizer">本地化查找器</param>
    protected RoleManagerBase( RoleManager<TRole> roleManager, IRoleRepositoryBase<TRole> roleRepository, IUserRepositoryBase<TUser> userRepository, IStringLocalizer localizer )
        : base( roleManager, roleRepository, userRepository, localizer ) {
    }
}

/// <summary>
/// 角色服务
/// </summary>
public abstract class RoleManagerBase<TRole, TRoleId, TParentRoleId, TUser, TUserId, TApplicationId, TAuditUserId> : IRoleManagerBase<TRole, TRoleId, TUserId>
    where TRole : RoleBase<TRole, TRoleId, TParentRoleId, TUser, TAuditUserId>
    where TUser : class, IAggregateRoot<TUserId> {
    /// <summary>
    /// 初始化角色服务
    /// </summary>
    /// <param name="roleManager">Identity角色服务</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="localizer">本地化查找器</param>
    protected RoleManagerBase( RoleManager<TRole> roleManager, IRoleRepositoryBase<TRole,TRoleId, TParentRoleId,TApplicationId,TUserId> roleRepository, 
        IUserRepositoryBase<TUser, TUserId, TRoleId> userRepository, IStringLocalizer localizer ) {
        Manager = roleManager ?? throw new ArgumentNullException( nameof( roleManager ) );
        RoleRepository = roleRepository ?? throw new ArgumentNullException( nameof( roleRepository ) );
        UserRepository = userRepository ?? throw new ArgumentNullException( nameof( userRepository ) );
        Localizer = localizer ?? throw new ArgumentNullException( nameof( localizer ) );
    }

    /// <summary>
    /// Identity角色服务
    /// </summary>
    protected RoleManager<TRole> Manager { get; }
    /// <summary>
    /// 角色仓储
    /// </summary>
    protected IRoleRepositoryBase<TRole, TRoleId, TParentRoleId, TApplicationId, TUserId> RoleRepository { get; }
    /// <summary>
    /// 用户仓储
    /// </summary>
    protected IUserRepositoryBase<TUser, TUserId, TRoleId> UserRepository { get; }
    /// <summary>
    /// 本地化查找器
    /// </summary>
    protected IStringLocalizer Localizer { get; }

    /// <inheritdoc />
    public virtual async Task CreateAsync( TRole entity ) {
        await Validate( entity );
        entity.Init();
        var parent = await RoleRepository.FindByIdAsync( entity.ParentId );
        entity.InitPath( parent );
        var result = await Manager.CreateAsync( entity );
        result.ThrowIfError();
    }

    /// <summary>
    /// 验证角色
    /// </summary>
    private async Task Validate( TRole entity ) {
        if ( await RoleRepository.ExistsByCodeAsync( entity ) )
            throw new Warning( string.Format( Localizer["DuplicateRoleCode"], entity.Code ) );
    }

    /// <inheritdoc />
    public async Task UpdateAsync( TRole entity ) {
        await Validate( entity );
        entity.InitPinYin();
        await RoleRepository.UpdatePathAsync( entity );
        var result = await Manager.UpdateAsync( entity );
        result.ThrowIfError();
    }

    /// <inheritdoc />
    public async Task AddUsersToRoleAsync( TRoleId roleId, IEnumerable<TUserId> userIds ) {
        if ( roleId.Equals(default) || userIds == null )
            return;
        var role = await RoleRepository.FindByIdAsync( roleId );
        var users = await UserRepository.GetUnassociatedUsers( roleId, userIds );
        users.ForEach( role.Users.Add );
    }

    /// <inheritdoc />
    public async Task RemoveUsersFromRoleAsync( TRoleId roleId, IEnumerable<TUserId> userIds ) {
        if ( roleId.Equals( default ) || userIds == null )
            return;
        var role = await RoleRepository.FindByIncludeUsersAsync( roleId );
        var users = await UserRepository.FindByIdsAsync( userIds );
        users.ForEach( user => role.Users.Remove( user ) );
    }
}