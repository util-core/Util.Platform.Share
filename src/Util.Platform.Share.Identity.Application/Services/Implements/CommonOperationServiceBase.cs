using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 常用操作资源服务
/// </summary>
public abstract class CommonOperationServiceBase<TUnitOfWork, TCommonOperation, TCommonOperationDto, TCommonOperationQuery> 
    : CrudServiceBase<TCommonOperation, TCommonOperationDto, TCommonOperationQuery,Guid>, ICommonOperationServiceBase<TCommonOperationDto, TCommonOperationQuery>
    where TUnitOfWork : IUnitOfWork
    where TCommonOperation : CommonOperationBase<TCommonOperation>,new() 
    where TCommonOperationDto : CommonOperationDtoBase,new() 
    where TCommonOperationQuery : CommonOperationQueryBase {
    /// <summary>
    /// 初始化常用操作资源服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="repository">仓储</param>
    protected CommonOperationServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, ICommonOperationRepositoryBase<TCommonOperation> repository ) 
        : base( serviceProvider, unitOfWork, repository ) {
        Repository = repository ?? throw new ArgumentNullException( nameof( repository ) );
    }

    /// <summary>
    /// 常用操作仓储
    /// </summary>
    protected ICommonOperationRepositoryBase<TCommonOperation> Repository { get; set; }

    /// <inheritdoc />
    protected override IQueryable<TCommonOperation> Filter( IQueryable<TCommonOperation> queryable, TCommonOperationQuery param ) {
        return queryable
            .WhereIfNotEmpty( t => t.Name.Contains( param.Name ) )
            .WhereIfNotEmpty( t => t.Remark.Contains( param.Remark ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled );
    }

    /// <inheritdoc />
    public virtual async Task<List<TCommonOperationDto>> GetEnabledNamesAsync() {
        var result = await Repository.FindAllAsync( t => t.Enabled );
        return result.Select( t => t.MapTo<TCommonOperationDto>() ).OrderBy( t => t.SortId ).ToList();
    }
}