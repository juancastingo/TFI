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

        public void ActualizarDocumento(BIZDocumento _Documento)
        {
            DocWorker.UpdateDocumento(_Documento);
        }

        public void ActualizarStatusDoc(int IdDoc, int IdStatus)
        {
            DocWorker.UpdateStatusDoc(IdDoc, IdStatus);
        }

        public double verCCEstado(int IDClienteEmp)
        {
            return DocWorker.CheckCCStatus(IDClienteEmp);
        }

        public int ObtenerDocPendientes(int a, int b)
        {
            return DocWorker.getPendingDocs(a, b);
        }

    }
}
