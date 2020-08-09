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

        public DocumentView GetDocumentById(long documentId, long employeeId, string password)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            dynParams.Add("@EmployeeId", employeeId);
            List<DocumentView> lstDocument = new List<DocumentView>();
            lstDocument = db.Fetch<DocumentView>(SPEnum.USPGETDOCUMENTBYEMPID.ToString(), dynParams);
            return lstDocument.FirstOrDefault();
        }
        public List<DocumentView> GetAllDocument(long employeeId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", employeeId);
            List<DocumentView> lstDocument = new List<DocumentView>();
            lstDocument = db.Fetch<DocumentView>(SPEnum.USPGETALLDOCUMENTBYID.ToString(), dynParams);
            return lstDocument;
        }
        public bool UpdatePassword(Strive.BusinessEntities.Document.DocumentView lstUpdateDocument)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", lstUpdateDocument.DocumentId);
            dynParams.Add("@EmployeeId", lstUpdateDocument.EmployeeId);
            dynParams.Add("@Password", lstUpdateDocument.Password);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPUPDATEDOCUMENTPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteDocument(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", id);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEDOCUMENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
