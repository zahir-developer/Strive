using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Document
{
    public class DocumentBpl : Strivebase, IDocumentBpl
    {
        public DocumentBpl(IDistributedCache cache, ITenantHelper tenantHelper, IConfiguration iConfig) : base(tenantHelper,cache,iConfig)
        {
        }

        public Result UploadDocument(List<Strive.BusinessEntities.Document.DocumentView> lstDocument)
        {
            try
            {
                foreach (var item in lstDocument)
                {
                    var fileFormat = Path.GetExtension(item.FileName);
                    if (fileFormat == ".doc" || fileFormat == ".docx" || fileFormat == ".pdf" || fileFormat == ".xls" || fileFormat == ".csv")
                    {
                        string FileName = item.FileName;
                        string UploadPath = _config.GetSection("StriveAdminSettings").GetSection("UploadPath").Value + FileName;
                        byte[] tempBytes = Convert.FromBase64String(item.Base64Url);
                        File.WriteAllBytes(UploadPath, tempBytes);
                    }
                    else
                    {
                        string errorMessage = "Invalid File Format";
                        _result = Helper.ErrorMessageResult(errorMessage);
                        return _result;
                    }

                }

                var uploadDoc = new DocumentRal(_tenant).UploadDocument(lstDocument);

                _resultContent.Add(uploadDoc.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetDocumentById(long documentId, long employeeId, string password)
        {
            try
            {
                var lstDocumentById = new DocumentRal(_tenant).GetDocumentById(documentId, employeeId, password);
                if (lstDocumentById != null)
                {
                    if (lstDocumentById.Password == password)
                    {
                        string path = _config.GetSection("StriveAdminSettings").GetSection("UploadPath").Value + lstDocumentById.FileName;
                        string base64data = string.Empty;
                        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            byte[] data = new byte[(int)fileStream.Length];
                            fileStream.Read(data, 0, data.Length);
                            base64data = Convert.ToBase64String(data);
                            lstDocumentById.Base64Url = base64data;
                        }
                    }
                    else
                    {
                        string errorMessage = "Invalid Password";
                        _result = Helper.ErrorMessageResult(errorMessage);
                        return _result;
                    }

                }

                _resultContent.Add(lstDocumentById.WithName("DocumentDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetAllDocument(long employeeId, long locationId)
        {
            try
            {
                var lstDocumentById = new DocumentRal(_tenant).GetAllDocument(employeeId, locationId);
                if (lstDocumentById.Count>0)
                {
                    foreach(var item in lstDocumentById)
                    {
                        string path = _config.GetSection("StriveAdminSettings").GetSection("UploadPath").Value + item.FileName;
                        string base64data = string.Empty;
                        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            byte[] data = new byte[(int)fileStream.Length];
                            fileStream.Read(data, 0, data.Length);
                            base64data = Convert.ToBase64String(data);
                            item.Base64Url = base64data;
                        }
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
        public Result UpdatePassword(Strive.BusinessEntities.Document.DocumentView lstUpdateDocument)
        {
            try
            {
                var updatePasswordForDocId = new DocumentRal(_tenant).UpdatePassword(lstUpdateDocument);
                _resultContent.Add(updatePasswordForDocId.WithName("PasswordUpdated"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;

        }


    }
}
