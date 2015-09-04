using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIZ;
using TFITest4.Models;

namespace TFITest4
{
    public class AutoMapperUIConfiguration
    {

        public static void Start()
        {
            Mapper.Initialize(config => {
                //config.AddProfile<SL.Utils.DataAccessMapperProfile>();
                config.AddProfile<UIMapperProfile>();            
            });
        }



        public static void Configure()
        {
            ConfigureUserMapping();
            ConfigureLugarMapping();
            ConfigureClienteMapping();
            ConfigureEstadoMapping();
        }

        private static void ConfigureUserMapping()
        {
            Mapper.CreateMap<RegisterModel, BIZUsuario>();
            Mapper.CreateMap<BIZUsuario, RegisterModel>();
        }
        private static void ConfigureLugarMapping()
        {
            Mapper.CreateMap<BIZPais, ModelPais>();
            Mapper.CreateMap<BIZLocalidad, ModelLocalidad>();
            Mapper.CreateMap<BIZProvincia, ModelProvincia>();
            Mapper.CreateMap<BIZDireccion, ModelDireccion>();
            Mapper.CreateMap<ModelPais, BIZPais>();
            Mapper.CreateMap<ModelLocalidad, BIZLocalidad>();
            Mapper.CreateMap<ModelProvincia, BIZProvincia>();
            Mapper.CreateMap<ModelDireccion, BIZDireccion>();
        }
        private static void ConfigureClienteMapping()
        {
            Mapper.CreateMap<BIZClienteEmpresa, ModelClienteEmpresa>();
            Mapper.CreateMap<ModelClienteEmpresa, BIZClienteEmpresa>();
        }
        private static void ConfigureEstadoMapping()
        {
            Mapper.CreateMap<BIZEstado, BIZEstado>(); //???????
            //Mapper.CreateMap<BIZEstado, mode
        }
    }
}