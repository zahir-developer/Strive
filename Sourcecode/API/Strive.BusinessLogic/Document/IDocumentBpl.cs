﻿using Strive.BusinessEntities.Model;
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
        Result UploadDocument(EmployeeDocumentModel documentModel);
        bool SaveDocument(EmployeeDocumentModel documentModel);
        List<EmployeeDocument> UploadFiles(List<EmployeeDocument> employeeDocuments);
        void DeleteFiles(List<EmployeeDocument> documents);
        string Upload(string Base64Url, string fileName);
        Result GetDocumentById(int documentId, string password);
        Result UpdatePassword(int documentId, string password);
        Result GetDocumentByEmployeeId(int employeeId);
        Result DeleteDocument(int documentId);
    }
}
