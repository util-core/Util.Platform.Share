using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 操作权限服务
/// </summary>
public abstract class OperationPermissionServiceBase<TUnitOfWork, TPermission, TResource, TApplication, TUser, TRole, TOperationPermissionDto, TPermissionRequest>
    : ServiceBase, IOperationPermissionServiceBase<TOperationPermissionDto, TPermissionRequest>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>, new()
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser> 
    where TOperationPermissionDto : OperationPermissionDtoBase<TOperationPermissionDto>,new()
    where TPermissionRequest : PermissionRequestBase, new() {

    #region 构造方法

    /// <summary>
    /// 初始化操作权限服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="resourceRepository">资源仓储</param>
    protected OperationPermissionServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IPermissionRepositoryBase<TPermission> permissionRepository,
        IUserRepositoryBase<TUser> userRepository, IRoleRepositoryBase<TRole> roleRepository, IResourceRepositoryBase<TResource> resourceRepository ) : base( serviceProvider ) {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        PermissionRepository = permissionRepository ?? throw new ArgumentNullException( nameof( permissionRepository ) );
        UserRepository = userRepository ?? throw new ArgumentNullException( nameof( userRepository ) );
        RoleRepository = roleRepository ?? throw new ArgumentNullException( nameof( roleRepository ) );
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 工作单元
    /// </summary>
    protected TUnitOfWork UnitOfWork { get; }
    /// <summary>
    /// 权限仓储
    /// </summary>
    protected IPermissionRepositoryBase<TPermission> PermissionRepository { get; }
    /// <summary>
    /// 用户仓储
    /// </summary>
    protected IUserRepositoryBase<TUser> UserRepository { get; set; }
    /// <summary>
    /// 角色仓储
    /// </summary>
    protected IRoleRepositoryBase<TRole> RoleRepository { get; }
    /// <summary>
    /// 资源仓储
    /// </summary>
    protected IResourceRepositoryBase<TResource> ResourceRepository { get; }

    #endregion

    #region GetOperationsAsync

    /// <inheritdoc />
    public virtual async Task<List<TOperationPermissionDto>> GetOperationsAsync( Guid applicationId, Guid roleId, bool isDeny ) {
        var checkedResourceIds = await PermissionRepository.GetOperationPermissionResourceIdsAsync( applicationId, roleId, isDeny );
        var resources = await ResourceRepository.Find().Where( t => t.ApplicationId == applicationId
                                                                   && t.Enabled
                                                                   && ( t.Type == ResourceType.Module || t.Type == ResourceType.Operation )
            )
            .Select( resource => new { resource.Id, resource.Name, resource.Level, resource.Type, resource.ParentId, resource.SortId } )
            .ToListAsync();
        return resources.Select( resource => {
            var dto = new TOperationPermissionDto();
            resource.MapTo( dto );
            if ( resource.Type == ResourceType.Operation ) {
                dto.IsOperation = true;
                dto.Hide = true;
            }
            dto.Checked = checkedResourceIds.Contains( dto.Id.ToGuid() );
            return dto;
        } ).ToList();
    }

    #endregion

    #region ClearOperationPermissionsAsync

    /// <inheritdoc />
    public virtual async Task ClearOperationPermissionsAsync( Guid applicationId, Guid roleId ) {
        var permissionIds = await PermissionRepository.ClearOperationPermissionsAsync( applicationId, roleId );
        await UnitOfWork.CommitAsync();
        Log.Append( "清除角色 {RoleId} 的操作权限成功,", roleId )
            .Append( "应用程序标识: {ApplicationId}", applicationId )
            .Append( "权限标识列表: {PermisionIds}", permissionIds.Join() )
            .LogInformation();
    }

    #endregion

    #region GrantOperationPermissionsAsync

    /// <inheritdoc />
    public virtual async Task GrantOperationPermissionsAsync( TPermissionRequest request ) {
        await SaveOperationPermissionsAsync( request, false );
    }

    /// <summary>
    /// 保存操作权限
    /// </summary>
    protected virtual async Task SaveOperationPermissionsAsync( TPermissionRequest request, bool isDeny ) {
        var applicationId = request.ApplicationId.SafeValue();
        var roleId = request.RoleId.SafeValue();
        var resourceIds = request.ResourceIds.ToGuidList();
        var originalResourceIds = await PermissionRepository.GetOperationPermissionResourceIdsAsync( applicationId, roleId, isDeny );
        var result = resourceIds.Compare( originalResourceIds );
        await PermissionRepository.AddAsync( CreatePermissions( roleId, result.CreateList, isDeny ) );
        await PermissionRepository.RemoveAsync( roleId, result.DeleteList, isDeny );
        await UnitOfWork.CommitAsync();
        WriteSaveOperationPermissionsLog( roleId, result, isDeny );
    }

    /// <summary>
    /// 创建权限列表
    /// </summary>
    protected virtual List<TPermission> CreatePermissions( Guid roleId, List<Guid> resourceIds, bool isDeny ) {
        return resourceIds.Select( resourceId => {
            var permission = new TPermission {
                RoleId = roleId,
                ResourceId = resourceId,
                IsDeny = isDeny
            };
            permission.Init();
            return permission;
        } ).ToList();
    }

    /// <summary>
    /// 保存操作权限日志
    /// </summary>
    protected virtual void WriteSaveOperationPermissionsLog( Guid roleId, KeyListCompareResult<Guid> result, bool isDeny ) {
        var operation = isDeny ? "拒绝" : "授予";
        if ( result.CreateList.Count > 0 ) {
            Log.Append( "角色 {RoleId} ", roleId )
                .Append( $"{operation}操作权限成功," )
                .AppendLine( "资源标识列表: {ResourceIds}.", result.CreateList.Join() );
        }
        if ( result.DeleteList.Count > 0 ) {
            Log.Append( "角色 {RoleId} ", roleId )
                .Append( $"取消{operation}操作权限成功," )
                .AppendLine( "资源标识列表: {ResourceIds}", result.DeleteList.Join() );
        }
        Log.LogInformation();
    }

    #endregion

    #region DenyOperationPermissionsAsync

    /// <inheritdoc />
    public virtual async Task DenyOperationPermissionsAsync( TPermissionRequest request ) {
        await SaveOperationPermissionsAsync( request, true );
    }

    #endregion
}