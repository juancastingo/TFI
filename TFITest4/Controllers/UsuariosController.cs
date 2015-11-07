using AutoMapper;
using BIZ;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFITest4.Models;

namespace TFITest4.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        //
        // GET: /Usuarios/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLUsuario userWorker = new BLLUsuario();
        private BLLGeneral generalWorker = new BLLGeneral();


        public ActionResult Index()
        {
            var usuarios = userWorker.obtenerUsuarios();
            var RetUser = Mapper.Map<List<BIZUsuario>, List<ModelUsuario>>(usuarios);
            //DAL.DALUsuario DUserTestBorrar = new DAL.DALUsuario();
            //List<BIZ.BIZUsuario> List = DUserTestBorrar.GetAllUsuarios();
            //var usuario = db.Usuario;
            return View(RetUser);
        }


        public ActionResult RegisterIN()
        {
            var tipos = userWorker.ObtenerTiposUsuario();
            var tiposU = tipos.Where(c => c.Tipo != "Externo" && c.Tipo != "Administrador");
            ViewBag.IDTipoUsuario = new SelectList(tiposU, "IDTipoUsuario", "Tipo");

            return View();
        }

        //
        // POST: /Usuarios/Create

        [HttpPost]
        public ActionResult RegisterIN(ModelUsuario _user)
        {
            try
            {
                BIZUsuario User = new BIZUsuario();
                User = AutoMapper.Mapper.Map<ModelUsuario, BIZUsuario>(_user);
                User.IDEstado = 13;
                User.IDClienteEmpresa = null;
                userWorker.InsertarUsuario(User);


                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha generado el usuario: " + _user.Usuario1, idUser, ip));
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {

                Nullable<int> idUser = null;
                string ip = "Unknown";
                try
                {
                    idUser = (int)Session["userID"];
                }
                catch (Exception ex) { }
                try
                {
                    ip = Session["_ip"].ToString();
                }
                catch (Exception ex) { }
                Bita.guardarBitacora(new BIZBitacora("Error", "Error al crear un usuario de forma interna", idUser, ip));
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Usuarios/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                var Tuser = userWorker.ObtenerUsarioByID(id);
                if (Tuser == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var user = Mapper.Map<BIZUsuario, ModelUsuario>(Tuser);
                    var tipos = userWorker.ObtenerTiposUsuario();
                    var tiposU = tipos.Where(c => c.Tipo != "Externo" && c.Tipo != "Administrador");
                    ViewBag.IDTipoUsuario = new SelectList(tiposU, "IDTipoUsuario", "Tipo",user.IDTipoUsuario);
                    ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("Usuario"), "IDEstado", "Detalle",user.IDEstado);
                    return View(user);
                }
            }
            catch
            {
                return HttpNotFound();
            }
        }

        //
        // POST: /Usuarios/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BIZUsuario usuario)
        {
            try
            {
                var user = userWorker.ObtenerUsarioByID(id);
                user.Email = usuario.Email;
                user.IDEstado = usuario.IDEstado;
                user.Telefono = usuario.Telefono;
               // user.IDTipoUsuario = usuario.IDTipoUsuario; //esto lo rompia porq no traia nada
                user.Nombre = usuario.Nombre;
                user.FechaUltimaMod = DateTime.Now;
                if (userWorker.ActualizarUsuario(user))
                {
                    TempData["OKNormal"] = Resources.Language.OKNormal;
                    return RedirectToAction("Index");

                } else {
                    TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }


    }
}
