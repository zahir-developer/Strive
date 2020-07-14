import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxUiLoaderModule} from 'ngx-ui-loader';
import {FullCalendarModule} from 'primeng/fullcalendar';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { CardComponent } from './components/card/card.component';
import { MessageServiceToastr } from './services/common-service/message.service';
import { AutoCompleteModule } from 'primeng/autocomplete';
import {AccordionModule} from 'primeng/accordion';
import {ConfirmDialogModule} from 'primeng/confirmdialog';


@NgModule({
  declarations: [CardComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    HttpClientModule,
    NgxUiLoaderModule,
    FullCalendarModule,
    AutoCompleteModule,
    AccordionModule,
    TimepickerModule.forRoot(),
    ConfirmDialogModule
  ],
  exports: [FullCalendarModule, TimepickerModule, CardComponent, AutoCompleteModule, AccordionModule, ConfirmDialogModule],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  providers: [MessageServiceToastr],

})
export class SharedModule { }
