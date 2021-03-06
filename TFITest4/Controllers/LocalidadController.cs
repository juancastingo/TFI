﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFITest4.Models;

namespace TFITest4.Controllers
{
    [Authorize]
    public class LocalidadController : Controller
    {
        //
        // GET: /Localidad/
        private BLLBitacora Bita = new BLLBitacora();
        private BLLDireccion direccionWorker = new BLLDireccion();
        public ActionResult Index()
        {            
            //var ListModelLocalidades = AutoMapper.Mapper.Map<List<BIZ.BIZLocalidad>, List<Models.ModelLocalidad>>(DDireccion.getAllLocalidades());
            var ListModelLocalidades = AutoMapper.Mapper.Map<List<BIZ.BIZLocalidad>, List<Models.ModelLocalidad>>(direccionWorker.getAllLocalidades());
            return View(ListModelLocalidades);
        }

        //
        // GET: /Localidad/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Localidad/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Localidad/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        // GET: /Localidad/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Localidad/Edit/5

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
        // GET: /Localidad/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Localidad/Delete/5

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
