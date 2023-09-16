namespace Util.Platform.Share.Identity.Data.Repositories; 

/// <summary>
/// 常用操作资源仓储
/// </summary>
public abstract class CommonOperationRepositoryBase<TUnitOfWork, TCommonOperation, TCommonOperationId, TAuditUserId> 
    : RepositoryBase<TCommonOperation, TCommonOperationId>, ICommonOperationRepositoryBase<TCommonOperation, TCommonOperationId>
    where TUnitOfWork : IUnitOfWork
    where TCommonOperation : CommonOperationBase<TCommonOperation, TCommonOperationId, TAuditUserId> {
    /// <summary>
    /// 初始化常用操作资源仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    protected CommonOperationRepositoryBase( TUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}