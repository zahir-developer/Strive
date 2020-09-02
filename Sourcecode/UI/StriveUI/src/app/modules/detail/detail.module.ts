import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DetailRoutingModule } from './detail-routing.module';
import { DetailComponent } from './detail.component';
import { DetailScheduleComponent } from './detail-schedule/detail-schedule.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateEditDetailScheduleComponent } from './create-edit-detail-schedule/create-edit-detail-schedule.component';
import { DialogModule } from 'primeng/dialog';


@NgModule({
  declarations: [DetailComponent, DetailScheduleComponent, CreateEditDetailScheduleComponent],
  imports: [
    CommonModule,
    DetailRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    DialogModule
  ]
})
export class DetailModule { }
