using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using SL;
using BLL;

namespace TFITest4.Controllers
{
    [Authorize]
    public class ProvinciaController : Controller
    {
        //
        // GET: /Provincia/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLDireccion direccionWorker = new BLLDireccion();

        public ActionResult Index()
        {
            List<BIZ.BIZProvincia> ListaProv = new List<BIZ.BIZProvincia>();
            ListaProv = direccionWorker.getAllProvincias();
            return View(ListaProv);
        }

        public ActionResult DownloadViewPDF()
        {
            //var model = new GeneratePDFModel();
            //Code to get content
           // return new Rotativa.ViewAsPdf("GeneratePDF", model){FileName = "TestViewAsPdf.pdf"}
            return new ActionAsPdf("GenerarPDF") { FileName = "invoice.pdf" };
           // return new ActionAsPdf("Index") { FileName = "Test.pdf" };
        }

        public ActionResult GenerarPDF(string name)
        {
            Utils utils = new Utils();
            string codigo = "123456";
            utils.generaCodigoBarras(codigo);
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", name);
            ViewBag.Barcode = codigo + ".jpg";
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.IDPais = new SelectList(direccionWorker.getAllPaises(), "IDPais", "Nombre");
            return View();
        }

        //
        // POST: /Provincia/Create

        [HttpPost]
        public ActionResult Create(BIZ.BIZProvincia Provincia)
        {
            //var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            if (ModelState.IsValid)
            {
                try
                {
                    direccionWorker.insertProvincia(Provincia);
                }
                catch
                {
                    //el error
                    ViewBag.AlertError = Resources.Language.ErrorNormal;
                    ViewBag.IDPais = new SelectList(direccionWorker.getAllPaises(), "IDPais", "Nombre");
                    TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                    return View(Provincia);
                }
            }
            TempData["OKNormal"] = Resources.Language.OKNormal;
            return RedirectToAction("Index");
            //  }

            //   ViewBag.Pais = new SelectList(tpais.getPaises(), "IDPais", "Pais");
            //   return View();
        }


        public ActionResult Edit(int id)
        {
            BIZ.BIZProvincia provincia = new BIZ.BIZProvincia();
            provincia = direccionWorker.GetProvinciaByID(id);
            //db.Provincia.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPais = new SelectList(direccionWorker.getAllPaises(), "IDPais", "Nombre", provincia.Pais.IDPais);
            return View(provincia);
        }

        [HttpPost]
        public ActionResult Edit(BIZ.BIZProvincia provincia)
        {
            if (ModelState.IsValid)
            {
                direccionWorker.UpdateProvincia(provincia);
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            ViewBag.AlertError = Resources.Language.ErrorNormal;
            ViewBag.IDPais = new SelectList(direccionWorker.getAllPaises(), "IDPais", "Nombre", provincia.Pais.IDPais);
            return View(provincia);
        }

        //public ActionResult Delete(int id)
        //{
        //    BIZ.BIZProvincia provincia = new BIZ.BIZProvincia();
        //    DAL.DALDireccion Dprov = new DAL.DALDireccion();
        //    provincia = Dprov.GetProvinciaByID(id);
        //    if (provincia == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(provincia);
        //}

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DAL.DALDireccion Dprov = new DAL.DALDireccion();
        //    int r = Dprov.DeleteProvincia(id);
        //    if (r == 0)
        //    {
        //        TempData["OKNormal"] = Resources.Language.OKNormal;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}
