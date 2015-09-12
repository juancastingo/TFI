using AutoMapper;
using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFITest4.Models;

namespace TFITest4
{
    class UIMapperProfile : Profile
    {
        protected override void Configure()
        {
            //usuarios
            Mapper.CreateMap<RegisterModel, BIZUsuario>();
            Mapper.CreateMap<BIZUsuario, RegisterModel>();
            //usuarios2
            Mapper.CreateMap<ModelUsuario, BIZUsuario>();
            Mapper.CreateMap<BIZUsuario, ModelUsuario>();

            Mapper.CreateMap<BIZPais, ModelPais>();
            Mapper.CreateMap<BIZLocalidad, ModelLocalidad>();
            Mapper.CreateMap<BIZProvincia, ModelProvincia>();
            Mapper.CreateMap<BIZDireccion, ModelDireccion>();
            Mapper.CreateMap<ModelPais, BIZPais>();
            Mapper.CreateMap<ModelLocalidad, BIZLocalidad>();
            Mapper.CreateMap<ModelProvincia, BIZProvincia>();
            Mapper.CreateMap<ModelDireccion, BIZDireccion>();

            Mapper.CreateMap<BIZClienteEmpresa, ModelClienteEmpresa>();
            Mapper.CreateMap<ModelClienteEmpresa, BIZClienteEmpresa>();
            

            // Mapper.CreateMap<BIZEstado, BIZEstado>();

            //precios
            Mapper.CreateMap<BIZPrecioDetalle, ModelPrecioDetalle>();
            Mapper.CreateMap<BIZListaPrecio, ModelListaPrecio>();
            Mapper.CreateMap<ModelPrecioDetalle, BIZPrecioDetalle>();
            Mapper.CreateMap<ModelListaPrecio, BIZListaPrecio>();
            

        }
    }
}
