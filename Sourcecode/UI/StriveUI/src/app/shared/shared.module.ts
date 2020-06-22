import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import {FullCalendarModule} from 'primeng/fullcalendar';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    HttpClientModule,
    NgxUiLoaderModule,
    FullCalendarModule,
    TimepickerModule.forRoot()
  ],
  exports: [FullCalendarModule, TimepickerModule],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  providers: [],

})
export class SharedModule { }
