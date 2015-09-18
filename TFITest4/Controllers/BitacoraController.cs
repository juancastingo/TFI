﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace TFITest4.Controllers
{
    [Authorize]
    public class BitacoraController : Controller
    {
        BLLBitacora Bita = new BLLBitacora();


        public ActionResult Index()
        {
            var Lbitacora = Bita.obtenerBitacora();
            return View(Lbitacora);
        }

        //
        // GET: /Bitacora/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Bitacora/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bitacora/Create

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
        // GET: /Bitacora/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Bitacora/Edit/5

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
        // GET: /Bitacora/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Bitacora/Delete/5

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
