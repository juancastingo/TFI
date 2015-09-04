using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.ORM;

namespace TFITest4.Controllers
{
    public class PaisTest2Controller : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /PaisTest2/

        public ActionResult Index()
        {
            return View(db.Pais.ToList());
        }

        //
        // GET: /PaisTest2/Details/5

        public ActionResult Details(int id = 0)
        {
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        //
        // GET: /PaisTest2/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PaisTest2/Create

        [HttpPost]
        public ActionResult Create(Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Pais.Add(pais);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pais);
        }

        //
        // GET: /PaisTest2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        //
        // POST: /PaisTest2/Edit/5

        [HttpPost]
        public ActionResult Edit(Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pais).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        //
        // GET: /PaisTest2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        //
        // POST: /PaisTest2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pais pais = db.Pais.Find(id);
            db.Pais.Remove(pais);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}