using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Organograma;
using System.IdentityModel;
using System.Security.Permissions;
using Thinktecture.IdentityModel.Mvc;
using WebApp.Models.ProcessoEletronico;
using WebApp.Controllers.Home;
using WebApp.Models.Home;
using WebApp.Autorizacao;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            HomeModel home = new HomeModel();
            HomeWorkService home_ws = new HomeWorkService();
            home.processosPorOrgao = home_ws.GetProcessosPorOrganizacaoPorOrgao(usuario.Orgao.guid, usuario.Token);
            home.usuario = usuario;

            return View("Index", home);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult VisualizarProcesso(string numeroProcesso)
        {
            ProcessoEletronicoModel processo = new ProcessoEletronicoModel();
            HomeWorkService home_ws = new HomeWorkService();
            processo = home_ws.GetProcessosPorOrganizacaoPorProcesso(numeroProcesso, usuario.Token);
                        
            return PartialView("_VisualizarProcesso", processo);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult DespacharProcesso(string numeroProcesso)
        {
            ProcessoEletronicoModel processo = new ProcessoEletronicoModel();
            HomeWorkService home_ws = new HomeWorkService();
            processo = home_ws.GetProcessosPorOrganizacaoPorProcesso(numeroProcesso, usuario.Token);

            return PartialView("_DespacharProcesso", processo);
        }

        public ActionResult GetUsuarioLogado()
        {            
            return PartialView("_usuario", usuario);
        }
    }
}
