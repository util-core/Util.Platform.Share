namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 模块服务
/// </summary>
public interface IModuleServiceBase<TModuleDto, in TCreateModuleRequest, in TResourceQuery> 
    : ITreeService<TModuleDto, TCreateModuleRequest, TModuleDto, TResourceQuery> 
    where TModuleDto : ITreeNode, new()
    where TCreateModuleRequest : IRequest, new() 
    where TResourceQuery : ITreeQueryParameter {
}