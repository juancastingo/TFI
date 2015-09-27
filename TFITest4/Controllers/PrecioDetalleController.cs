using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BIZ;

namespace TFITest4.Controllers
{
    public class PrecioDetalleController : Controller
    {
        //
        // GET: /PrecioDetalle/
        BLLPrecio precioWorker = new BLLPrecio();
        BLLProducto productoWorker = new BLLProducto();

        public ActionResult Index()
        {
            var lista = precioWorker.traerPrecioDetalles();
            return View(lista);
        }

        public ActionResult Edit(int id)
        {
            var precio = precioWorker.traerPrecioDetalle(id);
            if (precio == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle", preciodetalle.IDListaPrecio);
            return View(precio);
        }



        private DAL.ORM.IIDTest2Entities db = new DAL.ORM.IIDTest2Entities(); //borrar
        public ActionResult Create()
        {
            ViewBag.IDListaPrecio = new SelectList(precioWorker.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
            //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
            ViewBag.IDProducto = new SelectList(productoWorker.traerAllProductos(), "IDProducto", "Nombre");
            //ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre");
            return View();
        }

        //
        // POST: /PrecioDetalle/Create

        [HttpPost]
        public ActionResult Create(BIZPrecioDetalle PrecioDetalle)
        {
            try
            {
                precioWorker.CrearDetallePrecio(PrecioDetalle);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IDListaPrecio = new SelectList(precioWorker.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
                //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
                ViewBag.IDProducto = new SelectList(productoWorker.traerAllProductos(), "IDProducto", "Nombre");
                return View();
            }
        }


        //
        // POST: /PrecioDetalle/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, bool Activo)
        {
            try
            {
                precioWorker.ActualizarPrecioDetalle(id, Activo);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PrecioDetalle/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PrecioDetalle/Delete/5

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
