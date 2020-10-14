import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PayrollsComponent } from './payrolls/payrolls.component';


const payrollsRoutes: Routes = [
  { path: 'payrolls', component: PayrollsComponent }, {
    path: '', component: PayrollsComponent, children: [{
      path: 'payrolls', component: PayrollsComponent
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(payrollsRoutes)],
  exports: [RouterModule]
})
export class PayrollsRoutingModule { }
