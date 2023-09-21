using Util.Platform.Share.Tools.Dtos;
using Util.Platform.Share.Tools.Services.Abstractions;

namespace Util.Platform.Share.Tools.Services.Implements;

/// <summary>
/// 头像服务
/// </summary>
public class AvatarService : ServiceBase, IAvatarService {
    /// <summary>
    /// 初始化头像服务
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="avatarManager">头像服务</param>
    public AvatarService( IServiceProvider serviceProvider, IAvatarManager avatarManager ) : base( serviceProvider ) {
        AvatarManager = avatarManager ?? throw new ArgumentNullException( nameof( avatarManager ) );
    }

    /// <summary>
    /// 头像服务
    /// </summary>
    protected IAvatarManager AvatarManager { get; }

    /// <summary>
    /// 获取头像
    /// </summary>
    /// <param name="dto">头像参数</param>
    public async Task<byte[]> GetAvatarAsync( AvatarDto dto ) {
        if ( dto == null )
            return null;
        var length = dto.Length ?? 1;
        var size = dto.Size ?? 256;
        var fontSize = dto.FontSize ?? 0.5;
        return await AvatarManager.Text( dto.Text, length )
            .BackgroundColor( GetBackgroundColor() )
            .Font( GetFont( dto ) )
            .Size( size )
            .FontSize( fontSize )
            .ToStreamAsync();
    }

    /// <summary>
    /// 获取背景色
    /// </summary>
    protected virtual string GetBackgroundColor() {
        return Helpers.Random.GetValue( GetBackgroundColors() );
    }

    /// <summary>
    /// 获取背景色列表
    /// </summary>
    protected virtual List<string> GetBackgroundColors() {
        return new List<string> {
            "5d005d",
            "da3a00",
            "5c2893",
            "008272",
            "32105c",
            "0075da",
            "b600a0",
            "027d00",
            "008272",
            "004c1a",
            "001e51"
        };
    }

    /// <summary>
    /// 获取字体
    /// </summary>
    protected virtual string GetFont( AvatarDto dto ) {
        if ( dto.Font.IsEmpty() == false )
            return dto.Font;
        return "MiSans-Normal";
    }
}