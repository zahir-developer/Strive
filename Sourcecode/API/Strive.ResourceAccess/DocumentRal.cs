using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Document;
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
    public class DocumentRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public DocumentRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public bool UploadDocument(List<Strive.BusinessEntities.Document.DocumentView> lstDocument)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Document> lstDoc = new List<Document>();
            foreach (var item in lstDocument)
            {
                lstDoc.Add(new Document
                {
                    DocumentId = item.DocumentId,
                    EmployeeId = item.EmployeeId,
                    FileName = item.FileName,
                    FilePath = item.FilePath,
                    Password = item.Password,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                    IsActive = item.IsActive

                });

            }
            dynParams.Add("@tvpDocument", lstDoc.ToDataTable().AsTableValuedParameter("tvpDocument"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEDOCUMENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
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
        public bool UpdatePassword(long documentId, long employeeId, string password)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@DocumentId", documentId);
            dynParams.Add("@EmployeeId", employeeId);
            dynParams.Add("@Password", password);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPUPDATEDOCUMENTPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
