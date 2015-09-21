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
using SL;
using Rotativa;
using BLL;

namespace TFITest4.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        public static int gIdPedido;
        DAL.DALProducto ProdWorker = new DAL.DALProducto();
        DAL.DALDocumento DocWorker = new DAL.DALDocumento();
        BLLBitacora Bita = new BLLBitacora();


        //
        // GET: /Pedido/
        public ActionResult Index()
        {



            var ListaP = ProdWorker.getProductosConPrecio();
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




        [HttpPost]
        [ActionName("GetInit")]
        public ActionResult GetInit(string a)
        {
            //viejo
            //var ListCarrito = (Session["ListCarrito"] as List<modelCarrito>) ?? new List<modelCarrito>();
            //nuevo
            try
            {
                var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
                ViewBag.NrPedido = ListCarrito.IDDocumento;
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                string rIVA = "";
                if (UsuarioIN.TipoUsuario.IDTipoUsuario == 2)
                {
                    double IVA = UsuarioIN.ClienteEmpresa.TipoIVA.Valor;
                    rIVA = IVA.ToString();
                }
                else
                {
                    rIVA = "0";
                }

                return Json(new { ListCarrito, rIVA }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "error" }, JsonRequestBehavior.AllowGet);
            }
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
                    PrePedido.IDEstado = 15; //estado activo de PrePedido 15
                    PrePedido.IDUsuarioCreacion = UsuarioIN.IDUsuario;
                    PrePedido.IDUsuarioUltimaModificacion = UsuarioIN.IDUsuario;
                    PrePedido.FechaContable = null;
                    PrePedido.FechaVencimiento = null;
                    //DocumentoDetalle detalle;
                    foreach (modelCarrito ItemCarrito in ListCarrito.Productos)
                    {
                        detalle = new BIZDocumentoDetalle();
                        detalle.Cantidad = ItemCarrito.Cant;
                        detalle.IDPrecioDetalle = ItemCarrito.IDPrecioDetalle;
                        PrePedido.DocumentoDetalle.Add(detalle);
                    }
                    int IDDocNuevo = DocWorker.SaveDocumento(PrePedido);
                    ListCarrito.IDDocumento = IDDocNuevo;
                    //db.Documento.Add(PrePedido);
                    //db.SaveChanges();
                }
                else //Es un PrePedido modificado
                {
                    DocWorker.RemoveDetalleDoc(ListCarrito.IDDocumento);
                    BIZDocumento PrePedido = DocWorker.getDocByID(ListCarrito.IDDocumento);
                    PrePedido.FechaUltimaModificacion = DateTime.Now;
                    PrePedido.IDUsuarioUltimaModificacion = UsuarioIN.IDUsuario;
                    foreach (modelCarrito ItemCarrito in ListCarrito.Productos)
                    {
                        detalle = new BIZDocumentoDetalle();
                        detalle.Cantidad = ItemCarrito.Cant;
                        detalle.IDPrecioDetalle = ItemCarrito.IDPrecioDetalle;
                        PrePedido.DocumentoDetalle.Add(detalle);
                    }
                    DocWorker.UpdateDocumento(PrePedido);
                    //tengo q recargar la sesion del carrito
                    //db.Entry(documento).State = EntityState.Modified;
                    //db.SaveChanges();
                    //todavia nada....
                }
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.AlertError = Language.ErrorLogInAgain;
                return Json(new { Result = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult PedidoAjax2(ModelPedido item)
        {
            try
            {
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
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
                double IVA = UsuarioIN.ClienteEmpresa.TipoIVA.Valor;
                string rIVA = IVA.ToString();
                return Json(new { ListCarrito, rIVA }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er)
            {
                ViewBag.AlertError = Language.ErrorLogInAgain;
                return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Prepedidos()
        {
            try
            {
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                var docs = DocWorker.getDocsByEmpresa(UsuarioIN.ClienteEmpresa.IDClienteEmpresa, 7, 15); //7 es el tipo de documento. Acá PrePedido. 15 es el estado aca Activo de prepedido
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
            try
            {

                int IDPrePedido = int.Parse(PrePed);
                //aca cargo los datos del Prepedido en la sesion
                var PrePedido = DocWorker.getDocByID(IDPrePedido);
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
                var ListaP = ProdWorker.getProductosConPrecio();
                Boolean PrePedidoActualizado = false;
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

                int idUser = (int)Session["userID"];
                int NrPrePedido = Convert.ToInt32(PrePed);
                DocWorker.UpdateStatusDoc(NrPrePedido, 16, idUser); //16 es cancelado de PrePedido
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
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"]; //q usuario?
                //primero tengo q ver por cada producto del carrito si hay stock.
                //cargo el carrito q está en sesion.
                var ListCarrito = (Session["ListCarrito"] as ListCarrito) ?? new ListCarrito();
                //stockCarrito Stock = new stockCarrito();

                Boolean TodoOK = false;
                if (ListCarrito.Productos.Count != 0)
                {
                    TodoOK = true;
                    foreach (var p in ListCarrito.Productos)
                    {
                        //p.stock = 1; // aca me traería el stock del producto
                        p.stock = ProdWorker.CheckStockProd(p.id);
                        if (p.Cant > p.stock)
                        {
                            TodoOK = false;
                        }
                    }
                    if (TodoOK)
                    {
                        //aca hago el submit del pedido
                        BIZDocumento pedido = new BIZDocumento();
                        pedido.IDDocumentoTipo = 3; //tipo 3 es pedido
                        pedido.FechaEmision = DateTime.Now;
                        pedido.FechaUltimaModificacion = pedido.FechaEmision;
                        pedido.IDClienteEmpresa = UsuarioIN.IDClienteEmpresa;
                        pedido.IDEstado = 5; //estado activo de pedido 5 es "Pendiente de aprobacion"
                        pedido.IDUsuarioCreacion = UsuarioIN.IDUsuario;
                        pedido.IDUsuarioUltimaModificacion = UsuarioIN.IDUsuario;
                        BIZDocumentoDetalle detalle;
                        foreach (modelCarrito ItemCarrito in ListCarrito.Productos)
                        {
                            detalle = new BIZDocumentoDetalle();
                            detalle.Cantidad = ItemCarrito.Cant;
                            detalle.IDPrecioDetalle = ItemCarrito.IDPrecioDetalle;
                            pedido.DocumentoDetalle.Add(detalle);
                        }
                        int IDDocNuevo = DocWorker.SaveDocumento(pedido);

                        devolver = @Language.OKNormal;
                        return Json(new { Result = devolver, CarritoStock = "ir" }, JsonRequestBehavior.AllowGet);
                    }
                    else //algun producto no tiene stock. Lo veo con JS.
                    {
                        return Json(new { Result = devolver, CarritoStock = ListCarrito }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex) { devolver = @Language.ErrorLogInAgain; }
            return Json(new { Result = devolver, CarritoStock = "" }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Pedido()
        {
            try
            {
                BIZUsuario UsuarioIN = (BIZUsuario)Session["SUsuario"];
                var docs = DocWorker.getDocsByEmpresa(UsuarioIN.ClienteEmpresa.IDClienteEmpresa, 3, -1); //3 es el tipo de documento. Acá pedido. -1 es para traer todos
                float monto;
                foreach (var doc in docs)
                {
                    monto = 0;
                    foreach (var det in doc.DocumentoDetalle)
                    {
                        monto += (float)(det.Cantidad * det.PrecioDetalle.Precio);
                    }
                    doc.Monto = monto + (monto * UsuarioIN.ClienteEmpresa.TipoIVA.Valor / 100);
                }
                ViewBag.IVA = UsuarioIN.ClienteEmpresa.TipoIVA.Valor;
                ViewBag.Empresa = UsuarioIN.ClienteEmpresa.Nombre;
                return View(docs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("CerrarSesion", "Login");
            }
        }

        public ActionResult Controlar()
        {
            try
            {
                var docs = DocWorker.getDocsByEstado(3, 5); //3 es el tipo del documento documento. Acá pedido. 5 es el estado del doc. acá pendiente de aprobación
                //var docs = DALDoc.ge
                float monto;
                int IDEmpresa;

                foreach (var doc in docs)
                {
                    monto = 0;
                    foreach (var det in doc.DocumentoDetalle)
                    {
                        monto += (float)(det.Cantidad * det.PrecioDetalle.Precio);
                    }
                    doc.Monto = monto + (monto * doc.ClienteEmpresa.TipoIVA.Valor / 100);
                    IDEmpresa = (int)doc.IDClienteEmpresa;
                    double TCCstatus = DocWorker.CheckCCStatus(IDEmpresa);
                    doc.CCStatus = (TCCstatus + (TCCstatus * doc.ClienteEmpresa.TipoIVA.Valor / 100));
                }
                //ViewBag.IVA = UsuarioIN.ClienteEmpresa.TipoIVA.Valor;
                //ViewBag.Empresa = UsuarioIN.ClienteEmpresa.Nombre;
                return View(docs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("CerrarSesion", "Login");
            }
        }


        public ActionResult PedidoPDF(string NrPedido)
        {
            //var model = new GeneratePDFModel();
            //Code to get content
            // return new Rotativa.ViewAsPdf("GeneratePDF", model){FileName = "TestViewAsPdf.pdf"}
            gIdPedido = Convert.ToInt32(NrPedido);
            return new ActionAsPdf("makePDF") { FileName = "invoice" + gIdPedido + ".pdf" };
            // return new ActionAsPdf("Index") { FileName = "Test.pdf" };
        }

        public ActionResult makePDF()
        {
            var doc = DocWorker.getDocByID(gIdPedido);
            //Utils utils = new Utils();
            //string codigo = "123456";
            //utils.generaCodigoBarras(codigo);


            //ViewBag.Barcode = codigo + ".jpg";
            return View(doc);
        }


        public ActionResult VerPedido(string Pedido)
        {
            try
            {

                int IDPedido = int.Parse(Pedido);
                //aca cargo los datos del Prepedido en la sesion
                var PrePedido = DocWorker.getDocByID(IDPedido);
                ListCarrito carritoPedido = new ListCarrito();
                carritoPedido.IDDocumento = PrePedido.IDDocumento;
                modelCarrito item;
                foreach (var p in PrePedido.DocumentoDetalle)
                {
                    item = new modelCarrito();
                    item.id = p.PrecioDetalle.Producto.IDProducto;
                    item.Nombre = p.PrecioDetalle.Producto.Nombre;
                    item.IDPrecioDetalle = p.IDPrecioDetalle;
                    item.Cant = p.Cantidad;
                    item.Precio = (double)p.PrecioDetalle.Precio;
                    carritoPedido.Productos.Add(item);
                }


                //Session["CarritoPedido"] = carritoPedido;
                ViewBag.NrPedido = carritoPedido.IDDocumento;
                return Json(new { Result = "", Pedido = carritoPedido }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("CerrarSesion", "Login");
            }

        }


        public ActionResult AprobarRechazarPedido(string Pedido, string tipoActualizacion, string justif)
        {
            try
            {

                int IDPedido = int.Parse(Pedido);
                int idUser = (int)Session["userID"];
                int Tstatus = int.Parse(tipoActualizacion);
                int status;
                if (Tstatus == 1)
                {
                    status = 6;
                }
                else
                {
                    status = 22;
                }
                

                BIZDocumento doc = new BIZDocumento();
                if (justif != "")
                    DocWorker.UpdateStatusDoc(IDPedido, status, idUser, justif);
                else
                    DocWorker.UpdateStatusDoc(IDPedido, status, idUser);


                Nullable<int> idU = null;
                string ip = "Unknown";
                try { idU = (int)Session["userID"]; } catch (Exception ex) { }
                try { ip = Session["_ip"].ToString(); }  catch (Exception ex) { }
                string inf = "";
                if (status == 6)
                    inf = "Se ha aprobado el pedido nr ";
                else
                    inf = "Se ha rechazado el pedido nr ";

                    Bita.guardarBitacora(new BIZBitacora("Informativo", inf + IDPedido, idU, ip));

                return Json(new { Result = ""}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}