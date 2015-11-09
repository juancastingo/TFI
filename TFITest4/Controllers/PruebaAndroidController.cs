using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace TFITest4.Controllers
{
    public class PruebaAndroidController : Controller
    {
        //
        // GET: /PruebaAndroid/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLDocumento docWorker = new BLLDocumento();

        public class persona
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }
        private class modelDoc
        {
            public int IDDocumento { get; set; }
            public Nullable<int> NrDocumento { get; set; }
            public Nullable<int> IDDocumentoTipo { get; set; }
            public Nullable<System.DateTime> FechaEmision { get; set; }
            public Nullable<System.DateTime> FechaContable { get; set; }
            public string ClienteEmpresa { get; set; }
            public Nullable<int> IDProveedor { get; set; }
            public Nullable<System.DateTime> FechaVencimiento { get; set; }
            public double Monto { get; set; }
            public double IVA { get; set; }
            public Nullable<int> IDEstado { get; set; }
            public Nullable<int> IDUsuarioCreacion { get; set; }
            public Nullable<int> IDDocumentoRef { get; set; }
            public string EmpresaLocal { get; set; }
            public Nullable<int> CodigoExterno { get; set; }
            public string IPCreacion { get; set; }
            public Nullable<int> IDUsuarioUltimaModificacion { get; set; }
            public Nullable<System.DateTime> FechaUltimaModificacion { get; set; }
            public string IPUltimaModificacion { get; set; }
        }

        private class modelDetalle
        {
            public string producto { get; set; }
            public int cantidad { get; set; }
            public double valor { get; set; }
        }

        private class modelDoc2
        {
            public int IDDocumento { get; set; }
            //public Nullable<int> NrDocumento { get; set; }
            //public Nullable<int> IDDocumentoTipo { get; set; }
            public string FechaEmision { get; set; }
            public double Monto { get; set; }
            public string Estado { get; set; }
            public List<modelDetalle> detalles { get; set; }
            public double IVA { get; set; }
        
        }

        public ActionResult Pedidos(string empresa,string password)
        {
            int empresaID = Convert.ToInt32(empresa);
            var docs = docWorker.ObtenerDocsXEmpresa(empresaID,3);
            modelDoc mdoc;
            List<modelDoc> listDoc = new List<modelDoc>();
            foreach (var d in docs)
            {
                if (d.IDEstado == 5)
                {
                    mdoc = new modelDoc();
                    mdoc.IDDocumento = d.IDDocumento;
                    mdoc.EmpresaLocal = d.ClienteEmpresa.NombreFantasia;
                    mdoc.Monto = 0;
                    mdoc.IVA = d.ClienteEmpresa.TipoIVA.Valor;
                    foreach (var detalle in d.DocumentoDetalle)
                    {
                        mdoc.Monto += (double)(detalle.Cantidad * detalle.PrecioDetalle.Precio);
                    }
                    listDoc.Add(mdoc);
                }
            }

            return Json(new { listDoc }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalle(string idDoc, string password)
        {
            int IdocID = Convert.ToInt32(idDoc);
            var doc = docWorker.ObtenerDocXID(IdocID);
            var docR = new modelDoc2();
            docR.Estado = doc.EstadoMisc.Detalle;
            docR.FechaEmision = doc.FechaEmision.ToString();
            docR.IDDocumento = doc.IDDocumento;
            docR.IVA = doc.ClienteEmpresa.TipoIVA.Valor;
            modelDetalle det;
            docR.detalles = new List<modelDetalle>();
            foreach (var d in doc.DocumentoDetalle)
            { 
                det = new modelDetalle();
                det.producto = d.PrecioDetalle.Producto.Nombre;
                det.cantidad = d.Cantidad;
                det.valor = (double)d.PrecioDetalle.Precio;
                docR.detalles.Add(det);
                docR.Monto += (double)(d.Cantidad * d.PrecioDetalle.Precio);
            }
            return Json(new { docR }, JsonRequestBehavior.AllowGet);
        }

        private class modelCliente
        {
            public int idCliente {get; set;}
            public string nombre {get; set;}
        }

        public ActionResult Clientes(string idDoc, string password)
        {
            List<modelCliente> listaR = new List<modelCliente>();
            BLL.BLLCliente ClienteWorker = new BLL.BLLCliente();
            var clientes = ClienteWorker.ObtenerClienteEmpresas();
            modelCliente cli;
            foreach (var c in clientes)
            {
                cli = new modelCliente();
                cli.idCliente = c.IDClienteEmpresa;
                cli.nombre = c.Nombre;
                listaR.Add(cli);
            }
            return Json(new { listaR }, JsonRequestBehavior.AllowGet);
        }

    }
}
