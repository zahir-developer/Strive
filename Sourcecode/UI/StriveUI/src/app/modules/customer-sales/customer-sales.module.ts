import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerSalesComponent } from './customer-sales.component';
import { CustomerSalesRoutingModule } from './customer-sales-routing.module';



@NgModule({
  declarations: [  
    CustomerSalesComponent
   ],
  imports: [
    CommonModule,
    SharedModule,
    CustomerSalesRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class CustomerSalesModule { }
