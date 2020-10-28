import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WhiteLabellingComponent } from './white-labelling.component';
import { AlertGuard } from 'src/app/shared/guards/alert-guard.service';


const labellingRoutes: Routes = [
  { path: 'white-labelling', component: WhiteLabellingComponent, canDeactivate: [AlertGuard] },
  { path: '', component: WhiteLabellingComponent, canDeactivate: [AlertGuard]  }
];

@NgModule({
  imports: [RouterModule.forChild(labellingRoutes)],
  exports: [RouterModule]
})
export class WhiteLabellingRoutingModule { }
