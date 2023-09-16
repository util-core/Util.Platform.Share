using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Services.Abstractions;

/// <summary>
/// 登录服务
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TUserId">用户标识类型</typeparam>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TAuditUserId">审计用户标识类型</typeparam>
public interface ISignInManagerBase<in TUser, TUserId, TRole, TAuditUserId> : IDomainService 
    where TUser : UserBase<TUser, TUserId, TRole, TAuditUserId>
    where TRole : class {
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="password">密码</param>
    /// <param name="isPersistent">cookie是否持久保留,设置为false,当关闭浏览器则cookie失效</param>
    /// <param name="lockoutOnFailure">达到登录失败次数是否锁定</param>
    Task<Results.SignInResult> SignInAsync( TUser user, string password, bool isPersistent = false, bool lockoutOnFailure = true );
    /// <summary>
    /// 退出登录
    /// </summary>
    Task SignOutAsync();
}