using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 声明仓储
/// </summary>
public interface IClaimRepositoryBase<TClaim> : IClaimRepositoryBase<TClaim, Guid>
    where TClaim : ClaimBase<TClaim> {
}

/// <summary>
/// 声明仓储
/// </summary>
public interface IClaimRepositoryBase<TClaim, in TClaimId> : IRepository<TClaim, TClaimId> 
    where TClaim : class, IAggregateRoot<TClaimId> {
}