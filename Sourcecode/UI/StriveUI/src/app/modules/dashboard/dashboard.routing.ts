import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { AuthGuard } from 'src/app/shared/guards/auth-guard.service';

const dashboardModules: Routes = [{
    path: 'dashboard', component: DashboardComponent,
     data: { label: 'Dashboard', title: 'Dashboard', authorization: 'Dashboard' }
},
{
    path: '', component: DashboardComponent,
    children: [{
        path: 'dashboard', component: DashboardComponent, data: { label: 'Dashboard', title: 'Dashboard', authorization: 'Dashboard' }
    }]
}];

@NgModule({
    imports: [RouterModule.forChild(dashboardModules)],
    exports: [RouterModule]
})
export class DashboardRoutingModule { }
