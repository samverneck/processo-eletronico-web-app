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
            ProcessoEletronicoModel processo = new ProcessoEletronicoModel();
            HomeWorkService home_ws = new HomeWorkService();
            processo = home_ws.GetProcessoPorNumero(numeroProcesso, usuario.Token);

            return PartialView("_VisualizarProcesso", processo);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult DespacharProcesso(string numeroProcesso)
        {
            DespachoTela telaDespacho = new DespachoTela();

            ProcessoEletronicoModel processo = new ProcessoEletronicoModel();
            OrganizacaoModel orgaosDestino = new OrganizacaoModel();

            HomeWorkService home_ws = new HomeWorkService();
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            processo = home_ws.GetProcessoPorNumero(numeroProcesso, usuario.Token);

            telaDespacho.tiposDocumentais = autuacao_ws.GetTipoDocumental(processo.atividade.id, usuario.Token);
            telaDespacho.processo = home_ws.GetProcessoPorNumero(numeroProcesso, usuario.Token);
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

            SelectList cbxUnidades = new SelectList(unidades, "guid", "sigla", 0);
            return Json(cbxUnidades, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        //public ActionResult Upload(List<HttpPostedFileBase> anexos)
        public ActionResult Despachar(FormCollection form, IEnumerable<HttpPostedFileBase> anexos)
        {
            try
            {
                List<AnexoModel> conteudos = new List<AnexoModel>();

                foreach (var anexo in anexos)
                {
                    if (anexo != null)
                    {
                        AnexoModel conteudo = new AnexoModel();

                        byte[] byteArray = new byte[(int)anexo.ContentLength + 1];
                        Byte[] Content = new BinaryReader(anexo.InputStream).ReadBytes(anexo.ContentLength);

                        conteudo.nome = anexo.FileName;
                        //conteudo.descricao = anexo.FileName + anexo.FileName;
                        conteudo.id = 59;
                        conteudo.mimeType = anexo.ContentType;
                        conteudo.conteudo = Convert.ToBase64String(Content);

                        conteudos.Add(conteudo);
                    }
                }


                DespachoPostModel despacho = new DespachoPostModel();

                despacho.idProcesso = Convert.ToInt32(form["processo.id"].ToString());
                despacho.anexos = conteudos;
                despacho.texto = form["textoDespacho"].ToString();
                despacho.guidOrganizacaoDestino = form["orgaosDestino"].ToString();
                despacho.guidUnidadeDestino = form["unidadeDestino"].ToString();


                HomeWorkService home_ws = new HomeWorkService();

                home_ws.PostDespacho(despacho, usuario.Token);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Download(int id)
        {
            HomeWorkService home_ws = new HomeWorkService();
            AnexoModel anexo = home_ws.GetAnexo(id, usuario.Token);

            return File(Convert.FromBase64String(anexo.conteudo), "application/octet-stream", anexo.nome);
        }
    }
}
