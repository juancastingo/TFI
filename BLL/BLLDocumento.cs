using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL;

namespace BLL
{
    public class BLLDocumento
    {
        DALDocumento DocWorker = new DALDocumento();

        public List<BIZDocumento> ObtenerDocsXEmpresa(int IDEmpresa, int tipoDoc)
        {
            return DocWorker.getDocsByEmpresa(IDEmpresa, tipoDoc);
        }
        public List<BIZDocumento> ObtenerDocsXEmpresa(int IDEmpresa, int tipoDoc, int IdEstado)
        {
            return DocWorker.getDocsByEmpresa(IDEmpresa, tipoDoc, IdEstado);
        }

        public List<BIZDocumento> ObtenerDocsXEstado(int tipoDoc, int IDEstado)
        {
            return DocWorker.getDocsByEstado(tipoDoc, IDEstado);
        }

        public BIZDocumento ObtenerDocXID(int IDDocATrear)
        {
            return DocWorker.getDocByID(IDDocATrear);
        }

        public void RemoveDetalleDoc(int IDDocumento)
        {
            DocWorker.RemoveDetalleDoc(IDDocumento);
        }

        public int GuardarDocumento(BIZDocumento _Documento)
        {
            return DocWorker.SaveDocumento(_Documento);
        }

        public int GuardarDocumentoFac(BIZDocumento _Documento,BIZDocumento _Pedido)
        {
            int tipoDoc = Convert.ToInt32(_Documento.IDDocumentoTipo);
             int LN = DocWorker.getLastNumber(tipoDoc);
             _Documento.NrDocumento = LN;
             _Documento.FechaVencimiento = _Documento.FechaEmision.AddDays(30);
             int nrFac = DocWorker.SaveDocumento(_Documento);
             _Pedido.IDDocumentoRef = nrFac;
             _Pedido.IDEstado = 8; //Estado facturado de pedido
             _Pedido.FechaUltimaModificacion = _Documento.FechaUltimaModificacion;
             _Pedido.IDUsuarioUltimaModificacion = _Documento.IDUsuarioUltimaModificacion;
             DocWorker.update2documento(_Pedido);
             //DocWorker.updatePedidoNrRefYEstado(_Documento);
             return nrFac;
        }

        public void ActualizarDocumento(BIZDocumento _Documento)
        {
            DocWorker.UpdateDocumento(_Documento);
        }

        public void ActualizarStatusDoc(int IdDoc, int IdStatus, int IDUpdate)
        {
            DocWorker.UpdateStatusDoc(IdDoc, IdStatus, IDUpdate);
        }
        public void ActualizarStatusDoc(int IdDoc, int IdStatus, int IDUpdate, string just)
        {
            DocWorker.UpdateStatusDoc(IdDoc, IdStatus, IDUpdate, just);
        }

        public double verCCEstado(int IDClienteEmp)
        {
            return DocWorker.CheckCCStatus(IDClienteEmp);
        }

        public int ObtenerDocPendientes(int a, int b)
        {
            return DocWorker.getPendingDocs(a, b);
        }


        public void registrarDocs(DateTime fecha1, DateTime fecha2)
        {
            DocWorker.registerDocs(fecha1, fecha2);
        }

        public List<BIZDocumento> ObtenerDocsXTipo(int tipoDoc)
        {
            return DocWorker.getDocsByType(tipoDoc);
        }
    }
}
