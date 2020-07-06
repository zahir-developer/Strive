import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin.routing';
import { EmployeesComponent } from './employees/employees.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { TokenInterceptor } from 'src/app/shared/interceptor/token.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TableModule} from 'primeng/table';
import { CreateEditComponent } from './employees/create-edit/create-edit.component';
import {DialogModule} from 'primeng/dialog';
import { SchedulingComponent } from './scheduling/scheduling.component';
import {SharedModule} from 'src/app/shared/shared.module'
import { SetupComponent } from './system-setup/setup.component';
import { LocationSetupComponent } from './location-setup/location-setup.component';
import { LocationSetupListComponent } from './location-setup/location-setup-list/location-setup-list.component';
import { LocationCreateEditComponent } from './location-setup/location-create-edit/location-create-edit.component';
import { ServiceSetupComponent } from './service-setup/service-setup.component';
import { ServiceSetupListComponent } from './service-setup/service-setup-list/service-setup-list.component';
import { ServiceCreateEditComponent } from './service-setup/service-create-edit/service-create-edit.component';
import { ProductSetupComponent } from './product-setup/product-setup.component';
import { ProductSetupListComponent } from './product-setup/product-setup-list/product-setup-list.component';
import { ProductCreateEditComponent } from './product-setup/product-create-edit/product-create-edit.component';
import { VendorSetupComponent } from './vendor-setup/vendor-setup.component';
import { VendorSetupListComponent } from './vendor-setup/vendor-setup-list/vendor-setup-list.component';
import { VendorCreateEditComponent } from './vendor-setup/vendor-create-edit/vendor-create-edit.component';
import { ConfirmationService } from 'primeng/api';
import { OnlynumberDirective } from 'src/app/shared/Directive/only-number.directive';
import { CashRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';

@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent, SchedulingComponent,
  SetupComponent,LocationSetupComponent,LocationSetupListComponent,LocationCreateEditComponent,ServiceSetupComponent,ServiceSetupListComponent,
  ServiceCreateEditComponent,ProductSetupComponent,ProductSetupListComponent,ProductCreateEditComponent,VendorSetupComponent,
  VendorSetupListComponent,VendorCreateEditComponent,OnlynumberDirective,CashRegisterComponent,CloseoutRegisterComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    DialogModule,
    SharedModule
  ],
  providers: [
    ConfirmationService,
    {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AdminModule { }
