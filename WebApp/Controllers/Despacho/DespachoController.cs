using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class DespachoController : Controller
    {
        [ResourceAuthorize("Autuar", "Processo")]
        [HandleForbidden]
        public ActionResult DespacharProcesso(int idProcesso)
        {
            return PartialView("_formularioDespacho");
        }

        // GET: Despacho
        public ActionResult Index()
        {
            return View();
        }

        // GET: Despacho/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Despacho/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Despacho/Create
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

        // GET: Despacho/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Despacho/Edit/5
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

        // GET: Despacho/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Despacho/Delete/5
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
