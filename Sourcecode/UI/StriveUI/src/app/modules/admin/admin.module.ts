import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin.routing';
import { EmployeesComponent } from './employees/employees.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { TokenInterceptor } from 'src/app/shared/interceptor/token.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { CreateEditComponent } from './employees/create-edit/create-edit.component';
import { DialogModule } from 'primeng/dialog';
import {InputSwitchModule} from 'primeng/inputswitch';

import { SchedulingComponent } from './scheduling/scheduling.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SetupComponent } from './system-setup/setup.component';
import { ConfirmationService } from 'primeng/api';
import { ThemeComponent } from './theme/theme.component';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { CardComponent } from './card/card.component';
import { CashinRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';
import { RouterModule } from '@angular/router';
import { MultiSelectModule } from 'primeng/multiselect';
import { OnlynumberDirective } from 'src/app/shared/Directive/only-number.directive';
import { LocationSetupComponent } from './system-setup/location-setup/location-setup.component';
import { LocationCreateEditComponent } from './system-setup/location-setup/location-create-edit/location-create-edit.component';
import { LocationSetupListComponent } from './system-setup/location-setup/location-setup-list/location-setup-list.component';
import { ServiceCreateEditComponent } from './system-setup/service-setup/service-create-edit/service-create-edit.component';
import { ServiceSetupListComponent } from './system-setup/service-setup/service-setup-list/service-setup-list.component';
import { ServiceSetupComponent } from './system-setup/service-setup/service-setup.component';
import { ProductSetupComponent } from './system-setup/product-setup/product-setup.component';
import { ProductSetupListComponent } from './system-setup/product-setup/product-setup-list/product-setup-list.component';
import { ProductCreateEditComponent } from './system-setup/product-setup/product-create-edit/product-create-edit.component';
import { VendorSetupComponent } from './system-setup/vendor-setup/vendor-setup.component';
import { VendorCreateEditComponent } from './system-setup/vendor-setup/vendor-create-edit/vendor-create-edit.component';
import { VendorSetupListComponent } from './system-setup/vendor-setup/vendor-setup-list/vendor-setup-list.component';
import { EditEmployeeComponent } from './employees/edit-employee/edit-employee.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { EmployeeCollisionComponent } from './employees/employee-collision/employee-collision.component';
import { CollisionListComponent } from './employees/collision-list/collision-list.component';
import { ClientComponent } from './client/client.component';
import { ClientListComponent } from './client/client-list/client-list.component';
import { ClientCreateEditComponent } from './client/client-create-edit/client-create-edit.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleListComponent } from './vehicle/vehicle-list/vehicle-list.component';
import { DocumentListComponent } from './employees/document-list/document-list.component';
import { CreateDocumentComponent } from './employees/create-document/create-document.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewDocumentComponent } from './employees/view-document/view-document.component';
import { GiftCardComponent } from './gift-card/gift-card.component';
import { AddGiftCardComponent } from './gift-card/add-gift-card/add-gift-card.component';
import { AddActivityComponent } from './gift-card/add-activity/add-activity.component';
import { AddScheduleComponent } from './scheduling/add-schedule/add-schedule.component';
import { MembershipComponent } from './system-setup/membership/membership.component';
import { MembershipListComponent } from './system-setup/membership/membership-list/membership-list.component';
import { MembershipCreateEditComponent } from './system-setup/membership/membership-create-edit/membership-create-edit.component';
import { ClientStatementComponent } from './client/client-statement/client-statement.component';
import { ClientHistoryComponent } from './client/client-history/client-history.component';
import { TimeClockMaintenanceComponent } from './time-clock-maintenance/time-clock-maintenance.component';
import { TimeClockWeekComponent } from './time-clock-maintenance/time-clock-week/time-clock-week.component';
import { CheckListComponent } from './system-setup/check-list/check-list.component';
import { EmployeeHandBookComponent } from './system-setup/employee-hand-book/employee-hand-book.component';
import { CreateEditEmployeeHandBookComponent } from './system-setup/employee-hand-book/create-edit-employee-hand-book/create-edit-employee-hand-book.component';
import { TermsAndConditionsComponent } from './system-setup/terms-and-conditions/terms-and-conditions.component';
import { EmailBlastComponent } from './system-setup/email-blast/email-blast.component';
import { BonusSetupComponent } from './system-setup/bonus-setup/bonus-setup.component';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';

import { CreateEditTermsAndConditionsComponent } from './system-setup/terms-and-conditions/create-edit-terms-and-conditions/create-edit-terms-and-conditions.component';
import { AdSetupComponent } from './system-setup/ad-setup/ad-setup.component';
import { AdSetupListComponent } from './system-setup/ad-setup/ad-setup-list/ad-setup-list.component';
import { AdSetupCreateEditComponent } from './system-setup/ad-setup/ad-setup-create-edit/ad-setup-create-edit.component';
import { DropdownModule } from 'primeng/dropdown';
import { DealSetupComponent } from './system-setup/deal-setup/deal-setup.component';
import { DealsAddComponent } from './system-setup/deal-setup/deals-add/deals-add.component';
import { ServiceListComponent } from './client/service-list/service-list.component';
import { EditChecklistComponent } from './system-setup/check-list/edit-checklist/edit-checklist.component';
import { AddChecklistComponent } from './system-setup/check-list/add-checklist/add-checklist.component';
import { TenantSetupComponent } from './tenant-setup/tenant-setup.component';
import { AddTenantComponent } from './tenant-setup/add-tenant/add-tenant.component';
import { EmployeeHourlyRateComponent } from './employees/employee-hourly-rate/employee-hourly-rate.component';
import { AddActivityAdditionalComponent } from './gift-card/add-activity-additional/add-activity-additional.component';

@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent, SchedulingComponent,
    ThemeComponent, CardComponent, CashinRegisterComponent, CloseoutRegisterComponent,
    LocationSetupComponent, LocationCreateEditComponent, LocationSetupListComponent,
    ServiceCreateEditComponent, ServiceSetupListComponent, ServiceSetupComponent, ProductSetupComponent,
    ProductSetupListComponent, ProductCreateEditComponent, VendorSetupComponent, VendorCreateEditComponent,
    VendorSetupListComponent, OnlynumberDirective, SetupComponent, EditEmployeeComponent, EmployeeCollisionComponent,
    CollisionListComponent, ClientComponent, ClientListComponent, ClientCreateEditComponent,
    VehicleComponent, VehicleListComponent, CollisionListComponent, ClientComponent, ClientListComponent, ClientCreateEditComponent,
    VendorSetupListComponent, OnlynumberDirective, SetupComponent, EditEmployeeComponent,
    EmployeeCollisionComponent, CollisionListComponent, DocumentListComponent,
    CreateDocumentComponent, ViewDocumentComponent, GiftCardComponent, AddGiftCardComponent, AddActivityComponent,
    AddScheduleComponent, MembershipComponent, MembershipListComponent, MembershipCreateEditComponent, ClientStatementComponent, ClientHistoryComponent, TimeClockMaintenanceComponent, TimeClockWeekComponent, 
    CheckListComponent, EmployeeHandBookComponent, CreateEditEmployeeHandBookComponent
  , TermsAndConditionsComponent, BonusSetupComponent, CreateEditTermsAndConditionsComponent, AdSetupComponent, AdSetupListComponent, AdSetupCreateEditComponent, DealSetupComponent, DealsAddComponent, ServiceListComponent, EditChecklistComponent, AddChecklistComponent, TenantSetupComponent, AddTenantComponent, EmployeeHourlyRateComponent,EmailBlastComponent,
    AddActivityAdditionalComponent
],
  imports: [
    CommonModule,
    RouterModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    DialogModule,
    DropdownModule,
    InputSwitchModule,
    NgxMaterialTimepickerModule,
    SharedModule,
    NgbPaginationModule,
    MultiSelectModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  exports: [RouterModule],
  providers: [
    ConfirmationService,
    ThemeService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AdminModule { }
