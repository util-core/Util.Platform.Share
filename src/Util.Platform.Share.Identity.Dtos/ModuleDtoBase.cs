namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 模块参数
/// </summary>
public abstract class ModuleDtoBase<TModuleDto> : ModuleDtoBase<TModuleDto, Guid?, Guid?>
    where TModuleDto : ModuleDtoBase<TModuleDto> {
}

/// <summary>
/// 模块参数
/// </summary>
public abstract class ModuleDtoBase<TModuleDto,TApplicationId, TAuditUserId> : TreeDtoBase<TModuleDto> 
    where TModuleDto : ModuleDtoBase<TModuleDto, TApplicationId, TAuditUserId> {
    /// <summary>
    /// 应用程序标识
    ///</summary>
    [Display( Name = "identity.module.applicationId" )]
    public TApplicationId ApplicationId { get; set; }
    /// <summary>
    /// 应用程序
    /// </summary>
    [Display( Name = "identity.module.applicationName" )]
    public string ApplicationName { get; set; }
    /// <summary>
    /// 模块地址
    ///</summary>
    [Display( Name = "identity.module.uri" )]
    [MaxLength( 300 )]
    public string Uri { get; set; }
    /// <summary>
    /// 模块名称
    ///</summary>
    [Display( Name = "identity.module.name" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 备注
    ///</summary>
    [Display( Name = "identity.module.remark" )]
    [MaxLength( 500 )]
    public string Remark { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "identity.module.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public TAuditUserId CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "identity.module.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    public TAuditUserId LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    ///</summary>
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public override string GetText() {
        return Name;
    }
}