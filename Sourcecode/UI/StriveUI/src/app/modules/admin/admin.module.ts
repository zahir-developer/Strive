import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin.routing';
import { EmployeesComponent } from './employees/employees.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { TokenInterceptor } from 'src/app/shared/interceptor/token.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TableModule} from 'primeng/table';
import { CreateEditComponent } from './employees/create-edit/create-edit.component';
import {DialogModule} from 'primeng/dialog';


@NgModule({
  declarations: [AdminComponent, EmployeesComponent, EmployeeListComponent, CreateEditComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    DialogModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AdminModule { }
