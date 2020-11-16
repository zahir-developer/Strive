using Strive.Core.Models.Employee.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile
{
    public class EditPersonalDetailsViewModel : BaseViewModel
    {

        #region Properties

        public Gender gender { get; set; }

        #endregion Properties



        #region Commands

        public async Task GetGender()
        {
            var result = await AdminService.GetGender();

            if(result == null)
            {
                gender = null;
            }
            else
            {
                gender = new Gender();
                gender.Codes = new List<Codes>();
                gender = result;
            }
        }

        #endregion Commands

    }
}
