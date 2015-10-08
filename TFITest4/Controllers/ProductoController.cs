using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BIZ;
using System.IO;

namespace TFITest4.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        //private DAL.ORM.IIDTest2Entities db = new DAL.ORM.IIDTest2Entities();
        private BLLProducto productoWorker = new BLLProducto();
        private BLLGeneral generalWorker = new BLLGeneral();

        public ActionResult Index()
        {
            var prod = productoWorker.traerAllProductos();
            return View(prod.ToList());
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
            ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle");
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
            return View();
        }

        //
        // POST: /Producto/Create

        [HttpPost]
        public ActionResult Create(BIZProducto producto, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Random rnd1 = new Random();
                producto.Imagen = "/Pimages/" + Path.GetFileNameWithoutExtension(file.FileName) + rnd1.Next().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                //var path = Path.Combine(Server.MapPath("/Pimages"), producto.Imagen);
                var path = Server.MapPath(producto.Imagen);
                file.SaveAs(path);
                productoWorker.InsertarProducto(producto);

                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
            ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle");
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
            return View(producto);
        }

        //
        // GET: /Producto/Edit/5

        public ActionResult Edit(int id)
        {
            var prod = productoWorker.traerProdXId(id);
            ViewBag.IDProductoCategoria = new SelectList(productoWorker.traerAllProductoCat(), "IDProductoCategoria", "Detalle",prod.IDProductoCategoria );
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle",prod.IDEstado);
            //.TraerAllListaPrecio(), "IDListaPrecio", "Detalle");
            
            return View(prod);
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
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
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
