using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TFITest4.Models;
using TFITest4.Resources;
using BLL;

namespace TFITest4.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        BLLUsuario UsuarioWorker = new BLLUsuario();
        BLLDocumento DocWorker = new BLLDocumento();
        private BLLBitacora Bita = new BLLBitacora();


        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CerrarSesion()
        {
            try
            {
                Bita.guardarBitacora(new BIZBitacora("Informativo", "El usuario \"" + Session["usuario"] + "\" cerró sesión", (int)Session["userID"], Session["_ip"].ToString()));
            }
            catch (Exception ex) { }

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
            try
            {
                int pendientes = -1;
                if (Session["usuario"] == null)
                {
                    return Json(new { Result = "NO" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Session["grupo"].ToString() == "Creditos y Cobranzas")
                    {
                        pendientes = DocWorker.ObtenerDocPendientes(3, 5); //3 pedido. 5 Pend de aprob
                    }
                    if (Session["grupo"].ToString() == "Comercial") 
                    {
                        pendientes = DocWorker.ObtenerDocPendientes(3, 6); //3 pedido. 5 aprobado
                    }
                }

                return Json(new { Result = "", Pendientes = pendientes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "NO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Error()
        {
            Nullable<int> idUser = null;
            string ip = "Unknown";
            try
            {
                idUser = (int)Session["userID"];
            } 
            catch (Exception ex) {}
            try {
                 ip = Session["_ip"].ToString();
            } catch (Exception ex) {}
            Bita.guardarBitacora(new BIZBitacora("Advertencia", "Inento de acceso indebido", idUser, ip));
            ViewBag.AlertError = @Language.AccesoError;
            return View();
        }

        //menu reload test
        [HttpPost]
        public ActionResult MenuReload(string UserName, string Password)
        {
            BIZUsuario UserCheck = new BIZUsuario();
            UserCheck.Usuario1 = UserName;
            UserCheck.Password = Password;
            BIZUsuario _usuario = UsuarioWorker.validarLogin(UserCheck);
            //ip
            

            if (_usuario != null && _usuario.IDEstado == 13)
            {
                FormsAuthentication.SetAuthCookie(UserName, false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();

                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "El usuario \"" + _usuario.Usuario1 + "\" inició sesión", _usuario.IDUsuario, Session["_ip"].ToString()));
                }

                catch (Exception ex) { }
                Session.Timeout = 9999;
                Session["usuario"] = _usuario.Usuario1;
                Session["userID"] = _usuario.IDUsuario;
                Session["SUsuario"] = _usuario;
                ViewBag.UserGroup = _usuario.TipoUsuario.Tipo;
                Session["grupo"] = _usuario.TipoUsuario.Tipo;
                return PartialView("~/Views/Shared/Menu.cshtml");
            }
            else if (_usuario != null)
            {
                if (_usuario.IDEstado == 12)
                {
                    return Json(new { status = "error", message = @Language.UsuarioNoHabilitado });
                }
                else if (_usuario.IDEstado == 26)
                {

                    return Json(new { status = "error", message = @Language.UsuarioNoConfirmado });
                }
                else
                {
                    return Json(new { status = "error", message = @Language.RevisarUsuarioContra });
                }
            }
            else
            {
                return Json(new { status = "error", message = @Language.RevisarUsuarioContra });
            }
        }

        public ActionResult keepAlive()
        {
            try
            {
                Session["KeepSessionAlive"] = DateTime.Now;
            }
            catch (Exception ex)
            {
            }
            return Json(new { status = "", message = "" });
        }

        public ActionResult ForgetPassword()
        {
            return View();
            //return Json(new { status = "", message = result });
        }

        [HttpPost]
        public ActionResult ForgetPassword(string NombreUsuario)
        {
            try
            {
                BIZUsuario user = UsuarioWorker.ObtenerUserByUsuario(NombreUsuario);
                if (user != null)
                {
                    SL.Utils util = new SL.Utils();
                    var UserEncoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.Usuario1));
                    BIZ.BIZCorreo correo = new BIZCorreo();
                    correo.Subject = "Access this link to change your password";
                    correo.To = user.Email;
                    string link = "<a href='http://" + Request.Url.Host.ToLower() + ":" + Request.Url.Port + "/Login/resetPassword/?k=" + UserEncoded + "'>Link</a>";
                    string body = String.Format(link);
                    correo.Body = body;
                    util.sendMail(correo);
                    TempData["OKNormal"] = Resources.Language.OKNormal;
                }
                else
                {
                    TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
            }
            return View();
            //return Json(new { status = "", message = result });

        }

        public ActionResult resetPassword(string k)
        {
            try
            {
                var UserToReset = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(k));
                string newPass =  UsuarioWorker.ResetPassword(UserToReset);
                if (newPass != "")
                {
                    ViewBag.pass = newPass;
                }
                else
                {
                    TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                }
                
            }
            catch (Exception ex)
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
            }

            return View();
            //return Json(new { status = "", message = result });

        }


    }
}
