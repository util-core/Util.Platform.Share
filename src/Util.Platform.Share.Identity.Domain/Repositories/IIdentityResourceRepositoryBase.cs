﻿using Util.Platform.Share.Identity.Domain.Models;

namespace Util.Platform.Share.Identity.Domain.Repositories;

/// <summary>
/// 身份资源仓储
/// </summary>
public interface IIdentityResourceRepositoryBase<TIdentityResource> : IIdentityResourceRepositoryBase<TIdentityResource, Guid, Guid?>
    where TIdentityResource : IdentityResourceBase<TIdentityResource> {
}

/// <summary>
/// 身份资源仓储
/// </summary>
public interface IIdentityResourceRepositoryBase<TIdentityResource, TResourceId, TAuditUserId> : IScopeDependency 
    where TIdentityResource : IdentityResourceBase<TIdentityResource, TResourceId, TAuditUserId> {
    /// <summary>
    /// 通过标识查找身份资源
    /// </summary>
    /// <param name="id">身份资源标识</param>
    Task<TIdentityResource> FindByIdAsync( object id );
    /// <summary>
    /// 获取已启用的身份资源列表
    /// </summary>
    Task<List<TIdentityResource>> GetEnabledResources();
    /// <summary>
    /// 判断身份资源是否存在
    /// </summary>
    /// <param name="entity">身份资源</param>
    Task<bool> ExistsAsync( TIdentityResource entity );
    /// <summary>
    /// 添加身份资源
    /// </summary>
    /// <param name="entity">身份资源</param>
    Task AddAsync( TIdentityResource entity );
    /// <summary>
    /// 修改身份资源
    /// </summary>
    /// <param name="entity">身份资源</param>
    Task UpdateAsync( TIdentityResource entity );
}