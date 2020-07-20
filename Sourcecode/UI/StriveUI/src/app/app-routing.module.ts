import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HelpsComponent } from './helps/helps.component';
import {  AuthGuard} from './shared/guards/auth-guard.service';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, data: { label: 'login', title: 'login' } },
  { path: 'forgot-password', component: ForgotPasswordComponent, data: { label: 'forgot-password', title: 'forgot-password' } },
  { path: 'signup', component: SignupComponent, data: { label: 'login', title: 'login' } },
  { path: 'helps', component: HelpsComponent },
  { path: 'admin', canActivate: [AuthGuard], loadChildren: () => import('./modules/admin/admin.module').then(mod => mod.AdminModule) },
  { path: 'wash', canActivate: [AuthGuard], loadChildren: () => import('./modules/wash/wash.module').then(m => m.WashModule) },
  { path: 'dashboard', canActivate: [AuthGuard], 
  loadChildren: () => import('./modules/dashboard/dashboard.module').then(mod => mod.DashboardModule) },
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
