namespace Util.Platform.Share.Identity.Domain.Repositories; 

/// <summary>
/// 声明仓储
/// </summary>
public interface IClaimRepositoryBase<TClaim, in TClaimId> : IRepository<TClaim, TClaimId> where TClaim : class, IAggregateRoot<TClaimId> {
}