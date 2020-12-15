import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { SelectServicesComponent } from './select-services/select-services.component';
import { SelectLocationComponent } from './select-location/select-location.component';
import { SelectAppointmentDateComponent } from './select-appointment-date/select-appointment-date.component';
import { PreviewAppointmentDetailComponent } from './preview-appointment-detail/preview-appointment-detail.component';
import { AppointmentConfigurationComponent } from './appointment-configuration/appointment-configuration.component';


@NgModule({
  declarations: [CustomerComponent, CustomerDashboardComponent, SelectServicesComponent, SelectLocationComponent, SelectAppointmentDateComponent, PreviewAppointmentDetailComponent, AppointmentConfigurationComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule
  ]
})
export class CustomerModule { }
