using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Document
{
    public interface IDocumentBpl
    {
        Result UploadDocument(List<Strive.BusinessEntities.Document.DocumentView> lstDocument);
        Result GetDocumentById(long documentId, long employeeId, string password);
        Result UpdatePassword(Strive.BusinessEntities.Document.DocumentView lstUpdateDocument);
        Result GetAllDocument(long employeeId);
        Result DeleteDocument(long id);
    }
}
