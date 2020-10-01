import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PayrollsRoutingModule } from './payrolls-routing.module';
import { PayrollsComponent } from './payrolls/payrolls.component';
import { PayrollsGridComponent } from './payrolls-grid/payrolls-grid.component';


@NgModule({
  declarations: [PayrollsComponent, PayrollsGridComponent],
  imports: [
    CommonModule,
    PayrollsRoutingModule
  ]
})
export class PayrollsModule { }
