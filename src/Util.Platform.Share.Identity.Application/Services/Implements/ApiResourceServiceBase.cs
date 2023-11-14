using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// Api资源服务
/// </summary>
public abstract class ApiResourceServiceBase<TUnitOfWork, TResource, TApplication, TApiResource, TApiResourceDto, TCreateApiResourceRequest, TResourceQuery>
    : TreeServiceBase<TResource, TApiResourceDto, TCreateApiResourceRequest, TApiResourceDto, TResourceQuery>, IApiResourceServiceBase<TApiResourceDto, TCreateApiResourceRequest, TResourceQuery>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TApplication>, new()
    where TApplication : ApplicationBase<TApplication>
    where TApiResource : ApiResourceBase<TApiResource>
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto>, new()
    where TCreateApiResourceRequest : CreateApiResourceRequestBase, new()
    where TResourceQuery : ResourceQueryBase {

    #region 构造方法

    /// <summary>
    /// 初始化Api资源服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="apiResourceRepository">Api资源仓储</param>
    protected ApiResourceServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IResourceRepositoryBase<TResource> resourceRepository,
        IApiResourceRepositoryBase<TApiResource> apiResourceRepository ) : base( serviceProvider, unitOfWork, resourceRepository ) {
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        ApiResourceRepository = apiResourceRepository ?? throw new ArgumentNullException( nameof( apiResourceRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 资源仓储
    /// </summary>
    protected IResourceRepositoryBase<TResource> ResourceRepository { get; }
    /// <summary>
    /// Api资源仓储
    /// </summary>
    protected IApiResourceRepositoryBase<TApiResource> ApiResourceRepository { get; }

    #endregion

    #region Filter

    /// <summary>
    /// 过滤查询
    /// </summary>
    protected override IQueryable<TResource> Filter( IQueryable<TResource> queryable, TResourceQuery param ) {
        return queryable.Where( t => t.Type == ResourceType.Api )
            .Where( t => t.ApplicationId == param.ApplicationId )
            .WhereIfNotEmpty( t => t.Name.Contains( param.Name ) )
            .WhereIfNotEmpty( t => t.Uri.Contains( param.Uri ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled )
            .Include( t => t.Application )
            .Include( t => t.Parent );
    }

    #endregion

    #region GetByIdAsync

    /// <inheritdoc />
    public override async Task<TApiResourceDto> GetByIdAsync( object id ) {
        var resourceId = Util.Helpers.Convert.ToGuid( id );
        var resource = await ResourceRepository.Find( t => t.Id == resourceId )
            .Include( t => t.Application )
            .Include( t => t.Parent )
            .FirstOrDefaultAsync();
        return resource.MapTo<TApiResourceDto>();
    }

    #endregion

    #region GetResourcesAsync(获取资源列表)

    /// <inheritdoc />
    public virtual async Task<List<TApiResourceDto>> GetResourcesAsync( List<string> uri ) {
        if( uri == null || uri.Count == 0 )
            return new List<TApiResourceDto>();
        var resources = await ResourceRepository.FindAllAsync( t => uri.Contains( t.Uri ) && t.Type == ResourceType.Api && t.Enabled );
        if( resources == null )
            return new List<TApiResourceDto>();
        return resources.Select( t => t.MapTo<TApiResourceDto>() ).ToList();
    }

    #endregion

    #region GetEnabledResourcesAsync

    /// <inheritdoc />
    public virtual async Task<List<TApiResourceDto>> GetEnabledResourcesAsync() {
        var resources = await ApiResourceRepository.GetEnabledResources();
        return resources.Where( t => t.Uri.IsEmpty() == false ).Select( t => t.MapTo<TApiResourceDto>() ).ToList();
    }

    #endregion

    #region CreateAsync

    /// <summary>
    /// 创建Api资源
    /// </summary>
    /// <param name="request">创建Api资源参数</param>
    public override async Task<string> CreateAsync( TCreateApiResourceRequest request ) {
        var entity = request.MapTo<TApiResource>();
        entity.CheckNull( nameof( entity ) );
        entity.Init();
        var parent = await ApiResourceRepository.FindByIdAsync( entity.ParentId );
        entity.InitPath( parent );
        entity.SortId = await ApiResourceRepository.GenerateSortIdAsync( entity.ApplicationId.SafeValue(), entity.ParentId );
        await Validate( entity );
        await ApiResourceRepository.AddAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteCreationLog( entity );
        return entity.Id.ToString();
    }

    /// <summary>
    /// Api资源验证
    /// </summary>
    protected virtual async Task Validate( TApiResource entity ) {
        if( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.Uri == entity.Uri && t.Uri != null ) )
            throw new Warning( L["DuplicateApiResourceUri", entity.Uri] ) { IsLocalization = false };
        if( await ResourceRepository.ExistsAsync( t => t.Id != entity.Id && t.ApplicationId == entity.ApplicationId && t.ParentId == entity.ParentId && t.Name == entity.Name ) )
            throw new Warning( L["DuplicateApiResourceName", entity.Name] ) { IsLocalization = false };
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreationLog( TApiResource entity ) {
        Log.Append( "Api资源{ApiResourceName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// 更新Api资源
    /// </summary>
    /// <param name="request">Api资源参数</param>
    public override async Task UpdateAsync( TApiResourceDto request ) {
        if( request.Id.IsEmpty() )
            throw new InvalidOperationException( R.IdIsEmpty );
        var oldEntity = await ApiResourceRepository.FindByIdAsync( request.Id );
        oldEntity.CheckNull( nameof( oldEntity ) );
        var entity = request.MapTo( oldEntity.Clone() );
        entity.CheckNull( nameof( entity ) );
        await Validate( entity );
        entity.InitUri();
        entity.InitPinYin();
        var changes = oldEntity.GetChanges( entity );
        await ApiResourceRepository.UpdateAsync( entity );
        await CommitAsync();
        WriteUpdateLog( entity, changes );
    }

    /// <summary>
    /// 写更新日志
    /// </summary>
    protected virtual void WriteUpdateLog( TApiResource entity, ChangeValueCollection changeValues ) {
        Log.Append( "Api资源{ApiResourceName}修改成功,", entity.Name )
            .Append( "业务标识: {Id},", entity.Id )
            .Append( "修改内容: {changeValues}", changeValues )
            .LogInformation();
    }

    #endregion

    #region DeleteCommitAfterAsync

    /// <inheritdoc />
    protected override Task DeleteCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "Api资源{ApiResourceNames}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region EnableCommitAfterAsync

    /// <inheritdoc />
    protected override Task EnableCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "Api资源{ApiResourceNames}启用成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region DisableCommitAfterAsync

    /// <inheritdoc />
    protected override Task DisableCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "Api资源{ApiResourceNames}禁用成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion
}