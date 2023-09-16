namespace Util.Platform.Share.Identity.Domain.Models; 

/// <summary>
/// 用户角色
/// </summary>
public abstract class UserRoleBase<TUserId, TRoleId> {
    /// <summary>
    /// 初始化用户角色
    /// </summary>
    protected UserRoleBase() {
    }

    /// <summary>
    /// 初始化用户角色
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="roleId">角色标识</param>
    protected UserRoleBase( TUserId userId, TRoleId roleId ) {
        UserId = userId;
        RoleId = roleId;
    }

    /// <summary>
    /// 用户标识
    /// </summary>
    public TUserId UserId { get; set; }

    /// <summary>
    /// 角色标识
    /// </summary>
    public TRoleId RoleId { get; set; }
}