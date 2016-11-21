using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ArquivamentoController : Controller
    {
        // GET: Arquivamento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Arquivamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Arquivamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arquivamento/Create
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

        // GET: Arquivamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Arquivamento/Edit/5
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

        // GET: Arquivamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Arquivamento/Delete/5
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
