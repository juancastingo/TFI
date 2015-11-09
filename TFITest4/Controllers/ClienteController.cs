using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BIZ;
using TFITest4.Models;
using BLL;
using TFITest4.Resources;

namespace TFITest4.Models
{
    [Authorize]
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/
        private BLLGeneral generalWorker = new BLLGeneral();
        private BLLDireccion dirWorker = new BLLDireccion();
        private BLLCliente clienteWorker = new BLLCliente();
        private BLLBitacora Bita = new BLLBitacora();

        public ActionResult Index()
        {
            var clientes = Mapper.Map<List<BIZClienteEmpresa>, List<ModelClienteEmpresa>>(clienteWorker.ObtenerClienteEmpresas());
            return View(clientes);
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cliente/Create
        private class LocCliModel
        {
            public int IDLocalidad { get; set; }
            public string Nombre { get; set; }
        }

        public ActionResult Create()
        {
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
            ViewBag.IDTipoIVA = new SelectList(generalWorker.TrearTiposIVA(), "IDTipoIVA", "Detalle");
            var localidades = dirWorker.ObtenerLocalidades();
            List<LocCliModel> MiLocalidades = new List<LocCliModel>();
            LocCliModel miLoc;
            foreach (var l in localidades)
            {
                miLoc = new LocCliModel();
                miLoc.IDLocalidad = l.IDLocalidad;
                miLoc.Nombre = l.Nombre + " - " + l.Provincia.Nombre + " - " + l.Provincia.Pais.Nombre;
                MiLocalidades.Add(miLoc);
            }
            //ViewBag.IDLocalidad = new SelectList(dirWorker.TraerAllLocalidades(), "IDLocalidad", "Nombre");
            ViewBag.IDLocalidad = new SelectList(MiLocalidades, "IDLocalidad", "Nombre");
            //ModelClienteEmpresa c = new ModelClienteEmpresa();
            //c.Piso = 0;
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(ModelClienteEmpresa cliente)
        {
            try
            {
                var Scli = Mapper.Map<ModelClienteEmpresa, BIZClienteEmpresa>(cliente);
                Scli.Direccion = new BIZDireccion();
                Scli.Direccion.Calle = cliente.Calle;
                Scli.Direccion.Dpto = cliente.Dpto;
                Scli.Direccion.Numero = cliente.Numero.ToString();
                Scli.Direccion.Piso = cliente.Piso.ToString();
                Scli.FechaAlta = DateTime.Now;
                Scli.Direccion.Detalle = cliente.Detalle;
                Scli.Direccion.IDLocalidad = cliente.IDLocalidad;
                clienteWorker.agregarCliente(Scli);
                //clienteWorker.agregarCliente(cliente);
                // TODO: Add insert logic here
                //bitacora
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha registrado el cliente \"" + cliente.Nombre, (int)Session["userID"], Session["_ip"].ToString()));
                }
                catch (Exception ex) { }
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
                //poner bitacora
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
                Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar crear un cliente", idUser, ip));
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;


                ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
                ViewBag.IDTipoIVA = new SelectList(generalWorker.TrearTiposIVA(), "IDTipoIVA", "Detalle");
                var localidades = dirWorker.ObtenerLocalidades();
                List<LocCliModel> MiLocalidades = new List<LocCliModel>();
                LocCliModel miLoc;
                foreach (var l in localidades)
                {
                    miLoc = new LocCliModel();
                    miLoc.IDLocalidad = l.IDLocalidad;
                    miLoc.Nombre = l.Nombre + " - " + l.Provincia.Nombre + " - " + l.Provincia.Pais.Nombre;
                    MiLocalidades.Add(miLoc);
                }
                //ViewBag.IDLocalidad = new SelectList(dirWorker.TraerAllLocalidades(), "IDLocalidad", "Nombre");
                ViewBag.IDLocalidad = new SelectList(MiLocalidades, "IDLocalidad", "Nombre");
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return View(cliente);
            }
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id)
        {
            //ClienteEmpresa clienteempresa = db.ClienteEmpresa.Find(id);
            var cliente = clienteWorker.ObtenerCliente(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var rcliente = Mapper.Map<BIZClienteEmpresa, ModelClienteEmpresa>(cliente);

            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle", rcliente.IDEstado);
            ViewBag.IDTipoIVA = new SelectList(generalWorker.TrearTiposIVA(), "IDTipoIVA", "Detalle", rcliente.IDTipoIVA);


            var localidades = dirWorker.ObtenerLocalidades();
            List<LocCliModel> MiLocalidades = new List<LocCliModel>();
            LocCliModel miLoc;
            foreach (var l in localidades)
            {
                miLoc = new LocCliModel();
                miLoc.IDLocalidad = l.IDLocalidad;
                miLoc.Nombre = l.Nombre + " - " + l.Provincia.Nombre + " - " + l.Provincia.Pais.Nombre;
                MiLocalidades.Add(miLoc);
            }
            ViewBag.IDLocalidad = new SelectList(MiLocalidades, "IDLocalidad", "Nombre", rcliente.Direccion.IDLocalidad);
            //ViewBag.IDEstado = new SelectList(db.EstadoMisc, "IDEstado", "Tipo", clienteempresa.IDEstado);
            //ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle", clienteempresa.IDTipoIVA);
           
            return View(rcliente);
            //return View();
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ModelClienteEmpresa cliente)
        {
            try
            {
                // TODO: Add update logic here
                cliente.Direccion.IDLocalidad = cliente.IDLocalidad;
                var c = Mapper.Map<ModelClienteEmpresa, BIZClienteEmpresa>(cliente);
                clienteWorker.updateCliente(c);
                try
                {
                    Bita.guardarBitacora(new BIZBitacora("Informativo", "Se ha editado el cliente \"" + cliente.Nombre, (int)Session["userID"], Session["_ip"].ToString()));
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
                Bita.guardarBitacora(new BIZBitacora("Error", "Error al intentar editar un cliente", idUser, ip));
                TempData["ErrorNormal"] = Resources.Language.ErrorNormal;
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cliente/Delete/5

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
