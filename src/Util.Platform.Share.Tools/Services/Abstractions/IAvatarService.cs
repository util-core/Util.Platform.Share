using Util.Platform.Share.Tools.Dtos;

namespace Util.Platform.Share.Tools.Services.Abstractions;

/// <summary>
/// 头像服务
/// </summary>
public interface IAvatarService : IService {
    /// <summary>
    /// 获取头像
    /// </summary>
    /// <param name="dto">头像参数</param>
    Task<byte[]> GetAvatarAsync( AvatarDto dto );
}