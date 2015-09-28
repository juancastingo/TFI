using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using AutoMapper;
using BIZ;
using TFITest4.Models;
using BLL;

namespace TFITest4.Models
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/
        private BLLGeneral generalWorker = new BLLGeneral();
        private BLLDireccion dirWorker = new BLLDireccion();
        private BLLCliente clienteWorker = new BLLCliente();

        public ActionResult Index()
        {
            DALCliente DCli = new DALCliente();
            var clientes = Mapper.Map<List<BIZClienteEmpresa>,List<ModelClienteEmpresa>>(DCli.getAllClienteEmpresa());
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
        private class LocCliModel {
            public int IDLocalidad { get; set; }
            public string Nombre { get; set; }
        }

        public ActionResult Create()
        {
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
            ViewBag.IDTipoIVA = new SelectList(generalWorker.TrearTiposIVA(), "IDTipoIVA", "Detalle");
            var localidades = dirWorker.TraerAllLocalidades();
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
            ViewBag.IDLocalidad  = new SelectList(MiLocalidades,"IDLocalidad", "Nombre");
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create( ModelClienteEmpresa cliente)
        {
            //try
            //{
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

                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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

            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle",rcliente.IDEstado);
            ViewBag.IDTipoIVA = new SelectList(generalWorker.TrearTiposIVA(), "IDTipoIVA", "Detalle",rcliente.IDTipoIVA);


            var localidades = dirWorker.TraerAllLocalidades();
            List<LocCliModel> MiLocalidades = new List<LocCliModel>();
            LocCliModel miLoc;
            foreach (var l in localidades)
            {
                miLoc = new LocCliModel();
                miLoc.IDLocalidad = l.IDLocalidad;
                miLoc.Nombre = l.Nombre + " - " + l.Provincia.Nombre + " - " + l.Provincia.Pais.Nombre;
                MiLocalidades.Add(miLoc);
            }
            ViewBag.IDLocalidad = new SelectList(MiLocalidades, "IDLocalidad", "Nombre",rcliente.Direccion.IDLocalidad);
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
