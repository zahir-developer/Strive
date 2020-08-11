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

        public bool SaveDocument(EmployeeDocumentModel documents)
        {
            dbRepo.InsertPc<EmployeeDocumentModel>(documents, "EmployeeDocumentId");
            return true;
        }

        public DocumentViewModel GetDocumentById(long documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            List<DocumentViewModel> lstDocument = new List<DocumentViewModel>();
            lstDocument = db.Fetch<DocumentViewModel>(SPEnum.USPGETEMPLOYEEDOCUMENTBYID.ToString(), dynParams);
            return lstDocument.FirstOrDefault();
        }
        public List<DocumentViewModel> GetDocumentByEmployeeId(long employeeId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", employeeId);
            List<DocumentViewModel> lstDocument = new List<DocumentViewModel>();
            lstDocument = db.Fetch<DocumentViewModel>(SPEnum.USPGETEMPLOYEEDOCUMENTBYEMPID.ToString(), dynParams);
            return lstDocument;
        }
        public bool UpdatePassword(int documentId, string password)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            dynParams.Add("@Password", password);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPUPDATEDOCUMENTPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteDocument(int documentId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEEMPLOYEEDOCUMENTBYID.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
