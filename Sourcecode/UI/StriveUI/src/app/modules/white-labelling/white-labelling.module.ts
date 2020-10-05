import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WhiteLabellingRoutingModule } from './white-labelling-routing.module';
import { WhiteLabellingComponent } from './white-labelling.component';
import { WhiteLabellingSectionComponent } from './white-labelling-section/white-labelling-section.component';


@NgModule({
  declarations: [WhiteLabellingComponent, WhiteLabellingSectionComponent],
  imports: [
    CommonModule,
    WhiteLabellingRoutingModule
  ]
})
export class WhiteLabellingModule { }
