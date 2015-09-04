using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.ORM;
using System.IO;

namespace TFITest4.Controllers
{
    public class ProductoORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /ProductoORM/

        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.EstadoMisc).Include(p => p.ProductoCategoria);
            return View(producto.ToList());
        }

        //
        // GET: /ProductoORM/Details/5

        public ActionResult Details(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // GET: /ProductoORM/Create

        public ActionResult Create()
        {
            ViewBag.IDEstado = new SelectList(db.EstadoMisc.Where(b => b.Tipo == "Producto"), "IDEstado", "Detalle");
            ViewBag.IDProductoCategoria = new SelectList(db.ProductoCategoria, "IDProductoCategoria", "Detalle");
            return View();
        }

        //
        // POST: /ProductoORM/Create

        [HttpPost]
        public ActionResult Create(Producto producto, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Random rnd1 = new Random();
                producto.Imagen = "/Pimages/" + Path.GetFileNameWithoutExtension(file.FileName) + rnd1.Next().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                //var path = Path.Combine(Server.MapPath("/Pimages"), producto.Imagen);
                var path = Server.MapPath(producto.Imagen);
                file.SaveAs(path);
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEstado = new SelectList(db.EstadoMisc.Where(b => b.Tipo == "Producto"), "IDEstado", "Detalle", producto.IDEstado);
            ViewBag.IDProductoCategoria = new SelectList(db.ProductoCategoria, "IDProductoCategoria", "Detalle", producto.IDProductoCategoria);
            return View(producto);
        }

        //
        // GET: /ProductoORM/Edit/5

        public ActionResult Edit(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", producto.IDEstado);
            ViewBag.IDProductoCategoria = new SelectList(db.ProductoCategoria, "IDProductoCategoria", "Detalle", producto.IDProductoCategoria);
            return View(producto);
        }

        //
        // POST: /ProductoORM/Edit/5

        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", producto.IDEstado);
            ViewBag.IDProductoCategoria = new SelectList(db.ProductoCategoria, "IDProductoCategoria", "Detalle", producto.IDProductoCategoria);
            return View(producto);
        }

        //
        // GET: /ProductoORM/Delete/5

        public ActionResult Delete(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // POST: /ProductoORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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