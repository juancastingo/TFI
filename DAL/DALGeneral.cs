﻿using AutoMapper;
using BIZ;
using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALGeneral
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public DALGeneral()
        {
            DALAutommaper automapper = new DALAutommaper();
        }

        public BIZ.BIZEmpresaLocal getEmpresaLocal() 
        {
            var empresa = db.EmpresaLocal
                .Where(b => b.IDEmpresaLocal == 1).FirstOrDefault();

            var emp = AutoMapper.Mapper.Map<EmpresaLocal, BIZ.BIZEmpresaLocal>(empresa);
            return emp;
        }

        public List<BIZEstado> getEstadoMisc(string p)
        {
            var list = db.EstadoMisc.Where(b => b.Tipo == p);
            var rList = Mapper.Map<List<EstadoMisc>, List<BIZEstado>>(list.ToList());
            return rList;
        }

        public List<BIZTipoIVA> getTipoIVA()
        {
            var list = db.TipoIVA;
            var rList = Mapper.Map<List<TipoIVA>, List<BIZTipoIVA>>(list.ToList());
            return rList;
        }
    }
}
