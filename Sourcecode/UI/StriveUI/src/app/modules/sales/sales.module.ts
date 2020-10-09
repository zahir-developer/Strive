import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SalesRoutingModule} from './sales.routing';
import {SalesComponent} from './sales/sales.component';
import { SearchItemComponent } from './sales/search-item/search-item.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { EditItemComponent } from './sales/edit-item/edit-item.component';
import { PrintComponent } from './sales/print/print.component';
import {NgxPrintModule} from 'ngx-print';



@NgModule({
  declarations: [SalesComponent, SearchItemComponent, EditItemComponent, PrintComponent],
  imports: [
    CommonModule,
    RouterModule,
    SalesRoutingModule,
    SharedModule,
    NgxPrintModule
  ],
  exports: [RouterModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class SalesModule { }
