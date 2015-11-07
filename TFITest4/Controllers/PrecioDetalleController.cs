using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BIZ;
using TFITest4.Models;
using AutoMapper;

namespace TFITest4.Controllers
{
    [Authorize]
    public class PrecioDetalleController : Controller
    {
        //
        // GET: /PrecioDetalle/
        BLLPrecio precioWorker = new BLLPrecio();
        BLLProducto productoWorker = new BLLProducto();
        private BLLBitacora Bita = new BLLBitacora();

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
        public ActionResult Create(ModelPrecioDetalle PrecioDetalle)
        {
            try
            {
                var precioD = Mapper.Map<ModelPrecioDetalle, BIZPrecioDetalle>(PrecioDetalle);
                precioWorker.CrearDetallePrecio(precioD);
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IDListaPrecio = new SelectList(precioWorker.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
                //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
                ViewBag.IDProducto = new SelectList(productoWorker.traerAllProductos(), "IDProducto", "Nombre");
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return View();
            }
        }


        private class PrecioDetalleM
        {
            public int IDPrecioDetalle { get; set; }
            public double Precio { get; set; }
            public bool Estado { get; set; }
            public string Producto { get; set; }
        }

        public ActionResult DetallesLista(string IDLista)
        {
            
            try
            {
                var id = Convert.ToInt32(IDLista);
                var precio = precioWorker.TraerAllListaPrecio();
                BIZListaPrecio Lista = new BIZListaPrecio();
                foreach (var p in precio)
                {
                    if (p.IDListaPrecio == id)
                    {
                        Lista = p;
                    }
                }
                var list = Lista.PrecioDetalle.ToList();
                List<PrecioDetalleM> detalles = new List<PrecioDetalleM>();
                PrecioDetalleM detalle;
                
                foreach (var d in list)
                {
                    detalle = new PrecioDetalleM();
                    detalle.IDPrecioDetalle = d.IDPrecioDetalle;
                    detalle.Precio = (double)d.Precio;
                    detalle.Producto = d.Producto.Nombre;
                    detalle.Estado = (bool)d.Activo;
                    detalles.Add(detalle);
                    //detalle = new PrecioDetalleM();
                   
                }
                //return Json(new {  }, JsonRequestBehavior.AllowGet);
                return Json(new { detalles, Lista.Detalle   }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Lista = "" }, JsonRequestBehavior.AllowGet);
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
