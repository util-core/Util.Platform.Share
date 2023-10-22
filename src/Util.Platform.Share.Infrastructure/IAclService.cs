namespace Util.Platform.Share;

/// <summary>
/// 访问控制列表服务
/// </summary>
public interface IAclService : IScopeDependency {
    /// <summary>
    /// 设置用户访问控制列表
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="applicationId">应用程序标识</param>
    Task SetAclAsync( string userId, string applicationId );
}