import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WashComponent } from './wash.component';
import { WashRoutingModule } from './wash.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { WashesListComponent } from './washes-list/washes-list.component';
import { CreateEditWashesComponent } from './create-edit-washes/create-edit-washes.component';


@NgModule({
  declarations: [WashComponent, WashesListComponent, CreateEditWashesComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule, 
    FormsModule,
    WashRoutingModule
  ], 
  exports: [RouterModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class WashModule { }
