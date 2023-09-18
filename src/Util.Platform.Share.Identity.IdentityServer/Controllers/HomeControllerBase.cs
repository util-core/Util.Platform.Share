using Util.Platform.Share.Identity.IdentityServer.Models;

namespace Util.Platform.Share.Identity.IdentityServer.Controllers;

/// <summary>
/// ��������
/// </summary>
public abstract class HomeControllerBase : Controller {
    /// <summary>
    /// ��������
    /// </summary>
    private readonly IIdentityServerInteractionService _interaction;
    /// <summary>
    /// ��������
    /// </summary>
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <param name="interaction">��������</param>
    /// <param name="environment">��������</param>
    protected HomeControllerBase( IIdentityServerInteractionService interaction, IWebHostEnvironment environment ) {
        _interaction = interaction ?? throw new ArgumentNullException( nameof( interaction ) );
        _environment = environment ?? throw new ArgumentNullException( nameof( environment ) );
    }

    /// <summary>
    /// �����ĵ�,������������ʾ
    /// </summary>
    public virtual IActionResult Index() {
        if ( _environment.IsDevelopment() )
            return View();
        return NotFound();
    }

    /// <summary>
    /// ����ҳ
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