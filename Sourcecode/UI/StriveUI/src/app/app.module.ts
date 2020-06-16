import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SignupComponent } from './signup/signup.component';
import { HttpClientModule } from '@angular/common/http';
import { TableModule} from 'primeng/table';
import { HelpsComponent } from './helps/helps.component'
import { ViewCustomerDetailsComponent } from './helps/view-customer-details/view-customer-details.component';
import { CreateCustomerDetailsComponent } from './helps/create-customer-details/create-customer-details.component';
import { ToastrModule } from 'ngx-toastr';
import {DialogModule} from 'primeng/dialog';


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
    SignupComponent,
    HelpsComponent,
    ViewCustomerDetailsComponent,
    CreateCustomerDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    TableModule,
    DialogModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
  ],
  exports: [
    HttpClientModule,

  ],
  providers: [],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
