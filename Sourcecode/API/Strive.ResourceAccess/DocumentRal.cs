using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Document;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class DocumentRal : RalBase
    {
        public DocumentRal(ITenantHelper tenant) : base(tenant) { }

        public bool SaveEmployeeDocument(EmployeeDocumentModel documents)
        {
            dbRepo.InsertPc<EmployeeDocumentModel>(documents, "EmployeeDocumentId");
            return true;
        }

        public EmployeeDocumentViewModel GetEmployeeDocumentById(long documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            List<EmployeeDocumentViewModel> lstDocument = new List<EmployeeDocumentViewModel>();
            lstDocument = db.Fetch<EmployeeDocumentViewModel>(EnumSP.Document.USPGETEMPLOYEEDOCUMENTBYID.ToString(), dynParams);
            return lstDocument.FirstOrDefault();
        }
        public List<EmployeeDocumentViewModel> GetEmployeeDocumentByEmployeeId(long employeeId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", employeeId);
            List<EmployeeDocumentViewModel> lstDocument = new List<EmployeeDocumentViewModel>();
            lstDocument = db.Fetch<EmployeeDocumentViewModel>(EnumSP.Document.USPGETEMPLOYEEDOCUMENTBYEMPID.ToString(), dynParams);
            return lstDocument;
        }
        public bool UpdatePassword(int documentId, string password)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            dynParams.Add("@Password", password);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Document.USPUPDATEDOCUMENTPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteEmployeeDocument(int documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Document.USPDELETEEMPLOYEEDOCUMENTBYID.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public int AddDocument(DocumentDto documents)
        {
            return dbRepo.InsertPK(documents, "DocumentId");
        }
        public bool UpdateDocument(DocumentDto documentModel)
        {
            return dbRepo.Update(documentModel.Document);
        }
      
        public bool DeleteDocument(int documentType)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@documentType", documentType);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Document.USPDELETEDOCUMENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public DocumentViewModel GetDocument(int documentType, int? documentSubType = null)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@documentType", documentType);
            dynParams.Add("@documentSubType", documentSubType);
            var result = db.FetchMultiResult<DocumentViewModel>(EnumSP.Document.USPGETDOCUMENT.ToString(), dynParams);
            return result;
        }

        public bool DeleteDocumentById(int documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@documentId", documentId);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Document.USPDELETEDOCUMENTBYID.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public DocumentViewModel GetDocumentById(int documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@documentId", documentId);
            var result = db.FetchMultiResult<DocumentViewModel>(EnumSP.Document.USPGETDOCUMENTBYID.ToString(), dynParams);
            return result;
        }
        public List<DocumentView> GetAllDocument(int documentTypeId)
        {
            _prm.Add("@documentType", documentTypeId);
            var result = db.Fetch<DocumentView>(EnumSP.Document.USPGETALLDOCUMENT.ToString(), _prm);
            return result;
        }
    }
}
