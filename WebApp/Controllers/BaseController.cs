using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApp.Autorizacao;

namespace WebApp.Controllers
{
    [Authorize]
    public class BaseController: Controller
    {
        public UsuarioLogado usuario = new UsuarioLogado();
        public BaseController()
        {            
                                       
        }

        [AllowAnonymous]
        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        [AllowAnonymous]
        public void SignoutCleanup(string sid)
        {
            var cp = (ClaimsPrincipal)User;
            var sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }
    }
}