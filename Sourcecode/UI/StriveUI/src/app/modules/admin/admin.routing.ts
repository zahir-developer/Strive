import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EmployeesComponent } from './employees/employees.component';
import { SchedulingComponent } from './scheduling/scheduling.component';
import { CashRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';
import { ThemeComponent } from './theme/theme.component';


const adminRoutes: Routes = [
  { path: 'admin', component: AdminComponent }, {
      path: '', component: AdminComponent, children: [{
          path: 'employees', component: EmployeesComponent},
        {path: 'scheduling', component: SchedulingComponent},
        {path: 'theme', component: ThemeComponent},
        {path: 'cashregister', component: CashRegisterComponent},
        {path: 'closeoutregister', component: CloseoutRegisterComponent},
        {path: 'setup', loadChildren: () => import('./system-setup/setup.module').then(mod => mod.SetupModule)},     
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
