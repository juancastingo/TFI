using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TFITest4.Models;
using BLL;

namespace TFITest4.Controllers
{
    [Authorize]
    public class PaisController : Controller
    {
        //
        // GET: /Pais/
        private BLLBitacora Bita = new BLLBitacora();

        [Authorize]
        public ActionResult Index()
        {
            DAL.DALDireccion tProv = new DAL.DALDireccion();
            List<BIZ.BIZPais> ListaPaises = new List<BIZ.BIZPais>();
            ListaPaises = tProv.getAllPaises();
            Mapper.CreateMap<BIZ.BIZPais, ModelPais>();
            List<ModelPais> ListaMpais = Mapper.Map<List<BIZ.BIZPais>, List<ModelPais>>(ListaPaises);
            return View(ListaMpais);
            
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModelPais pais)
        {

            return View();
        }



    }
}
