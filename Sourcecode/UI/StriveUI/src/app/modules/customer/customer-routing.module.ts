import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer.component';

const CustomerModules: Routes = [{
  path: 'Customer', component: CustomerComponent,
   data: { label: 'Customer', title: 'Customer', authorization: 'Customer' }
},
{
  path: '', component: CustomerComponent,
  children: [{
      path: 'customer', component: CustomerComponent, data: { label: 'Customer', title: 'Customer', authorization: 'Customer' }
  }]
}];

@NgModule({
  imports: [RouterModule.forChild(CustomerModules)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
