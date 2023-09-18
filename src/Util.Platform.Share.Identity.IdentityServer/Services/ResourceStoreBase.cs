using Util.Applications.Dtos;
using Util.Data.Trees;
using Util.Platform.Share.Identity.Dtos;

namespace Util.Platform.Share.Identity.IdentityServer.Services;

/// <summary>
/// 资源存储器
/// </summary>
public abstract class ResourceStoreBase<TIdentityResourceService, TApiResourceService, TIdentityResourceDto, TResourceQuery, TApiResourceDto, TCreateApiResourceRequest>
    : ResourceStoreBase<TIdentityResourceService, TApiResourceService, TIdentityResourceDto, TResourceQuery, TApiResourceDto, TCreateApiResourceRequest, Guid?, Guid?>
    where TIdentityResourceService : IIdentityResourceServiceBase<TIdentityResourceDto, TResourceQuery>
    where TApiResourceService : IApiResourceServiceBase<TApiResourceDto, TCreateApiResourceRequest, TResourceQuery>
    where TIdentityResourceDto : IdentityResourceDtoBase, new()
    where TResourceQuery : ITreeQueryParameter
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto>, new()
    where TCreateApiResourceRequest : IRequest, new() {
    /// <summary>
    /// 初始化资源存储器
    /// </summary>
    /// <param name="identityResourceService">身份资源服务</param>
    /// <param name="apiResourceService">Api资源服务</param>
    protected ResourceStoreBase( TIdentityResourceService identityResourceService, TApiResourceService apiResourceService )
        : base( identityResourceService, apiResourceService ) {
    }
}

/// <summary>
/// 资源存储器
/// </summary>
public abstract class ResourceStoreBase<TIdentityResourceService, TApiResourceService, TIdentityResourceDto, TResourceQuery, TApiResourceDto, TCreateApiResourceRequest, TApplicationId, TAuditUserId> : IResourceStore
    where TIdentityResourceService : IIdentityResourceServiceBase<TIdentityResourceDto, TResourceQuery>
    where TApiResourceService : IApiResourceServiceBase<TApiResourceDto, TCreateApiResourceRequest, TResourceQuery>
    where TIdentityResourceDto : IdentityResourceDtoBase<TAuditUserId>, new()
    where TResourceQuery : ITreeQueryParameter
    where TApiResourceDto : ApiResourceDtoBase<TApiResourceDto, TApplicationId, TAuditUserId>, new()
    where TCreateApiResourceRequest : IRequest, new() {
    /// <summary>
    /// 初始化资源存储器
    /// </summary>
    /// <param name="identityResourceService">身份资源服务</param>
    /// <param name="apiResourceService">Api资源服务</param>
    protected ResourceStoreBase( TIdentityResourceService identityResourceService, TApiResourceService apiResourceService ) {
        IdentityResourceService = identityResourceService ?? throw new ArgumentNullException( nameof( identityResourceService ) );
        ApiResourceService = apiResourceService ?? throw new ArgumentNullException( nameof( apiResourceService ) );
    }

    /// <summary>
    /// 身份资源服务
    /// </summary>
    protected TIdentityResourceService IdentityResourceService { get; set; }

    /// <summary>
    /// Api资源服务
    /// </summary>
    protected TApiResourceService ApiResourceService { get; set; }

    /// <summary>
    /// 查找身份资源列表
    /// </summary>
    /// <param name="scopeNames">范围名称</param>
    public virtual async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync( IEnumerable<string> scopeNames ) {
        var resources = await IdentityResourceService.GetResourcesAsync( scopeNames?.ToList() );
        var result = resources.Select( ToIdentityResource ).ToList();
        AddDefaultIdentityResources( result );
        return result;
    }

    /// <summary>
    /// 添加默认的身份资源
    /// </summary>
    protected virtual void AddDefaultIdentityResources( List<IdentityResource> result ) {
        AddOpenId( result );
        AddProfile( result );
    }

    /// <summary>
    /// 添加openid身份资源
    /// </summary>
    protected virtual void AddOpenId( List<IdentityResource> result ) {
        if ( result.Any( t => t.Name == "openid" ) )
            return;
        result.Add( new IdentityResource( "openid", new[] { "sub" } ) );
    }

    /// <summary>
    /// 添加profile身份资源
    /// </summary>
    protected virtual void AddProfile( List<IdentityResource> result ) {
        if ( result.Any( t => t.Name == "profile" ) )
            return;
        result.Add( new IdentityResource( "profile", new[] { "profile", "name" } ) );
    }

    /// <summary>
    /// 转换为身份资源
    /// </summary>
    protected virtual IdentityResource ToIdentityResource( TIdentityResourceDto dto ) {
        if ( dto == null )
            return null;
        var result = dto.MapTo<IdentityResource>();
        result.Name = dto.Uri;
        result.DisplayName = dto.Name;
        result.Description = dto.Remark;
        dto.Claims?.ForEach( result.UserClaims.Add );
        return result;
    }

    /// <summary>
    /// 查找Api范围列表
    /// </summary>
    public virtual async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync( IEnumerable<string> scopeNames ) {
        var result = await ApiResourceService.GetResourcesAsync( scopeNames?.ToList() );
        return result.Select( ToApiScope ).ToList();
    }

    /// <summary>
    /// 转换为Api范围
    /// </summary>
    protected virtual ApiScope ToApiScope( TApiResourceDto dto ) {
        if ( dto == null )
            return null;
        var result = new ApiScope( dto.Uri, dto.Name ) {
            Description = dto.Remark,
            Enabled = dto.Enabled.SafeValue()
        };
        dto.Claims?.ForEach( result.UserClaims.Add );
        return result;
    }

    /// <summary>
    /// 查找Api资源列表
    /// </summary>
    public virtual async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync( IEnumerable<string> scopeNames ) {
        var result = await ApiResourceService.GetResourcesAsync( scopeNames?.ToList() );
        return result.Select( ToApiResource ).ToList();
    }

    /// <summary>
    /// 转换为Api资源
    /// </summary>
    protected virtual ApiResource ToApiResource( TApiResourceDto dto ) {
        if ( dto == null )
            return null;
        var result = new ApiResource( dto.Uri, dto.Name ) {
            Description = dto.Remark,
            Enabled = dto.Enabled.SafeValue()
        };
        dto.Claims?.ForEach( result.UserClaims.Add );
        return result;
    }

    /// <summary>
    /// 查找Api资源列表
    /// </summary>
    public virtual async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync( IEnumerable<string> apiResourceNames ) {
        var result = await ApiResourceService.GetResourcesAsync( apiResourceNames?.ToList() );
        return result.Select( ToApiResource ).ToList();
    }

    /// <summary>
    /// 获取全部资源
    /// </summary>
    public virtual async Task<Resources> GetAllResourcesAsync() {
        var identityResources = ( await IdentityResourceService.GetEnabledResourcesAsync() ).Select( ToIdentityResource );
        var apiResources = ( await ApiResourceService.GetEnabledResourcesAsync() ).Select( ToApiResource );
        return new Resources( identityResources, apiResources, new List<ApiScope>() );
    }
}