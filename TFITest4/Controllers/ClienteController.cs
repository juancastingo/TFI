﻿using System;
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

        public ActionResult Create()
        {
            DAL.ORM.IIDTest2Entities db = new DAL.ORM.IIDTest2Entities();
            ViewBag.IDDireccion = new SelectList(db.Direccion, "IDDireccion", "Calle");
            ViewBag.IDEstado = new SelectList(generalWorker.traerEstadoMisc("ClienteEmpresa"), "IDEstado", "Detalle");
            ViewBag.IDTipoIVA = new SelectList(db.TipoIVA, "IDTipoIVA", "Detalle");
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create( ModelClienteEmpresa cliente)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cliente/Edit/5

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
