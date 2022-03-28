import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
import { MonthlyMoneyOwnedComponent } from './monthly-money-owned/monthly-money-owned.component';
import { IrregularityReportComponent } from './irregularity-report/irregularity-report.component';


const routes: Routes = [
  { path: 'reports', component: ReportsComponent},
{path: '', component: ReportsComponent, children: [
{path: 'daily-staus', component: DailyStatusComponent},
{path: 'eod', component: EodComponent},
{path: 'daily-tip', component: DailyTipComponent},
{path: 'monthly-tip', component: MonthlyTipComponent},
{path: 'monthly-sales', component: MonthlySalesComponent},
{path: 'monthly-customer-summary', component: MonthlyCustomerSummaryComponent},
{path: 'monthly-customer-detail', component: MonthlyCustomerDetailComponent},
{path: 'hourly-wash', component: HourlyWashComponent},
{path: 'daily-sales', component: DailySalesComponent},
{path: 'monthly-money-owned', component: MonthlyMoneyOwnedComponent},
{path: 'irregularity-report', component: IrregularityReportComponent}
]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
