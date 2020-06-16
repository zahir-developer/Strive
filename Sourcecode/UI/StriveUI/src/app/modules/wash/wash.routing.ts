import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WashComponent } from './wash.component';


const washRoutes: Routes = [
  { path: 'wash', component: WashComponent}, {
    path: '', component: WashComponent, children: [{
        path: 'wash', component: WashComponent
    }]
}
];


@NgModule({
  imports: [
    RouterModule.forChild(washRoutes)
  ],
  exports: [
    RouterModule,
  ],
})

export class WashRoutingModule { }
