import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
import { MonthlyMoneyOwnedComponent } from './monthly-money-owned/monthly-money-owned.component';
import { RouterModule } from '@angular/router';
import {NgxPrintModule} from 'ngx-print';
import { IrregularityReportComponent } from './irregularity-report/irregularity-report.component';

@NgModule({
  declarations: [ReportsComponent, DailyStatusComponent, EodComponent, DailyTipComponent,
    MonthlyTipComponent, MonthlySalesComponent, MonthlyCustomerSummaryComponent, MonthlyCustomerDetailComponent,
    HourlyWashComponent, DailySalesComponent, MonthlyMoneyOwnedComponent, IrregularityReportComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    ReportsRoutingModule,
    NgxPrintModule
  ],
  exports: [RouterModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class ReportsModule { }
