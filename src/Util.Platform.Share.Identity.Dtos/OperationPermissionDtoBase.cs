namespace Util.Platform.Share.Identity.Dtos;

/// <summary>
/// 操作权限参数
/// </summary>
public abstract class OperationPermissionDtoBase<TOperationPermissionDto> : TreeDtoBase<TOperationPermissionDto> 
    where TOperationPermissionDto : OperationPermissionDtoBase<TOperationPermissionDto> {
    /// <summary>
    /// 名称
    ///</summary>
    [Display( Name = "identity.operationPermission.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 是否操作资源
    /// </summary>
    public bool IsOperation { get; set; }

    /// <inheritdoc />
    public override string GetText() {
        return Name;
    }
}