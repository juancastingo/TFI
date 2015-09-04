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
    public class PrecioDetalleController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /PrecioDetalle/

        public ActionResult Index()
        {
            var preciodetalle = db.PrecioDetalle.Include(p => p.ListaPrecio).Include(p => p.Producto);
            return View(preciodetalle.ToList());
        }

        //
        // GET: /PrecioDetalle/Details/5

        public ActionResult Details(int id = 0)
        {
            PrecioDetalle preciodetalle = db.PrecioDetalle.Find(id);
            if (preciodetalle == null)
            {
                return HttpNotFound();
            }
            return View(preciodetalle);
        }

        //
        // GET: /PrecioDetalle/Create

        public ActionResult Create()
        {
            ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
            ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre");
            return View();
        }

        //
        // POST: /PrecioDetalle/Create

        [HttpPost]
        public ActionResult Create(PrecioDetalle preciodetalle)
        {
            if (ModelState.IsValid)
            {
                db.PrecioDetalle.Add(preciodetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle", preciodetalle.IDListaPrecio);
            ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre", preciodetalle.IDProducto);
            return View(preciodetalle);
        }

        //
        // GET: /PrecioDetalle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrecioDetalle preciodetalle = db.PrecioDetalle.Find(id);
            if (preciodetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle", preciodetalle.IDListaPrecio);
            ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre", preciodetalle.IDProducto);
            return View(preciodetalle);
        }

        //
        // POST: /PrecioDetalle/Edit/5

        [HttpPost]
        public ActionResult Edit(PrecioDetalle preciodetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preciodetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle", preciodetalle.IDListaPrecio);
            ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre", preciodetalle.IDProducto);
            return View(preciodetalle);
        }

        //
        // GET: /PrecioDetalle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrecioDetalle preciodetalle = db.PrecioDetalle.Find(id);
            if (preciodetalle == null)
            {
                return HttpNotFound();
            }
            return View(preciodetalle);
        }

        //
        // POST: /PrecioDetalle/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrecioDetalle preciodetalle = db.PrecioDetalle.Find(id);
            db.PrecioDetalle.Remove(preciodetalle);
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