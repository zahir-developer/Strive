import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailComponent } from './detail.component';

const detailRoutes: Routes = [
  { path: 'detail', component: DetailComponent }, {
    path: '', component: DetailComponent, children: [{
      path: 'detail', component: DetailComponent
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(detailRoutes)],
  exports: [RouterModule]
})
export class DetailRoutingModule { }
