import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EmployeesComponent } from './employees/employees.component';
import { SchedulingComponent } from './scheduling/scheduling.component';
import { CashinRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';
import { ThemeComponent } from './theme/theme.component';
import { AuthGuard } from 'src/app/shared/guards/auth-guard.service';


const adminRoutes: Routes = [
  { path: 'admin', canActivate: [AuthGuard], component: AdminComponent }, {
      path: '', component: AdminComponent, children: [{
          path: 'employees', component: EmployeesComponent},
        {path: 'scheduling', component: SchedulingComponent},
        {path: 'theme', component: ThemeComponent},
        {path: 'cashregister', component: CashinRegisterComponent},
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
