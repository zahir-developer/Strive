import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrintComponent } from './sales/print/print.component';
import { SalesComponent } from './sales/sales.component';

const salesRoutes: Routes = [
 {path: 'sales', component: SalesComponent},
 {path: '', component: SalesComponent, children: [{
        path: 'print', component: PrintComponent }]
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
