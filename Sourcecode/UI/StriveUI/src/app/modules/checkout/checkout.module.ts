import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutGridComponent } from './checkout-grid/checkout-grid.component';
import { CheckoutComponent } from './checkout.component';


@NgModule({
  declarations: [CheckoutGridComponent, CheckoutComponent],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule
  ]
})
export class CheckoutModule { }
