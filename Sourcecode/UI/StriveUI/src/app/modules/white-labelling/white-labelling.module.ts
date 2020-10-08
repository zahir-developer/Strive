import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from 'src/app/shared/shared.module';
import { WhiteLabellingRoutingModule } from './white-labelling-routing.module';
import { WhiteLabellingComponent } from './white-labelling.component';
import { WhiteLabellingSectionComponent } from './white-labelling-section/white-labelling-section.component';


@NgModule({
  declarations: [WhiteLabellingComponent, WhiteLabellingSectionComponent],
  imports: [
    CommonModule,
    WhiteLabellingRoutingModule,
    SharedModule
  ]
})
export class WhiteLabellingModule { }
