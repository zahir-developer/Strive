import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WhiteLabellingComponent } from './white-labelling.component';


const labellingRoutes: Routes = [
  { path: 'white-labelling', component: WhiteLabellingComponent }, {
    path: '', component: WhiteLabellingComponent, children: [{
      path: 'white-labelling', component: WhiteLabellingComponent
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(labellingRoutes)],
  exports: [RouterModule]
})
export class WhiteLabellingRoutingModule { }
