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
    public class UsuarioORMController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        //
        // GET: /UsuarioORM/

        public ActionResult Index()
        {
            DAL.DALUsuario DUserTestBorrar = new DAL.DALUsuario();
            List<BIZ.BIZUsuario> List =  DUserTestBorrar.GetAllUsuarios();
            var usuario = db.Usuario.Include(u => u.TipoUsuario);
            return View(usuario.ToList());
        }

        //
        // GET: /UsuarioORM/Details/5

        public ActionResult Details(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // GET: /UsuarioORM/Create

        public ActionResult Create()
        {
            ViewBag.IDTipoUsuario = new SelectList(db.TipoUsuario, "IDTipoUsuario", "Tipo");
            return View();
        }

        //
        // POST: /UsuarioORM/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTipoUsuario = new SelectList(db.TipoUsuario, "IDTipoUsuario", "Tipo", usuario.IDTipoUsuario);
            return View(usuario);
        }

        //
        // GET: /UsuarioORM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTipoUsuario = new SelectList(db.TipoUsuario, "IDTipoUsuario", "Tipo", usuario.IDTipoUsuario);
            return View(usuario);
        }

        //
        // POST: /UsuarioORM/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTipoUsuario = new SelectList(db.TipoUsuario, "IDTipoUsuario", "Tipo", usuario.IDTipoUsuario);
            return View(usuario);
        }

        //
        // GET: /UsuarioORM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /UsuarioORM/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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