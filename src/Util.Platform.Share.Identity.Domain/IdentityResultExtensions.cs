using Microsoft.AspNetCore.Identity;

namespace Util.Platform.Share.Identity.Domain; 

/// <summary>
/// Identity结果扩展
/// </summary>
public static class IdentityResultExtensions {
    /// <summary>
    /// 失败抛出异常
    /// </summary>
    /// <param name="result">Identity结果</param>
    public static void ThrowIfError( this IdentityResult result ) {
        result.CheckNull( nameof( result ) );
        if ( result.Succeeded == false )
            throw new Warning( result.Errors.First().Description );
    }
}