using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using Strive.BusinessEntities.Vendor;

namespace Strive.BusinessLogic
{
    public class VendorBpl : Strivebase, IVendorBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public VendorBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        public Result GetVendorDetails()
        {
            try
            {
                var lstVendor = new VendorRal(_tenant).GetVendorDetails();
                _resultContent.Add(lstVendor.WithName("Vendor"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveVendorDetails(VendorView lstVendor)
        {
            try
            {
                bool blnStatus = new VendorRal(_tenant).SaveVendorDetails(lstVendor);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteVendorById(int id)
        {
            try
            {
                var lstVendor = new VendorRal(_tenant).DeleteVendorById(id);
                _resultContent.Add(lstVendor.WithName("Vendor"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetVendorById(long id)
        {
            try
            {
                var lstVendorById = new VendorRal(_tenant).GetVendorById(id);
                _resultContent.Add(lstVendorById.WithName("VendorDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        //    public Result GetVendorDetails()
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
