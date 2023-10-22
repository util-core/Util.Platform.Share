namespace Util.Platform.Share.Middlewares;

/// <summary>
/// 应用配置扩展
/// </summary>
public static class ProgramExtensions {
    /// <summary>
    /// 注册访问控制列表加载中间件
    /// </summary>
    /// <param name="builder">应用程序生成器</param>
    public static IApplicationBuilder UseLoadAcl( this IApplicationBuilder builder ) {
        return builder.UseMiddleware<LoadAclMiddleware>();
    }
}