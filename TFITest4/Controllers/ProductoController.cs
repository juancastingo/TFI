using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BIZ;
using System.IO;
using AutoMapper;
using TFITest4.Models;

namespace TFITest4.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        private BLLProducto productoWorker = new BLLProducto();
        private BLLGeneral generalWorker = new BLLGeneral();
        private BLLBitacora Bita = new BLLBitacora();

        public ActionResult Index()
        {
            try
            {
                var prod = productoWorker.traerAllProductos();
                var rprod = Mapper.Map<List<BIZProducto>, List<ModelProducto>>(prod);
                return View(rprod);
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
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar listar productos", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index", "Home");
            }
            //var producto = db.Producto;
            //return View(producto.ToList());
        }

        //
        // GET: /Producto/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Producto/Create

        public ActionResult Create()
        {
            try
            {
                ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle");
                ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("Producto"), "IDEstado", "Detalle");
                return View();
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
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar mostrar vista para crear producto", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");

            }
        }

        //
        // POST: /Producto/Create

        [HttpPost]
        public ActionResult Create(BIZProducto producto, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Random rnd1 = new Random();
                    producto.Imagen = "/Pimages/" + Path.GetFileNameWithoutExtension(file.FileName) + rnd1.Next().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    //var path = Path.Combine(Server.MapPath("/Pimages"), producto.Imagen);
                    var path = Server.MapPath(producto.Imagen);
                    file.SaveAs(path);
                    productoWorker.InsertarProducto(producto);
                    try
                    {
                        Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha creado el producto: " + producto.Nombre, (int)Session["userID"], Session["_ip"].ToString()));
                    }
                    catch (Exception ex) { }
                    TempData["OKNormal"] = Resources.Language.OKNormal;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                    ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle");
                    ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("Producto"), "IDEstado", "Detalle");
                    return View(producto);
                }
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
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar crear producto", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }


        //
        // GET: /Producto/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                var prod = productoWorker.traerProdXId(id);
                ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle", prod.IDProductoCategoria);
                ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("Producto"), "IDEstado", "Detalle", prod.IDEstado);
                //.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
                var rprod = Mapper.Map<BIZProducto, ModelProducto>(prod);
                return View(rprod);
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
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar mostrar vista para editar un producto", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Producto/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BIZProducto producto, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                if (file != null)
                {
                    Random rnd1 = new Random();
                    producto.Imagen = "/Pimages/" + Path.GetFileNameWithoutExtension(file.FileName) + rnd1.Next().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    //var path = Path.Combine(Server.MapPath("/Pimages"), producto.Imagen);
                    var path = Server.MapPath(producto.Imagen);
                    file.SaveAs(path);
                }
                productoWorker.ActualizarProducto(producto);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha editando el producto con id: " + id, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }

                TempData["OKNormal"] = Resources.Language.OKNormal;
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
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar editar un producto", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Producto/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Producto/Delete/5

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
