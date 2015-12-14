using AutoMapper;
using BIZ;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TFITest4.Models
{
    [Authorize]
    public class ListaPrecioController : Controller
    {
        //
        // GET: /ListaPrecio2/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLPrecio precioWorker = new BLLPrecio();

        public ActionResult Index()
        {
            try
            {
                var lista = precioWorker.getAllListaPrecio();
                var rList = Mapper.Map<List<BIZListaPrecio>, List<ModelListaPrecio>>(lista);
                return View(rList);
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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error intentar listar Lista de precios", idUser, ip));
                }
                catch (Exception ex) { }
                ViewBag.AlertError = Resources.Language.ErrorNormal;
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /ListaPrecio2/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /ListaPrecio2/Create

        [HttpPost]
        public ActionResult Create(BIZListaPrecio ListaPrecio)
        {
            try
            {
                ListaPrecio.FechaUltimaMod = DateTime.Now;
                precioWorker.CreateListaPrecio(ListaPrecio);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha creado la lista de precios: " + ListaPrecio.Detalle, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }

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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar crear lista de precios", idUser, ip));
                }
                catch (Exception ex) { }
                ViewBag.AlertError = Resources.Language.ErrorNormal;
                return View();
            }
        }

        //
        // GET: /ListaPrecio2/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                var Lista = precioWorker.GetByID(id);
                var rList = Mapper.Map<BIZListaPrecio, ModelListaPrecio>(Lista);
                return View(rList);
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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error intentar editar Lista de precios", idUser, ip));
                }
                catch (Exception ex) { }
                ViewBag.AlertError = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");

            }
        }

        //
        // POST: /ListaPrecio2/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BIZListaPrecio collection)
        {
            try
            {
                precioWorker.UpdateListaPrecio(collection);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha editado la lista de precios: " + collection.IDListaPrecio, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }
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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar editar lista de precios", idUser, ip));
                }
                catch (Exception ex) { }
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }


        public ActionResult CopiarLista(string IDLista, string CopiaText, string CopiaFactor,string FechaDesde)
        {
            try
            {
                BIZ.BIZListaPrecio ListaACopiar = new BIZListaPrecio();
                ListaACopiar.IDListaPrecio = Convert.ToInt32(IDLista); //esto lo pongo acá aunq sea la lista a copiar
                ListaACopiar.Activo = false;
                ListaACopiar.Detalle = CopiaText;
                double factor = Convert.ToDouble(CopiaFactor);
                ListaACopiar.FechaDesde = Convert.ToDateTime(FechaDesde);
                precioWorker.copiarLista(ListaACopiar, factor);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha copiado la lista de precios: " + IDLista + " a una llamada: "+ CopiaText, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }
                return Json(new { Result = ""}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex2)
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
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar copiar lista de precios", idUser, ip));
                }
                catch (Exception ex) { }

                return Json(new { Result = "error" }, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
