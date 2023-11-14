using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 应用程序服务
/// </summary>
public abstract class ApplicationServiceBase<TUnitOfWork, TApplication, TIdentityResource, TApiResource, TApplicationDto, TApplicationQuery>
    : CrudServiceBase<TApplication, TApplicationDto, TApplicationQuery, Guid>, IApplicationServiceBase<TApplicationDto, TApplicationQuery>
    where TUnitOfWork : IUnitOfWork
    where TApplication : ApplicationBase<TApplication>, new()
    where TIdentityResource : IdentityResourceBase<TIdentityResource>
    where TApiResource : ApiResourceBase<TApiResource>
    where TApplicationDto : ApplicationDtoBase, new()
    where TApplicationQuery : ApplicationQueryBase {

    #region 构造方法

    /// <summary>
    /// 初始化应用程序服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="applicationRepository">应用程序仓储</param>
    /// <param name="identityResourceRepository">身份资源仓储</param>
    /// <param name="apiResourceRepository">Api资源仓储</param>
    protected ApplicationServiceBase( IServiceProvider serviceProvider, ICache cache, TUnitOfWork unitOfWork,
        IApplicationRepositoryBase<TApplication> applicationRepository, IIdentityResourceRepositoryBase<TIdentityResource> identityResourceRepository,
        IApiResourceRepositoryBase<TApiResource> apiResourceRepository ) : base( serviceProvider, unitOfWork, applicationRepository ) {
        CacheService = cache ?? throw new ArgumentNullException( nameof( cache ) );
        ApplicationRepository = applicationRepository ?? throw new ArgumentNullException( nameof( applicationRepository ) );
        IdentityResourceRepository = identityResourceRepository ?? throw new ArgumentNullException( nameof( identityResourceRepository ) );
        ApiResourceRepository = apiResourceRepository ?? throw new ArgumentNullException( nameof( apiResourceRepository ) );
    }

    #endregion

    #region 属性

    /// <summary>
    /// 缓存服务
    /// </summary>
    protected ICache CacheService { get; }
    /// <summary>
    /// 应用程序仓储
    /// </summary>
    protected IApplicationRepositoryBase<TApplication> ApplicationRepository { get; }
    /// <summary>
    /// 身份资源仓储
    /// </summary>
    public IIdentityResourceRepositoryBase<TIdentityResource> IdentityResourceRepository { get; set; }
    /// <summary>
    /// Api资源仓储
    /// </summary>
    public IApiResourceRepositoryBase<TApiResource> ApiResourceRepository { get; set; }

    #endregion

    #region Filter

    /// <inheritdoc />
    protected override IQueryable<TApplication> Filter( IQueryable<TApplication> queryable, TApplicationQuery query ) {
        return queryable
            .WhereIfNotEmpty( t => t.Code.Contains( query.Code ) )
            .WhereIfNotEmpty( t => t.Name.Contains( query.Name ) )
            .WhereIfNotEmpty( t => t.Remark.Contains( query.Remark ) )
            .Between( t => t.CreationTime, query.BeginCreationTime, query.EndCreationTime );
    }

    #endregion

    #region GetEnabledApplicationsAsync

    /// <inheritdoc />
    public virtual async Task<List<TApplicationDto>> GetEnabledApplicationsAsync() {
        var entities = await ApplicationRepository.NoTracking().FindAllAsync( t => t.Enabled );
        return entities.Select( ToDto ).ToList();
    }

    #endregion

    #region GetByCodeAsync

    /// <inheritdoc />
    public virtual async Task<TApplicationDto> GetByCodeAsync( string code ) {
        var application = await ApplicationRepository.SingleAsync( t => t.Code == code );
        return ToDto( application );
    }

    #endregion

    #region IsOriginAllowedAsync

    /// <inheritdoc />
    public virtual async Task<bool> IsOriginAllowedAsync( string origin ) {
        return await ApplicationRepository.IsOriginAllowedAsync( origin );
    }

    #endregion

    #region GetScopesAsync

    /// <inheritdoc />
    public virtual async Task<List<Item>> GetScopesAsync() {
        var result = new List<Item>();
        var identityResources = await IdentityResourceRepository.GetEnabledResources();
        if( identityResources != null )
            result.AddRange( identityResources.Select( t => new Item( t.Name, t.Uri ) ) );
        var apiResources = await ApiResourceRepository.GetEnabledResources();
        if( apiResources != null )
            result.AddRange( apiResources.Select( t => new Item( t.Name, t.Uri ) ) );
        return result;
    }

    #endregion

    #region CreateAsync

    /// <inheritdoc />
    protected override async Task CreateBeforeAsync( TApplication entity ) {
        await Validate( entity );
    }

    /// <summary>
    /// 验证
    /// </summary>
    protected virtual async Task Validate( TApplication entity ) {
        if( await ApplicationRepository.ExistsAsync( t => t.Id != entity.Id && t.Code == entity.Code ) )
            throw new Warning( L["DuplicateApplicationCode", entity.Code] ) { IsLocalization = false };
        if( await ApplicationRepository.ExistsAsync( t => t.Id != entity.Id && t.Name == entity.Name ) )
            throw new Warning( L["DuplicateApplicationName", entity.Name] ) { IsLocalization = false };
    }

    /// <inheritdoc />
    protected override Task CreateCommitAfterAsync( TApplication entity ) {
        Log.Append( "应用程序{ApplicationName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region UpdateAsync

    /// <inheritdoc />
    protected override async Task UpdateBeforeAsync( TApplication entity ) {
        await Validate( entity );
    }

    /// <inheritdoc />
    protected override Task UpdateCommitAfterAsync( TApplication entity, ChangeValueCollection changeValues ) {
        Log.Append( "应用程序{ApplicationName}修改成功,", entity.Name )
            .Append( "业务标识: {Id},", entity.Id )
            .Append( "修改内容: {changeValues}", changeValues )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion

    #region DeleteAsync

    /// <inheritdoc />
    protected override Task DeleteCommitAfterAsync( List<TApplication> entities ) {
        Log.Append( "应用程序{ApplicationNames}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
        return Task.CompletedTask;
    }

    #endregion
}