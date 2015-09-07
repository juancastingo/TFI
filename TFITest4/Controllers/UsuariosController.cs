using AutoMapper;
using BIZ;
using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFITest4.Models;

namespace TFITest4.Controllers
{
    public class UsuariosController : Controller
    {
        private IIDTest2Entities db = new IIDTest2Entities(); //sacar
        //
        // GET: /Usuarios/

        public ActionResult Index()
        {
            DAL.DALUsuario Duser = new DAL.DALUsuario();
            var usuarios = Duser.GetAllUsuarios();
            var RetUser = Mapper.Map<List<BIZUsuario>, List<ModelUsuario>>(usuarios);
            //DAL.DALUsuario DUserTestBorrar = new DAL.DALUsuario();
            //List<BIZ.BIZUsuario> List = DUserTestBorrar.GetAllUsuarios();
            //var usuario = db.Usuario;
            return View(RetUser);
        }

        //
        // GET: /Usuarios/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Usuarios/Create

        public ActionResult RegisterIN()
        {
            DAL.DALUsuario UserWorker = new DAL.DALUsuario();
            var TiposUsuario = UserWorker.GetAllTipoUsuario();
            ViewBag.IDTipoUsuario = new SelectList(UserWorker.GetAllTipoUsuario(), "IDTipoUsuario", "Tipo");
            return View();
        }

        //
        // POST: /Usuarios/Create

        [HttpPost]
        public ActionResult RegisterIN(RegisterModel _user)
        {
            try
            {
                BIZUsuario User = new BIZUsuario();
                User = AutoMapper.Mapper.Map<Models.RegisterModel, BIZUsuario>(_user);
                User.IDEstado = 13;
                DAL.DALUsuario DALUser = new DAL.DALUsuario();
                DALUser.InsertUsuario(User);
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Usuarios/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Usuarios/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Usuarios/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Usuarios/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
