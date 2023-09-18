using Microsoft.AspNetCore.Identity;
using Util.Platform.Share.Identity.Applications.Options;

namespace Util.Platform.Share.Identity.Applications; 

/// <summary>
/// Identity配置扩展
/// </summary>
public static class IdentityOptionsExtensions {
    /// <summary>
    /// 加载权限配置
    /// </summary>
    /// <param name="options">Identity配置</param>
    /// <param name="permissionOptions">权限配置</param>
    public static void Load( this IdentityOptions options, PermissionOptions permissionOptions ) {
        if( options == null || permissionOptions == null )
            return;
        permissionOptions.MapTo( options );
    }
}