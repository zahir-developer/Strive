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
import { SchedulingComponent } from './scheduling/scheduling.component';
import { SharedModule } from 'src/app/shared/shared.module'
import { SetupComponent } from './system-setup/setup.component';
import { ConfirmationService } from 'primeng/api';
import { ThemeComponent } from './theme/theme.component';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { HeaderComponent } from './header/header.component';
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
import { VehicleCreateEditComponent } from './vehicle/vehicle-create-edit/vehicle-create-edit.component';
import { DocumentListComponent } from './employees/document-list/document-list.component';
import { CreateDocumentComponent } from './employees/create-document/create-document.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewDocumentComponent } from './employees/view-document/view-document.component';
import { GiftCardComponent } from './gift-card/gift-card.component';
import { AddGiftCardComponent } from './gift-card/add-gift-card/add-gift-card.component';
import { AddActivityComponent } from './gift-card/add-activity/add-activity.component';

@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent, SchedulingComponent,
    ThemeComponent, HeaderComponent, CardComponent, CashinRegisterComponent, CloseoutRegisterComponent,
    LocationSetupComponent, LocationCreateEditComponent, LocationSetupListComponent,
    ServiceCreateEditComponent, ServiceSetupListComponent, ServiceSetupComponent, ProductSetupComponent,
    ProductSetupListComponent, ProductCreateEditComponent, VendorSetupComponent, VendorCreateEditComponent,
    VendorSetupListComponent, OnlynumberDirective, SetupComponent, EditEmployeeComponent, EmployeeCollisionComponent,
    CollisionListComponent, ClientComponent, ClientListComponent, ClientCreateEditComponent,
    VehicleComponent, VehicleListComponent, VehicleCreateEditComponent,
    CollisionListComponent, ClientComponent, ClientListComponent, ClientCreateEditComponent,
    VendorSetupListComponent, OnlynumberDirective, SetupComponent, EditEmployeeComponent,
    EmployeeCollisionComponent, CollisionListComponent, DocumentListComponent, CreateDocumentComponent, ViewDocumentComponent, GiftCardComponent, AddGiftCardComponent, AddActivityComponent],
  imports: [
    CommonModule,
    RouterModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    DialogModule,
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
