namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 用户仓储
/// </summary>
public abstract class UserRepositoryBase<TUnitOfWork, TUser,TRole> 
    : UserRepositoryBase<TUnitOfWork, TUser, Guid, TRole, Guid, Guid?, Guid?>, IUserRepositoryBase<TUser>
    where TUnitOfWork : IUnitOfWork
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole,TUser> {
    /// <summary>
    /// 初始化用户仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected UserRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}

/// <summary>
/// 用户仓储
/// </summary>
public abstract class UserRepositoryBase<TUnitOfWork, TUser, TUserId, TRole, TRoleId, TRoleParentId, TAuditUserId>
    : RepositoryBase<TUser, TUserId>, IUserRepositoryBase<TUser, TUserId, TRoleId>, IUserPasswordStore<TUser>,
    IUserSecurityStampStore<TUser>, IUserLockoutStore<TUser>, IUserEmailStore<TUser>, IUserPhoneNumberStore<TUser>
    where TUnitOfWork : IUnitOfWork
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId>
    where TRole : RoleBase<TRole, TRoleId, TRoleParentId, TUser, TAuditUserId> {
    /// <summary>
    /// 初始化用户仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected UserRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    /// <summary>
    /// 验证用户
    /// </summary>
    protected virtual void ValidateUser( TUser user, CancellationToken cancellationToken ) {
        user.CheckNull( nameof( user ) );
        cancellationToken.ThrowIfCancellationRequested();
    }

    /// <inheritdoc />
    public virtual bool IsAdmin( TUserId userId ) {
        return Find().Any( user => user.Id.Equals( userId ) && user.Roles.Any( t => t.IsAdmin ) );
    }

    /// <inheritdoc />
    public virtual async Task<bool> IsAdminAsync( TUserId userId, CancellationToken cancellationToken = default ) {
        return await Find().AnyAsync( user => user.Id.Equals( userId ) && user.Roles.Any( t => t.IsAdmin ), cancellationToken );
    }

    /// <inheritdoc />
    public virtual async Task<List<TUser>> GetUnassociatedUsers( TRoleId roleId, IEnumerable<TUserId> userIds, CancellationToken cancellationToken = default ) {
        if ( roleId.Equals( default ) || userIds == null )
            return new List<TUser>();
        var userKeys = userIds.ToList();
        var associatedUserIds = await Find( user => user.Roles.Any( role => role.Id.Equals( roleId ) && userKeys.Contains( user.Id ) ) ).Select( t => t.Id ).ToListAsync( cancellationToken );
        userIds = userKeys.Except( associatedUserIds );
        return await FindByIdsAsync( userIds, cancellationToken );
    }

    /// <summary>
    /// 获取用户标识
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetUserIdAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.Id.SafeString() );
    }

    /// <summary>
    /// 获取用户名
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetUserNameAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.UserName );
    }

    /// <summary>
    /// 设置用户名
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="userName">用户名</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetUserNameAsync( TUser user, string userName, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.UserName = userName;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取标准化用户名
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetNormalizedUserNameAsync( TUser user, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.NormalizedUserName );
    }

    /// <summary>
    /// 设置标准化用户名
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="normalizedName">标准化用户名</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetNormalizedUserNameAsync( TUser user, string normalizedName, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IdentityResult> CreateAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        await AddAsync( user, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public override async Task<IdentityResult> UpdateAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        await base.UpdateAsync( user, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IdentityResult> DeleteAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        await base.RemoveAsync( user, cancellationToken );
        return IdentityResult.Success;
    }

    /// <summary>
    /// 通过标识查找
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TUser> FindByIdAsync( string userId, CancellationToken cancellationToken = default ) {
        cancellationToken.ThrowIfCancellationRequested();
        return await FindByIdAsync( userId.ToGuid(), cancellationToken );
    }

    /// <summary>
    /// 通过用户名查找
    /// </summary>
    /// <param name="normalizedUserName">标准化用户名</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<TUser> FindByNameAsync( string normalizedUserName, CancellationToken cancellationToken = default ) {
        cancellationToken.ThrowIfCancellationRequested();
        return SingleAsync( t => t.NormalizedUserName == normalizedUserName, cancellationToken );
    }

    /// <summary>
    /// 设置密码散列
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="passwordHash">密码散列</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetPasswordHashAsync( TUser user, string passwordHash, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取密码散列
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetPasswordHashAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.PasswordHash );
    }

    /// <summary>
    /// 是否设置密码
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<bool> HasPasswordAsync( TUser user, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.PasswordHash.IsEmpty() == false );
    }

    /// <summary>
    /// 设置安全戳
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="stamp">安全戳</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetSecurityStampAsync( TUser user, string stamp, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        if ( string.IsNullOrWhiteSpace( stamp ) )
            throw new ArgumentNullException( nameof( stamp ) );
        user.SecurityStamp = stamp;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取安全戳
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetSecurityStampAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.SecurityStamp );
    }

    /// <summary>
    /// 获取锁定结束日期
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<DateTimeOffset?> GetLockoutEndDateAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.LockoutEnd );
    }

    /// <summary>
    /// 设置锁定结束日期
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="lockoutEnd">锁定结束日期</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetLockoutEndDateAsync( TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.LockoutEnd = lockoutEnd;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 增加访问失败次数
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<int> IncrementAccessFailedCountAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.AccessFailedCount = user.AccessFailedCount.SafeValue() + 1;
        return Task.FromResult( user.AccessFailedCount.SafeValue() );
    }

    /// <summary>
    /// 重置访问失败次数
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task ResetAccessFailedCountAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.AccessFailedCount = 0;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取登录失败次数
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<int> GetAccessFailedCountAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.AccessFailedCount.SafeValue() );
    }

    /// <summary>
    /// 获取锁定启用状态
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<bool> GetLockoutEnabledAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.LockoutEnabled );
    }

    /// <summary>
    /// 设置锁定启用状态
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="enabled">是否启用锁定</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetLockoutEnabledAsync( TUser user, bool enabled, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.LockoutEnabled = enabled;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置电子邮件
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="email">电子邮件</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetEmailAsync( TUser user, string email, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.Email = email;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取电子邮件
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetEmailAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.Email );
    }

    /// <summary>
    /// 获取电子邮件确认状态
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<bool> GetEmailConfirmedAsync( TUser user, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.EmailConfirmed );
    }

    /// <summary>
    /// 确认电子邮件
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="confirmed">是否确认</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetEmailConfirmedAsync( TUser user, bool confirmed, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.EmailConfirmed = confirmed;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 通过电子邮件查找
    /// </summary>
    /// <param name="normalizedEmail">标准化电子邮件</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TUser> FindByEmailAsync( string normalizedEmail, CancellationToken cancellationToken = default ) {
        cancellationToken.ThrowIfCancellationRequested();
        return await SingleAsync( u => u.NormalizedEmail == normalizedEmail, cancellationToken );
    }

    /// <summary>
    /// 获取标准化电子邮件
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetNormalizedEmailAsync( TUser user, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.NormalizedEmail );
    }

    /// <summary>
    /// 设置标准化电子邮件
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="normalizedEmail">标准化电子邮件</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetNormalizedEmailAsync( TUser user, string normalizedEmail, CancellationToken cancellationToken = default ) {
        ValidateUser( user, cancellationToken );
        user.NormalizedEmail = normalizedEmail;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置手机号
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="phoneNumber">手机号</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetPhoneNumberAsync( TUser user, string phoneNumber, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.PhoneNumber = phoneNumber;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取手机号
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<string> GetPhoneNumberAsync( TUser user, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.PhoneNumber );
    }

    /// <summary>
    /// 获取手机号确认状态
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task<bool> GetPhoneNumberConfirmedAsync( TUser user, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        return Task.FromResult( user.PhoneNumberConfirmed );
    }

    /// <summary>
    /// 设置手机号确认状态
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="confirmed">是否确认</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task SetPhoneNumberConfirmedAsync( TUser user, bool confirmed, CancellationToken cancellationToken ) {
        ValidateUser( user, cancellationToken );
        user.PhoneNumberConfirmed = confirmed;
        return Task.CompletedTask;
    }
}