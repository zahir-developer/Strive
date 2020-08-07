import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WashComponent } from './wash.component';
import { WashRoutingModule } from './wash.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { WashesListComponent } from './washes-list/washes-list.component';


@NgModule({
  declarations: [WashComponent, WashesListComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule, 
    FormsModule,
    WashRoutingModule
  ], 
  exports: [RouterModule]
})
export class WashModule { }
