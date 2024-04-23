using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 用户服务
/// </summary>
public abstract class UserServiceBase<TUnitOfWork, TUser, TRole, TUserDto, TCreateUserRequest, TUserQuery>
    : CrudServiceBase<TUser, TUserDto, TCreateUserRequest, TUserDto, TUserQuery>, IUserServiceBase<TUserDto, TCreateUserRequest, TUserQuery>
    where TUnitOfWork : IUnitOfWork
    where TUser : UserBase<TUser, TRole>,new()
    where TRole : RoleBase<TRole, TUser> 
    where TUserDto : UserDtoBase, new() 
    where TCreateUserRequest : CreateUserRequestBase, new()
    where TUserQuery : UserQueryBase {

    #region 构造方法

    /// <summary>
    /// 初始化用户服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userManager">用户服务</param>
    protected UserServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IUserRepositoryBase<TUser> userRepository, IUserManagerBase<TUser, TRole> userManager )
        : base( serviceProvider, unitOfWork, userRepository ) {
        UserRepository = userRepository ?? throw new ArgumentNullException( nameof( userRepository ) );
        UserManager = userManager ?? throw new ArgumentNullException( nameof( userManager ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 用户仓储
    /// </summary>
    public IUserRepositoryBase<TUser> UserRepository { get; set; }
    /// <summary>
    /// 用户服务
    /// </summary>
    public IUserManagerBase<TUser, TRole> UserManager { get; set; }

    #endregion

    #region Filter

    /// <inheritdoc />
    protected override IQueryable<TUser> Filter( IQueryable<TUser> queryable, TUserQuery param ) {
        return queryable
            .WhereIfNotEmpty( t => t.UserName.Contains( param.UserName ) )
            .WhereIfNotEmpty( t => t.PhoneNumber.Contains( param.PhoneNumber ) )
            .WhereIfNotEmpty( t => t.Email.Contains( param.Email ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled )
            .WhereIfNotEmpty( t => t.Roles.Any( role => role.Id == param.RoleId ) )
            .WhereIfNotEmpty( t => t.Roles.All( role => role.Id != param.ExceptRoleId ) )
            .Between( t => t.DisabledTime, param.BeginDisabledTime, param.EndDisabledTime, false )
            .WhereIfNotEmpty( t => t.LockoutEnabled == param.LockoutEnabled )
            .Between( t => t.LockoutEnd, param.BeginLockoutEnd, param.EndLockoutEnd, false )
            .WhereIfNotEmpty( t => t.RegisterIp.Contains( param.RegisterIp ) )
            .Between( t => t.LastLoginTime, param.BeginLastLoginTime, param.EndLastLoginTime, false )
            .WhereIfNotEmpty( t => t.LastLoginIp.Contains( param.LastLoginIp ) )
            .Between( t => t.CurrentLoginTime, param.BeginCurrentLoginTime, param.EndCurrentLoginTime, false )
            .WhereIfNotEmpty( t => t.CurrentLoginIp.Contains( param.CurrentLoginIp ) )
            .WhereIfNotEmpty( t => t.Remark.Contains( param.Remark ) )
            .Between( t => t.CreationTime, param.BeginCreationTime, param.EndCreationTime, false )
            .Between( t => t.LastModificationTime, param.BeginLastModificationTime, param.EndLastModificationTime, false );
    }

    #endregion

    #region CreateAsync

    /// <inheritdoc />
    public override async Task<string> CreateAsync( TCreateUserRequest request ) {
        var entity = request.MapTo<TUser>();
        await UserManager.CreateAsync( entity, request.Password );
        await UnitOfWork.CommitAsync();
        WriteCreateLog( entity );
        return entity.Id.SafeString();
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreateLog( TUser entity ) {
        Log.Append( "用户{UserName}创建成功,", entity.UserName )
            .Append( "用户标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion
}