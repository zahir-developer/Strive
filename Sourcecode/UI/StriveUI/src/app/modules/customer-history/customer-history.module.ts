import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerHistoryRoutingModule } from './customer-history-routing.module';
import { CustomerHistoryComponent } from './customer-history.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [CustomerHistoryComponent],
  imports: [
    CommonModule,
    CustomerHistoryRoutingModule,
    SharedModule,
    NgbPaginationModule
  ]
})
export class CustomerHistoryModule { }
