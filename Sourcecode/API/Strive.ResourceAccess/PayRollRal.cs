using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.PayRoll;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class PayRollRal : RalBase
    {
        public PayRollRal(ITenantHelper tenant) : base(tenant) { }

        public PayRollViewModel GetPayRoll(PayRollDto payRoll)
        {
            _prm.Add("@LocationId", payRoll.LocationId);
            _prm.Add("@StartDate", payRoll.StartDate);
            _prm.Add("@EndDate", payRoll.EndDate);
            var res = db.FetchMultiResult<PayRollViewModel>(EnumSP.Payroll.USPGETPAYROLLLIST.ToString(), _prm);
            return res;
        }
        public bool AddPayRoll(PayRollAddDto payRollAdd)
        {
            return dbRepo.InsertPc(payRollAdd, "PayrollId");
        }
        public List<PayRollAdjusmentViewModel> UpdatePayRoll(PayRollUpdateDto payRollUpdate)
        {
            _prm.Add("@LiabilityId", payRollUpdate.LiabilityId);
            _prm.Add("@Amount", payRollUpdate.Amount);
            return db.Fetch<PayRollAdjusmentViewModel>(EnumSP.Payroll.USPUPDATEADJUSMENT.ToString(), _prm);            
        }
        public bool UpdateEmployeeAdjustment(List<EmployeeAdjustmentDto> employeeAdjustment)
        {
            foreach(var item in employeeAdjustment)
            {
                _prm.Add("@EmployeeId", item.id);
                _prm.Add("@Adjustment", item.adjustment);
                _prm.Add("@LocationId", item.LocationId);
                
                db.Save(EnumSP.Payroll.USPUPDATEEMPLOYEEADJUSTMENT.ToString(), _prm);
            }
            return true;
        }

        public bool AddPayRollProcess(PayrollProcessAddDto payrollProcessAdd)
        {
            return dbRepo.InsertPc(payrollProcessAdd, "PayrollProcessId");
        }

        public bool GetPayrollProcessStatus(PayRollProcessDto payRollProcess)
        {
            _prm.Add("@FromDate", payRollProcess.StartDate);
            _prm.Add("@ToDate", payRollProcess.EndDate);
            _prm.Add("@employeeId", payRollProcess.EmpId);
            _prm.Add("@locationId", payRollProcess.LocationId);
            var res = db.Fetch<PayrollProcessViewModel>(EnumSP.Payroll.USPGETPAYROLLPROCESS.ToString(), _prm);
           if(res.Count>0)
            {
                return true;
            }
           else return false;
                   
        }
    }
}
