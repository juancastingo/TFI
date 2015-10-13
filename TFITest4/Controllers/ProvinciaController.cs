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

        public ActionResult Index()
        {
            DAL.DALDireccion tProv = new DAL.DALDireccion();
            List<BIZ.BIZProvincia> ListaProv = new List<BIZ.BIZProvincia>();
            ListaProv = tProv.getAllProvincias();
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
            DAL.DALDireccion tPais = new DAL.DALDireccion();
            ViewBag.IDPais = new SelectList(tPais.getAllPaises(), "IDPais", "Nombre");
            return View();
        }

        //
        // POST: /Provincia/Create

        [HttpPost]
        public ActionResult Create(BIZ.BIZProvincia Provincia)
        {
            DAL.DALDireccion tProvincia = new DAL.DALDireccion();
            //var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            if (ModelState.IsValid)
            {
                try
                {
                    tProvincia.insertProvincia(Provincia);
                }
                catch
                {
                    //el error
                    ViewBag.AlertError = Resources.Language.ErrorNormal;
                    DAL.DALDireccion tPais = new DAL.DALDireccion();
                    ViewBag.IDPais = new SelectList(tPais.getAllPaises(), "IDPais", "Nombre");
                    return View();
                }
            }
            TempData["OKNormal"] = Resources.Language.OKNormal;
            return RedirectToAction("Index");
            //  }

            // DAL.DALPais tpais = new DAL.DALPais();
            //   ViewBag.Pais = new SelectList(tpais.getPaises(), "IDPais", "Pais");
            //   return View();
        }


        public ActionResult Edit(int id)
        {
            BIZ.BIZProvincia provincia = new BIZ.BIZProvincia();
            DAL.DALDireccion Dprov = new DAL.DALDireccion();
            provincia = Dprov.GetProvinciaByID(id);
            //db.Provincia.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPais = new SelectList(Dprov.getAllPaises(), "IDPais", "Nombre", provincia.Pais.IDPais);
            return View(provincia);
        }

        [HttpPost]
        public ActionResult Edit(BIZ.BIZProvincia provincia)
        {
            DAL.DALDireccion Dprov = new DAL.DALDireccion();
            if (ModelState.IsValid)
            {
                Dprov.UpdateProvincia(provincia);
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            ViewBag.AlertError = Resources.Language.ErrorNormal;
            ViewBag.IDPais = new SelectList(Dprov.getAllPaises(), "IDPais", "Nombre", provincia.Pais.IDPais);
            return View(provincia);
        }

        public ActionResult Delete(int id)
        {
            BIZ.BIZProvincia provincia = new BIZ.BIZProvincia();
            DAL.DALDireccion Dprov = new DAL.DALDireccion();
            provincia = Dprov.GetProvinciaByID(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DAL.DALDireccion Dprov = new DAL.DALDireccion();
            int r = Dprov.DeleteProvincia(id);
            if (r == 0)
            {
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }
    }
}
