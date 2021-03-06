import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { MessengerEmployeeListComponent } from './messenger-employee-list/messenger-employee-list.component';
import { MessengerEmployeeSearchComponent } from './messenger-employee-search/messenger-employee-search.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [MessengerComponent, MessengerEmployeeListComponent, MessengerEmployeeSearchComponent],
  imports: [
    CommonModule,
    MessengerRoutingModule,
    SharedModule
  ]
})
export class MessengerModule { }
