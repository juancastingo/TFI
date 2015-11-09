using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace TFITest4.Controllers
{

    public class ContabilidadController : Controller
    {

        private BLLDocumento DocWorker = new BLLDocumento();
        private BLLBitacora Bita = new BLLBitacora();

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult RegistrarMes(string f1, string f2)
        {
            DateTime fecha1 = Convert.ToDateTime(f1);
            DateTime fecha2 = Convert.ToDateTime(f2);
            DocWorker.registrarDocs(fecha1, fecha2);
            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Asientos()
        {

            return View();
        }
    }
}
