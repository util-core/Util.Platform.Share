namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 角色仓储
/// </summary>
public abstract class RoleRepositoryBase<TUnitOfWork,TRole,TUser> 
    : RoleRepositoryBase<TUnitOfWork, TRole, Guid, Guid?, Guid?, TUser, Guid, Guid?>, IRoleRepositoryBase<TRole>
    where TUnitOfWork : IUnitOfWork
    where TRole : RoleBase<TRole, TUser>
    where TUser : UserBase<TUser, TRole> {
    /// <summary>
    /// 初始化角色仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected RoleRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}

/// <summary>
/// 角色仓储
/// </summary>
public abstract class RoleRepositoryBase<TUnitOfWork, TRole, TRoleId, TRoleParentId, TApplicationId, TUser, TUserId, TAuditUserId>
    : TreeRepositoryBase<TRole, TRoleId, TRoleParentId>, IRoleStore<TRole>, IRoleRepositoryBase<TRole, TRoleId, TRoleParentId, TApplicationId, TUserId>
    where TUnitOfWork : IUnitOfWork
    where TRole : RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId>
    where TUser: UserBase<TUser, TUserId, TRole, TAuditUserId> {
    /// <summary>
    /// 初始化角色仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected RoleRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IdentityResult> CreateAsync( TRole role, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        await AddAsync( role, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 验证角色
    /// </summary>
    protected virtual void ValidateRole( TRole role, CancellationToken cancellationToken ) {
        role.CheckNull( nameof( role ) );
        cancellationToken.ThrowIfCancellationRequested();
    }

    /// <inheritdoc />
    public virtual async Task<bool> ExistsByCodeAsync( TRole entity ) {
        return await ExistsAsync( t => !t.Id.Equals( entity.Id ) && t.Code == entity.Code );
    }

    /// <inheritdoc />
    public virtual async Task<TRole> FindByIncludeUsersAsync( TRoleId roleId ) {
        return await Find( t => t.Id.Equals( roleId ) ).Include( t => t.Users ).SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public virtual async Task<List<TRoleId>> GetRoleIdsByUserIdAsync( TUserId userId ) {
        var roles = await Find( t => t.Users.Select( u => u.Id ).Contains( userId ) ).ToListAsync();
        var result = new List<TRoleId>();
        roles.ForEach( role => result.AddRange( role.GetParentIdsFromPath( false ) ) );
        return result.Distinct().ToList();
    }

    /// <summary>
    /// 修改角色
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public override async Task<IdentityResult> UpdateAsync( TRole role, CancellationToken cancellationToken = default ) {
        ValidateRole( role, cancellationToken );
        await base.UpdateAsync( role, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IdentityResult> DeleteAsync( TRole role, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        await RemoveAsync( role, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 获取角色标识
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetRoleIdAsync( TRole role, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        return Task.FromResult( role.Id.SafeString() );
    }

    /// <summary>
    /// 获取角色名称
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetRoleNameAsync( TRole role, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        return Task.FromResult( role.Name );
    }

    /// <summary>
    /// 设置角色名称
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="roleName">角色名</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetRoleNameAsync( TRole role, string roleName, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        role.Name = roleName;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取标准化名称
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetNormalizedRoleNameAsync( TRole role, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        return Task.FromResult( role.NormalizedName );
    }

    /// <summary>
    /// 设置标准化角色名称
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="normalizedName">标准化角色名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetNormalizedRoleNameAsync( TRole role, string normalizedName, CancellationToken cancellationToken ) {
        ValidateRole( role, cancellationToken );
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 通过标识查找
    /// </summary>
    /// <param name="roleId">角色标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TRole> FindByIdAsync( string roleId, CancellationToken cancellationToken ) {
        return await FindByIdAsync( roleId.ToGuid(), cancellationToken );
    }

    /// <summary>
    /// 通过名称获取角色
    /// </summary>
    /// <param name="normalizedRoleName">标准化角色名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TRole> FindByNameAsync( string normalizedRoleName, CancellationToken cancellationToken ) {
        cancellationToken.ThrowIfCancellationRequested();
        return await SingleAsync( r => r.NormalizedName == normalizedRoleName, cancellationToken );
    }
}