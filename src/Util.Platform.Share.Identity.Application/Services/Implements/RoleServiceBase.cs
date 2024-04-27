using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Domain.Services.Abstractions;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 角色服务
/// </summary>
public abstract class RoleServiceBase<TUnitOfWork, TRole, TUser, TRoleDto, TCreateRoleRequest, TUpdateRoleRequest, TRoleQuery, TRoleUsersRequest>
    : CrudServiceBase<TRole, TRoleDto, TCreateRoleRequest, TUpdateRoleRequest, TRoleQuery>, IRoleServiceBase<TRoleDto, TCreateRoleRequest, TUpdateRoleRequest, TRoleQuery, TRoleUsersRequest>
    where TUnitOfWork : IUnitOfWork
    where TRole : RoleBase<TRole, TUser>, new()
    where TUser : UserBase<TUser, TRole>
    where TRoleDto : RoleDtoBase<TRoleDto>, new()
    where TCreateRoleRequest : CreateRoleRequestBase, new()
    where TUpdateRoleRequest : UpdateRoleRequestBase, new()
    where TRoleQuery : RoleQueryBase
    where TRoleUsersRequest : RoleUsersRequestBase {

    #region 构造方法

    /// <summary>
    /// 初始化角色服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="roleManager">角色服务</param>
    protected RoleServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IRoleRepositoryBase<TRole> roleRepository, IRoleManagerBase<TRole> roleManager )
        : base( serviceProvider, unitOfWork, roleRepository ) {
        RoleRepository = roleRepository ?? throw new ArgumentNullException( nameof( roleRepository ) );
        RoleManager = roleManager ?? throw new ArgumentNullException( nameof( roleManager ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 角色仓储
    /// </summary>
    protected IRoleRepositoryBase<TRole> RoleRepository { get; set; }
    /// <summary>
    /// 角色服务
    /// </summary>
    protected IRoleManagerBase<TRole> RoleManager { get; set; }

    #endregion

    #region Filter

    /// <inheritdoc />
    protected override IQueryable<TRole> Filter( IQueryable<TRole> queryable, TRoleQuery param ) {
        if ( param.Type.IsEmpty() )
            param.Type = "Role";
        return queryable.Where( t => t.Type.Contains( param.Type ) )
            .WhereIfNotEmpty( t => t.Code.Contains( param.Code ) )
            .WhereIfNotEmpty( t => t.Name.Contains( param.Name ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled )
            .WhereIfNotEmpty( t => t.Remark.Contains( param.Remark ) )
            .Between( t => t.CreationTime, param.BeginCreationTime, param.EndCreationTime, false )
            .Between( t => t.LastModificationTime, param.BeginLastModificationTime, param.EndLastModificationTime, false );
    }

    #endregion

    #region CreateAsync

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="request">创建角色参数</param>
    public override async Task<string> CreateAsync( TCreateRoleRequest request ) {
        var role = request.MapTo<TRole>();
        await RoleManager.CreateAsync( role );
        await CommitAsync();
        WriteCreateLog( role );
        return role.Id.SafeString();
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreateLog( TRole entity ) {
        Log.Append( "角色{RoleName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// 修改角色
    /// </summary>
    /// <param name="request">修改角色参数</param>
    public override async Task UpdateAsync( TUpdateRoleRequest request ) {
        if ( request.Id.IsEmpty() )
            throw new InvalidOperationException( R.IdIsEmpty );
        var oldEntity = await RoleRepository.FindByIdAsync( request.Id );
        oldEntity.CheckNull( nameof( oldEntity ) );
        var entity = request.MapTo( oldEntity.Clone() );
        var changes = oldEntity.GetChanges( entity );
        await RoleManager.UpdateAsync( entity );
        await CommitAsync();
        WriteUpdateLog( entity, changes );
    }

    /// <summary>
    /// 写更新日志
    /// </summary>
    protected virtual void WriteUpdateLog( TRole entity, ChangeValueCollection changeValues ) {
        Log.Append( "角色{RoleName}修改成功,", entity.Name )
            .Append( "业务标识: {Id},", entity.Id )
            .Append( "修改内容: {changeValues}", changeValues )
            .LogInformation();
    }

    #endregion

    #region DeleteCommitAfterAsync

    /// <inheritdoc />
    protected override Task DeleteCommitAfterAsync( List<TRole> entities ) {
        Log.Append( "角色{RoleNames}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region AddUsersToRoleAsync(添加用户到角色)

    /// <inheritdoc />
    public virtual async Task AddUsersToRoleAsync( TRoleUsersRequest request ) {
        request.CheckNull( nameof( request ) );
        request.Validate();
        await RoleManager.AddUsersToRoleAsync( request.RoleId.SafeValue(), request.UserIds.ToGuidList() );
        await UnitOfWork.CommitAsync();
        Log.Append( "添加用户到角色成功," )
            .Append( "角色标识: {RoleId},", request.RoleId )
            .Append( "用户标识: {UserIds}", request.UserIds )
            .LogInformation();
    }

    #endregion

    #region RemoveUsersFromRoleAsync(从角色移除用户)

    /// <inheritdoc />
    public virtual async Task RemoveUsersFromRoleAsync( TRoleUsersRequest request ) {
        request.CheckNull( nameof( request ) );
        request.Validate();
        await RoleManager.RemoveUsersFromRoleAsync( request.RoleId.SafeValue(), request.UserIds.ToGuidList() );
        await UnitOfWork.CommitAsync();
        Log.Append( "从角色移除用户成功," )
            .Append( "角色标识: {RoleId},", request.RoleId )
            .Append( "用户标识: {UserIds}", request.UserIds )
            .LogInformation();
    }

    #endregion
}