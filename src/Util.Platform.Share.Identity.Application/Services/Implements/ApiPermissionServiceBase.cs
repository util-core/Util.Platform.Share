using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Applications.Services.Abstractions;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// Api权限服务
/// </summary>
public abstract class ApiPermissionServiceBase<TUnitOfWork, TPermission, TResource, TApplication, TUser, TRole, TApiResourceDto, TPermissionRequest>
    : ServiceBase, IApiPermissionServiceBase<TApiResourceDto, TPermissionRequest>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>, new()
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto>, new()
    where TPermissionRequest : PermissionRequestBase {

    #region 构造方法

    /// <summary>
    /// 初始化Api权限服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="resourceRepository">资源仓储</param>
    protected ApiPermissionServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IPermissionRepositoryBase<TPermission> permissionRepository,
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

    #region GetApisAsync

    /// <inheritdoc />
    public virtual async Task<List<TApiResourceDto>> GetApisAsync( Guid applicationId, Guid roleId, bool isDeny ) {
        var checkedResourceIds = await PermissionRepository.GetApiPermissionResourceIdsAsync( applicationId, roleId, isDeny );
        var resources = await ResourceRepository.Find().Where( t => t.ApplicationId == applicationId
                                                                   && t.Enabled
                                                                   && t.Type == ResourceType.Api
            )
            .Select( resource => new { resource.Id, resource.Name, resource.Uri, resource.Level, resource.ParentId, resource.SortId } )
            .ToListAsync();
        return resources.Select( resource => {
            var dto = new TApiResourceDto();
            resource.MapTo( dto );
            dto.Checked = checkedResourceIds.Contains( dto.Id.ToGuid() );
            return dto;
        } ).ToList();
    }

    #endregion

    #region ClearApiPermissionsAsync

    /// <inheritdoc />
    public virtual async Task ClearApiPermissionsAsync( Guid applicationId, Guid roleId ) {
        var permissionIds = await PermissionRepository.ClearApiPermissionsAsync( applicationId, roleId );
        await UnitOfWork.CommitAsync();
        Log.Append( "清除角色 {RoleId} 的Api权限成功,", roleId )
            .Append( "应用程序标识: {ApplicationId}", applicationId )
            .Append( "权限标识列表: {PermisionIds}", permissionIds.Join() )
            .LogInformation();
    }

    #endregion

    #region GrantApiPermissionsAsync

    /// <inheritdoc />
    public virtual async Task GrantApiPermissionsAsync( TPermissionRequest request ) {
        await SaveApiPermissionsAsync( request, false );
    }

    /// <summary>
    /// 保存Api权限
    /// </summary>
    protected virtual async Task SaveApiPermissionsAsync( TPermissionRequest request, bool isDeny ) {
        request.CheckNull( nameof( request ) );
        request.Validate();
        var applicationId = request.ApplicationId.SafeValue();
        var roleId = request.RoleId.SafeValue();
        var resourceIds = request.ResourceIds.ToGuidList();
        var originalResourceIds = await PermissionRepository.GetApiPermissionResourceIdsAsync( applicationId, roleId, isDeny );
        var result = resourceIds.Compare( originalResourceIds );
        await PermissionRepository.AddAsync( CreatePermissions( roleId, result.CreateList, isDeny ) );
        await PermissionRepository.RemoveAsync( roleId, result.DeleteList, isDeny );
        await UnitOfWork.CommitAsync();
        WriteSaveApiPermissionsLog( roleId, result, isDeny );
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
    /// 保存Api权限日志
    /// </summary>
    protected virtual void WriteSaveApiPermissionsLog( Guid roleId, KeyListCompareResult<Guid> result, bool isDeny ) {
        var operation = isDeny ? "拒绝" : "授予";
        if ( result.CreateList.Count > 0 ) {
            Log.Append( "角色 {RoleId} ", roleId )
                .Append( $"{operation}Api权限成功," )
                .AppendLine( "资源标识列表: {ResourceIds}.", result.CreateList.Join() );
        }
        if ( result.DeleteList.Count > 0 ) {
            Log.Append( "角色 {RoleId} ", roleId )
                .Append( $"取消{operation}Api权限成功," )
                .AppendLine( "资源标识列表: {ResourceIds}", result.DeleteList.Join() );
        }
        Log.LogInformation();
    }

    #endregion

    #region DenyApiPermissionsAsync

    /// <inheritdoc />
    public virtual async Task DenyApiPermissionsAsync( TPermissionRequest request ) {
        await SaveApiPermissionsAsync( request, true );
    }

    #endregion
}