using Util.Platform.Share.Identity.IdentityServer.Models;

namespace Util.Platform.Share.Identity.IdentityServer.Controllers;

/// <summary>
/// 主控制器
/// </summary>
public abstract class HomeControllerBase : Controller {
    /// <summary>
    /// 交互服务
    /// </summary>
    private readonly IIdentityServerInteractionService _interaction;
    /// <summary>
    /// 主机环境
    /// </summary>
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// 初始化主控制器
    /// </summary>
    /// <param name="interaction">交互服务</param>
    /// <param name="environment">主机环境</param>
    protected HomeControllerBase( IIdentityServerInteractionService interaction, IWebHostEnvironment environment ) {
        _interaction = interaction ?? throw new ArgumentNullException( nameof( interaction ) );
        _environment = environment ?? throw new ArgumentNullException( nameof( environment ) );
    }

    /// <summary>
    /// 发现文档,仅开发环境显示
    /// </summary>
    public virtual IActionResult Index() {
        if ( _environment.IsDevelopment() )
            return View();
        return NotFound();
    }

    /// <summary>
    /// 错误页
    /// </summary>
    public virtual async Task<IActionResult> Error( string errorId ) {
        var model = new ErrorViewModel();
        var message = await _interaction.GetErrorContextAsync( errorId );
        if ( message != null ) {
            model.Error = message;
            if ( _environment.IsDevelopment() == false )
                message.ErrorDescription = null;
        }
        return View( "Error", model );
    }
}