import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerHistoryComponent } from './customer-history.component';


const CustomerHistoryModules: Routes = [{
  path: 'customer-history', component: CustomerHistoryComponent,
   data: { label: 'Customer-History', title: 'Customer-History' }
},
{
  path: '', component: CustomerHistoryComponent,
  children: [{
      path: 'customer-history', component: CustomerHistoryComponent,
       data: { label: 'Customer-History', title: 'Customer-History' }
  }]
}];

@NgModule({
  imports: [RouterModule.forChild(CustomerHistoryModules)],
  exports: [RouterModule]
})
export class CustomerHistoryRoutingModule { }
