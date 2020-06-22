import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EmployeesComponent } from './employees/employees.component';
import { SchedulingComponent } from './scheduling/scheduling.component';


const adminRoutes: Routes = [
  { path: 'admin', component: AdminComponent }, {
      path: '', component: AdminComponent, children: [{
          path: 'employees', component: EmployeesComponent},
        {path: 'scheduling', component: SchedulingComponent}]
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
