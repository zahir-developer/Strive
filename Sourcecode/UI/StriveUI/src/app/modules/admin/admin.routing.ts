import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EmployeesComponent } from './employees/employees.component';
import { SchedulingComponent } from './scheduling/scheduling.component';
import { CashinRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';
import { ThemeComponent } from './theme/theme.component';
import { AuthGuard } from 'src/app/shared/guards/auth-guard.service';
import { SetupComponent } from './system-setup/setup.component';
import { LocationSetupComponent } from './system-setup/location-setup/location-setup.component';
import { ServiceSetupComponent } from './system-setup/service-setup/service-setup.component';
import { ProductSetupComponent } from './system-setup/product-setup/product-setup.component';
import { VendorSetupComponent } from './system-setup/vendor-setup/vendor-setup.component';
import { ClientComponent } from './client/client.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { GiftCardComponent } from './gift-card/gift-card.component';
import { MembershipComponent } from './system-setup/membership/membership.component';
import { TimeClockMaintenanceComponent } from './time-clock-maintenance/time-clock-maintenance.component';
import { TimeClockWeekComponent } from './time-clock-maintenance/time-clock-week/time-clock-week.component';
import { CheckListComponent } from './system-setup/check-list/check-list.component';
import { EmployeeHandBookComponent } from './system-setup/employee-hand-book/employee-hand-book.component';


const adminRoutes: Routes = [
  { path: 'admin', canActivate: [AuthGuard], component: AdminComponent }, {
    path: '', component: AdminComponent, children: [{
      path: 'employees', component: EmployeesComponent
    },
    { path: 'scheduling', component: SchedulingComponent },
    { path: 'theme', component: ThemeComponent },
    { path: 'cashregister', component: CashinRegisterComponent },
    { path: 'closeoutregister', component: CloseoutRegisterComponent },
    { path: 'client', component: ClientComponent },
    { path: 'vehicle', component: VehicleComponent },
    { path: 'gift-card', component: GiftCardComponent },
    { path: 'time-clock', component: TimeClockMaintenanceComponent },
    { path: 'time-clock-week', component: TimeClockWeekComponent },
    
    {
      path: 'setup', component: SetupComponent, children: [
        { path: '', redirectTo: 'location' },
        { path: 'location', component: LocationSetupComponent },
        { path: 'service', component: ServiceSetupComponent },
        { path: 'product', component: ProductSetupComponent },
        { path: 'vendor', component: VendorSetupComponent },
        { path: 'membership', component: MembershipComponent },
        { path: 'checkList', component: CheckListComponent },
        { path: 'empHandBook', component: EmployeeHandBookComponent },

      ]
    }
    ]
  }
];


@NgModule({
  imports: [
    RouterModule.forChild(adminRoutes)
  ],
  exports: [
    RouterModule,
  ],
})

export class AdminRoutingModule { }
