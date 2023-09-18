namespace Util.Platform.Share.Identity.Applications.Services.Abstractions;

/// <summary>
/// 用户服务
/// </summary>
public interface IUserServiceBase<TUserDto, in TCreateUserRequest, in TUserQuery> 
    : ICrudService<TUserDto, TCreateUserRequest, TUserDto, TUserQuery> 
    where TUserDto : IDto, new() 
    where TCreateUserRequest : IRequest, new() 
    where TUserQuery: IPage {
}