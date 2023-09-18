using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 常用操作资源仓储
/// </summary>
public interface ICommonOperationRepositoryBase<TCommonOperation> : ICommonOperationRepositoryBase<TCommonOperation, Guid>
    where TCommonOperation : CommonOperationBase<TCommonOperation> {
}

/// <summary>
/// 常用操作资源仓储
/// </summary>
public interface ICommonOperationRepositoryBase<TCommonOperation, in TCommonOperationId> : IRepository<TCommonOperation, TCommonOperationId> 
    where TCommonOperation : class, IAggregateRoot<TCommonOperationId> {
}