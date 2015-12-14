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
            try
            {
                var lista = precioWorker.traerPrecioDetalles();
                return View(lista);
            }
            catch
            {

                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar listar detalles de precios", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var precio = precioWorker.traerPrecioDetalle(id);
                if (precio == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle", preciodetalle.IDListaPrecio);
                return View(precio);
            }
            catch
            {
                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar mostrar precio para editar", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }



        public ActionResult Create()
        {
            try
            {
                ViewBag.IDListaPrecio = new SelectList(precioWorker.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
                //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
                ViewBag.IDProducto = new SelectList(productoWorker.traerAllProductos(), "IDProducto", "Nombre");
                //ViewBag.IDProducto = new SelectList(db.Producto, "IDProducto", "Nombre");
                return View();
            }
            catch {
                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar mostrar vista para crear precio detalle", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            
            
            }

        }

        //
        // POST: /PrecioDetalle/Create

        [HttpPost]
        public ActionResult Create(ModelPrecioDetalle PrecioDetalle, string Precio)
        {
            try
            {
                PrecioDetalle.Precio = Convert.ToDouble(Precio);
                var precioD = Mapper.Map<ModelPrecioDetalle, BIZPrecioDetalle>(PrecioDetalle);
                precioWorker.CrearDetallePrecio(precioD);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha creado el precio detalle para lista: "+ PrecioDetalle.IDListaPrecio, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }

                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IDListaPrecio = new SelectList(precioWorker.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
                //ViewBag.IDListaPrecio = new SelectList(db.ListaPrecio, "IDListaPrecio", "Detalle");
                ViewBag.IDProducto = new SelectList(productoWorker.traerAllProductos(), "IDProducto", "Nombre");
                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar crear precio detalle", idUser, ip));
                }
                catch (Exception ex) { }
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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha editado el precio detalle con id: "+ id, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }
                return RedirectToAction("Index");
            }
            catch
            {
                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar editar precio detalle", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
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
