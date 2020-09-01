import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DetailRoutingModule } from './detail-routing.module';
import { DetailComponent } from './detail.component';
import { DetailScheduleComponent } from './detail-schedule/detail-schedule.component';


@NgModule({
  declarations: [DetailComponent, DetailScheduleComponent],
  imports: [
    CommonModule,
    DetailRoutingModule
  ]
})
export class DetailModule { }
