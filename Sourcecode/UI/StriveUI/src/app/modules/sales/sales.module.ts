import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SalesRoutingModule} from './sales.routing';
import {SalesComponent} from './sales/sales.component';
import { SearchItemComponent } from './sales/search-item/search-item.component';




@NgModule({
  declarations: [SalesComponent, SearchItemComponent],
  imports: [
    CommonModule,
    SalesRoutingModule
  ]
})
export class SalesModule { }
