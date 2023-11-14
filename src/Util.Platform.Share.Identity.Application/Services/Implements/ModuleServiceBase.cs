using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 模块服务
/// </summary>
public abstract class ModuleServiceBase<TUnitOfWork, TResource, TApplication, TModule, TModuleDto, TCreateModuleRequest, TResourceQuery> 
    : TreeServiceBase<TResource, TModuleDto, TCreateModuleRequest, TModuleDto, TResourceQuery>, IModuleServiceBase<TModuleDto, TCreateModuleRequest, TResourceQuery>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TApplication>,new()
    where TApplication : ApplicationBase<TApplication> 
    where TModule : ModuleBase<TModule> 
    where TModuleDto : ModuleDtoBase<TModuleDto>, new()
    where TCreateModuleRequest : CreateModuleRequestBase, new()
    where TResourceQuery : ResourceQueryBase {

    #region 构造方法

    /// <summary>
    /// 初始化模块服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="moduleRepository">模块仓储</param>
    protected ModuleServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IResourceRepositoryBase<TResource> resourceRepository,
        IModuleRepositoryBase<TModule> moduleRepository ) : base( serviceProvider, unitOfWork, resourceRepository ) {
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        ModuleRepository = moduleRepository ?? throw new ArgumentNullException( nameof( moduleRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 资源仓储
    /// </summary>
    protected IResourceRepositoryBase<TResource> ResourceRepository { get; }
    /// <summary>
    /// 模块仓储
    /// </summary>
    protected IModuleRepositoryBase<TModule> ModuleRepository { get; }

    #endregion

    #region Filter

    /// <summary>
    /// 过滤查询
    /// </summary>
    protected override IQueryable<TResource> Filter( IQueryable<TResource> queryable, TResourceQuery param ) {
        return queryable.Where( t => t.Type == ResourceType.Module )
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
    public override async Task<TModuleDto> GetByIdAsync( object id ) {
        var resourceId = Util.Helpers.Convert.ToGuid( id );
        var resource = await ResourceRepository.Find( t => t.Id == resourceId )
            .Include( t => t.Application )
            .Include( t => t.Parent )
            .FirstOrDefaultAsync();
        return resource.MapTo<TModuleDto>();
    }

    #endregion

    #region CreateAsync

    /// <summary>
    /// 创建模块
    /// </summary>
    /// <param name="request">创建模块参数</param>
    public override async Task<string> CreateAsync( TCreateModuleRequest request ) {
        var entity = request.MapTo<TModule>();
        entity.CheckNull( nameof( entity ) );
        entity.Init();
        var parent = await ModuleRepository.FindByIdAsync( entity.ParentId );
        entity.InitPath( parent );
        entity.SortId = await ModuleRepository.GenerateSortIdAsync( entity.ApplicationId.SafeValue(), entity.ParentId );
        await Validate( entity );
        await ModuleRepository.AddAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteCreationLog( entity );
        return entity.Id.ToString();
    }

    /// <summary>
    /// 模块验证
    /// </summary>
    protected virtual async Task Validate( TModule entity ) {
        var result = await ModuleRepository.ExistsAsync( entity );
        if ( result )
            ThrowNameDuplicationException( entity );
    }

    /// <summary>
    /// 抛出模块名称重复异常
    /// </summary>
    protected virtual void ThrowNameDuplicationException( TModule entity ) {
        throw new Warning( L["DuplicateModuleName", entity.Name] ) { IsLocalization = false };
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreationLog( TModule entity ) {
        Log.Append( "模块{ModuleName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// 更新模块
    /// </summary>
    /// <param name="request">模块参数</param>
    public override async Task UpdateAsync( TModuleDto request ) {
        if ( request.Id.IsEmpty() )
            throw new InvalidOperationException( R.IdIsEmpty );
        var oldEntity = await ModuleRepository.FindByIdAsync( request.Id );
        oldEntity.CheckNull( nameof( oldEntity ) );
        var entity = request.MapTo( oldEntity.Clone() );
        entity.CheckNull( nameof( entity ) );
        await Validate( entity );
        entity.InitPinYin();
        var changes = oldEntity.GetChanges( entity );
        await ModuleRepository.UpdateAsync( entity );
        await CommitAsync();
        WriteUpdateLog( entity, changes );
    }

    /// <summary>
    /// 写更新日志
    /// </summary>
    protected virtual void WriteUpdateLog( TModule entity, ChangeValueCollection changeValues ) {
        Log.Append( "模块{ModuleName}修改成功,", entity.Name )
            .Append( "业务标识: {Id},", entity.Id )
            .Append( "修改内容: {changeValues}", changeValues )
            .LogInformation();
    }

    #endregion

    #region DeleteCommitAfterAsync

    /// <inheritdoc />
    protected override Task DeleteCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "模块{ModuleNames}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region EnableCommitAfterAsync

    /// <inheritdoc />
    protected override Task EnableCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "模块{ModuleNames}启用成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region DisableCommitAfterAsync

    /// <inheritdoc />
    protected override Task DisableCommitAfterAsync( List<TResource> entities ) {
        Log.Append( "模块{ModuleNames}禁用成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion
}