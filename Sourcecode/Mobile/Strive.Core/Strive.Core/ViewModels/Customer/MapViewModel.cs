﻿using MvvmCross;
using Strive.Core.Models.TimInventory;
using Strive.Core.Services.Implementations;
using Strive.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class MapViewModel : BaseViewModel
    {
        public ILocationService LocationService = Mvx.IoCProvider.Resolve<ILocationService>();
        public Location Locations;
    
        public MapViewModel()
        {

        }
        
        public async Task<Location> GetAllLocationsCommand()
        {
            Locations = await LocationService.GetAllLocationAddress();
            return Locations;
        }
    }
}
