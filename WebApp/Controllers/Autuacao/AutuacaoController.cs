using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Controllers.Autuacao;
using WebApp.Models.Autuacao;

namespace WebApp.Controllers
{
    public class AutuacaoController : Controller
    {
        // GET: Autuacao
        public ActionResult Index()
        {
            FormAutuacaoModel formAutuacao = new FormAutuacaoModel();
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();

            formAutuacao.planosClassificacao = autuacao_ws.GetPlanosClassificacao(1, 6);
            formAutuacao.sinalizacoes = autuacao_ws.GetSinalizacoes(1);

            formAutuacao.idOrgaoAutuador = 6;
            formAutuacao.nomeOrgaoAutuador = "Instituto de Telecnologia de Informação e Comunicação do Espírito Santo";
            formAutuacao.siglaOrgaoAutuador = "PRODEST";

            formAutuacao.idUsuarioAutuador = 20;
            formAutuacao.nomeUsuarioAutuador = "Fernando Silva";
            formAutuacao.idOrganizacaoPai = 1;
            
            formAutuacao.idUnidadeAutuadora = 1;
            formAutuacao.nomeUnidadeAutuadora = "Gerência de Sistemas de Informação";
            formAutuacao.siglaUnidadeAutuadora = "GESIN";


            return View("Index", formAutuacao);
        }

        public FormAutuacaoModel ExibeMunicipios()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            FormAutuacaoModel formAutuacao = new FormAutuacaoModel();
            return formAutuacao;
        }

        [HttpGet]
        public ActionResult MunicipiosPorUf(string uf)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetMunicipios(uf), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult OrganizacoesPorEsferaPoderUf(string poder, string esfera, string uf)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacoes(poder, esfera, uf), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult OrganizacoesOutrosOrgaos()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacoesOutrosOrgaos(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult OrganizacaoPorId(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetOrganizacao(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UnidadesPorOrganizacao(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetUnidadesPorOrganizacao(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TiposContato()
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetTiposContatos(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PlanosClassificacao(int id, int idOrganizacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetPlanosClassificacao(id, idOrganizacao), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Funcoes(int id, int idPlanoClassificacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetFuncoes(id, idPlanoClassificacao), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Atividades(int id, int idFuncao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetAtividades(id, idFuncao), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Sinalizacoes(int id)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.GetSinalizacoes(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Autuar(AutuacaoModel autuacao)
        {
            AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
            return Json(autuacao_ws.PostAtuacao(autuacao, 1));
        }

        //public async Task<ActionResult> ExibeMunicipiosPartial()
        //{
        //    AutuacaoWorkService autuacao_ws = new AutuacaoWorkService();
        //    FormAutuacaoModel formAutuacao = new FormAutuacaoModel();            
        //    formAutuacao.municipios = await autuacao_ws.GetMunicipios();

        //    return View("ExibeMunicipios", ViewBag.Municipios);            
        //}

        // GET: Autuacao/Details/5
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
