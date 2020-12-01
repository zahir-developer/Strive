import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HelpsComponent } from './helps/helps.component';
import { AuthGuard } from './shared/guards/auth-guard.service';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { SelectLocationComponent } from './select-location/select-location.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, data: { label: 'login', title: 'login' } },
  { path: 'forgot-password', component: ForgotPasswordComponent, data: { label: 'forgot-password', title: 'forgot-password' } },
  { path: 'location', component: SelectLocationComponent, data: { label: 'select-location', title: 'select-location' } },
  { path: 'signup', component: SignupComponent, data: { label: 'login', title: 'login' } },
  { path: 'helps', component: HelpsComponent },
  { path: 'admin', canActivate: [AuthGuard], loadChildren: () => import('./modules/admin/admin.module').then(mod => mod.AdminModule) },
  { path: 'wash', canActivate: [AuthGuard], loadChildren: () => import('./modules/wash/wash.module').then(m => m.WashModule) },
  { path: 'detail', loadChildren: () => import('./modules/detail/detail.module').then(m => m.DetailModule) },
  { path: 'checkout', loadChildren: () => import('./modules/checkout/checkout.module').then(m => m.CheckoutModule) },
  {
    path: 'dashboard', canActivate: [AuthGuard], data: { label: 'Dashboard', title: 'Dashboard', authorization: 'Dashboard' },
    loadChildren: () => import('./modules/dashboard/dashboard.module').then(mod => mod.DashboardModule)
  },
  {path: 'sales', loadChildren: () => import('./modules/sales/sales.module').then(m => m.SalesModule)},
  {path: 'white-labelling', loadChildren: () =>
  import('./modules/white-labelling/white-labelling.module').then(m => m.WhiteLabellingModule)},
  { path: 'payrolls', loadChildren: () => import('./modules/payrolls/payrolls.module').then(m => m.PayrollsModule) },
  {path: 'messenger', loadChildren: () => import('./modules/messenger/messenger.module').then(m => m.MessengerModule)},
  {path: 'reports', loadChildren: () => import('./modules/reports/reports.module').then(mod => mod.ReportsModule)},
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full'
  }
];
const config: ExtraOptions = {
  useHash: true,
  // enableTracing: true,
  preloadingStrategy: PreloadAllModules
};
@NgModule({
  imports: [RouterModule.forRoot(routes, config)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
