namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 应用程序仓储
/// </summary>
/// <typeparam name="TApplication">应用程序类型</typeparam>
/// <typeparam name="TApplicationId">应用程序标识类型</typeparam>
public interface IApplicationRepositoryBase<TApplication, in TApplicationId> : IRepository<TApplication, TApplicationId> where TApplication : class, IAggregateRoot<TApplicationId> {
    /// <summary>
    /// 通过应用程序编码查找
    /// </summary>
    /// <param name="code">应用程序编码</param>
    Task<TApplication> GetByCodeAsync( string code );
    /// <summary>
    /// 是否允许跨域访问
    /// </summary>
    /// <param name="origin">域名</param>
    Task<bool> IsOriginAllowedAsync( string origin );
}