using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 操作资源服务
/// </summary>
public abstract class OperationServiceBase<TUnitOfWork, TResource, TApplication, TOperation, TOperationDto, TResourceQuery, TCreateOperationRequest>
    : QueryServiceBase<TResource, TOperationDto, TResourceQuery>, IOperationServiceBase<TOperationDto, TResourceQuery, TCreateOperationRequest>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication> 
    where TOperation : OperationBase<TOperation>
    where TOperationDto : OperationDtoBase,new()
    where TResourceQuery : ResourceQueryBase
    where TCreateOperationRequest : CreateOperationRequestBase {

    #region 构造方法

    /// <summary>
    /// 初始化操作资源服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="operationRepository">操作资源仓储</param>
    protected OperationServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IResourceRepositoryBase<TResource> resourceRepository,
        IOperationRepositoryBase<TOperation> operationRepository ) : base( serviceProvider, resourceRepository ) {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        OperationRepository = operationRepository ?? throw new ArgumentNullException( nameof( operationRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 工作单元
    /// </summary>
    protected TUnitOfWork UnitOfWork { get; }
    /// <summary>
    /// 资源仓储
    /// </summary>
    protected IResourceRepositoryBase<TResource> ResourceRepository { get; }
    /// <summary>
    /// 操作资源仓储
    /// </summary>
    protected IOperationRepositoryBase<TOperation> OperationRepository { get; }

    #endregion

    #region Filter

    /// <summary>
    /// 过滤查询
    /// </summary>
    protected override IQueryable<TResource> Filter( IQueryable<TResource> queryable, TResourceQuery param ) {
        var moduleId = param.ParentId.ToGuid();
        return queryable.Where( t => t.Type == ResourceType.Operation )
            .Where( t => t.ApplicationId == param.ApplicationId )
            .Where( t => t.ParentId == moduleId )
            .WhereIfNotEmpty( t => t.Name.Contains( param.Name ) )
            .WhereIfNotEmpty( t => t.Uri.Contains( param.Uri ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled );
    }

    #endregion

    #region GetByIdAsync

    /// <inheritdoc />
    public override async Task<TOperationDto> GetByIdAsync( object id ) {
        var resourceId = Util.Helpers.Convert.ToGuid( id );
        var resource = await ResourceRepository.Find( t => t.Id == resourceId )
            .Include( t => t.Application )
            .Include( t => t.Parent )
            .FirstOrDefaultAsync();
        return resource.MapTo<TOperationDto>();
    }

    #endregion

    #region CreateAsync

    /// <inheritdoc />
    public virtual async Task<string> CreateAsync( TCreateOperationRequest request ) {
        var entity = request.MapTo<TOperation>();
        entity.CheckNull( nameof( entity ) );
        entity.Init();
        await Validate( entity );
        await OperationRepository.AddAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteCreationLog( entity );
        return entity.Id.ToString();
    }

    /// <summary>
    /// 操作资源验证
    /// </summary>
    protected virtual async Task Validate( TOperation entity ) {
        if ( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.ApplicationId == entity.ApplicationId && t.Uri == entity.Uri ) )
            throw new Warning( L["DuplicateOperationUri", entity.Uri] );
        if ( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.ApplicationId == entity.ApplicationId && t.ParentId == entity.ModuleId && t.Name == entity.Name ) )
            throw new Warning( L["DuplicateOperationName", entity.Name] );
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreationLog( TOperation entity ) {
        Log.Append( "操作资源{OperationName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion

    #region UpdateAsync

    /// <inheritdoc />
    public virtual async Task UpdateAsync( TOperationDto request ) {
        if ( request.Id.IsEmpty() )
            throw new InvalidOperationException( R.IdIsEmpty );
        var oldEntity = await OperationRepository.FindByIdAsync( request.Id );
        oldEntity.CheckNull( nameof( oldEntity ) );
        var entity = request.MapTo( oldEntity.Clone() );
        entity.CheckNull( nameof( entity ) );
        await Validate( entity );
        var changes = oldEntity.GetChanges( entity );
        await OperationRepository.UpdateAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteUpdateLog( entity, changes );
    }

    /// <summary>
    /// 写更新日志
    /// </summary>
    protected virtual void WriteUpdateLog( TOperation entity, ChangeValueCollection changeValues ) {
        Log.Append( "操作资源{OperationName}修改成功,", entity.Name )
            .Append( "业务标识: {Id},", entity.Id )
            .Append( "修改内容: {changeValues}", changeValues )
            .LogInformation();
    }

    #endregion

    #region DeleteAsync

    /// <inheritdoc />
    public virtual async Task DeleteAsync( string ids ) {
        if ( ids.IsEmpty() )
            return;
        var entities = await ResourceRepository.FindByIdsAsync( ids );
        if ( entities.IsEmpty() )
            return;
        foreach ( var entity in entities ) {
            await ResourceRepository.RemoveAsync( entity );
        }
        await UnitOfWork.CommitAsync();
        Log.Append( "操作资源{OperationName}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
    }

    #endregion
}