namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 应用程序仓储
/// </summary>
public abstract class ApplicationRepositoryBase<TUnitOfWork, TApplication> 
    : ApplicationRepositoryBase<TUnitOfWork, TApplication, Guid, Guid?>, IApplicationRepositoryBase<TApplication>
    where TUnitOfWork : IUnitOfWork
    where TApplication : ApplicationBase<TApplication> {
    /// <summary>
    /// 初始化应用程序仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected ApplicationRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}

/// <summary>
/// 应用程序仓储
/// </summary>
public abstract class ApplicationRepositoryBase<TUnitOfWork, TApplication, TApplicationId, TAuditUserId>
    : RepositoryBase<TApplication, TApplicationId>, IApplicationRepositoryBase<TApplication, TApplicationId>
    where TUnitOfWork : IUnitOfWork
    where TApplication : ApplicationBase<TApplication, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化应用程序仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected ApplicationRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }

    /// <inheritdoc />
    public async Task<TApplication> GetByCodeAsync( string code ) {
        return await SingleAsync( t => t.Code == code );
    }

    /// <inheritdoc />
    public async Task<bool> IsOriginAllowedAsync( string origin ) {
        return await Find().AnyAsync( t => t.AllowedCorsOrigins.Contains( origin ) );
    }
}