import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportsRoutingModule } from './reports-routing.module';
import { ReportsComponent } from './reports.component';
import { DailyStatusComponent } from './daily-status/daily-status.component';
import { EodComponent } from './eod/eod.component';
import { DailyTipComponent } from './daily-tip/daily-tip.component';
import { MonthlyTipComponent } from './monthly-tip/monthly-tip.component';
import { MonthlySalesComponent } from './monthly-sales/monthly-sales.component';
import { MonthlyCustomerSummaryComponent } from './monthly-customer-summary/monthly-customer-summary.component';
import { MonthlyCustomerDetailComponent } from './monthly-customer-detail/monthly-customer-detail.component';
import { HourlyWashComponent } from './hourly-wash/hourly-wash.component';
import { DailySalesComponent } from './daily-sales/daily-sales.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [ReportsComponent, DailyStatusComponent, EodComponent, DailyTipComponent,
    MonthlyTipComponent, MonthlySalesComponent, MonthlyCustomerSummaryComponent, MonthlyCustomerDetailComponent,
    HourlyWashComponent, DailySalesComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReportsRoutingModule
  ]
})
export class ReportsModule { }
