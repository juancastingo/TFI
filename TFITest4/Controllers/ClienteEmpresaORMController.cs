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
    public class ClienteEmpresaORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /ClienteEmpresaORM/

        public ActionResult Index()
        {
            var clienteempresa = db.ClienteEmpresa.Include(c => c.Direccion).Include(c => c.TipoIVA);
            return View(clienteempresa.ToList());
        }

        //
        // GET: /ClienteEmpresaORM/Details/5

        public ActionResult Details(int id = 0)
        {
            ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            if (clienteempresa == null)
            {
                return HttpNotFound();
            }
            return View(clienteempresa);
        }

        //
        // GET: /ClienteEmpresaORM/Create

        public ActionResult Create()
        {
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle");
            ViewBag.TipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle");
            return View();
        }

        //
        // POST: /ClienteEmpresaORM/Create

        [HttpPost]
        public ActionResult Create(ClienteEmpresa clienteempresa)
        {
            if (ModelState.IsValid)
            {
                db.ClienteEmpresa.Add(clienteempresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle", clienteempresa.IDDireccion);
            ViewBag.TipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.TipoIVA);
            return View(clienteempresa);
        }

        //
        // GET: /ClienteEmpresaORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            if (clienteempresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle", clienteempresa.IDDireccion);
            ViewBag.TipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.TipoIVA);
            return View(clienteempresa);
        }

        //
        // POST: /ClienteEmpresaORM/Edit/5

        [HttpPost]
        public ActionResult Edit(ClienteEmpresa clienteempresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteempresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle", clienteempresa.IDDireccion);
            ViewBag.TipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.TipoIVA);
            return View(clienteempresa);
        }

        //
        // GET: /ClienteEmpresaORM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            if (clienteempresa == null)
            {
                return HttpNotFound();
            }
            return View(clienteempresa);
        }

        //
        // POST: /ClienteEmpresaORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            db.ClienteEmpresa.Remove(clienteempresa);
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