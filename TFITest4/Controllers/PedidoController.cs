using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.ORM;
using BIZ;
using TFITest4.Resources;
using System.Data.Objects;
using System.Data.SqlClient;

namespace TFITest4.Controllers
{
    public class PedidoController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /Pedido/
        [Authorize]
        public ActionResult Index()
        {
           
            //var ListaPrecio = db.Database.SqlQuery<int>("select TOP 1 IDListaPrecio from ListaPrecio where FechaDesde < GETDATE() and Activo = 1 order by FechaDesde Desc");
            //int IDListaPrecioActual = ListaPrecio.FirstOrDefault();
            //var ListaPrecios = db.PrecioDetalle
            //    .Where(b => b.IDListaPrecio == IDListaPrecioActual);
            //var productos = db.Producto.Include(p => p.EstadoMisc).Include(p => p.ProductoCategoria)
            //    .Where(b => b.IDEstado == 3);
            //List<BIZProducto> ListaP = new List<BIZProducto>();
            //BIZProducto Prod;
            //foreach (var p in productos)
            //{
            //    Prod = new BIZProducto();
            //    Prod.IDProducto = p.IDProducto;
            //    Prod.Nombre = p.Nombre;
            //    Prod.Imagen = p.Imagen;
            //    Prod.Descripcion = p.Descripcion;
            //    Prod.ProductoCategoria.Detalle = p.ProductoCategoria.Detalle;
            //    Prod.ProductoCategoria.IDProductoCategoria = p.ProductoCategoria.IDProductoCategoria;
            //    foreach (var precioDetalle in p.PrecioDetalle)
            //    {
            //        if (precioDetalle.IDListaPrecio == IDListaPrecioActual)
            //        {
            //            if ((bool)precioDetalle.Activo)
            //            {
            //                Prod.PrecioActual = (double)precioDetalle.Precio;
            //                Prod.IDPrecioDetalle = precioDetalle.IDPrecioDetalle;

            //            }
            //        }
            //    }
            //    if (Prod.PrecioActual != 0)
            //    {
            //        ListaP.Add(Prod);
            //    }
            //}

            DAL.DALProducto DalWorker = new DAL.DALProducto();
            var ListaP = DalWorker.getProductosConPrecio();
            // guardo todo en sesion
            Session["productosSesion"] = ListaP;

            // carrito
            //viejo
            //var cartObjects = (Session["CartObjects"] as List<modelCarrito>) ?? new List<modelCarrito>();
            //nuevo
            var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
            double Subtotal = 0;
            if (ListCarrito.Productos.Count != 0)
            {
                foreach (modelCarrito elem in ListCarrito.Productos)
                {
                    Subtotal += (double)elem.Precio * (int)elem.Cant;
                }
            }
            ViewBag.subtotal = Subtotal;
            ViewBag.NrPedido = ListCarrito.IDDocumento;



            return View(ListaP);
        }

        public class ListCarrito
        {
            public ListCarrito()
            {
                this.Productos = new List<modelCarrito>();
            }
            public List<modelCarrito> Productos { get; set; }
            public int IDDocumento { get; set; }
        }

        public class modelCarrito
        {
            public int id { get; set; }
            public int Cant { get; set; }
            public string Nombre { get; set; }
            public double Precio { get; set; }
            public int IDPrecioDetalle { get; set; }
            public int stock { get; set; }
        }

        public class ModelPedido
        {
            public string id { get; set; }
            public string Cantidad { get; set; }
        }


        //viejo pedido ajax sin ID en la lista
        [HttpPost]
        public ActionResult PedidoAjax(ModelPedido item)
        {
            try
            {
                var productosSesion = Session["productosSesion"];
                var ListaP = (List<BIZProducto>)productosSesion;
                var ListCarrito = (Session["ListCarrito"] as List<modelCarrito>) ?? new List<modelCarrito>();
                int id = Convert.ToInt32(item.id);
                int cantidad = Convert.ToInt32(item.Cantidad);
                if (ListCarrito.Count == 0)
                {

                    modelCarrito carri = new modelCarrito();
                    carri.id = id;
                    //carri. = b.Name;
                    carri.Cant = cantidad;
                    // carri.Precio = Convert.ToInt32(item.);
                    if (cantidad > 0) //evitamos negativos
                    {
                        foreach (var p in ListaP)
                        {
                            if (carri.id == p.IDProducto)
                            {
                                carri.Precio = p.PrecioActual;
                                carri.Nombre = p.Nombre;
                                carri.IDPrecioDetalle = p.IDPrecioDetalle;
                            }
                        }
                    }
                    //carri.Precio = 100;
                    //var cartObjects = (Session["CartObjects"] as List<carrito>) ?? new List<carrito>();
                    ListCarrito.Add(carri);
                    Session["ListCarrito"] = ListCarrito;
                }
                else
                {
                    bool existe = false;

                    for (int i = ListCarrito.Count - 1; i >= 0; i--)
                    {
                        //  foreach (carrito elem in cartObjects)
                        // {

                        if (id == ListCarrito[i].id)
                        {
                            ListCarrito[i].Cant += cantidad;
                            existe = true;
                        }
                        if (ListCarrito[i].Cant <= 0)
                        {
                            ListCarrito.RemoveAt(i);
                        }

                    }

                    if (!existe)
                    {
                        modelCarrito carri = new modelCarrito();
                        carri.id = id;
                        //carri.Name = b.Name;
                        carri.Cant = cantidad;
                        foreach (var p in ListaP)
                        {
                            if (carri.id == p.IDProducto)
                            {
                                carri.Precio = p.PrecioActual;
                                carri.Nombre = p.Nombre;
                                carri.IDPrecioDetalle = p.IDPrecioDetalle;
                            }
                        }
                        if (carri.Cant > 0)  // esto para evitar cantidades negativas
                        {
                            ListCarrito.Add(carri);
                        }
                        //Session["CartObjects"] = cartObjects;

                    }
                }

                Session["ListCarrito"] = ListCarrito;



                double Subtotal = 0;

                foreach (modelCarrito elem in ListCarrito)
                {
                    Subtotal += (double)elem.Precio * (int)elem.Cant;
                }

                return Json(ListCarrito, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                ViewBag.AlertError = "Please reload the page";
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [ActionName("GetInit")]
        public ActionResult GetInit(string a)
        { 
            //viejo
            //var ListCarrito = (Session["ListCarrito"] as List<modelCarrito>) ?? new List<modelCarrito>();
            //nuevo
            var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
            ViewBag.NrPedido = ListCarrito.IDDocumento;
           
            return Json(ListCarrito, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LimpiarCarrito()
        {
            //viejo
            //Session["ListCarrito"] = null;
            //nuevo solo borro los items y no el id del PrePedido
            var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
            ListCarrito.Productos = new List<modelCarrito>();
            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarCarrito()
        {
            try
            {
                var ListCarrito = (Session["ListCarrito"] as ListCarrito);
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                DAL.DALDocumento DalWorker = new DAL.DALDocumento();
                BIZDocumentoDetalle detalle;
                if (ListCarrito.IDDocumento == 0) //es un PrePedido Nuevo
                {
                    var productosSesion = Session["productosSesion"];
                    var ListaP = (List<BIZProducto>)productosSesion;
                    BIZDocumento PrePedido = new BIZDocumento();
                    //Documento PrePedido = new Documento();
                    PrePedido.IDDocumentoTipo = 7;
                    PrePedido.FechaEmision = DateTime.Now;
                    PrePedido.FechaUltimaModificacion = PrePedido.FechaEmision;
                    PrePedido.IDClienteEmpresa = UsuarioIN.IDClienteEmpresa;
                    PrePedido.IDEstado = 15; //estado activo de PrePedido 16 es "Cancelado"
                    PrePedido.IDUsuarioCreacion = UsuarioIN.IDUsuario;
                    PrePedido.IDUsuarioUltimaModificacion = UsuarioIN.IDUsuario;
                    //DocumentoDetalle detalle;
                    foreach (modelCarrito ItemCarrito in ListCarrito.Productos)
                    {
                        detalle = new BIZDocumentoDetalle();
                        detalle.Cantidad = ItemCarrito.Cant;
                        detalle.IDPrecioDetalle = ItemCarrito.IDPrecioDetalle;
                        PrePedido.DocumentoDetalle.Add(detalle);
                    }
                    int IDDocNuevo = DalWorker.SaveDocumento(PrePedido);
                    ListCarrito.IDDocumento = IDDocNuevo;
                    //db.Documento.Add(PrePedido);
                    //db.SaveChanges();
                }
                else //Es un PrePedido modificado
                {
                    DalWorker.RemoveDetalleDoc(ListCarrito.IDDocumento);
                    BIZDocumento PrePedido = DalWorker.getDocByID(ListCarrito.IDDocumento);
                    PrePedido.FechaUltimaModificacion = DateTime.Now;
                    PrePedido.IDUsuarioUltimaModificacion = UsuarioIN.IDUsuario;
                    foreach (modelCarrito ItemCarrito in ListCarrito.Productos)
                    {
                        detalle = new BIZDocumentoDetalle();
                        detalle.Cantidad = ItemCarrito.Cant;
                        detalle.IDPrecioDetalle = ItemCarrito.IDPrecioDetalle;
                        PrePedido.DocumentoDetalle.Add(detalle);
                    }
                    DalWorker.UpdateDocumento(PrePedido);
                    //tengo q recargar la sesion del carrito
                    //db.Entry(documento).State = EntityState.Modified;
                    //db.SaveChanges();
                    //todavia nada....
                }
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            } catch(Exception ex) {
                ViewBag.AlertError = Language.ErrorLogInAgain;
                return Json(new { Result = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult PedidoAjax2(ModelPedido item)
        {
            try
            {
                var productosSesion = Session["productosSesion"];
                var ListaP = (List<BIZProducto>)productosSesion;
                var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
                int id = Convert.ToInt32(item.id);
                int cantidad = Convert.ToInt32(item.Cantidad);
                if (ListCarrito.Productos.Count == 0)
                {

                    modelCarrito carri = new modelCarrito();
                    carri.id = id;
                    //carri. = b.Name;
                    carri.Cant = cantidad;
                    // carri.Precio = Convert.ToInt32(item.);
                    if (cantidad > 0) //evitamos negativos
                    {
                        foreach (var p in ListaP)
                        {
                            if (carri.id == p.IDProducto)
                            {
                                carri.Precio = p.PrecioActual;
                                carri.Nombre = p.Nombre;
                                carri.IDPrecioDetalle = p.IDPrecioDetalle;
                                break;
                            }
                        }
                        ListCarrito.Productos.Add(carri);
                        Session["ListCarrito"] = ListCarrito;
                    }
                    //carri.Precio = 100;
                    //var cartObjects = (Session["CartObjects"] as List<carrito>) ?? new List<carrito>();

                }
                else
                {
                    bool existe = false;

                    for (int i = ListCarrito.Productos.Count - 1; i >= 0; i--)
                    {
                        //  foreach (carrito elem in cartObjects)
                        // {

                        if (id == ListCarrito.Productos[i].id)
                        {
                            ListCarrito.Productos[i].Cant += cantidad;
                            existe = true;
                        }
                        if (ListCarrito.Productos[i].Cant <= 0)
                        {
                            ListCarrito.Productos.RemoveAt(i);
                        }

                    }

                    if (!existe)
                    {
                        modelCarrito carri = new modelCarrito();
                        carri.id = id;
                        //carri.Name = b.Name;
                        carri.Cant = cantidad;
                        foreach (var p in ListaP)
                        {
                            if (carri.id == p.IDProducto)
                            {
                                carri.Precio = p.PrecioActual;
                                carri.Nombre = p.Nombre;
                                carri.IDPrecioDetalle = p.IDPrecioDetalle;
                            }
                        }
                        if (carri.Cant > 0)  // esto para evitar cantidades negativas
                        {
                            ListCarrito.Productos.Add(carri);
                        }
                        //Session["CartObjects"] = cartObjects;

                    }
                }

                Session["ListCarrito"] = ListCarrito;



                double Subtotal = 0;

                foreach (modelCarrito elem in ListCarrito.Productos)
                {
                    Subtotal += (double)elem.Precio * (int)elem.Cant;
                }

                return Json(ListCarrito, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                ViewBag.AlertError = Language.ErrorLogInAgain;
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Prepedidos()
        {
            try
            {
                DAL.DALDocumento DALDoc = new DAL.DALDocumento();
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                var docs = DALDoc.getDocsByEmpresa(UsuarioIN.ClienteEmpresa.IDClienteEmpresa, 7, 15); //7 es el tipo de documento. Acá PrePedido. 15 es el estado aca Activo de prepedido
                float monto;
                foreach (var doc in docs)
                {
                    monto = 0;
                    foreach (var det in doc.DocumentoDetalle)
                    {
                        monto += (float)(det.Cantidad * det.PrecioDetalle.Precio);
                    }
                    doc.Monto = monto;
                }

                ViewBag.Empresa = UsuarioIN.ClienteEmpresa.Nombre;
                return View(docs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("CerrarSesion", "Login");
            }
        }

        public ActionResult CargarPedidoViejo(string PrePed)
        {
            try {

                int IDPrePedido = int.Parse(PrePed);
                //aca cargo los datos del Prepedido en la sesion
                DAL.DALDocumento DALDoc = new DAL.DALDocumento();
                var PrePedido = DALDoc.getDocByID(IDPrePedido);
                ListCarrito carrito = new ListCarrito();
                carrito.IDDocumento = PrePedido.IDDocumento;
                modelCarrito item;
                foreach (var p in PrePedido.DocumentoDetalle)
                {
                    item = new modelCarrito();
                    item.id = p.PrecioDetalle.Producto.IDProducto;
                    item.Nombre = p.PrecioDetalle.Producto.Nombre;
                    item.IDPrecioDetalle = p.IDPrecioDetalle;
                    item.Cant = p.Cantidad;
                    item.Precio = (double)p.PrecioDetalle.Precio;
                    carrito.Productos.Add(item);
                }
                //ya tengo el carrito. Acá tengo q verificar los precios y actualizarlo.
                //agarro los productos con precios.
                DAL.DALProducto DALProductoWorker = new DAL.DALProducto();
                var ListaP = DALProductoWorker.getProductosConPrecio();
                Boolean PrePedidoActualizado  = false;
                foreach (var prodCarr in carrito.Productos)
                {
                    foreach (var prodBase in ListaP)
                    {
                        if (prodCarr.id == prodBase.IDProducto)
                        {
                            if (prodCarr.IDPrecioDetalle != prodBase.IDPrecioDetalle)
                            {
                                prodCarr.IDPrecioDetalle = prodBase.IDPrecioDetalle;
                                prodCarr.Precio = prodBase.PrecioActual;
                                PrePedidoActualizado = true;
                            }
                            break;
                        }
                    }
                }
                string msgReturn = "";
                if (PrePedidoActualizado)
                {
                    msgReturn = Language.PrePedidoActualizado;
                }

                Session["ListCarrito"] = carrito;
                ViewBag.NrPedido = carrito.IDDocumento;
                return Json(new { Result = msgReturn }, JsonRequestBehavior.AllowGet);
                }
            catch (Exception ex)
            {
                return RedirectToAction("CerrarSesion", "Login");
            }

        }


        public ActionResult DescartarPrePedido(string PrePed)
        {
            try
            {
                DAL.DALDocumento DalWorker = new DAL.DALDocumento();
                int NrPrePedido = Convert.ToInt32(PrePed);
                DalWorker.UpdateStatusDoc(NrPrePedido, 16); //16 es cancelado de PrePedido
                var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
                if (ListCarrito.IDDocumento == NrPrePedido)
                {
                    Session["ListCarrito"] = null;
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NuevoPrePedido()
        {
            try
            {
                Session["ListCarrito"] = null;
            }
            catch (Exception ex) { }
            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }

        //public  class stockCarrito {
        //    public stockCarrito()
        //    {
        //        this.Productos = new List<modelCarrito>();
        //    }
        //    public int IDCarrito;
        //    public List<modelCarrito> Productos { get; set; }
        //}

        public ActionResult SubmitPedido()
        {
            string devolver = "";
            try
            {
                //prueba stock
                //db.
                //db.Database.<"Exec StockCheck @IDProd",1>();
                //int StockProdCant = db.Database.ExecuteSqlCommand("Exec StockCheck @IDProd= " + 1);
                //int StockProdCant = db.Database.SqlQuery<int>("Exec StockCheck @IDProd", 1);
              

                //int StockProdCant = db.StockCheck(idParam);
                //System.Nullable<int> iReturnValue = db.StockCheck(3).SingleOrDefault();
                 //primero tengo q ver por cada producto del carrito si hay stock.
                 //cargo el carrito q está en sesion.
                var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
                DAL.DALProducto ProdWorker = new DAL.DALProducto();
                //stockCarrito Stock = new stockCarrito();

                Boolean TodoOK = false;
                if (ListCarrito.Productos.Count != 0)
                {
                    TodoOK = true;
                    foreach (var p in ListCarrito.Productos)
                   {
                       //p.stock = 1; // aca me traería el stock del producto
                       p.stock = ProdWorker.CheckStockProd(p.id);
                        if (p.Cant > p.stock) {
                            TodoOK = false;
                        }
                 }
                    if (TodoOK)
                    {
                        //aca hago el submit del pedido
                        devolver = @Language.OKNormal;
                    }
                    else //algun producto no tiene stock. Lo veo con JS.
                    {
                        return Json(new { Result = devolver, CarritoStock = ListCarrito }, JsonRequestBehavior.AllowGet);
                    }
             }
                }
            catch (Exception ex) { devolver = @Language.ErrorLogInAgain; }
            return Json(new { Result = devolver }, JsonRequestBehavior.AllowGet);
        }






        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}