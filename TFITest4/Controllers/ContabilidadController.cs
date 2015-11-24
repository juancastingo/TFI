using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using TFITest4.Models;


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

        public ActionResult GetAsientos(DateTime fecha, DateTime fecha2)
        {
            try
            {
                var docs = DocWorker.obtenerDocumentsPorFechaContable(fecha, fecha2);
                List<ModelAsiento> asientos = new List<ModelAsiento>();
                ModelAsiento asiento = new ModelAsiento();
                foreach (var d in docs)
                {
                    asiento = new ModelAsiento();
                    asiento.IDDocumento = d.IDDocumento;
                    asiento.FechaContable = (DateTime)d.FechaContable;
                    asiento.debe = d.DocumentoTipo.Cuenta.Nombre;
                    asiento.haber = d.DocumentoTipo.Cuenta1.Nombre;
                    double aux = 0;
                    foreach (var det in d.DocumentoDetalle)
                    {
                        aux += (det.Cantidad * (double)det.PrecioDetalle.Precio);
                    }

                    asiento.monto = aux + (aux * d.ClienteEmpresa.TipoIVA.Valor/100);
                    asientos.Add(asiento);

                }
                return Json(new {status = "", docs = asientos });
            }
            catch (Exception ex)
            {
            }
            return Json(new { status = "Error" });
        }

    }
}
