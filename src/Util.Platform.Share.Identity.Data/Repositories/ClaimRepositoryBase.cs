namespace Util.Platform.Share.Identity.Data.Repositories;

/// <summary>
/// 声明仓储
/// </summary>
public abstract class ClaimRepositoryBase<TUnitOfWork, TClaim> : ClaimRepositoryBase<TUnitOfWork, TClaim, Guid, Guid?>, IClaimRepositoryBase<TClaim>
    where TUnitOfWork : IUnitOfWork
    where TClaim : ClaimBase<TClaim> {
    /// <summary>
    /// 初始化声明仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected ClaimRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}

/// <summary>
/// 声明仓储
/// </summary>
public abstract class ClaimRepositoryBase<TUnitOfWork,TClaim, TClaimId, TAuditUserId> : RepositoryBase<TClaim, TClaimId>, IClaimRepositoryBase<TClaim, TClaimId>
    where TUnitOfWork: IUnitOfWork
    where TClaim : ClaimBase<TClaim, TClaimId, TAuditUserId> {
    /// <summary>
    /// 初始化声明仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected ClaimRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}