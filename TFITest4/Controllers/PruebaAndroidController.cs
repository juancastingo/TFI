using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace TFITest4.Controllers
{
    public class PruebaAndroidController : Controller
    {
        //
        // GET: /PruebaAndroid/

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

        public ActionResult Pedidos(string empresa)
        {
            DALDocumento docWorker = new DALDocumento();
            int empresaID = Convert.ToInt32(empresa);
            var docs = docWorker.getDocsByEmpresa(empresaID,3);
            modelDoc mdoc;
            List<modelDoc> listDoc = new List<modelDoc>();
            foreach (var d in docs)
            {
                mdoc = new modelDoc();
                mdoc.IDDocumento = d.IDDocumento;
                mdoc.EmpresaLocal = d.ClienteEmpresa.NombreFantasia;
                mdoc.Monto = 0;
                foreach (var detalle in d.DocumentoDetalle)
                {
                    mdoc.Monto += (double)(detalle.Cantidad * detalle.PrecioDetalle.Precio);
                }
                listDoc.Add(mdoc);
            }
            var p = new persona();
            p.id = 3;
            p.nombre = empresa;
            var list = new List<persona>();
            list.Add(p);
            p = new persona();
            p.id = 4;
            p.nombre = "pepe";
            list.Add(p);
            //ViewData["CantidadPedido"] = BLL.Pedido.CantidadEnPedido(pSession, 1);
            //ViewData["precioTotal"] = BLL.Pedido.PrecioTodosPedido(pSession);
            return Json(new { listDoc }, JsonRequestBehavior.AllowGet);
        }

        //asi lo agarro con jquery
        //var a = "";
//       $.ajax({
    //       type: "GET",
    //       url: 'http://192.168.2.112/PruebaAndroid/pedidos',
    //       data: "asd",
    //       contentType: 'application/json; charset=utf-8',
    //       success: function (data) {
    //           a = data;
    //           console.log(data);
    //      },
    //      error: function(e) {
    //          console.log(e)
    //      }
//      });
        //a.list.forEach(function(element, index, array) {
        //     console.log(element['id'] + " " + element['nombre'])
        //})


    }
}
