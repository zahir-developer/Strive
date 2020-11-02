import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing';
import { TodayScheduleDashboardComponent } from './today-schedule-dashboard/today-schedule-dashboard.component';
import { GraphDashboardComponent } from './graph-dashboard/graph-dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { FilterDashboardComponent } from './filter-dashboard/filter-dashboard.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DialogModule } from 'primeng';



@NgModule({
  declarations: [DashboardComponent, TodayScheduleDashboardComponent, GraphDashboardComponent, FilterDashboardComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    ChartsModule,
    SharedModule,
    DialogModule
  ]
})
export class DashboardModule { }
