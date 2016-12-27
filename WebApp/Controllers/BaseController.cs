using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
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

        public string ConvertAnexoBase64(string anexo)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(anexo);
            return Convert.ToBase64String(plainTextBytes);           
        }

        public string ConvertAnexoBase64_2(string anexo)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(anexo);            
            MemoryStream stream = new MemoryStream(byteArray);
            Byte[] Content = new BinaryReader(stream).ReadBytes(anexo.Length);

            var conteudo = Convert.ToBase64String(Content);
            return conteudo;
        }

        public string ConvertAnexoBase64_3(string anexo)
        {
            byte[] byteArray = new byte[(int)anexo.Length + 1];
            Stream stream = new MemoryStream(byteArray);
            Byte[] Content = new BinaryReader(stream).ReadBytes(anexo.Length);
            
            var conteudo = Convert.ToBase64String(Content);

            return conteudo;

        }
    }
}