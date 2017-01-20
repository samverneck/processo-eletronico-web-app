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
using WebApp.Models.Autuacao;
using System.IO;
using WebApp.Models.Despacho;
using WebApp.Controllers.Autuacao;

namespace WebApp.Controllers
{
    //[Authorize]
    public class HomeController : BaseController
    {
        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            HomeModel home = new HomeModel();
            HomeWorkService home_ws = new HomeWorkService();
            home.processosPorOrgao = home_ws.GetProcessosPorOrgao(usuario.Orgao.guid, usuario.Token);
            home.usuario = usuario;

            return View("Index", home);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult VisualizarProcesso(string numeroProcesso)
        {
            HomeWorkService home_ws = new HomeWorkService();
            var retorno = home_ws.GetProcessoPorNumero(numeroProcesso, usuario.Token);

            if (!retorno.IsSuccessStatusCode)
            {
                AdicionarMensagem(TipoMensagem.Atencao, retorno.content);
            }
                        
            var processo = JsonConvert.DeserializeObject<ProcessoEletronicoModel>(retorno.content);

            return PartialView("_VisualizarProcesso", processo);            
        }

        public ActionResult VisualizarDespacho(int id)
        {
            HomeWorkService home_ws = new HomeWorkService();
            var retorno = home_ws.GetDespachoPorId(id, usuario.Token);

            if (!retorno.IsSuccessStatusCode)            
            {
                AdicionarMensagem(TipoMensagem.Atencao, retorno.content);
            }

            var despacho = JsonConvert.DeserializeObject<DespachoGetModel>(retorno.content);

            return PartialView("_VisualizarDespacho", despacho);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult DespacharProcesso(string numeroProcesso)
        {
            DespachoTela telaDespacho = new DespachoTela();
            OrganizacaoModel orgaosDestino = new OrganizacaoModel();

            HomeWorkService home_ws = new HomeWorkService();
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            var processo = home_ws.GetProcessoPorNumero(numeroProcesso, usuario.Token);

            telaDespacho.processo = JsonConvert.DeserializeObject<ProcessoEletronicoModel>(processo.content);
            telaDespacho.tiposDocumentais = autuacao_ws.GetTipoDocumental(telaDespacho.processo.atividade.id, usuario.Token);
            telaDespacho.orgaosDestino = autuacao_ws.GetOrgaosPorPatriarca(usuario.Patriarca.guid, usuario.Token);

            return PartialView("_DespacharProcesso", telaDespacho);
        }

        public ActionResult GetUsuarioLogado()
        {
            return PartialView("_usuario", usuario);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult unidadesPorOrganizacao(string guidOrganizacao)
        {
            List<UnidadeModel> unidades = new List<UnidadeModel>();

            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            unidades = autuacao_ws.GetUnidadesPorOrganizacao(guidOrganizacao, usuario.Token);

            foreach (var unidade in unidades)
            {
                unidade.nome = unidade.sigla + " - " + unidade.nome;
            }

            SelectList cbxUnidades = new SelectList(unidades, "guid", "nome", 0);
            return Json(cbxUnidades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DespacharProcessoPost(DespachoPostModel despacho)
        {
            if (despacho.anexos != null)
            {
                foreach (var anexo in despacho.anexos)
                {                    
                    anexo.conteudo = ConvertAnexoBase64(anexo.conteudo);                 
                }
            }

            HomeWorkService home_ws = new HomeWorkService();

            var resultado = home_ws.PostDespacho(despacho, usuario.Token);

            if (resultado.IsSuccessStatusCode)
            {
                AdicionarMensagem(TipoMensagem.Sucesso, "Despacho realizado com sucesso!");
            }
            else
            {
                AdicionarMensagem(TipoMensagem.Atencao, resultado.content);
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Download(int id)
        {            
            HomeWorkService home_ws = new HomeWorkService();
            AnexoModel anexo = home_ws.GetAnexo(id, usuario.Token);

            byte[] fileBase64 = Convert.FromBase64String(anexo.conteudo);
            string file = Encoding.UTF8.GetString(fileBase64, 0, fileBase64.Length);

            string[] fileDados = new string[] { file , anexo.nome};

            return Json(fileDados, JsonRequestBehavior.AllowGet);

        }
    }
}
