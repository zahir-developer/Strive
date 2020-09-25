import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {SalesComponent} from './sales/sales.component';

const salesRoutes: Routes = [
 {path: 'sales', component: SalesComponent},
 {path: '', component: SalesComponent, children: [{
        path: 'sales', component: SalesComponent }]
}
];


@NgModule({
  imports: [
    RouterModule.forChild(salesRoutes)
  ],
  exports: [
    RouterModule,
  ],
})

export class SalesRoutingModule { }
