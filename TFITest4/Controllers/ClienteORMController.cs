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
    public class ClienteORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /ClienteORM/

        public ActionResult Index()
        {
            var clienteempresa = db.ClienteEmpresa.Include(c => c.Direccion).Include(c => c.EstadoMisc).Include(c => c.TipoIVA);
            return View(clienteempresa.ToList());
        }

        //
        // GET: /ClienteORM/Details/5

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
        // GET: /ClienteORM/Create

        public ActionResult Create()
        {
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle");
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo");
            ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle");
            return View();
        }

        //
        // POST: /ClienteORM/Create

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
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", clienteempresa.IDEstado);
            ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.IDTipoIVA);
            return View(clienteempresa);
        }

        //
        // GET: /ClienteORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            if (clienteempresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle", clienteempresa.IDDireccion);
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", clienteempresa.IDEstado);
            ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.IDTipoIVA);
            return View(clienteempresa);
        }

        //
        // POST: /ClienteORM/Edit/5

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
            ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", clienteempresa.IDEstado);
            ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.IDTipoIVA);
            return View(clienteempresa);
        }

        //
        // GET: /ClienteORM/Delete/5

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
        // POST: /ClienteORM/Delete/5

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