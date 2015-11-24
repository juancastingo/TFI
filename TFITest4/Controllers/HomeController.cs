using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;

namespace TFITest4.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLDocumento docWorker = new BLLDocumento();


        public ActionResult Index()
        {
           
              //BIZBitacora bitacora = new BIZBitacora("Bitacora test2",null);
            //Bita.guardarBitacora(new BIZBitacora("Bitacora test3", null));

            if (TempData["Resultado"] != null)
                @ViewBag.AlertOK = TempData["Resultado"].ToString();

            ViewBag.url = Request.Url.Host.ToLower();
            ViewBag.numero = "3".PadLeft(8, '0'); //para la factura

            if (Session["SUsuario"] != null)
            {
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                ViewBag.UserGroup = UsuarioIN.TipoUsuario.Tipo;
            }

            


            ////traer productos con precios
            //var ListaProdPrecio = db.Database.SqlQuery<string>(
            //           "SELECT * FROM dbo.Producto");

            ////var producto = db.Producto.

            ////var ListaPrecio = db.ListaPrecio
            ////    .Where(b => b.FechaDesde < DateTime.Now)
            ////    .OrderByDescending(b => b.FechaDesde).FirstOrDefault();

            ////aca agarro el ID de la lista de precios vigente
            //var ListaPrecio = db.Database.SqlQuery<int>("select TOP 1 IDListaPrecio from ListaPrecio where FechaDesde < GETDATE() and Activo = 1 order by FechaDesde Desc");
            //int IDListaPrecioActual = ListaPrecio.FirstOrDefault();

            ////aca me traigo la lista con el ID
            //var ListaPrecios = db.PrecioDetalle
            //    .Where(b => b.IDListaPrecio == IDListaPrecioActual);

            ////aca traigo todos los producto que tengan estado "3" //Activo
            //var productos = db.Producto
            //    .Where(b => b.IDEstado == 3);

            ////List<Producto> Lista = new List<Producto>();
            //List<BIZProducto> ListaP = new List<BIZProducto>();
            //foreach (var p in productos)
            //{
            //    BIZProducto Prod = new BIZProducto();
            //    Prod.Nombre = p.Nombre;
            //    Prod.Imagen = p.Imagen;
            //    Prod.Descripcion = p.Descripcion;
            //    Prod.ProductoCategoria.Detalle = p.ProductoCategoria.Detalle;
            //    Prod.ProductoCategoria.IDProductoCategoria = p.ProductoCategoria.IDProductoCategoria;
            //    foreach (var precioDetalle in p.PrecioDetalle)
            //    {
            //        if (precioDetalle.IDListaPrecio == IDListaPrecioActual)
            //        {
            //            if ((bool)precioDetalle.Activo) //aca me fijo si está activo
            //            {
            //                Prod.PrecioActual = (double)precioDetalle.Precio;
            //            }
            //        }
            //    }
            //    if (Prod.PrecioActual != 0) // si no tiene precio no lo agrego...
            //    {
            //        ListaP.Add(Prod);
            //    }
            //}

            //DAL.DALProducto DalWorker = new DAL.DALProducto();
            //var ListaP = DalWorker.getProductosConPrecio();
            ////esto es para limpiar categorias repetidas
            //List<BIZProductoCategoria> AuxcatList = new List<BIZProductoCategoria>();
            //foreach (var p in ListaP.Select(x => x.ProductoCategoria).Distinct())
            //{
            //    AuxcatList.Add(p);
            //}
            //List<BIZProductoCategoria> catList = new List<BIZProductoCategoria>();
            //foreach (var c in AuxcatList)
            //{
            //    bool existe = false;
            //    foreach (var h in catList)
            //    {
            //        if (h.IDProductoCategoria == c.IDProductoCategoria)
            //        {
            //            existe = true;
            //        }
            //    }
            //    if (!existe)
            //    {
            //        catList.Add(c);
            //    }
            //}
            ////agrego los productos a la categoria
            //foreach (var c in catList)
            //{
            //    foreach (var prod in ListaP)
            //    {
            //        if (prod.ProductoCategoria.IDProductoCategoria == c.IDProductoCategoria)
            //        {
            //            c.Producto.Add(prod);
            //        }
            //    }
            //}


            //ViewBag.categorias2 = catList;

            //var categorias = db.ProductoCategoria;
            //ViewBag.categorias = categorias.ToList();

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


        public ActionResult siempre()
        {
            try
            {
                string userIpAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                if (userIpAddress == "::1")
                    Session["_ip"] = "127.0.0.1";
                else
                    Session["_ip"] = userIpAddress;
            }
            catch (Exception ex)
            {
                Session["_ip"] = "Unknown";
            }

            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PDFMaker(string fghjhtyuighj)
        {
            try
            {
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                if (UsuarioIN != null)
                {
                    if (UsuarioIN.TipoUsuario.Tipo == "Externo")
                    {
                        BIZDocumento doc = docWorker.ObtenerDocXID(Convert.ToInt32(fghjhtyuighj));
                        if (doc.ClienteEmpresa.IDClienteEmpresa == UsuarioIN.ClienteEmpresa.IDClienteEmpresa)
                        {
                            ViewBag.nr = Convert.ToInt32(fghjhtyuighj);
                        }
                        else
                        {
                            ViewBag.nr = -1;
                        }
                    }
                    else
                    {
                        ViewBag.nr = Convert.ToInt32(fghjhtyuighj);
                    }
                }
                else
                {
                    ViewBag.nr = 0;
                }
            }
            catch (Exception ex) {
                ViewBag.nr = 0;
            }
            return View();
        }


        public FilePathResult GetFileFromDisk()
        {
            return File("/Content/", "multipart/form-data", "iid.apk");
        }

    }
}

