import { SetupComponent } from './setup.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocationSetupComponent } from '../location-setup/location-setup.component';
import { ProductSetupComponent } from '../product-setup/product-setup.component';
import { VendorSetupComponent } from '../vendor-setup/vendor-setup.component';
import { ServiceSetupComponent } from '../service-setup/service-setup.component';
import { AuthGuard } from 'src/app/shared/guards/auth-guard.service';

const setupRoutes: Routes = [
    { path: 'setup', canActivate: [AuthGuard], component: SetupComponent }, {
        path: '', component: SetupComponent, children: [
          { path: '', redirectTo: 'location' },
          {path: 'location', component: LocationSetupComponent},
          {path: 'service', component: ServiceSetupComponent},
          {path: 'product', component: ProductSetupComponent},
          {path: 'vendor', component: VendorSetupComponent}
          ]
    }
  ];
  
  
  @NgModule({
    imports: [
      RouterModule.forChild(setupRoutes)
    ],
    exports: [
      RouterModule,
    ],
  })
  
  export class SetupRoutingModule { }
  