using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TFITest4.Models;

namespace TFITest4.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
                //BIZ.Usuario _usuario = new BIZ.Usuario();
                //_usuario = BLL.Negocio.SeleccionarUnUsuario(model.UserName, model.Password);
                //string _usuario = "juan";
               
                //if (model.UserName != null && model.UserName != "b")
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, false);
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.Cache.SetAllowResponseInBrowserHistory(false);
                //    Response.Cache.SetNoStore();

                    //BIZ.Bitacora _unaBitacora = new BIZ.Bitacora();
                    //_unaBitacora._fecha = DateTime.Now;
                    //_unaBitacora._descripcion = "Usuario " + model.UserName + " inicio sesion";
                    //_unaBitacora._origen = "UI Session";
                    //_unaBitacora._tipo = "Informativo";
                    //BLL.Bitacora.InsetarBitacora(_unaBitacora);

                    ////Session["Perfil"] = _usuario._idTipoUsuario._tipo;
                    //Session["usuario"] = model.UserName;





                    //ViewBag.controlPostLogin = "parent.document.location.reload()";
                    //ViewBag.controlPostLogin = "parent.$.fancybox.close();";  //esto funciona para cerrar el fancy
                //    return View(model);
                //}
                //else {
                    //BIZ.Bitacora _unaBitacora = new BIZ.Bitacora();
                    //_unaBitacora._fecha = DateTime.Now;
                    //_unaBitacora._descripcion = "Usuario o Contraseña inválidos," + model.UserName;
                    //_unaBitacora._origen = "UI Session";
                    //_unaBitacora._tipo = "Informativo";
                    //BLL.Bitacora.InsetarBitacora(_unaBitacora);
            //        ModelState.AddModelError("", "Usuario o Contraseña inválidos");
            //        return View(model);
            //    }
            //    //return RedirectToAction("index", "home");
            //}

        //    return View(model);
        //}



        public ActionResult CerrarSesion()
        {
            //BIZ.Bitacora _unaBitacora = new BIZ.Bitacora();
            //_unaBitacora._fecha = DateTime.Now;
            //_unaBitacora._descripcion = "El usuario" + Session["usuario"] + " cerro sesion";
            //_unaBitacora._origen = "UI Session";
            //_unaBitacora._tipo = "Informativo";
            //BLL.Bitacora.InsetarBitacora(_unaBitacora);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            ViewBag.AlertOK = "Se ha cerrado la sesion correctamente";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            return View();
            // TempData["Resultado"] = "Se ha cerrado la sesion";
            // return RedirectToAction("Index", "Home");
        }

        public ActionResult ControlSesion()
        {

            if (Session["usuario"] == null)
            {
                return Json(new { Result = "NO" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Result = "" }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Error()
        {
            ViewBag.AlertError = "no podes entrar aca borrar";
            return View();
        }


        //menu reload test
        [HttpPost]
        public ActionResult MenuReload(string UserName, string Password)
        {
            BIZUsuario UserCheck = new BIZUsuario();
            UserCheck.Usuario1 = UserName;
            UserCheck.Password = Password;
            DAL.DALUsuario DalWorker = new DAL.DALUsuario();
            BIZUsuario _usuario = DalWorker.ValidateLogin(UserCheck);
            if (_usuario != null)
            {
                FormsAuthentication.SetAuthCookie(UserName, false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();

                //BIZ.Bitacora _unaBitacora = new BIZ.Bitacora();
                //_unaBitacora._fecha = DateTime.Now;
                //_unaBitacora._descripcion = "Usuario " + model.UserName + " inicio sesion";
                //_unaBitacora._origen = "UI Session";
                //_unaBitacora._tipo = "Informativo";
                //BLL.Bitacora.InsetarBitacora(_unaBitacora);
                //Session["Perfil"] = _usuario._idTipoUsuario._tipo;
                Session.Timeout = 9999;
                Session["usuario"] = _usuario.Usuario1;
                Session["SUsuario"] = _usuario;

                return PartialView("~/Views/Shared/Menu.cshtml");

            }
            else
            {
                return Json(new { status = "error", message = "Please Check Username/Password" });
            }
        }

        public ActionResult keepAlive()
        {
            try
            {
                Session["KeepSessionAlive"] = DateTime.Now;
            }  catch(Exception ex){
            }
            return Json(new { status = "", message = "" });
        }


    }
}
