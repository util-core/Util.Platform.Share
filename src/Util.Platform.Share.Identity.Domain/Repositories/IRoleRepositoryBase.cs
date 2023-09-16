namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 角色仓储
/// </summary>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TRoleId">角色标识类型</typeparam>
/// <typeparam name="TRoleParentId">角色父标识类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
public interface IRoleRepositoryBase<TRole, TRoleId, in TRoleParentId, in TApplicationId, in TUserId> : ITreeRepository<TRole, TRoleId, TRoleParentId> 
    where TRole : class, ITreeEntity<TRole, TRoleId, TRoleParentId> {
    /// <summary>
    /// 通过角色编码判断是否存在
    /// </summary>
    /// <param name="entity">角色</param>
    Task<bool> ExistsByCodeAsync( TRole entity );
    /// <summary>
    /// 查找并加载用户
    /// </summary>
    /// <param name="roleId">角色标识</param>
    Task<TRole> FindByIncludeUsersAsync( TRoleId roleId );
    /// <summary>
    /// 获取用户关联的角色列表
    /// </summary>
    /// <param name="userId">用户标识</param>
    Task<List<TRoleId>> GetRoleIdsByUserIdAsync( TUserId userId );
}