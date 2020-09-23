import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DetailRoutingModule } from './detail-routing.module';
import { DetailComponent } from './detail.component';
import { DetailScheduleComponent } from './detail-schedule/detail-schedule.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateEditDetailScheduleComponent } from './create-edit-detail-schedule/create-edit-detail-schedule.component';
import { DialogModule } from 'primeng/dialog';
import { TodayScheduleComponent } from './today-schedule/today-schedule.component';
import { AssignDetailComponent } from './assign-detail/assign-detail.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ViewPastNotesComponent } from './view-past-notes/view-past-notes.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    DetailComponent,
    DetailScheduleComponent,
    CreateEditDetailScheduleComponent,
    TodayScheduleComponent,
    AssignDetailComponent,
    ViewPastNotesComponent],
  imports: [
    CommonModule,
    DetailRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    DialogModule,
    NgMultiSelectDropDownModule.forRoot(),
    NgbPaginationModule
  ]
})
export class DetailModule { }
