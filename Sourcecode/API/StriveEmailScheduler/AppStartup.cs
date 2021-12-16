using Strive.BusinessLogic;
using Strive.BusinessLogic.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace StriveEmailScheduler
{

    class AppStartup
    {
        private readonly IClientBpl _clientBpl;
        private readonly IEmployeeBpl _employeeBpl;
        public AppStartup(IClientBpl clientBpl, IEmployeeBpl employeeBpl)
        {
            _clientBpl = clientBpl;
            _employeeBpl = employeeBpl;

        }

        public void Execute(string[] args)
        {
            try
            {
                //if (args != null && args.Length > 0)
                //{
                //    if (!string.IsNullOrWhiteSpace(args[0]))
                //    {
                       _clientBpl.SendClientEmail();
                            _employeeBpl.SendEmployeeEmail();
                        
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
