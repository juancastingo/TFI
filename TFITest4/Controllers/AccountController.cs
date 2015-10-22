using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFITest4;
using BIZ;
using TFITest4.Resources;
using BLL;
using TFITest4.Models;


namespace TFITest4.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // register get

        BLLUsuario UsuarioWorker = new BLLUsuario();
        BLLDireccion direccionWorker = new BLLDireccion();
        BLLCliente ClienteWorker = new BLLCliente();
        BLLBitacora Bita = new BLLBitacora();

        public ActionResult Register()
        {
            //DAL.DALUsuario D_User = new DAL.DALUsuario();
            //DAL.DALDireccion D_Direccion = new DAL.DALDireccion();
            ViewBag.IDLocalidad = new SelectList(direccionWorker.ObtenerLocalidades(), "IDLocalidad", "Nombre");
            ViewBag.IDTipoUsuario = new SelectList(UsuarioWorker.ObtenerTiposUsuario(), "IDTipoUsuario", "Tipo");
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel U)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    ViewBag.AlertOK = "Usuario creado";
                    return RedirectToAction("Index", "Home"); //aca tengo q poner email de confirmación. O no?
                }
                else
                {
                    //DAL.DALUsuario D_User = new DAL.DALUsuario();
                    //DAL.DALDireccion D_Direccion = new DAL.DALDireccion();
                    ViewBag.IDLocalidad = new SelectList(direccionWorker.ObtenerLocalidades(), "IDLocalidad", "Nombre");
                    ViewBag.IDTipoUsuario = new SelectList(UsuarioWorker.ObtenerTiposUsuario(), "IDTipoUsuario", "Tipo");
                    return View();
                }
            }
            catch
            {
                //DAL.DALUsuario D_User = new DAL.DALUsuario();
               // DAL.DALDireccion D_Direccion = new DAL.DALDireccion();
                ViewBag.IDLocalidad = new SelectList(direccionWorker.ObtenerLocalidades(), "IDLocalidad", "Nombre");
                ViewBag.IDTipoUsuario = new SelectList(UsuarioWorker.ObtenerTiposUsuario(), "IDTipoUsuario", "Tipo");
                return View();
            }
        }

        public ActionResult RegisterOut()
        {
            //DAL.DALCliente DCliente = new DAL.DALCliente();
            ViewBag.IDClienteEmpresa = new SelectList(ClienteWorker.ObtenerClienteEmpresas(), "IDClienteEmpresa", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult RegisterOut(Models.RegisterModel U)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    BIZUsuario User = new BIZUsuario();
                    User = AutoMapper.Mapper.Map<Models.RegisterModel, BIZUsuario>(U);
                    User.IDEstado = 12;
                    User.IDTipoUsuario = 2;
                    UsuarioWorker.InsertarUsuario(User);
                    //DALUser.InsertUsuario(User);
                    TempData["OKNormal"] = Resources.Language.OKNormal;
                    SL.Utils util = new SL.Utils();
                    var UserEncoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(User.Usuario1));
                    BIZ.BIZCorreo correo = new BIZCorreo();
                    correo.Subject = "Please Validate IID Account";
                    correo.To = User.Email;
                    string link = "<a href='http://" + Request.Url.Host.ToLower() + ":" + Request.Url.Port + "/Account/validateUser?k=" + UserEncoded + "'>Link</a>";
                    string body = String.Format(Language.MailValidationLink, User.Nombre, link);
                    //correo.Body = "Hi " + User.Nombre + ", <p>Plese validate your IID account with the following link</p> <a href='http://" + Request.Url.Host.ToLower() +":" + Request.Url.Port + "/Account/validateUser?k=" + UserEncoded + "'>Link</a>";
                    correo.Body = body;
                    util.sendMail(correo);
                    string _ip = "Unknown";
                    try
                    {
                        _ip = (string)Session["_ip"];
                    } catch(Exception) {}

                    Bita.guardarBitacora(new BIZBitacora("Informativo","Se ha creado el usuario " + User.Usuario1, null,_ip));
                    return RedirectToAction("EmailConfirm");
                }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                //DAL.DALCliente DCliente = new DAL.DALCliente();
                ViewBag.IDClienteEmpresa = new SelectList(ClienteWorker.ObtenerClienteEmpresas(), "IDClienteEmpresa", "Nombre");
                return View(U);
            //}
            //catch(Exception err)
            //{
                //TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                //DAL.DALCliente DCliente = new DAL.DALCliente();
                //ViewBag.IDClienteEmpresa = new SelectList(DCliente.getAllClienteEmpresa(), "IDClienteEmpresa", "Nombre");
                //return View(U);
            //}
        }

        public class palabra
        {
            public string name { get; set; }
            public Boolean existe { get; set; }
        }

        [HttpPost]
        [ActionName("AjaxUserCheck")]
        public ActionResult AjaxUserCheck(palabra b)
        {
            BIZUsuario User = new BIZUsuario();
            User.Usuario1 = b.name;
            BLLUsuario DUser = new BLLUsuario();
            b.existe = DUser.CheckByName(User);
            return Json(b, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EmailConfirm()
        {
            return View();
        }

        public ActionResult ValidateUser(string k)
        {
            try
            {
                var UserToValidate = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(k));
                ViewBag.valor = UserToValidate;
                //aca tengo q poner al usuario activo
                BIZ.BIZUsuario User = new BIZUsuario();
                User.Usuario1 = UserToValidate;
                UsuarioWorker.ValidarUsuario(User);
                TempData["OKNormal"] = Resources.Language.OKNormal;
            }
            catch
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
            }
            return View();
        }

        [Authorize]
        public ActionResult ChangePass()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePass(RegisterModel usuario)
        {
            try
            {
                BIZUsuario user = new BIZUsuario();
                user.Usuario1 = (string)Session["usuario"];
                user.Password = usuario.PrevPass;
                BIZUsuario _usuario = UsuarioWorker.validarLogin(user);
                if (_usuario != null)
                {
                    _usuario.Password = usuario.Password;
                    UsuarioWorker.ActualizarUsuario(_usuario);
                    TempData["OKNormal"] = Resources.Language.okCambioPass;
                    return Redirect("/Login/CerrarSesion");
                }
                else
                {
                    TempData["ErrorNormal"] = Resources.Language.errorCambioPass; //cambiar por error de contraseña vieja
                    return View(usuario);
                }
            }
            catch
            {
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return View(usuario);
            }
        }
    }
}
