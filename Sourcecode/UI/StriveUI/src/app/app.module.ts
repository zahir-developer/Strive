import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './signup/signup.component';
import {  HttpClientModule } from '@angular/common/http';


const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  bgsColor: '#FF7900',
  bgsOpacity: 0.5,
  bgsPosition: 'bottom-right',
  bgsSize: 60,
  bgsType: 'ball-spin-clockwise',
  blur: 5,
  fgsColor: '#FF7900',
  fgsPosition: 'center-center',
  fgsSize: 60,
  fgsType: 'ball-spin-clockwise',
  gap: 24,
  logoPosition: 'center-center',
  logoSize: 120,
  logoUrl: '',
  overlayColor: 'rgba(40, 40, 40, 0.8)',
  pbColor: '#FF7900',
  pbDirection: 'ltr',
  pbThickness: 3,
  hasProgressBar: true,
  text: 'Loading',
  textColor: '#FFFFFF',
  textPosition: 'center-center',
};


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidenavComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
  ],
  exports: [
    HttpClientModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
