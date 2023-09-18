namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 角色用户参数
/// </summary>
public abstract class RoleUsersRequestBase : RoleUsersRequestBase<Guid?> {
}

/// <summary>
/// 角色用户参数
/// </summary>
public abstract class RoleUsersRequestBase<TRoleId> : RequestBase {
    /// <summary>
    /// 角色标识
    /// </summary>
    public TRoleId RoleId { get; set; }

    /// <summary>
    /// 用户标识列表
    /// </summary>
    public string UserIds { get; set; }
}