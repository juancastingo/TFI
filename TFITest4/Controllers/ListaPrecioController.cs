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
    public class ListaPrecioController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.ListaPrecio.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            ListaPrecio listaprecio = db.ListaPrecio.Find(id);
            if (listaprecio == null)
            {
                return HttpNotFound();
            }
            return View(listaprecio);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(ListaPrecio listaprecio)
        {
            if (ModelState.IsValid)
            {
                db.ListaPrecio.Add(listaprecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listaprecio);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ListaPrecio listaprecio = db.ListaPrecio.Find(id);
            if (listaprecio == null)
            {
                return HttpNotFound();
            }
            return View(listaprecio);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(ListaPrecio listaprecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaprecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listaprecio);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ListaPrecio listaprecio = db.ListaPrecio.Find(id);
            if (listaprecio == null)
            {
                return HttpNotFound();
            }
            return View(listaprecio);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaPrecio listaprecio = db.ListaPrecio.Find(id);
            db.ListaPrecio.Remove(listaprecio);
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