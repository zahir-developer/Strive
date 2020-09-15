using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography;

namespace Strive.BusinessLogic.Document
{
    public class DocumentBpl : Strivebase, IDocumentBpl
    {
        public DocumentBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result UploadDocument(EmployeeDocumentModel documentModel)
        {
            try
            {
                string error = ValidateFiles(documentModel.EmployeeDocument);
                if (!(error == string.Empty))
                {
                    _result = Helper.ErrorMessageResult(error);
                }

                documentModel.EmployeeDocument = UploadFiles(documentModel.EmployeeDocument);
                var documentSave = false;
                if (documentModel.EmployeeDocument != null)
                {
                    if (!documentModel.EmployeeDocument.Any(s => s.Filename == string.Empty))
                    {
                        documentSave = SaveDocument(documentModel);

                        if (!documentSave)
                        {
                            DeleteFiles(documentModel.EmployeeDocument);
                        }

                        _resultContent.Add(documentSave.WithName("Status"));
                        _result = Helper.BindSuccessResult(_resultContent);
                    }
                    else
                    {
                        documentSave = false;
                        _resultContent.Add(documentSave.WithName("Status"));
                        _result = Helper.BindSuccessResult(_resultContent);
                    }

                }
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public string ValidateFiles(List<EmployeeDocument> employeeDocument)
        {
            string error = string.Empty;
            foreach (var doc in employeeDocument)
            {
                error = ValidateFileFormat(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT, doc.Filename);
                if (!string.IsNullOrEmpty(error))
                    return error;
            }
            return error;
        }

        public List<EmployeeDocument> UploadFiles(List<EmployeeDocument> employeeDocuments)
        {

            foreach (var doc in employeeDocuments)
            {
                doc.Filename = Upload(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT, doc.Base64, doc.Filename);
                doc.FileType = Path.GetExtension(doc.Filename);
                if (doc.Filename == string.Empty)
                {
                    DeleteFiles(employeeDocuments);
                }
            }

            return employeeDocuments;
        }

        public string Upload(GlobalUpload.UploadFolder uploadFolder, string Base64Url, string fileName)
        {
            string uploadPath = GetUploadFolderPath(uploadFolder);
            fileName = fileName.Replace(Path.GetExtension(fileName), string.Empty) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            if (!File.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            uploadPath = uploadPath + fileName;
            byte[] tempBytes = Convert.FromBase64String(Base64Url);
            File.WriteAllBytes(uploadPath, tempBytes);
            return fileName;

        }

        public string ValidateFileFormat(GlobalUpload.UploadFolder upload, string fileName)
        {
            string invalid = string.Empty;
            if (GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT == upload)
            {
                if (_tenant.DocumentFormat.Contains(Path.GetExtension(fileName).ToUpper()))
                    return string.Empty;
                else
                    return "Invalid file format uploaded. Valid formats: " + _tenant.DocumentFormat;
            }
            else if (GlobalUpload.UploadFolder.PRODUCTIMAGE == upload)
            {
                if (_tenant.ProductImageFormat.Contains(Path.GetExtension(fileName).ToUpper()))
                    return string.Empty;
                else
                    return invalid + _tenant.ProductImageFormat;
            }
            else
                return string.Empty;
        }

        public void DeleteFiles(List<EmployeeDocument> documents)
        {
            string uploadPath = GetUploadFolderPath(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT);

            string filePath = string.Empty;
            foreach (var doc in documents)
            {
                filePath = uploadPath + doc.Filename;
                if (File.Exists(uploadPath))
                {
                    File.Delete(uploadPath);
                }
            }
        }

        public bool SaveDocument(EmployeeDocumentModel documentModel)
        {
            return new DocumentRal(_tenant).SaveDocument(documentModel);
        }

        public Result GetDocumentById(int documentId, string password)
        {
            try
            {
                var document = new DocumentRal(_tenant).GetDocumentById(documentId);
                string base64 = string.Empty;
                if (document != null)
                {
                    if (document.IsPasswordProtected)
                    {
                        if (document.Password == password)
                            base64 = GetBase64(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT, document.FileName);
                        else
                        {
                            string errorMessage = "Invalid Password !!!";
                            _result = Helper.ErrorMessageResult(errorMessage);
                            return _result;
                        }
                    }
                    else
                    {
                        base64 = GetBase64(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT, document.FileName);
                    }
                }

                _resultContent.Add(base64.WithName("Document"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public string GetBase64(GlobalUpload.UploadFolder module, string fileName)
        {
            string baseFolder = GetUploadFolderPath(module);

            string path = baseFolder + fileName;

            string base64data = string.Empty;

            if (!File.Exists(path))
                return string.Empty;

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[(int)fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                base64data = Convert.ToBase64String(data);
            }

            return base64data;
        }

        public Result GetDocumentByEmployeeId(int employeeId)
        {
            try
            {
                var lstDocumentById = new DocumentRal(_tenant).GetDocumentByEmployeeId(employeeId);
                if (lstDocumentById.Count > 0)
                {
                    foreach (var item in lstDocumentById)
                    {
                        item.Base64Url = GetBase64(GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT, item.FileName);
                    }
                }

                _resultContent.Add(lstDocumentById.WithName("GetAllDocuments"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result UpdatePassword(int documentId, string password)
        {
            try
            {
                return ResultWrap(new DocumentRal(_tenant).UpdatePassword, documentId, password, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;

        }

        public Result DeleteDocument(int documentId)
        {
            try
            {
                return ResultWrap(new DocumentRal(_tenant).DeleteDocument, documentId, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public void DeleteFile(GlobalUpload.UploadFolder uploadFolder, string fileName)
        {
            string filePath = GetUploadFolderPath(uploadFolder) + fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetUploadFolderPath(GlobalUpload.UploadFolder module)
        {
            string path = string.Empty;
            string subPath = string.Empty;
            switch (module)
            {
                case GlobalUpload.UploadFolder.EMPLOYEEDOCUMENT:
                    subPath = _tenant.DocumentUploadFolder;
                    break;
                case GlobalUpload.UploadFolder.PRODUCTIMAGE:
                    subPath = _tenant.ProductImageFolder;
                    break;
                default:
                    subPath = "";
                    break;
            }

            return path + subPath;

        }

        public void SaveThumbnail(int Width, int Height, string base64String, string saveFilePath)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);

            var streamImg = new MemoryStream(byteBuffer);
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping   
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    // Save the file path, note we use png format to support png file   
                    objBitmap.Save(saveFilePath);
                }
            }
        }

    }

}
