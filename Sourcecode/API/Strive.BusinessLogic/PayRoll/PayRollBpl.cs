using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.PayRoll;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.PayRoll
{
    public class PayRollBpl : Strivebase, IPayRollBpl
    {
        public PayRollBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetPayRoll(PayRollDto payRoll)
        {
            return ResultWrap(new PayRollRal(_tenant).GetPayRoll, payRoll, "Result");
        }
        public Result AddPayRoll(PayRollAddDto payRollAdd)
        {
            try
            {
                return ResultWrap(new PayRollRal(_tenant).AddPayRoll, payRollAdd, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdatePayRoll(PayRollUpdateDto payRollUpdate)
        {
            try
            {
                return ResultWrap(new PayRollRal(_tenant).UpdatePayRoll, payRollUpdate, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdateEmployeeAdjustment(List<EmployeeAdjustmentDto> employeeAdjustment)
        {
            try
            {
                var result = new PayRollRal(_tenant).UpdateEmployeeAdjustment(employeeAdjustment);

                JObject _resultContent = new JObject();
                Result _result = new Result();
                _resultContent.Add(result.WithName("Result"));
                _result = Helper.BindSuccessResult(_resultContent);

                return _result;
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


    }
}
