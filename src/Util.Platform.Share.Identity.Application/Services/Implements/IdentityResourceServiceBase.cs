using Util.Platform.Share.Identity.Applications.Services.Abstractions;
using Util.Platform.Share.Identity.Domain.Models;
using Util.Platform.Share.Identity.Domain.Repositories;
using Util.Platform.Share.Identity.Queries;

namespace Util.Platform.Share.Identity.Applications.Services.Implements;

/// <summary>
/// 身份资源服务
/// </summary>
public abstract class IdentityResourceServiceBase<TUnitOfWork, TResource, TApplication, TIdentityResource, TIdentityResourceDto, TResourceQuery>
    : QueryServiceBase<TResource, TIdentityResourceDto, TResourceQuery>, IIdentityResourceServiceBase<TIdentityResourceDto, TResourceQuery>
    where TUnitOfWork : IUnitOfWork
    where TResource : ResourceBase<TResource, TApplication>
    where TApplication : ApplicationBase<TApplication>
    where TIdentityResource : IdentityResourceBase<TIdentityResource>
    where TIdentityResourceDto : IdentityResourceDtoBase, new()
    where TResourceQuery : ResourceQueryBase {

    #region 构造方法

    /// <summary>
    /// 初始化身份资源服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="resourceRepository">资源仓储</param>
    /// <param name="identityResourceRepository">身份资源仓储</param>
    protected IdentityResourceServiceBase( IServiceProvider serviceProvider, TUnitOfWork unitOfWork, IResourceRepositoryBase<TResource> resourceRepository,
        IIdentityResourceRepositoryBase<TIdentityResource> identityResourceRepository ) : base( serviceProvider, resourceRepository ) {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        ResourceRepository = resourceRepository ?? throw new ArgumentNullException( nameof( resourceRepository ) );
        IdentityResourceRepository = identityResourceRepository ?? throw new ArgumentNullException( nameof( identityResourceRepository ) );
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
    /// 身份资源仓储
    /// </summary>
    protected IIdentityResourceRepositoryBase<TIdentityResource> IdentityResourceRepository { get; set; }

    #endregion

    #region Filter

    /// <summary>
    /// 过滤查询
    /// </summary>
    protected override IQueryable<TResource> Filter( IQueryable<TResource> queryable, TResourceQuery param ) {
        return queryable.Where( t => t.Type == ResourceType.Identity )
            .WhereIfNotEmpty( t => t.Name.Contains( param.Name ) )
            .WhereIfNotEmpty( t => t.Uri.Contains( param.Uri ) )
            .WhereIfNotEmpty( t => t.Remark.Contains( param.Remark ) )
            .WhereIfNotEmpty( t => t.Enabled == param.Enabled )
            .Between( t => t.CreationTime, param.BeginCreationTime, param.EndCreationTime, false )
            .Between( t => t.LastModificationTime, param.BeginLastModificationTime, param.EndLastModificationTime, false );
    }

    #endregion

    #region GetResourcesAsync

    /// <inheritdoc />
    public virtual async Task<List<TIdentityResourceDto>> GetResourcesAsync( List<string> uri ) {
        if ( uri == null || uri.Count == 0 )
            return new List<TIdentityResourceDto>();
        var resources = await ResourceRepository.FindAllAsync( t => uri.Contains( t.Uri ) && t.Type == ResourceType.Identity && t.Enabled );
        if ( resources == null )
            return new List<TIdentityResourceDto>();
        return resources.Select( t => t.MapTo<TIdentityResourceDto>() ).ToList();
    }

    #endregion

    #region GetEnabledResourcesAsync

    /// <inheritdoc />
    public virtual async Task<List<TIdentityResourceDto>> GetEnabledResourcesAsync() {
        var resources = await ResourceRepository.FindAllAsync( t => t.Type == ResourceType.Identity && t.Enabled );
        return resources.Select( t => t.MapTo<TIdentityResourceDto>() ).ToList();
    }

    #endregion

    #region CreateAsync

    /// <inheritdoc />
    public virtual async Task<string> CreateAsync( TIdentityResourceDto request ) {
        request.CheckNull( nameof( request ) );
        request.Validate();
        var entity = request.MapTo<TIdentityResource>();
        entity.CheckNull( nameof( entity ) );
        entity.Init();
        await Validate( entity );
        await IdentityResourceRepository.AddAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteCreationLog( entity );
        return entity.Id.ToString();
    }

    /// <summary>
    /// 身份资源验证
    /// </summary>
    protected virtual async Task Validate( TIdentityResource entity ) {
        var result = await IdentityResourceRepository.ExistsAsync( entity );
        if ( result )
            throw new Warning( L["DuplicateIdentityResource", entity.Name] );
    }

    /// <summary>
    /// 写创建日志
    /// </summary>
    protected virtual void WriteCreationLog( TIdentityResource entity ) {
        Log.Append( "身份资源{IdentityResourceName}创建成功,", entity.Name )
            .Append( "业务标识: {Id}", entity.Id )
            .LogInformation();
    }

    #endregion

    #region UpdateAsync

    /// <inheritdoc />
    public virtual async Task UpdateAsync( TIdentityResourceDto request ) {
        request.CheckNull( nameof( request ) );
        request.Validate();
        if ( request.Id.IsEmpty() )
            throw new InvalidOperationException( R.IdIsEmpty );
        var oldEntity = await IdentityResourceRepository.FindByIdAsync( request.Id );
        oldEntity.CheckNull( nameof( oldEntity ) );
        var entity = request.MapTo( oldEntity.Clone() );
        entity.CheckNull( nameof( entity ) );
        await Validate( entity );
        var changes = oldEntity.GetChanges( entity );
        await IdentityResourceRepository.UpdateAsync( entity );
        await UnitOfWork.CommitAsync();
        WriteUpdateLog( entity, changes );
    }

    /// <summary>
    /// 写更新日志
    /// </summary>
    protected virtual void WriteUpdateLog( TIdentityResource entity, ChangeValueCollection changeValues ) {
        Log.Append( "身份资源{IdentityResourceName}修改成功,", entity.Name )
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
        if ( entities?.Count == 0 )
            return;
        await ResourceRepository.RemoveAsync( entities );
        await UnitOfWork.CommitAsync();
        Log.Append( "身份资源{IdentityResourceName}删除成功,", entities.Select( t => t.Name ).Join() )
            .Append( "业务标识: {Id}", entities.Select( t => t.Id ).Join() )
            .LogInformation();
    }

    #endregion
}