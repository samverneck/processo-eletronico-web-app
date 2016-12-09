using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using WebApp.Autorizacao;
using WebApp.Controllers.Autuacao;
using WebApp.Models;
using WebApp.Models.Autuacao;
using WebApp.Models.Organograma;

namespace WebApp.Controllers
{
    [Authorize]
    public class AutuacaoController : BaseController
    {
        //Guid provisório
        int _guidPatriarca = 13811;
        int _guidOrgao = 5570;

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            FormAutuacaoModel formAutuacao = new FormAutuacaoModel();
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            formAutuacao.planosClassificacao = autuacao_ws.GetPlanosClassificacao(_guidPatriarca, _guidOrgao, usuario.Token);
            formAutuacao.sinalizacoes = autuacao_ws.GetSinalizacoes(_guidPatriarca, usuario.Token);

            OrgaoModel orgao = usuario.Orgao;

            formAutuacao.guidOrgao = orgao.guid;
            formAutuacao.nomeOrgaoAutuador = orgao.razaoSocial;
            formAutuacao.siglaOrgaoAutuador = usuario.SiglaOrganizacao;
            
            OrgaoModel patriarca = usuario.Patriarca;
                        
            formAutuacao.nomeUsuarioAutuador = usuario.Nome;
            formAutuacao.guidPatriarca = patriarca.guid ;
            

            /*
             Por 
             
             */

            //formAutuacao.idUnidadeAutuadora = 1;
            //formAutuacao.nomeUnidadeAutuadora = "Gerência de Sistemas de Informação";
            //formAutuacao.siglaUnidadeAutuadora = "GESIN";


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
        public ActionResult OrganizacaoPorId(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacao(id, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult UnidadesPorOrganizacao(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetUnidadesPorOrganizacao(id, usuario.Token), JsonRequestBehavior.AllowGet);
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
            return Json(autuacao_ws.GetPlanosClassificacao(_guidPatriarca, _guidOrgao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Funcoes(int id, int idPlanoClassificacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetFuncoes(_guidPatriarca, idPlanoClassificacao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Atividades(int id, int idFuncao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetAtividades(_guidPatriarca, idFuncao, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpGet]
        public ActionResult Sinalizacoes(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetSinalizacoes(_guidPatriarca, usuario.Token), JsonRequestBehavior.AllowGet);
        }

        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        [HttpPost]
        public ActionResult Autuar(AutuacaoModel autuacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.PostAtuacao(autuacao, _guidPatriarca, usuario.Token));
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
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
