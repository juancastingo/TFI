using AutoMapper;
using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TFITest4.Models
{
    public class ListaPrecioController : Controller
    {
        //
        // GET: /ListaPrecio2/

        public ActionResult Index()
        {
            DAL.DALPrecio PrecioWorker = new DAL.DALPrecio();
            var lista = PrecioWorker.getAllListaPrecio();
            var rList = Mapper.Map<List<BIZListaPrecio>, List<ModelListaPrecio>>(lista);
            return View(rList);
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
                DAL.DALPrecio PrecioWorker = new DAL.DALPrecio();
                ListaPrecio.FechaUltimaMod = DateTime.Now;
                PrecioWorker.CreateListaPrecio(ListaPrecio);
                TempData["OKNormal"] = Resources.Language.OKNormal;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ListaPrecio2/Edit/5

        public ActionResult Edit(int id)
        {
            DAL.DALPrecio PrecioWorker = new DAL.DALPrecio();
            var Lista = PrecioWorker.GetByID(id);
            var rList = Mapper.Map<BIZListaPrecio, ModelListaPrecio>(Lista);
            return View(rList);
        }

        //
        // POST: /ListaPrecio2/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BIZListaPrecio collection)
        {
            try
            {
                DAL.DALPrecio PrecioWorker = new DAL.DALPrecio();
                PrecioWorker.UpdateListaPrecio(collection);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ListaPrecio2/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ListaPrecio2/Delete/5

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
