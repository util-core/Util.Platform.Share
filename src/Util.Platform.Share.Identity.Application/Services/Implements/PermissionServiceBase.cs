using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 权限服务
/// </summary>
public abstract class PermissionServiceBase<TUnitOfWork, TPermission, TResource, TApplication, TUser, TRole, TModule, TOperationApi, TAppResources, TModuleDto> 
    : ServiceBase, IPermissionServiceBase<TAppResources>
    where TUnitOfWork : IUnitOfWork
    where TPermission : PermissionBase<TPermission, TResource>, new()
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TUser : UserBase<TUser, TRole>
    where TRole : RoleBase<TRole, TUser>
    where TModule : ModuleBase<TModule>
    where TOperationApi : OperationApiBase<TOperationApi>, new() 
    where TAppResources : AppResourcesBase<TModuleDto>,new()
    where TModuleDto : ModuleDtoBase<TModuleDto> {

    #region 构造方法

    /// <summary>
    /// 初始化权限服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="moduleRepository">模块仓储</param>
    /// <param name="operationApiRepository">操作Api仓储</param>
    protected PermissionServiceBase( IServiceProvider serviceProvider, ICache cache, TUnitOfWork unitOfWork,
        IPermissionRepositoryBase<TPermission> permissionRepository, IUserRepositoryBase<TUser> userRepository, IRoleRepositoryBase<TRole> roleRepository,
        IResourceRepositoryBase<TResource> resourceRepository, IModuleRepositoryBase<TModule> moduleRepository, IOperationApiRepositoryBase<TOperationApi> operationApiRepository ) : base( serviceProvider ) {
        CacheService = cache ?? throw new ArgumentNullException( nameof( cache ) );
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        PermissionRepository = permissionRepository ?? throw new ArgumentNullException( nameof( permissionRepository ) );
        UserRepository = userRepository ?? throw new ArgumentNullException( nameof( userRepository ) );
        RoleRepository = roleRepository ?? throw new ArgumentNullException( nameof( roleRepository ) );
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        ModuleRepository = moduleRepository ?? throw new ArgumentNullException( nameof( moduleRepository ) );
        OperationApiRepository = operationApiRepository ?? throw new ArgumentNullException( nameof( operationApiRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 缓存服务
    /// </summary>
    protected ICache CacheService { get; }
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
    /// <summary>
    /// 模块仓储
    /// </summary>
    protected IModuleRepositoryBase<TModule> ModuleRepository { get; }
    /// <summary>
    /// 操作Api仓储
    /// </summary>
    protected IOperationApiRepositoryBase<TOperationApi> OperationApiRepository { get; }

    #endregion

    #region IsAdmin

    /// <inheritdoc />
    public virtual bool IsAdmin( Guid userId ) {
        return UserRepository.IsAdmin( userId );
    }

    #endregion

    #region IsAdminAsync

    /// <inheritdoc />
    public virtual async Task<bool> IsAdminAsync( Guid userId, CancellationToken cancellationToken = default ) {
        return await UserRepository.IsAdminAsync( userId, cancellationToken );
    }

    #endregion

    #region GetAppResourcesAsync

    /// <inheritdoc />
    public virtual async Task<TAppResources> GetAppResourcesAsync( Guid applicationId, Guid userId, CancellationToken cancellationToken = default ) {
        if ( applicationId.IsEmpty() || userId.IsEmpty() )
            return new TAppResources();
        var isAdmin = await IsAdminAsync( userId, cancellationToken );
        if ( isAdmin )
            return await GetAdminAppResources( applicationId, cancellationToken );
        return await GetUserAppResources( applicationId, userId, cancellationToken );
    }

    /// <summary>
    /// 获取管理员应用资源
    /// </summary>
    protected virtual async Task<TAppResources> GetAdminAppResources( Guid applicationId, CancellationToken cancellationToken ) {
        var modules = await ModuleRepository.GetEnabledModulesAsync( applicationId, cancellationToken );
        return new TAppResources {
            IsAdmin = true,
            Modules = modules.Where( t => t.IsHide.SafeValue() == false ).Select( t => t.MapTo<TModuleDto>() ).ToList()
        };
    }

    /// <summary>
    /// 获取用户应用资源
    /// </summary>
    protected virtual async Task<TAppResources> GetUserAppResources( Guid applicationId, Guid userId, CancellationToken cancellationToken ) {
        var roleIds = await RoleRepository.GetRoleIdsByUserIdAsync( userId );
        var resources = await GetAclResources( applicationId, roleIds, cancellationToken );
        var modules = await GetModulesByOperations( resources );
        var acl = resources.Select( t => t.Uri ).ToList();
        return new TAppResources {
            Modules = modules,
            Acl = acl
        };
    }

    /// <summary>
    /// 获取前端访问控制资源
    /// </summary>
    protected virtual async Task<List<TResource>> GetAclResources( Guid applicationId, List<Guid> roleIds, CancellationToken cancellationToken ) {
        var grantQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where ( resource.Type == ResourceType.Operation || resource.Type == ResourceType.Column ) &&
                      resource.ApplicationId == applicationId &&
                      resource.Enabled &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny == false
                select new { resource.Uri, resource.ParentId, resource.Type }
            ;
        var denyQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where ( resource.Type == ResourceType.Operation || resource.Type == ResourceType.Column ) &&
                      resource.ApplicationId == applicationId &&
                      resource.Enabled &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny
                select new { resource.Uri, resource.ParentId, resource.Type }
            ;
        var result = await grantQuery.Except( denyQuery ).AsNoTracking().ToListAsync( cancellationToken );
        return result.Select( t => t.MapTo<TResource>() ).ToList();
    }

    /// <summary>
    /// 通过操作资源获取模块
    /// </summary>
    protected virtual async Task<List<TModuleDto>> GetModulesByOperations( List<TResource> resources ) {
        var moduleIds = resources.Where( t => t.Type == ResourceType.Operation ).Select( t => t.ParentId.SafeValue() ).Distinct().ToList();
        var modules = await ModuleRepository.FindByIdsNoTrackingAsync( moduleIds );
        await AddMissingParents( modules );
        return modules.Where( t => t.Enabled && t.IsHide.SafeValue() == false ).DistinctBy( t => t.Id ).Select( t => t.MapTo<TModuleDto>() ).ToList();
    }

    /// <summary>
    /// 添加缺失的父模块列表
    /// </summary>
    protected virtual async Task AddMissingParents( List<TModule> modules ) {
        var parentIds = modules.GetMissingParentIds();
        var parents = await ModuleRepository.FindByIdsNoTrackingAsync( parentIds.Select( t => t.ToGuid() ) );
        modules.AddRange( parents );
    }

    #endregion

    #region GetAclAsync

    /// <inheritdoc />
    public virtual async Task<List<string>> GetAclAsync( Guid userId, CancellationToken cancellationToken = default ) {
        if ( userId.IsEmpty() )
            return new List<string>();
        var roleIds = await RoleRepository.GetRoleIdsByUserIdAsync( userId );
        var result = await GetAcl( roleIds, cancellationToken );
        var operationApis = await GetOperationApis( roleIds, cancellationToken );
        result.AddRange( operationApis );
        return result.Where( uri => uri.IsEmpty() == false ).Distinct().ToList();
    }

    /// <summary>
    /// 获取访问控制列表
    /// </summary>
    protected virtual async Task<List<string>> GetAcl( List<Guid> roleIds, CancellationToken cancellationToken ) {
        var grantQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where resource.Enabled &&
                      resource.Uri != null &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny == false
                select resource.Uri
            ;
        var denyQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where resource.Enabled &&
                      resource.Uri != null &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny
                select resource.Uri
            ;
        return await grantQuery.Except( denyQuery ).ToListAsync( cancellationToken );
    }

    /// <summary>
    /// 获取操作绑定的Api资源标识列表
    /// </summary>
    protected virtual async Task<List<string>> GetOperationApis( List<Guid> roleIds, CancellationToken cancellationToken ) {
        var OperationIdsQuery = GetOperationIdsQueryable( roleIds );
        var queryable = from apiResource in ResourceRepository.Find()
                        join operationApi in OperationApiRepository.Find() on apiResource.Id equals operationApi.ApiId
                        join operationId in OperationIdsQuery on operationApi.OperationId equals operationId
                        where apiResource.Enabled &&
                              apiResource.Uri != null
                        select apiResource.Uri;
        return await queryable.ToListAsync( cancellationToken );
    }

    /// <summary>
    /// 获取授予访问的操作资源标识查询对象
    /// </summary>
    protected virtual IQueryable<Guid> GetOperationIdsQueryable( List<Guid> roleIds ) {
        var grantQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where resource.Enabled &&
                      resource.Uri != null &&
                      resource.Type == ResourceType.Operation &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny == false
                select resource.Id
            ;
        var denyQuery =
                from resource in ResourceRepository.Find()
                join permission in PermissionRepository.Find() on resource.Id equals permission.ResourceId
                where resource.Enabled &&
                      resource.Uri != null &&
                      resource.Type == ResourceType.Operation &&
                      roleIds.Contains( permission.RoleId ) &&
                      permission.IsDeny
                select resource.Id
            ;
        return grantQuery.Except( denyQuery );
    }

    #endregion
}