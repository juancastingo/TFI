using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL.ORM;
using AutoMapper;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace DAL
{
    public class DALDocumento
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        public DALDocumento()
        {
            DALAutommaper automapper = new DALAutommaper();
        }

        public List<BIZDocumento> getDocsByEmpresa(int IDEmpresa, int tipoDoc)
        {

            var Daldocs = db.Documento
                .Where(b => b.IDClienteEmpresa == IDEmpresa); //aca tengo q traer de sesion
            if (tipoDoc != null)
            {
                Daldocs = db.Documento
               .Where(b => b.IDClienteEmpresa == IDEmpresa)
               .Where(b => b.IDDocumentoTipo == tipoDoc);
            }
            var Ddocs = Daldocs.ToList();
            var documentos = Mapper.Map<List<Documento>, List<BIZDocumento>>(Ddocs);
            return documentos;
        }

        public List<BIZDocumento> getDocsByEmpresa(int IDEmpresa, int tipoDoc, int IdEstado)
        {

            var Daldocs = db.Documento
                .Where(b => b.IDClienteEmpresa == IDEmpresa);

            if (tipoDoc != null)
            {
                if (IdEstado == -1)
                {
                    Daldocs = db.Documento
                   .Where(b => b.IDClienteEmpresa == IDEmpresa)
                   .Where(b => b.IDDocumentoTipo == tipoDoc);
                }
                else
                {
                    Daldocs = db.Documento
                   .Where(b => b.IDClienteEmpresa == IDEmpresa)
                   .Where(b => b.IDDocumentoTipo == tipoDoc)
                   .Where(b => b.IDEstado == IdEstado);
                }
            }

            var Ddocs = Daldocs.ToList();
            var documentos = Mapper.Map<List<Documento>, List<BIZDocumento>>(Ddocs);
            return documentos;
        }

        public List<BIZDocumento> getDocsByEstado(int tipoDoc, int IDEstado)
        {

            var Daldocs = db.Documento
                .Where(b => b.IDDocumentoTipo == tipoDoc)
                .Where(b => b.IDEstado == IDEstado);

            var Ddocs = Daldocs.ToList();
            var documentos = Mapper.Map<List<Documento>, List<BIZDocumento>>(Ddocs);
            return documentos;
        }

        public BIZDocumento getDocByID(int IDDocATrear)
        {
            var Daldoc = db.Documento
                .Where(b => b.IDDocumento == IDDocATrear).FirstOrDefault(); //aca tengo q traer de sesion

            var documento = Mapper.Map<Documento, BIZDocumento>(Daldoc);
            return documento;
        }

        public void RemoveDetalleDoc(int IDDocumento)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM DocumentoDetalle WHERE IDDocumento = " + IDDocumento);
        }






        public int SaveDocumento(BIZDocumento _Documento)
        {
            //Documento SaveDoc = Mapper.Map<BIZDocumento, Documento>(_Documento); //no se puede mappear aca?
            Documento OtroDoc = new Documento();
            OtroDoc.CodigoExterno = _Documento.CodigoExterno;
            OtroDoc.FechaContable = _Documento.FechaContable;
            OtroDoc.FechaEmision = _Documento.FechaEmision;
            OtroDoc.FechaUltimaModificacion = OtroDoc.FechaEmision; //porq lo estoy creando
            OtroDoc.FechaVencimiento = _Documento.FechaVencimiento;
            OtroDoc.IDClienteEmpresa = _Documento.IDClienteEmpresa;
            OtroDoc.IDDocumento = _Documento.IDDocumento;
            OtroDoc.IDDocumentoRef = _Documento.IDDocumentoRef;
            OtroDoc.IDDocumentoTipo = _Documento.IDDocumentoTipo;
            OtroDoc.IDEmpresaLocal = 1;
            OtroDoc.IDEstado = _Documento.IDEstado;
            OtroDoc.IDProveedor = _Documento.IDProveedor;
            OtroDoc.IDUsuarioCreacion = _Documento.IDUsuarioCreacion;
            OtroDoc.IPCreacion = _Documento.IPCreacion;
            OtroDoc.IPUltimaModificacion = _Documento.IPUltimaModificacion;
            OtroDoc.Monto = _Documento.Monto;
            OtroDoc.IDUsuarioUltimaModificacion = OtroDoc.IDUsuarioCreacion; //porq lo estoy creando
            DocumentoDetalle DocDetalle;
            foreach (var Detalle in _Documento.DocumentoDetalle)
            {
                DocDetalle = new DocumentoDetalle();
                DocDetalle.Cantidad = Detalle.Cantidad;
                DocDetalle.IDPrecioDetalle = Detalle.IDPrecioDetalle;
                OtroDoc.DocumentoDetalle.Add(DocDetalle);
            }
            db.Documento.Add(OtroDoc);
            db.SaveChanges();
            return OtroDoc.IDDocumento;
        }

        public void UpdateDocumento(BIZDocumento _Documento)
        {

            //Documento SaveDoc = Mapper.Map<BIZDocumento, Documento>(_Documento); //no se puede mappear aca?
            Documento BaseDoc = new Documento();
            DateTime fecha = (DateTime)_Documento.FechaUltimaModificacion;

            string query = "UPDATE Documento SET FechaUltimaModificacion='" + fecha.ToString("yyyy-MM-dd HH:mm:ss") + "',IDUsuarioUltimaModificacion=" + _Documento.IDUsuarioUltimaModificacion + "  WHERE IDDocumento = " + _Documento.IDDocumento;
            db.Database.ExecuteSqlCommand(query);
            DocumentoDetalle detalle;
            foreach (var d in _Documento.DocumentoDetalle)
            {
                detalle = new DocumentoDetalle();
                detalle.IDDocumento = _Documento.IDDocumento;
                detalle.IDPrecioDetalle = d.IDPrecioDetalle;
                detalle.Cantidad = d.Cantidad;
                db.DocumentoDetalle.Add(detalle);
            }
            db.SaveChanges();
            //_Documento.FechaUltimaModificacion



            //var original = db.Documento.Find(_Documento.IDDocumento);

            //if (original != null)
            //{
            //db.Documento
            //((IObjectContextAdapter)db).ObjectContext.Detach(SaveDoc);

            //db.Entry(SaveDoc).State = EntityState.Modified;
            // db.SaveChanges();
            //}




            //Documento OtroDoc = new Documento();
            //OtroDoc.IDDocumento = _Documento.IDDocumento;
            //OtroDoc.CodigoExterno = _Documento.CodigoExterno;
            //OtroDoc.FechaContable = _Documento.FechaContable;
            //OtroDoc.FechaEmision = _Documento.FechaEmision;
            //OtroDoc.FechaUltimaModificacion = _Documento.FechaUltimaModificacion; //porq lo estoy creando
            //OtroDoc.FechaVencimiento = _Documento.FechaVencimiento;
            //OtroDoc.IDClienteEmpresa = _Documento.IDClienteEmpresa;
            //OtroDoc.IDDocumento = _Documento.IDDocumento;
            //OtroDoc.IDDocumentoRef = _Documento.IDDocumentoRef;
            //OtroDoc.IDDocumentoTipo = _Documento.IDDocumentoTipo;
            //OtroDoc.IDEmpresaLocal = _Documento.IDEmpresaLocal;
            //OtroDoc.IDEstado = _Documento.IDEstado;
            //OtroDoc.IDProveedor = _Documento.IDProveedor;
            //OtroDoc.IDUsuarioCreacion = _Documento.IDUsuarioCreacion;
            //OtroDoc.IPCreacion = _Documento.IPCreacion;
            //OtroDoc.IPUltimaModificacion = _Documento.IPUltimaModificacion;
            //OtroDoc.Monto = _Documento.Monto;
            //OtroDoc.IDUsuarioUltimaModificacion = _Documento.IDUsuarioUltimaModificacion; 
            //DocumentoDetalle DocDetalle;
            //foreach (var Detalle in _Documento.DocumentoDetalle)
            //{
            //    DocDetalle = new DocumentoDetalle();
            //    DocDetalle.Cantidad = Detalle.Cantidad;
            //    DocDetalle.IDPrecioDetalle = Detalle.IDPrecioDetalle;
            //    OtroDoc.DocumentoDetalle.Add(DocDetalle);
            //}
            //db.Entry(SaveDoc).State = EntityState.Modified;

            //db.SaveChanges();
            //db.Entry(OtroDoc).State = EntityState.Modified;
            //db.SaveChanges();
        }
        public void UpdateStatusDoc(int IdDoc, int IdStatus)
        {
            string query = "UPDATE Documento SET IDEstado=" + IdStatus + "  WHERE IDDocumento = " + IdDoc;
            db.Database.ExecuteSqlCommand(query);
        }

    }
}
