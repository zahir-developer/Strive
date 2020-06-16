import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HelpsComponent } from './helps/helps.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent, data: { label: 'login', title: 'login' } },
  { path: 'signup', component: SignupComponent, data: { label: 'login', title: 'login' } },
  { path: 'helps', component: HelpsComponent},
  {path: 'admin', loadChildren: () => import('./modules/admin/admin.module').then(mod => mod.AdminModule)},
  {path: 'wash', loadChildren: () => import('./modules/wash/wash.module').then(mod => mod.WashModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
