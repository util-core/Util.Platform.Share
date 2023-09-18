namespace Util.Platform.Share.Identity.IdentityServer.Models; 

/// <summary>
/// 错误参数
/// </summary>
public class ErrorViewModel {
    /// <summary>
    /// 初始化错误参数
    /// </summary>
    public ErrorViewModel() {
    }

    /// <summary>
    /// 初始化错误参数
    /// </summary>
    /// <param name="error">错误消息</param>
    public ErrorViewModel( string error ) {
        Error = new ErrorMessage { Error = error };
    }

    /// <summary>
    /// 错误消息
    /// </summary>
    public ErrorMessage Error { get; set; }
}