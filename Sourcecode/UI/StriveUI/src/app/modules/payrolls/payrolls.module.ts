import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';

import { PayrollsRoutingModule } from './payrolls-routing.module';
import { PayrollsComponent } from './payrolls/payrolls.component';
import { PayrollsGridComponent } from './payrolls-grid/payrolls-grid.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [PayrollsComponent, PayrollsGridComponent],
  imports: [
    CommonModule,
    PayrollsRoutingModule,
    SharedModule,
    NgbPaginationModule
  ]
})
export class PayrollsModule { }
