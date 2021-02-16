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
            {
                foreach (var item in adsetup)
                {
                    item.Base64 = new DocumentBpl(_cache, _tenant). GetBase64(GlobalUpload.DocumentType.ADS, item.Image);
                }
            }           

            _resultContent.Add(adsetup.WithName("GetAllAdSetup"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public Result GetAdSetupById(int id)
        {
           var adsetup=new AdSetupRal(_tenant).GetAdSetupById(id);



            adsetup.Base64 = new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.DocumentType.ADS, adsetup.Image);

            _resultContent.Add(adsetup.WithName("GetAdSetupById"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public Result DeleteAdSetup(int id)
        {
            return ResultWrap(new AdSetupRal(_tenant).DeleteAdSetup, id, "AdSetupDelete");
        }


      
       


    }
}
