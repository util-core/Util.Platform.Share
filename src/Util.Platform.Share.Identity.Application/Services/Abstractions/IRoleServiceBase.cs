namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 角色服务
/// </summary>
public interface IRoleServiceBase<TRoleDto, in TCreateRoleRequest, in TUpdateRoleRequest, in TRoleQuery, in TRoleUsersRequest> 
    : ICrudService<TRoleDto, TCreateRoleRequest, TUpdateRoleRequest, TRoleQuery> 
    where TRoleDto : IDto, new() 
    where TCreateRoleRequest : IRequest, new()
    where TUpdateRoleRequest : IDto, new() 
    where TRoleQuery : IPage
    where TRoleUsersRequest : class {
    /// <summary>
    /// 添加用户到角色
    /// </summary>
    /// <param name="request">角色用户参数</param>
    Task AddUsersToRoleAsync( TRoleUsersRequest request );
    /// <summary>
    /// 从角色移除用户
    /// </summary>
    /// <param name="request">角色用户参数</param>
    Task RemoveUsersFromRoleAsync( TRoleUsersRequest request );
}