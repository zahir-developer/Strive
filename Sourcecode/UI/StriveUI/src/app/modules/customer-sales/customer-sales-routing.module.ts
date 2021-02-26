import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerSalesComponent } from './customer-sales.component';

const CustomerSalesModules: Routes = [{
  path: 'Customer-sales', component: CustomerSalesComponent,
   data: { label: 'Customer-sales', title: 'Customer-sales', authorization: 'Customer-sales' }

},
{
  path: '', component: CustomerSalesComponent,
  children: [{
      path: 'Customer-sales', component: CustomerSalesComponent, data: { label: 'Customer-sales', title: 'Customer-sales', authorization: 'Customer-sales' }
  }]
}
];

@NgModule({
  imports: [RouterModule.forChild(CustomerSalesModules)],
  exports: [RouterModule]
})
export class CustomerSalesRoutingModule { }
