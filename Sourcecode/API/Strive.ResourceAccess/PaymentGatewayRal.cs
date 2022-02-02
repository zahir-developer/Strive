using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.DTO.PaymentGateway;
using Strive.BusinessEntities.Location;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using Strive.RepositoryCqrs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Strive.ResourceAccess
{
    public class PaymentGatewayRal : RalBase
    {
        private readonly Db _db;

        public PaymentGatewayRal(ITenantHelper tenant) : base(tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }

        public bool DeletePaymentGateway(int id)
        {
            _prm.Add("PaymentGatewayId", id.toInt());
            _prm.Add("UserId", _tenant.EmployeeId);
            _prm.Add("Date", DateTime.UtcNow);
            db.Save(EnumSP.Payroll.USPDELETEPAYMENTGATEWAY.ToString(), _prm);
            return true;
        }

        public List<RecurringPaymentDetails> GetRecurringPaymentDetails(int locationId, int FailedAttempts)
        {
            _prm.Add("locationId", locationId.toInt());
            _prm.Add("FailedAttempts", FailedAttempts.toInt());
            return db.Fetch<RecurringPaymentDetails>(EnumSP.Payroll.UPSGETALLRECURRINGPAYMENTDETAILS.ToString(), _prm);
        }
        public List<MerchantDetails> GetMerchantDetails(int locationId)
        {
            if (locationId > 0)
            {
                _prm.Add("locationId", locationId.toInt());
            }
            return db.Fetch<MerchantDetails>(EnumSP.Payroll.UPSGETMERCHANTDETAILS.ToString(), _prm);
        }
    }
}
