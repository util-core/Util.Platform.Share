using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 操作资源服务
/// </summary>
public abstract class OperationServiceBase<TUnitOfWork, TResource, TApplication, TOperation, TOperationApi, TOperationDto, TResourceQuery, TCreateOperationRequest, TApiResourceDto>
    : QueryServiceBase<TResource, TOperationDto, TResourceQuery>, IOperationServiceBase<TOperationDto, TResourceQuery, TCreateOperationRequest, TApiResourceDto>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication> 
    where TOperation : OperationBase<TOperation>
    where TOperationApi : OperationApiBase<TOperationApi>,new()
    where TOperationDto : OperationDtoBase,new()
    where TResourceQuery : ResourceQueryBase
    where TCreateOperationRequest : CreateOperationRequestBase 
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto> {

    #region 构造方法

    /// <summary>
    /// 初始化操作资源服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="operationRepository">操作资源仓储</param>
    /// <param name="operationApiRepository">操作Api仓储</param>
    /// <param name="localizer">本地化查找器</param>
    protected OperationServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IResourceRepositoryBase<TResource> resourceRepository,
        IOperationRepositoryBase<TOperation> operationRepository, IOperationApiRepositoryBase<TOperationApi> operationApiRepository, IStringLocalizer localizer ) : base( serviceProvider, resourceRepository ) {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        OperationRepository = operationRepository ?? throw new ArgumentNullException( nameof( operationRepository ) );
        OperationApiRepository = operationApiRepository ?? throw new ArgumentNullException( nameof( operationApiRepository ) );
        Localizer = localizer ?? throw new ArgumentNullException( nameof( localizer ) );
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
    /// <summary>
    /// 操作Api仓储
    /// </summary>
    protected IOperationApiRepositoryBase<TOperationApi> OperationApiRepository { get; }
    /// <summary>
    /// 本地化查找器
    /// </summary>
    protected IStringLocalizer Localizer { get; }

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
        await SetApiResources( request.ApiApplicationId.SafeValue(), entity.Id, request.ApiRourceIds );
        await UnitOfWork.CommitAsync();
        WriteCreationLog( entity );
        return entity.Id.ToString();
    }

    /// <summary>
    /// 操作资源验证
    /// </summary>
    protected virtual async Task Validate( TOperation entity ) {
        if ( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.ApplicationId == entity.ApplicationId && t.Uri == entity.Uri ) )
            ThrowUriDuplicationException( entity );
        if ( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.ApplicationId == entity.ApplicationId && t.ParentId == entity.ModuleId && t.Name == entity.Name ) )
            ThrowNameDuplicationException( entity );
    }

    /// <summary>
    /// 抛出操作资源标识重复异常
    /// </summary>
    protected virtual void ThrowUriDuplicationException( TOperation entity ) {
        throw new Warning( string.Format( Localizer["DuplicateOperationUri"], entity.Uri ) );
    }

    /// <summary>
    /// 抛出操作资源名称重复异常
    /// </summary>
    protected virtual void ThrowNameDuplicationException( TOperation entity ) {
        throw new Warning( string.Format( Localizer["DuplicateOperationName"], entity.Name ) );
    }

    /// <summary>
    /// 设置绑定的Api资源
    /// </summary>
    protected virtual async Task SetApiResources( Guid apiApplicationId, Guid operationId, List<string> apiRourceIds ) {
        if ( apiRourceIds.IsEmpty() )
            return;
        if ( apiApplicationId.IsEmpty() )
            throw new ArgumentNullException( nameof( apiApplicationId ) );
        var originalApiResourceIds = await OperationApiRepository.GetApiIdsByOperationIdAsync( operationId, apiApplicationId );
        var result = apiRourceIds.ToGuidList().Compare( originalApiResourceIds );
        await OperationApiRepository.AddAsync( CreateOperationApis( operationId, result.CreateList ) );
        await OperationApiRepository.RemoveAsync( operationId, result.DeleteList );
    }

    /// <summary>
    /// 创建操作API列表
    /// </summary>
    protected virtual List<TOperationApi> CreateOperationApis( Guid operationId, List<Guid> apiResourceIds ) {
        return apiResourceIds.Select( resourceId => {
            var entity = new TOperationApi {
                OperationId = operationId,
                ApiId = resourceId
            };
            entity.Init();
            return entity;
        } ).ToList();
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
        await SetApiResources( request.ApiApplicationId.SafeValue(), entity.Id, request.ApiRourceIds );
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
            await OperationApiRepository.RemoveAsync( entity.Id );
        }
        await UnitOfWork.CommitAsync();
        Log.Append( "操作资源{OperationName}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
    }

    #endregion

    #region GetApiResourceIdsAsync

    /// <inheritdoc />
    public virtual async Task<List<Guid>> GetApiResourceIdsAsync( Guid operationId ) {
        return await OperationApiRepository.GetApiIdsByOperationIdAsync( operationId );
    }

    #endregion

    #region GetApiResourcesAsync

    /// <inheritdoc />
    public virtual async Task<List<TApiResourceDto>> GetApiResourcesAsync( Guid operationId ) {
        var apiIds = await GetApiResourceIdsAsync( operationId );
        var apis = await ResourceRepository.FindByIdsAsync( apiIds );
        return apis.Where( t => t.Uri.IsEmpty() == false ).Select( t => t.MapTo<TApiResourceDto>() ).ToList();
    }

    #endregion
}