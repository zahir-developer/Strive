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
import { ConfirmationService } from 'primeng/api';
import { ThemeComponent } from './theme/theme.component';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { HeaderComponent } from './header/header.component';
import { CardComponent } from './card/card.component';
import { CashinRegisterComponent } from './cash-register/cash-register.component';
import { CloseoutRegisterComponent } from './closeout-register/closeout-register.component';

@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent, SchedulingComponent,
     ThemeComponent, HeaderComponent, CardComponent,CashinRegisterComponent,CloseoutRegisterComponent],
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
