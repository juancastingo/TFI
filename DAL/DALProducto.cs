using BIZ;
using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProducto
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public List<BIZProducto> getProductosConPrecio() {

            var ListaPrecio = db.Database.SqlQuery<int>("select TOP 1 IDListaPrecio from ListaPrecio where FechaDesde < GETDATE() and Activo = 1 order by FechaDesde Desc");
            int IDListaPrecioActual = ListaPrecio.FirstOrDefault();
            //aca me traigo la lista con el ID
            var ListaPrecios = db.PrecioDetalle
                .Where(b => b.IDListaPrecio == IDListaPrecioActual);

            //aca traigo todos los producto que tengan estado "3" //Activo
            var productos = db.Producto
                .Where(b => b.IDEstado == 3);

            //List<Producto> Lista = new List<Producto>();
            List<BIZProducto> ListaP = new List<BIZProducto>();
            foreach (var p in productos)
            {
                BIZProducto Prod = new BIZProducto();
                Prod.Nombre = p.Nombre;
                Prod.IDProducto = p.IDProducto;
                Prod.Imagen = p.Imagen;
                Prod.Descripcion = p.Descripcion;
                Prod.ProductoCategoria.Detalle = p.ProductoCategoria.Detalle;
                Prod.ProductoCategoria.IDProductoCategoria = p.ProductoCategoria.IDProductoCategoria;
                foreach (var precioDetalle in p.PrecioDetalle)
                {
                    if (precioDetalle.IDListaPrecio == IDListaPrecioActual)
                    {
                        if ((bool)precioDetalle.Activo) //aca me fijo si está activo
                        {
                            Prod.PrecioActual = (double)precioDetalle.Precio;
                            Prod.IDPrecioDetalle = precioDetalle.IDPrecioDetalle;
                        }
                    }
                }
                if (Prod.PrecioActual != 0) // si no tiene precio no lo agrego...
                {
                    ListaP.Add(Prod);
                }
            }
            return ListaP;

        }

        public int CheckStockProd(int IDProd)
        {
            System.Nullable<int> iReturnValue = db.StockCheck(IDProd).SingleOrDefault();
            if (iReturnValue == null)
            {
                iReturnValue = 0;
            }
            return (int)iReturnValue;
        }

    }
}
