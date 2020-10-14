import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CheckoutComponent } from './checkout.component';


const checkoutRoutes: Routes = [
  { path: 'checkout', component: CheckoutComponent }, {
    path: '', component: CheckoutComponent, children: [{
      path: 'checkout', component: CheckoutComponent
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(checkoutRoutes)],
  exports: [RouterModule]
})
export class CheckoutRoutingModule { }
