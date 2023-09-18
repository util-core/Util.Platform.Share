using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 声明服务
/// </summary>
public abstract class ClaimServiceBase<TUnitOfWork, TClaim, TClaimDto, TClaimQuery> 
    : CrudServiceBase<TClaim, TClaimDto, TClaimQuery,Guid>, IClaimServiceBase<TClaimDto, TClaimQuery>
    where TUnitOfWork : IUnitOfWork 
    where TClaim : ClaimBase<TClaim>,new()
    where TClaimDto : ClaimDtoBase,new() 
    where TClaimQuery : ClaimQueryBase {
    /// <summary>
    /// 初始化声明服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="repository">声明仓储</param>
    /// <param name="localizer">本地化查找器</param>
    protected ClaimServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IClaimRepositoryBase<TClaim> repository,
        IStringLocalizer localizer ) : base( serviceProvider, unitOfWork, repository ) {
        Repository = repository ?? throw new ArgumentNullException( nameof( repository ) );
        Localizer = localizer ?? throw new ArgumentNullException( nameof( localizer ) );
    }

    /// <summary>
    /// 声明仓储
    /// </summary>
    protected IClaimRepositoryBase<TClaim> Repository { get; }
    /// <summary>
    /// 本地化查找器
    /// </summary>
    protected IStringLocalizer Localizer { get; }

    /// <summary>
    /// 获取已启用的声明列表
    /// </summary>
    public virtual async Task<List<TClaimDto>> GetEnabledClaimsAsync() {
        var entities = await Repository.FindAllAsync( t => t.Enabled );
        return entities.Select( ToDto ).ToList();
    }

    /// <inheritdoc />
    protected override IQueryable<TClaim> Filter( IQueryable<TClaim> queryable, TClaimQuery query ) {
        return queryable.WhereIfNotEmpty( t => t.Name.Contains( query.Name ) )
            .WhereIfNotEmpty( t => t.Remark.Contains( query.Remark ) )
            .WhereIfNotEmpty( t => t.Name.Contains( query.Keyword ) );
    }

    /// <inheritdoc />
    protected override async Task SaveBeforeAsync( List<TClaim> creationList, List<TClaim> updateList, List<TClaim> deleteList ) {
        var list = creationList.Concat( updateList ).ToList();
        var duplicateClaimName = list.GroupBy( t => t.Name ).Where( t => t.Count() > 1 ).Select( t => t.Key ).SingleOrDefault();
        if ( duplicateClaimName.IsEmpty() == false )
            ThrowNameDuplicationException( duplicateClaimName );
        foreach ( var entity in list ) {
            if ( await Repository.ExistsAsync( t => !updateList.Select( u => u.Id ).Contains( t.Id ) && t.Name == entity.Name ) )
                ThrowNameDuplicationException( entity.Name );
        }
    }

    /// <summary>
    /// 抛出声明名称重复异常
    /// </summary>
    protected virtual void ThrowNameDuplicationException( string name ) {
        throw new Warning( string.Format( Localizer["DuplicateClaimName"], name ) );
    }

    /// <inheritdoc />
    protected override Task SaveCommitAfterAsync( List<TClaim> creationList, List<TClaim> updateList, List<TClaim> deleteList, List<Tuple<TClaim, ChangeValueCollection>> changeValues ) {
        foreach ( var entity in creationList ) {
            Log.Append( "声明{ClaimName}创建成功,", entity.Name )
                .Append( "业务标识: {Id}", entity.Id )
                .LogInformation();
        }
        foreach ( var item in changeValues ) {
            Log.Append( "声明{ClaimName}修改成功,", item.Item1.Name )
                .Append( "业务标识: {Id},", item.Item1.Id )
                .Append( "修改内容: {changeValues}", item.Item2 )
                .LogInformation();
        }
        if ( deleteList.Count > 0 ) {
            Log.Append( "声明{ApplicationNames}删除成功,", deleteList.Select( t => t.Name ).Join() )
                .Append( "业务标识: {Id}", deleteList.Select( t => t.Id ).Join() )
                .LogInformation();
        }
        return Task.CompletedTask;
    }
}