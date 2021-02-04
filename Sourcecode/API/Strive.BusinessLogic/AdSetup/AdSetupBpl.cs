using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.AdSetup
{
    public class AdSetupBpl : Strivebase, IAdSetupBpl
    {
        public AdSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result AddAdSetup(AdSetupDto adSetup)
        {
            int documentId = new DocumentBpl(_cache, _tenant).AddDocument(adSetup.Document);
            adSetup.AdSetupAddDto.AdSetup.DocumentId = documentId;
            return ResultWrap(new AdSetupRal(_tenant).AddAdSetup, adSetup.AdSetupAddDto, "AddNewAdSetup");
        }

        public Result UpdateAdSetup(AdSetupDto adSetup)
       {
            if (adSetup.RemoveDocument != null)
            {
                new DocumentBpl(_cache, _tenant).DeleteDocumentById(adSetup.RemoveDocument.Document.DocumentId, adSetup.RemoveDocument.DocumentType);

                int documentId = new DocumentBpl(_cache, _tenant).AddDocument(adSetup.Document);
                adSetup.AdSetupAddDto.AdSetup.DocumentId = documentId;
            }
            return ResultWrap(new AdSetupRal(_tenant).UpdateAdSetup, adSetup.AdSetupAddDto, "UpdateAdSetup");
        }
        public Result GetAllAdSetup()
        {
           var adsetup=new AdSetupRal(_tenant).GetAllAdSetup();
            if (adsetup.Count > 0)
            //{
            //    foreach (var item in adsetup)
            //    {
            //        item.Base64 = GetBase64(GlobalUpload.DocumentType.ADS, item.Image);
            //    }
            //}           

            _resultContent.Add(adsetup.WithName("GetAllAdSetup"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public Result GetAdSetupById(int id)
        {
           var adsetup=new AdSetupRal(_tenant).GetAdSetupById(id);



            adsetup.Base64 = GetBase64(GlobalUpload.DocumentType.ADS, adsetup.Image);

            _resultContent.Add(adsetup.WithName("GetAdSetupById"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public Result DeleteAdSetup(int id)
        {
            return ResultWrap(new AdSetupRal(_tenant).DeleteAdSetup, id, "AdSetupDelete");
        }


        public string GetBase64(GlobalUpload.DocumentType module, string fileName)
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

        private string GetUploadFolderPath(GlobalUpload.DocumentType module)
        {
            string path = string.Empty;
            string subPath = string.Empty;
            switch (module)
            {
                case GlobalUpload.DocumentType.EMPLOYEEDOCUMENT:
                    subPath = _tenant.DocumentUploadFolder;
                    break;
                case GlobalUpload.DocumentType.PRODUCTIMAGE:
                    subPath = _tenant.ProductImageFolder;
                    break;
                case GlobalUpload.DocumentType.LOGO:
                    subPath = _tenant.LogoImageFolder;
                    break;

                default:
                    subPath = _tenant.GeneralDocumentFolder + module.ToString() + "\\";
                    break;
            }

            subPath = subPath.Replace("TENANT_NAME", _tenant.SchemaName);

            return path + subPath;

        }


    }
}
