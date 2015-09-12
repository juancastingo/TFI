using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.ORM;
using BIZ;
using AutoMapper;
using System.Data;

namespace DAL
{
    public class DALUsuario
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public DALUsuario() {
            DALAutommaper automapper = new DALAutommaper();
        }

        public List<BIZ.BIZTipoUsuario> GetAllTipoUsuario()
        {
            BIZTipoUsuario B_TP;
            List<BIZTipoUsuario> rList = new List<BIZTipoUsuario>();
            var elementos = from d in db.TipoUsuario select d;
            IEnumerator<TipoUsuario> enu = elementos.GetEnumerator();
            while (enu.MoveNext())
            {
                B_TP = new BIZTipoUsuario();
                B_TP.IDTipoUsuario = enu.Current.IDTipoUsuario;
                B_TP.Tipo = enu.Current.Tipo;
                rList.Add(B_TP);
            }
            return rList;
        }

        public List<BIZ.BIZUsuario> GetAllUsuarios()
        {
            //BIZUsuario oUsuario;
            List<BIZUsuario> rList = new List<BIZUsuario>();
            var usuario = db.Usuario.Include("TipoUsuario");
            rList = Mapper.Map<List<Usuario>, List<BIZUsuario>>(usuario.ToList());
           // foreach (Usuario u in usuario)
           // {
            //    oUsuario = new BIZUsuario();
            //    oUsuario.Email = u.Email;
                //oUsuario.Estado = new BIZEstado();
           //     oUsuario.EstadoMisc.IDEstado = u.EstadoMisc.IDEstado;

            //}

           
            return rList;
        }

        public void InsertUsuario(BIZUsuario oUsuario)
        {
            //var TSave = Mapper.Map<BIZUsuario, Usuario>(oUsuario);
            Usuario TSave = new Usuario();
            TSave.Email = oUsuario.Email;
            TSave.IDEstado = oUsuario.IDEstado;
            TSave.FechaAlta = DateTime.Now;
            TSave.FechaUltimaMod = TSave.FechaAlta;
            TSave.IDClienteEmpresa = oUsuario.IDClienteEmpresa;
            TSave.IDEstado = oUsuario.IDEstado;
            TSave.IDTipoUsuario = oUsuario.IDTipoUsuario;
            TSave.Nombre = oUsuario.Nombre;
            TSave.Password = oUsuario.Password;
            TSave.Telefono = oUsuario.Telefono;
            TSave.Usuario1 = oUsuario.Usuario1;
            TSave.IDClienteEmpresa = oUsuario.IDClienteEmpresa;
            TSave.Documento = null;
            TSave.Documento1 = null;
            TSave.Bitacora = null;
            db.Usuario.Add(TSave);
            db.SaveChanges();
        }

        public Boolean CheckByName(BIZUsuario oUser) {
            var TUser = db.Usuario
                    .Where(b => b.Usuario1 == oUser.Usuario1)
                    .FirstOrDefault();
            if (TUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ValidateUser(BIZUsuario oUser)
        {
            var TUser = db.Usuario
                    .Where(b => b.Usuario1 == oUser.Usuario1)
                    .FirstOrDefault();
            TUser.IDEstado = 13;
            db.Entry(TUser).State = EntityState.Modified;
            db.SaveChanges();
        }

        public BIZUsuario ValidateLogin(BIZUsuario oUser)
        {
            try
            {
            var TUser = db.Usuario
                .Where(b => b.Usuario1 == oUser.Usuario1)
                .Where(c => c.Password == oUser.Password)
                .FirstOrDefault();
            var UserReturn = Mapper.Map<Usuario, BIZUsuario>(TUser);
            return UserReturn;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public BIZUsuario getUserByUsuario(string username)
        {
            try
            {
                var TUser = db.Usuario
                    .Where(b => b.Usuario1 == username)
                    .FirstOrDefault();
                var UserReturn = Mapper.Map<Usuario, BIZUsuario>(TUser);
                return UserReturn;
            } catch(Exception ex) {
                return null;
            }
        }
        public string ResetPassword(string username)
        {
            try
            {
                var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                if (getUserByUsuario(username) != null)
                {
                    string query = "UPDATE Usuario SET Password='" + result + "' WHERE Usuario ='" + username + "'";
                    db.Database.ExecuteSqlCommand(query);
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }


    }
}
