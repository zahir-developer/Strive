import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WashComponent } from './wash.component';
import { WashRoutingModule } from './wash.routing';


@NgModule({
  declarations: [WashComponent],
  imports: [
    CommonModule,
    WashRoutingModule
  ]
})
export class WashModule { }
