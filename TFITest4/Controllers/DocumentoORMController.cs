using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.ORM;

namespace TFITest4.Controllers
{
    public class DocumentoORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /DocumentoORM/

        public ActionResult Index()
        {
            var documento = db.Documento.Include(d => d.DocumentoTipo).Include(d => d.Proveedor);
            return View(documento.ToList());
        }

        //
        // GET: /DocumentoORM/Details/5

        public ActionResult Details(int id = 0)
        {
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        //
        // GET: /DocumentoORM/Create

        public ActionResult Create()
        {
            ViewBag.IDDocumentoTipo = new SelectList(db.DocumentoTipo, "IDDocumentoTipo", "NombreDocumento");
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre");
            return View();
        }

        //
        // POST: /DocumentoORM/Create

        [HttpPost]
        public ActionResult Create(Documento documento)
        {
            if (ModelState.IsValid)
            {
                db.Documento.Add(documento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDocumentoTipo = new SelectList(db.DocumentoTipo, "IDDocumentoTipo", "NombreDocumento", documento.IDDocumentoTipo);
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", documento.IDProveedor);
            return View(documento);
        }

        //
        // GET: /DocumentoORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDocumentoTipo = new SelectList(db.DocumentoTipo, "IDDocumentoTipo", "NombreDocumento", documento.IDDocumentoTipo);
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", documento.IDProveedor);
            return View(documento);
        }

        //
        // POST: /DocumentoORM/Edit/5

        [HttpPost]
        public ActionResult Edit(Documento documento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDocumentoTipo = new SelectList(db.DocumentoTipo, "IDDocumentoTipo", "NombreDocumento", documento.IDDocumentoTipo);
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", documento.IDProveedor);
            return View(documento);
        }

        //
        // GET: /DocumentoORM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        //
        // POST: /DocumentoORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Documento documento = db.Documento.Find(id);
            db.Documento.Remove(documento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}