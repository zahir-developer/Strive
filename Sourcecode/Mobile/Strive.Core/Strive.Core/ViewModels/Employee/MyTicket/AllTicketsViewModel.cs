using Strive.Core.Models.Employee.MyTickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyTicket
{
    public class AllTicketsViewModel : BaseViewModel
    {
        #region Properties

        public AllTickets allTickets;



        #endregion Properties




        #region Commands

        public async Task GetAllTickets()
        {
            var result = await AdminService.GetAllTickets();

            if(result != null)
            {
                allTickets = new AllTickets();
                allTickets.Washes = new List<Washes>();
                allTickets = result;
            }
            else
            {
                allTickets = null;
            }

        }


        #endregion Commands


    }
}
