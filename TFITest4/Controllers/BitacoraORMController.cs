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
    public class BitacoraORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        //
        // GET: /BitacoraORM/

        public ActionResult Index()
        {
            var bitacora = db.Bitacora.Include(b => b.Usuario);
            return View(bitacora.ToList());
        }

        //
        // GET: /BitacoraORM/Details/5

        public ActionResult Details(int id = 0)
        {
            Bitacora bitacora = db.Bitacora.Find(id);
            if (bitacora == null)
            {
                return HttpNotFound();
            }
            return View(bitacora);
        }

        //
        // GET: /BitacoraORM/Create

        public ActionResult Create()
        {
            ViewBag.IDUsuario = new SelectList(db.Usuario, "IDUsuario", "Nombre");
            return View();
        }

        //
        // POST: /BitacoraORM/Create

        [HttpPost]
        public ActionResult Create(Bitacora bitacora)
        {
            if (ModelState.IsValid)
            {
                db.Bitacora.Add(bitacora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUsuario = new SelectList(db.Usuario, "IDUsuario", "Nombre", bitacora.IDUsuario);
            return View(bitacora);
        }

        //
        // GET: /BitacoraORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bitacora bitacora = db.Bitacora.Find(id);
            if (bitacora == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUsuario = new SelectList(db.Usuario, "IDUsuario", "Nombre", bitacora.IDUsuario);
            return View(bitacora);
        }

        //
        // POST: /BitacoraORM/Edit/5

        [HttpPost]
        public ActionResult Edit(Bitacora bitacora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDUsuario = new SelectList(db.Usuario, "IDUsuario", "Nombre", bitacora.IDUsuario);
            return View(bitacora);
        }

        //
        // GET: /BitacoraORM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bitacora bitacora = db.Bitacora.Find(id);
            if (bitacora == null)
            {
                return HttpNotFound();
            }
            return View(bitacora);
        }

        //
        // POST: /BitacoraORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Bitacora bitacora = db.Bitacora.Find(id);
            db.Bitacora.Remove(bitacora);
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