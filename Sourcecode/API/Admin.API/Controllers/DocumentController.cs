using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public Result UploadDocument([FromBody] List<Strive.BusinessEntities.Document.DocumentView> lstDocument)
        {
            return _documentBpl.UploadDocument(lstDocument);
        }
        [HttpGet]
        [Route("GetDocumentById/{documentId},{employeeId},{password}")] 
        public Result GetDocumentById(long documentId, long employeeId, string password)
        {
            return _documentBpl.GetDocumentById(documentId, employeeId, password);
        }
        [HttpPut]
        [Route("UpdatePassword/{documentId},{employeeId},{password}")] 
        public Result UpdatePassword(long documentId, long employeeId, string password)
        {
            return _documentBpl.UpdatePassword(documentId, employeeId, password);
        }
        [HttpGet]
        [Route("GetAllDocument/{employeeId},{locationId}")]
        public Result GetAllDocument(long employeeId, long locationId)
        {
            return _documentBpl.GetAllDocument(employeeId, locationId);
        }
    }
}
