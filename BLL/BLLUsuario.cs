﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;

namespace BLL
{
    public class BLLUsuario
    {
        DAL.DALUsuario D_User = new DAL.DALUsuario();
        DAL.DALDireccion D_Direccion = new DAL.DALDireccion();


        public List<BIZTipoUsuario> ObtenerTiposUsuario()
        {
            //DAL.DALUsuario D_User = new DAL.DALUsuario();
            return D_User.GetAllTipoUsuario();
        }

        public bool CheckByName(BIZUsuario User)
        {
            return D_User.CheckByName(User);
        }

        public void ValidarUsuario(BIZUsuario User)
        {
            D_User.ValidateUser(User);
        }

        public void InsertarUsuario(BIZUsuario User)
        {
            D_User.InsertUsuario(User);
        }

        public BIZUsuario validarLogin(BIZUsuario UserCheck)
        {
            return D_User.ValidateLogin(UserCheck);
        }


        public BIZUsuario ObtenerUserByUsuario(string NombreUsuario)
        {
            return D_User.getUserByUsuario(NombreUsuario);
        }

        public BIZUsuario ObtenerUsarioByID(int IDUsuario)
        {
            return D_User.getUserByID(IDUsuario);
        }

        public string ResetPassword(string UserToReset)
        {
            return D_User.ResetPassword(UserToReset);
        }

        public List<BIZUsuario> obtenerUsuarios()
        {
            return D_User.GetAllUsuarios();
        }

        public bool ActualizarUsuario(BIZUsuario usuario)
        {
            return D_User.UpdateUser(usuario);
        }
    }
}
