import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxUiLoaderModule} from 'ngx-ui-loader';
import {FullCalendarModule} from 'primeng/fullcalendar';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { CardComponent } from './components/card/card.component';
import { MessageServiceToastr } from './services/common-service/message.service';
import { AutoCompleteModule } from 'primeng/autocomplete';
import {AccordionModule} from 'primeng/accordion';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { StateDropdownComponent } from './components/state-dropdown/state-dropdown.component';
import { CountryDropdownComponent } from './components/country-dropdown/country-dropdown.component';
import { RouterModule } from '@angular/router';



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    NgbModule,
    HttpClientModule,
    NgxUiLoaderModule,
    FullCalendarModule,
    AutoCompleteModule,
    AccordionModule,
    TimepickerModule.forRoot()
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  declarations: [CardComponent, ConfirmationDialogComponent, StateDropdownComponent, CountryDropdownComponent],
  exports: [CommonModule, FullCalendarModule, TimepickerModule, CardComponent, AutoCompleteModule, 
    AccordionModule, ConfirmationDialogComponent, 
    StateDropdownComponent, CountryDropdownComponent, RouterModule, FormsModule, HttpClientModule, ReactiveFormsModule ],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  providers: [MessageServiceToastr],

})
export class SharedModule { }
