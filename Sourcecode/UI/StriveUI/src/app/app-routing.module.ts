import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HelpsComponent } from './helps/helps.component';
import { AuthGuard } from './shared/guards/auth-guard.service';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { SelectLocationComponent } from './select-location/select-location.component';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserSignupComponent } from './user-signup/user-signup.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, data: { label: 'login', title: 'login' } },
  { path: 'login/:token', component: LoginComponent, data: { label: 'login', title: 'login' } },
  { path: 'forgot-password', component: ForgotPasswordComponent, data: { label: 'forgot-password', title: 'forgot-password' } },
  { path: 'location', component: SelectLocationComponent, data: { label: 'select-location', title: 'select-location' } },
  // { path: 'signup', component: SignupComponent, data: { label: 'login', title: 'login' } },
  { path: 'signup', component: UserSignupComponent, data: { label: 'signup', title: 'signup' } },
  { path: 'signup/:token', component: UserSignupComponent, data: { label: 'signup', title: 'signup' } },
  { path: 'helps', component: HelpsComponent },
  { path: 'side-nav', component: SidenavComponent },

  { path: 'admin', canActivate: [AuthGuard], data: { label: 'Admin', title: 'Admin', authorization: 'Admin' }, loadChildren: () => import('./modules/admin/admin.module').then(mod => mod.AdminModule) },
  {
    path: 'wash', canActivate: [AuthGuard], data: { label: 'Washes', title: 'Washes', authorization: 'Washes' },
    loadChildren: () => import('./modules/wash/wash.module').then(m => m.WashModule)
  },
  {
    path: 'detail', canActivate: [AuthGuard],  data: { label: 'Detail', title: 'Detail', authorization: 'Detail' },
    loadChildren: () => import('./modules/detail/detail.module').then(m => m.DetailModule)
  },
  {
    path: 'checkout', canActivate: [AuthGuard], data: { label: 'Checkout', title: 'Checkout', authorization: 'Checkout' },
    loadChildren: () => import('./modules/checkout/checkout.module').then(m => m.CheckoutModule)
  },
  {
    path: 'dashboard', canActivate: [AuthGuard], data: { label: 'Dashboard', title: 'Dashboard', authorization: 'Dashboard' },
    loadChildren: () => import('./modules/dashboard/dashboard.module').then(mod => mod.DashboardModule)
  },
  {
    path: 'sales', canActivate: [AuthGuard], data: { label: 'Sales', title: 'Sales', authorization: 'Sales' },
    loadChildren: () => import('./modules/sales/sales.module').then(m => m.SalesModule)
  },
  {
    path: 'white-labelling', canActivate: [AuthGuard], data: { label: 'White Labelling', title: 'White Labelling', authorization: 'WhiteLabelling' },
    loadChildren: () =>
      import('./modules/white-labelling/white-labelling.module').then(m => m.WhiteLabellingModule)
  },
  {
    path: 'payrolls', canActivate: [AuthGuard], data: { label: 'PayRoll', title: 'PayRoll', authorization: 'PayRoll' },
    loadChildren: () => import('./modules/payrolls/payrolls.module').then(m => m.PayrollsModule)
  },
  {
    path: 'messenger', canActivate: [AuthGuard], data: { label: 'Messenger', title: 'Messenger', authorization: 'Messenger' },
    loadChildren: () => import('./modules/messenger/messenger.module').then(m => m.MessengerModule)
  },
  {
    path: 'reports', canActivate: [AuthGuard], data: { label: 'Report', title: 'Report', authorization: 'Report' },
    loadChildren: () => import('./modules/reports/reports.module').then(mod => mod.ReportsModule)
  },
  {
    path: 'customer', canActivate: [AuthGuard], data: { label: 'Customer', title: 'Customer', authorization: 'Customer' },
    loadChildren: () => import('./modules/customer/customer.module').then(mod => mod.CustomerModule)
  },
  {
    path: 'customer-history', data: { label: 'Customer-History', title: 'Customer-History' },
    loadChildren: () => import('./modules/customer-history/customer-history.module').then(mod => mod.CustomerHistoryModule)
  },
  {
    path: 'Customer-sales', data: { label: 'Customer-sales', title: 'Customer-sales', authorization: 'Customer-sales' },
    loadChildren: () => import('./modules/customer-sales/customer-sales.module').then(mod => mod.CustomerSalesModule)
  },
  { path: 'unauthorized', component: UnauthorizedComponent, data: { label: 'unauthorized', title: 'unauthorized' } },
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full'
  }
];
const config: ExtraOptions = {
  useHash: true,
  preloadingStrategy: PreloadAllModules
};
@NgModule({
  imports: [RouterModule.forRoot(routes, config)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
