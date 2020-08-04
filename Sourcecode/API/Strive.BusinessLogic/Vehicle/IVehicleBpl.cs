using Strive.BusinessEntities.Client;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strive.BusinessLogic.Vehicle
{
    public interface IVehicleBpl
    {
        Result GetAllVehicle();
        Result SaveClientVehicle(List<ClientVehicle> vehicle);

        Result DeleteVehicle(int id);
        Result GetClientVehicleById(int id);
        
    }
}
