import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

const dashboardModules: Routes = [{
    path: 'dashboard', component: DashboardComponent
},
{
    path: '', component: DashboardComponent,
    children: [{
        path: 'dashboard', component: DashboardComponent
    }]
}];

@NgModule({
    imports: [RouterModule.forChild(dashboardModules)],
    exports: [RouterModule]
})
export class DashboardRoutingModule { }
