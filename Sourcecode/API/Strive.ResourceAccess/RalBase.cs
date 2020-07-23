using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class RalBase
    {
        public IDbConnection _dbconnection;
        public ITenantHelper _tenant;
        public Db db;
        public RalBase(ITenantHelper tenant)
        {
            _tenant = tenant;
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public RalBase(ITenantHelper tenant, bool isAuth)
        {
            _tenant = tenant;
            if (isAuth)
                _dbconnection = tenant.dbAuth();
            else
                _dbconnection = tenant.db();

            db = new Db(_dbconnection);
        }
    }

}
