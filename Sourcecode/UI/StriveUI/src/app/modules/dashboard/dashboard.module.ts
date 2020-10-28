import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing';
import { TodayScheduleDashboardComponent } from './today-schedule-dashboard/today-schedule-dashboard.component';
import { GraphDashboardComponent } from './graph-dashboard/graph-dashboard.component';
import { ChartsModule } from 'ng2-charts';



@NgModule({
  declarations: [DashboardComponent, TodayScheduleDashboardComponent, GraphDashboardComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    ChartsModule
  ]
})
export class DashboardModule { }
