using Admin.API.Helpers;
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
    public class DocumentController : StriveControllerBase<IDocumentBpl>
    {
        public DocumentController(IDocumentBpl documentBpl) : base(documentBpl) { }

        [HttpPost]
        [Route("SaveDocument")]
        public Result UploadDocument([FromBody]EmployeeDocumentModel documentModel)
        {
            return _bplManager.UploadDocument(documentModel);
        }
        [HttpGet]
        [Route("GetDocumentById/{documentId},{password}")]
        public Result GetDocumentById(int documentId, string password)
        {
            return _bplManager.GetDocumentById(documentId, password);
        }
        [HttpPut]
        [Route("UpdatePassword")]
        public Result UpdatePassword(int documentId, string password)
        {
            return _bplManager.UpdatePassword(documentId, password);
        }
        [HttpGet]
        [Route("GetDocument/{employeeId}")]
        public Result GetDocument(int employeeId)
        {
            return _bplManager.GetDocumentByEmployeeId(employeeId);
        }
        [HttpDelete]
        [Route("{documentId}")]
        public Result DeleteDocument(int documentId)
        {
            return _bplManager.DeleteDocument(documentId);
        }

    }
}
