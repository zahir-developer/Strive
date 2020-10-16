import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from 'src/app/shared/shared.module';
import { WhiteLabellingRoutingModule } from './white-labelling-routing.module';
import { WhiteLabellingComponent } from './white-labelling.component';
import { WhiteLabellingSectionComponent } from './white-labelling-section/white-labelling-section.component';
import { CustomThemeComponent } from './custom-theme/custom-theme.component';
import { ColorPickerModule } from 'ngx-color-picker';
import { DialogModule } from 'primeng';


@NgModule({
  declarations: [WhiteLabellingComponent, WhiteLabellingSectionComponent, CustomThemeComponent],
  imports: [
    CommonModule,
    WhiteLabellingRoutingModule,
    SharedModule,
    ColorPickerModule,
    DialogModule
  ]
})
export class WhiteLabellingModule { }
