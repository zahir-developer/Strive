import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EmployeesComponent } from './employees/employees.component';
import { SchedulingComponent } from './scheduling/scheduling.component';
import { SetupComponent } from './system-setup/setup.component';
import { BasicSetupComponent } from './basic-setup/basic-setup.component';
import { ServiceSetupComponent } from './service-setup/service-setup.component';
import { ProductSetupComponent } from './product-setup/product-setup.component';
import { VendorSetupComponent } from './vendor-setup/vendor-setup.component';
import { ProductSetupListComponent } from './product-setup/product-setup-list/product-setup-list.component';
import { ThemeComponent } from './theme/theme.component';


const adminRoutes: Routes = [
  { path: 'admin', component: AdminComponent }, {
      path: '', component: AdminComponent, children: [{
          path: 'employees', component: EmployeesComponent},
        {path: 'scheduling', component: SchedulingComponent},
        {path: 'setup', component: SetupComponent},
        {path: 'theme', component: ThemeComponent},
        {path: 'basic', component: BasicSetupComponent},
        {path: 'service', component: ServiceSetupComponent},
        {path: 'product', component: ProductSetupComponent},
        {path: 'vendor', component: VendorSetupComponent}     
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
