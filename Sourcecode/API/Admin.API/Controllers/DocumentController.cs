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
        [Route("SaveEmployeeDocument")]
        public Result UploadEmployeeDocument([FromBody]EmployeeDocumentModel documentModel)
        {
            return _bplManager.UploadEmployeeDocument(documentModel);
        }
        [HttpGet]
        [Route("GetEmployeeDocumentById/{documentId},{password}")]
        public Result GetEmployeeDocumentById(int documentId, string password)
        {
            return _bplManager.GetEmployeeDocumentById(documentId, password);
        }
        [HttpPut]
        [Route("UpdatePassword")]
        public Result UpdatePassword(int documentId, string password)
        {
            return _bplManager.UpdatePassword(documentId, password);
        }
        [HttpGet]
        [Route("GetEmployeeDocument/{employeeId}")]
        public Result GetEmployeeDocument(int employeeId)
        {
            return _bplManager.GetEmployeeDocumentByEmployeeId(employeeId);
        }
        [HttpDelete]
        [Route("DeleteEmployeeDocument/{documentId}")]
        public Result DeleteEmployeeDocument(int documentId)
        {
            return _bplManager.DeleteEmployeeDocument(documentId);
        }

        [HttpPost]
        [Route("AddDocument")]
        public Result AddDocument([FromBody] DocumentDto documentModel)
        {
            return _bplManager.AddDocument(documentModel);
        }

        [HttpGet]
        [Route("GetDocument/{documentTypeId}/{documentType}")]
        public Result GetDocument(int documentTypeId, GlobalUpload.DocumentType documentType)
        {
            return _bplManager.GetDocument(documentTypeId, documentType);
        }

        [HttpDelete]
        [Route("DeleteDocument/{documentTypeId}/{documentType}")]
        public Result DeleteDocument(int documentTypeId, GlobalUpload.DocumentType documentType)
        {
            return _bplManager.DeleteDocument(documentTypeId, documentType);
        }

    }
}
