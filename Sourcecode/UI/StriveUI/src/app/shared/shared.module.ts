import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FullCalendarModule } from 'primeng/fullcalendar';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { CardComponent } from './components/card/card.component';
import { MessageServiceToastr } from './services/common-service/message.service';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AccordionModule } from 'primeng/accordion';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { StateDropdownComponent } from './components/state-dropdown/state-dropdown.component';
import { CountryDropdownComponent } from './components/country-dropdown/country-dropdown.component';
import { RouterModule } from '@angular/router';
import { PhoneMaskDirective } from './Directive/phone-mask.directive';
import { RainProbabilityComponent } from './components/rain-probability/rain-probability.component';
import { TemperatureComponent } from './components/temperature/temperature.component';
import { LastWeekComponent } from './components/last-week/last-week.component';
import { LastThreeMonthComponent } from './components/last-three-month/last-three-month.component';
import { LastMonthComponent } from './components/last-month/last-month.component';
import { TwoDecimalNumberDirective } from './Directive/two-decimal-number.directive';
import { MaxLengthDirective } from './Directive/max-length.directive';
import { CityComponent } from './components/city/city.component';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NoOfWashesComponent } from './components/no-of-washes/no-of-washes.component';
import { NoOfDetailsComponent } from './components/no-of-details/no-of-details.component';
import { WashEmployeesComponent } from './components/wash-employees/wash-employees.component';
import { ScoreComponent } from './components/score/score.component';
import { ForecastedCarsComponent } from './components/forecasted-cars/forecasted-cars.component';
import { AverageWashTimeComponent } from './components/average-wash-time/average-wash-time.component';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { CalendarMaskDirective } from './Directive/calendar-mask.directive';
import { DatePipe } from '@angular/common';
import { PrintWashComponent } from './components/print-wash/print-wash.component';
import { ClientFormComponent } from './components/client-form/client-form.component';
import { VehicleCreateEditComponent } from './components/vehicle-create-edit/vehicle-create-edit.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { TooltipModule } from 'primeng/tooltip';
import { MonthPickerComponent } from './components/month-picker/month-picker.component';
import { YearPickerComponent } from './components/year-picker/year-picker.component';
import { LocationDropdownComponent } from './components/location-dropdown/location-dropdown.component';
import { ExportFiletypeComponent } from './components/export-filetype/export-filetype.component';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    PopoverModule.forRoot(),
    NgbModule,
    HttpClientModule,
    NgxUiLoaderModule,
    FullCalendarModule,
    AutoCompleteModule,
    AccordionModule,
    TimepickerModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TypeaheadModule.forRoot(),
    ConfirmDialogModule,
    NgMultiSelectDropDownModule.forRoot(),
    TooltipModule,
    NgbPaginationModule,
    ChartsModule
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  declarations: [CardComponent, ConfirmationDialogComponent, StateDropdownComponent, CountryDropdownComponent, PhoneMaskDirective,
    RainProbabilityComponent, TemperatureComponent, LastWeekComponent, LastThreeMonthComponent,
    LastMonthComponent, TwoDecimalNumberDirective, MaxLengthDirective, CityComponent, NoOfWashesComponent,
    NoOfDetailsComponent, WashEmployeesComponent, ScoreComponent, ForecastedCarsComponent,
    AverageWashTimeComponent, CalendarMaskDirective, PrintWashComponent, ClientFormComponent, VehicleCreateEditComponent,
    MonthPickerComponent, YearPickerComponent, LocationDropdownComponent, ExportFiletypeComponent],
  exports: [CommonModule, FullCalendarModule, TimepickerModule, CardComponent, AutoCompleteModule,
    AccordionModule, ConfirmationDialogComponent, ConfirmDialogModule,
    StateDropdownComponent, CountryDropdownComponent, RouterModule, FormsModule, HttpClientModule, ReactiveFormsModule, PhoneMaskDirective,
    RainProbabilityComponent, TemperatureComponent, LastWeekComponent, LastThreeMonthComponent,
    LastMonthComponent, TwoDecimalNumberDirective, MaxLengthDirective, CityComponent, TypeaheadModule, BsDatepickerModule,
    NoOfWashesComponent, NoOfDetailsComponent, WashEmployeesComponent, ScoreComponent, ForecastedCarsComponent,
     AverageWashTimeComponent, PopoverModule, CalendarMaskDirective, PrintWashComponent, ClientFormComponent, 
     VehicleCreateEditComponent, TooltipModule, MonthPickerComponent, YearPickerComponent, LocationDropdownComponent,
     NgbPaginationModule, ExportFiletypeComponent, ChartsModule],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  providers: [MessageServiceToastr, DatePipe],

})
export class SharedModule { }
