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
import { BasicSetupComponent } from './basic-setup/basic-setup.component';
import { BasicSetupListComponent } from './basic-setup/basic-setup-list/basic-setup-list.component';
import { BasicCreateEditComponent } from './basic-setup/basic-create-edit/basic-create-edit.component';
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
import { ThemeComponent } from './theme/theme.component';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { HeaderComponent } from './header/header.component';
import { CardComponent } from './card/card.component';

@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent, SchedulingComponent,
  SetupComponent,BasicSetupComponent,BasicSetupListComponent,BasicCreateEditComponent,ServiceSetupComponent,ServiceSetupListComponent,
  ServiceCreateEditComponent,ProductSetupComponent,ProductSetupListComponent,ProductCreateEditComponent,VendorSetupComponent,
  VendorSetupListComponent,VendorCreateEditComponent, ThemeComponent, HeaderComponent, CardComponent],
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
    ThemeService,
    {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AdminModule { }
