using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Document;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{

    [Authorize]
    [Route("Admin/[Controller]")]
    public class DocumentController : ControllerBase
    {
        readonly IDocumentBpl _documentBpl = null;

        public DocumentController(IDocumentBpl documentBpl)
        {
            _documentBpl = documentBpl;
        }

        [HttpPost]
        [Route("SaveDocument")]
        public Result UploadDocument([FromBody]EmployeeDocumentModel documentModel)
        {
            return _documentBpl.UploadDocument(documentModel);
        }
        [HttpGet]
        [Route("GetDocumentById/{documentId},{employeeId},{password}")] 
        public Result GetDocumentById(long documentId, long employeeId, string password)
        {
            return _documentBpl.GetDocumentById(documentId, employeeId, password);
        }
        [HttpPost]
        [Route("UpdatePassword")] 
        public Result UpdatePassword([FromBody] Strive.BusinessEntities.Document.DocumentView lstUpdateDocument)
        {
            return _documentBpl.UpdatePassword(lstUpdateDocument);
        }
        [HttpGet]
        [Route("GetAllDocument/{employeeId}")]
        public Result GetAllDocument(long employeeId)
        {
            return _documentBpl.GetAllDocument(employeeId);
        }
        [HttpDelete]
        [Route("{id}")]
        public Result DeleteDocument(long id)
        {
            return _documentBpl.DeleteDocument(id);
        }

    }
}
