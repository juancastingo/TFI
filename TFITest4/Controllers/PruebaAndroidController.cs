using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public ActionResult Pedidos(string pUsuario)
        {
            var p = new persona();
            p.id = 3;
            p.nombre = "asd";
            var list = new List<persona>();
            list.Add(p);
            p = new persona();
            p.id = 4;
            p.nombre = "pepe";
            list.Add(p);
            //ViewData["CantidadPedido"] = BLL.Pedido.CantidadEnPedido(pSession, 1);
            //ViewData["precioTotal"] = BLL.Pedido.PrecioTodosPedido(pSession);
            return Json(new { list }, JsonRequestBehavior.AllowGet);
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
