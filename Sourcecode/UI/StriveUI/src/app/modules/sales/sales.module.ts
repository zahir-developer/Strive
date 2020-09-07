import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SalesRoutingModule} from './sales.routing';
import {SalesComponent} from './sales/sales.component';
import { SearchItemComponent } from './sales/search-item/search-item.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [SalesComponent, SearchItemComponent],
  imports: [
    CommonModule,
    SalesRoutingModule,
    SharedModule
  ],
  exports: [RouterModule]
})
export class SalesModule { }
