using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ORM;
using BIZ;
using System.Data;
using AutoMapper;

namespace DAL
{
    public class DALDireccion
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public DALDireccion()
        {
            DALAutommaper automapper = new DALAutommaper();

        }

        public List<BIZProvincia> getAllProvincias()
        {
            return Mapper.Map<List<Provincia>, List<BIZProvincia>>(db.Provincia.ToList());

            //BIZProvincia oProvincia;
            //List<BIZProvincia> listaP = new List<BIZProvincia>();
            //var elementos = from d in db.Provincia.Include("pais") select d;
            //IEnumerator<Provincia> enu = elementos.GetEnumerator();
            //while (enu.MoveNext())
            //{
            //    oProvincia = new BIZProvincia();
            //    oProvincia.IDProvincia = enu.Current.IDProvincia;
            //    oProvincia.Nombre = enu.Current.Nombre;
            //    oProvincia.Pais.IDPais = enu.Current.Pais.IDPais;
            //    oProvincia.Pais.Nombre = enu.Current.Pais.Nombre;
            //    listaP.Add(oProvincia);
            //}

            //return listaP;
        }


        public void insertProvincia(BIZProvincia oProvincia)
        {

            Provincia tProvincia = new Provincia();
            var Tprov = db.Provincia
                    .Where(b => b.Nombre == oProvincia.Nombre)
                    .Where(b => b.IDPais == oProvincia.IDPais)
                    .FirstOrDefault();
            if (Tprov == null)
            {
                tProvincia.Nombre = oProvincia.Nombre;
                tProvincia.IDPais = oProvincia.IDPais;
                db.Provincia.Add(tProvincia);
                db.SaveChanges();
            }
            else
            {
                throw new System.InvalidOperationException("Error. Provincia Existente");
            }


        }

        public BIZProvincia GetProvinciaByID(int id)
        {

            BIZProvincia oProvincia = new BIZProvincia();
            var Tprovincia = db.Provincia
                .Include("Pais")
                .SingleOrDefault(x => x.IDProvincia == id);
            if (Tprovincia != null)
            {
                oProvincia.IDProvincia = Tprovincia.IDProvincia;
                oProvincia.Nombre = Tprovincia.Nombre;
                oProvincia.Pais.IDPais = Tprovincia.Pais.IDPais;
                oProvincia.Pais.Nombre = Tprovincia.Pais.Nombre;
            }
            else
            {
                return null;
            }

            //mappermap

            return oProvincia;


        }

        public List<BIZLocalidad> getAllLocalidades()
        {
            List<BIZLocalidad> Rlista = new List<BIZLocalidad>();
            var TList = db.Localidad.Include("Provincia").ToList();
            Rlista = AutoMapper.Mapper.Map<List<Localidad>, List<BIZLocalidad>>(TList);
            return Rlista;
            //foreach (var tItem in TList)
            //{
            //    var rItem = AutoMapper.Mapper.Map<Localidad, BIZLocalidad>(tItem);
            //    rItem.Provincia = AutoMapper.Mapper.Map<Provincia, BIZProvincia>(tItem.Provincia);
            //    Rlista.Add(rItem);
            //}
            
        }


        public List<BIZ.BIZPais> getAllPaises()
        {
            return Mapper.Map<List<Pais>, List<BIZPais>>(db.Pais.ToList());
        }


        public void UpdateProvincia(BIZProvincia oProvincia)
        {

            Provincia Tprovincia = new Provincia();
            //Tprovincia = Mapper.Map<BIZProvincia, Provincia>(oProvincia);
            Tprovincia.IDProvincia = oProvincia.IDProvincia;
            Tprovincia.Nombre = oProvincia.Nombre;
            Tprovincia.IDPais = oProvincia.IDPais;
            db.Entry(Tprovincia).State = EntityState.Modified;
            db.SaveChanges();
        }

        public int DeleteProvincia(int id)
        {
            try
            {
                Provincia provincia = db.Provincia.Find(id);
                if (provincia != null)
                {

                    db.Provincia.Remove(provincia);
                    db.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 2;
            }
        }

    }
}
