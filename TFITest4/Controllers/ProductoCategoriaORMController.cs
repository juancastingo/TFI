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
    public class ProductoCategoriaORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /ProductoCategoriaORM/

        public ActionResult Index()
        {
            return View(db.ProductoCategoria.ToList());
        }

        //
        // GET: /ProductoCategoriaORM/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductoCategoria productocategoria = db.ProductoCategoria.Find(id);
            if (productocategoria == null)
            {
                return HttpNotFound();
            }
            return View(productocategoria);
        }

        //
        // GET: /ProductoCategoriaORM/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ProductoCategoriaORM/Create

        [HttpPost]
        public ActionResult Create(ProductoCategoria productocategoria)
        {
            if (ModelState.IsValid)
            {
                db.ProductoCategoria.Add(productocategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productocategoria);
        }

        //
        // GET: /ProductoCategoriaORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductoCategoria productocategoria = db.ProductoCategoria.Find(id);
            if (productocategoria == null)
            {
                return HttpNotFound();
            }
            return View(productocategoria);
        }

        //
        // POST: /ProductoCategoriaORM/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductoCategoria productocategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productocategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productocategoria);
        }

        //
        // GET: /ProductoCategoriaORM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductoCategoria productocategoria = db.ProductoCategoria.Find(id);
            if (productocategoria == null)
            {
                return HttpNotFound();
            }
            return View(productocategoria);
        }

        //
        // POST: /ProductoCategoriaORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoCategoria productocategoria = db.ProductoCategoria.Find(id);
            db.ProductoCategoria.Remove(productocategoria);
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