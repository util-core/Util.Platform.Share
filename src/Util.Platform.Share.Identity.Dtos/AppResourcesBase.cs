namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 前端应用资源
/// </summary>
public abstract class AppResourcesBase<TModuleDto> : AppResourcesBase<TModuleDto, Guid?, Guid?>
    where TModuleDto : ModuleDtoBase<TModuleDto> {
}

/// <summary>
/// 前端应用资源
/// </summary>
public abstract class AppResourcesBase<TModuleDto, TApplicationId, TAuditUserId>
    where TModuleDto : ModuleDtoBase<TModuleDto, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 初始化前端应用资源
    /// </summary>
    protected AppResourcesBase() {
        Modules = new List<TModuleDto>();
        Acl = new List<string>();
    }

    /// <summary>
    /// 模块资源
    /// </summary>
    public List<TModuleDto> Modules { get; set; }
    /// <summary>
    /// 前端访问控制列表,包含操作资源标识和列集资源标识
    /// </summary>
    public List<string> Acl { get; set; }
    /// <summary>
    /// 是否管理员
    /// </summary>
    public bool IsAdmin { get; set; }
}