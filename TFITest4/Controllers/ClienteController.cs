using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using AutoMapper;
using BIZ;
using TFITest4.Models;

namespace TFITest4.Models
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

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
            return View();
        }

        //
        // POST: /Cliente/Create

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
