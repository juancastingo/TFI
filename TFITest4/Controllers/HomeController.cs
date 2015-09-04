using BIZ;
using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TFITest4.Controllers
{
    public class HomeController : Controller
    {

        private IIDTest2Entities db = new IIDTest2Entities();
        //
        // GET: /Home/


        public ActionResult Index()
        {
            //var sequenceQueryResult = db.Database.SqlQuery<string>("select * from Producto").FirstOrDefault();
           // var ProductList = db.Producto.SqlQuery("select * from Producto where IDProductoCategoria = 3");
           // var Producto = db.Database.SqlQuery<int>(
           //            "SELECT count(*) FROM dbo.Producto");
            
           // int cant = Producto.Count();
           // foreach (Producto p in ProductList)
           // {
           //     Console.WriteLine(p.Nombre);
           // }
            // lo de arriba anda todo creo...


            if (TempData["Resultado"] != null)
                @ViewBag.AlertOK = TempData["Resultado"].ToString();

            ViewBag.url = Request.Url.Host.ToLower();
            ViewBag.numero = "3".PadLeft(8, '0'); //para la factura


            //traer productos con precios
            var ListaProdPrecio = db.Database.SqlQuery<string>(
                       "SELECT * FROM dbo.Producto");

            //var producto = db.Producto.

            //var ListaPrecio = db.ListaPrecio
            //    .Where(b => b.FechaDesde < DateTime.Now)
            //    .OrderByDescending(b => b.FechaDesde).FirstOrDefault();

            var ListaPrecio = db.Database.SqlQuery<int>("select TOP 1 IDListaPrecio from ListaPrecio where FechaDesde < GETDATE() and Activo = 1 order by FechaDesde Desc");
            int IDListaPrecioActual = ListaPrecio.FirstOrDefault();

            var ListaPrecios = db.PrecioDetalle
                .Where(b => b.IDListaPrecio == IDListaPrecioActual);

            var productos = db.Producto
                .Where(b => b.IDEstado == 3);

            //List<Producto> Lista = new List<Producto>();
            List<BIZProducto> ListaP = new List<BIZProducto>();
            foreach (var p in productos)
            {
                BIZProducto Prod = new BIZProducto();
                Prod.Nombre = p.Nombre;
                Prod.Imagen = p.Imagen;
                Prod.Descripcion = p.Descripcion;
                Prod.ProductoCategoria.Detalle = p.ProductoCategoria.Detalle;
                Prod.ProductoCategoria.IDProductoCategoria = p.ProductoCategoria.IDProductoCategoria;
                foreach (var precioDetalle in p.PrecioDetalle)
                {
                    if (precioDetalle.IDListaPrecio == IDListaPrecioActual)
                    {
                        if ((bool)precioDetalle.Activo)
                        {
                            Prod.PrecioActual = (double)precioDetalle.Precio;
                        }
                    }
                }
                if (Prod.PrecioActual != 0)
                {
                    ListaP.Add(Prod);
                }
            }
            List<BIZProductoCategoria> AuxcatList = new List<BIZProductoCategoria>();
            foreach (var p in ListaP.Select(x => x.ProductoCategoria).Distinct())
            {
                AuxcatList.Add(p);
            }
            List<BIZProductoCategoria> catList = new List<BIZProductoCategoria>();
            foreach (var c in AuxcatList)
            {
                bool existe = false;
                foreach (var h in catList)
                {
                    if (h.IDProductoCategoria == c.IDProductoCategoria)
                    {
                        existe = true;
                    }
                }
                if (!existe)
                {
                    catList.Add(c);
                }
            }
            //agrego los productos a la categoria
            foreach (var c in catList)
            {
                foreach (var prod in ListaP)
                {
                    if (prod.ProductoCategoria.IDProductoCategoria == c.IDProductoCategoria)
                    {
                        c.Producto.Add(prod);
                    }
                }
            }


            ViewBag.categorias2 = catList;


           // var producto = db.Producto
            //    .Where(b

            //var producto = db.Producto
            //    .Where(b => b.IDEstado == 3)
            //    .Where(b => b.PrecioDetalle.



            var categorias = db.ProductoCategoria;
            ViewBag.categorias = categorias.ToList();
            //return View(producto.ToList());
            return View();
        }

        public class palabra
        {
            public string name { get; set; }
        }

        [HttpPost]
        [ActionName("AjaxRequest")]
        public ActionResult AjaxRequest(palabra b)
        {
            //Prueba DB, ya no lo uso en este ejemplo
            // var alimento = db.alimento.Where(a => a.Nombre == b.Name)
            SL.Utils utils = new SL.Utils();
            BIZ.BIZCorreo correo = new BIZ.BIZCorreo();
            correo.Body = "Cuerpo";
            correo.To = "stingo@stingo.com.ar";
            correo.Subject = "subjectttt";
            utils.sendMail(correo);


            return Json(b, JsonRequestBehavior.AllowGet);

        }


  

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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

