using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using WebApp.Autorizacao;
using WebApp.Controllers.Autuacao;
using WebApp.Models;
using WebApp.Models.Autuacao;
using WebApp.Models.Organograma;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Controllers
{
    [Authorize]
    public class AutuacaoController : BaseController
    {
        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            FormAutuacaoModel formAutuacao = new FormAutuacaoModel();
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            formAutuacao.planosClassificacao = autuacao_ws.GetPlanosClassificacao(usuario.Orgao.guid, usuario.Token);
            formAutuacao.sinalizacoes = autuacao_ws.GetSinalizacoes(usuario.Patriarca.guid, usuario.Token);

            OrganizacaoModel orgao = usuario.Orgao;

            formAutuacao.guidOrgao = orgao.guid;
            formAutuacao.nomeOrgaoAutuador = orgao.razaoSocial;
            formAutuacao.siglaOrgaoAutuador = usuario.SiglaOrganizacao;
            formAutuacao.unidadesOrgao = autuacao_ws.GetUnidadesPorOrganizacao(usuario.Orgao.guid, usuario.Token);

            OrganizacaoModel patriarca = usuario.Patriarca;

            formAutuacao.nomeUsuarioAutuador = usuario.Nome;
            formAutuacao.guidPatriarca = patriarca.guid;

            return View("Index", formAutuacao);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public FormAutuacaoModel ExibeMunicipios()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            FormAutuacaoModel formAutuacao = new FormAutuacaoModel();
            return formAutuacao;
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult MunicipiosPorUf(string uf)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetMunicipios(uf, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult OrganizacoesPorEsferaPoderUf(string poder, string esfera, string uf)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacoes(poder, esfera, uf, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult OrganizacoesOutrosOrgaos()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacoesOutrosOrgaos(usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult OrganizacaoPorGuid(string guidOrganizacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacaoPorGuid(guidOrganizacao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult OrganizacoesPorPatriarca()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();            
            return Json(autuacao_ws.GetOrgaosPorPatriarca(usuario.Patriarca.guid, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult UnidadesPorOrganizacao(string guidOrganizacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetUnidadesPorOrganizacao(guidOrganizacao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult TiposContato()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetTiposContatos(usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult PlanosClassificacao(int id, int idOrganizacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetPlanosClassificacao(usuario.Orgao.guid, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Funcoes(int id, int idPlanoClassificacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetFuncoes(idPlanoClassificacao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Atividades(int id, int idFuncao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetAtividades(idFuncao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Sinalizacoes(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetSinalizacoes(usuario.Patriarca.guid, usuario.Token), JsonRequestBehavior.AllowGet);
        }


        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult TiposDocumentais(int idAtividade)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetTipoDocumental(idAtividade, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpPost]
        public ActionResult Autuar(AutuacaoModel autuacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            foreach (var anexo in autuacao.anexos)
            {
                int i = 0;
                byte[] byteArray = Encoding.UTF8.GetBytes(anexo.conteudo);
                MemoryStream stream = new MemoryStream(byteArray);
                Byte[] Content = new BinaryReader(stream).ReadBytes(anexo.conteudo.Length);

                autuacao.anexos[i].conteudo = Convert.ToBase64String(Content);

                i++;
            }

            var resultado = autuacao_ws.PostAutuacao(autuacao, usuario.Token);
            string[] codeStatus = resultado.Split(',');

            return Json(codeStatus[0].Split(':')[1], JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Autuacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autuacao/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autuacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Autuacao/Edit/5
        [HttpPost]
        public ActionResult SalvarPessoaFisica(InteressadoPessoaFisica pf)
        {
            return Json(pf, JsonRequestBehavior.AllowGet);
        }

        // GET: Autuacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Autuacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
