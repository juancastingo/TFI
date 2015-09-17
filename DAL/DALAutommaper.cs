using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL.ORM;

namespace DAL
{
    public class DALAutommaper
    {
        public DALAutommaper()
        {
            //mappers usuario y relacionados
            AutoMapper.Mapper.CreateMap<Usuario, BIZUsuario>();
            AutoMapper.Mapper.CreateMap<BIZUsuario, Usuario>();
            AutoMapper.Mapper.CreateMap<BIZTipoUsuario, TipoUsuario>();
            AutoMapper.Mapper.CreateMap<TipoUsuario, BIZTipoUsuario>();

            //creo mappers direcciones
            AutoMapper.Mapper.CreateMap<Localidad, BIZLocalidad>();
            AutoMapper.Mapper.CreateMap<Provincia, BIZProvincia>();
            AutoMapper.Mapper.CreateMap<Pais, BIZPais>();
            AutoMapper.Mapper.CreateMap<BIZLocalidad, Localidad>();
            AutoMapper.Mapper.CreateMap<BIZProvincia, Provincia>();
            AutoMapper.Mapper.CreateMap<BIZPais, Pais>();
            AutoMapper.Mapper.CreateMap<Direccion, BIZDireccion>();
            AutoMapper.Mapper.CreateMap<BIZDireccion, Direccion>();

            AutoMapper.Mapper.CreateMap<EstadoMisc, BIZEstado>();
            AutoMapper.Mapper.CreateMap<BIZEstado, EstadoMisc>();
            AutoMapper.Mapper.CreateMap<ClienteEmpresa, BIZClienteEmpresa>();
            AutoMapper.Mapper.CreateMap<BIZClienteEmpresa, ClienteEmpresa>();
            AutoMapper.Mapper.CreateMap<TipoIVA, BIZTipoIVA>();
            AutoMapper.Mapper.CreateMap<BIZTipoIVA, TipoIVA>();

            //precios
            AutoMapper.Mapper.CreateMap<ListaPrecio, BIZListaPrecio>();
            AutoMapper.Mapper.CreateMap<PrecioDetalle, BIZPrecioDetalle>();
            //al reves
            AutoMapper.Mapper.CreateMap<BIZListaPrecio, ListaPrecio>();
            AutoMapper.Mapper.CreateMap<BIZPrecioDetalle, PrecioDetalle>();

            //producto
            AutoMapper.Mapper.CreateMap<Producto, BIZProducto>();
            AutoMapper.Mapper.CreateMap<ProductoCategoria, BIZProductoCategoria>();
            //al reves
            AutoMapper.Mapper.CreateMap<BIZProducto, Producto>();
            AutoMapper.Mapper.CreateMap<BIZProductoCategoria, ProductoCategoria>();

            //doc
            AutoMapper.Mapper.CreateMap<Documento, BIZDocumento>();
            AutoMapper.Mapper.CreateMap<DocumentoTipo, BIZDocumentoTipo>();
            AutoMapper.Mapper.CreateMap<DocumentoDetalle, BIZDocumentoDetalle>();
            //al reves
            AutoMapper.Mapper.CreateMap<BIZDocumento, Documento>();
            AutoMapper.Mapper.CreateMap<BIZDocumentoTipo, DocumentoTipo>();
            AutoMapper.Mapper.CreateMap<BIZDocumentoDetalle, DocumentoDetalle>();

            //precio
            AutoMapper.Mapper.CreateMap<ListaPrecio, BIZListaPrecio>();
            AutoMapper.Mapper.CreateMap<BIZListaPrecio, ListaPrecio>();
            //al reves
            AutoMapper.Mapper.CreateMap<PrecioDetalle, BIZPrecioDetalle>();
            AutoMapper.Mapper.CreateMap<BIZPrecioDetalle, PrecioDetalle>();

            //empresa local
            AutoMapper.Mapper.CreateMap<EmpresaLocal, BIZEmpresaLocal>();
            AutoMapper.Mapper.CreateMap<BIZEmpresaLocal, EmpresaLocal>();
        }
    }
}
