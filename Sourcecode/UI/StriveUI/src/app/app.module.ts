import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, APP_INITIALIZER } from '@angular/core';
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
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { TokenInterceptor } from './shared/interceptor/token.interceptor';
import { TableModule} from 'primeng/table';
import {MomentModule} from 'angular2-moment';
import { HelpsComponent } from './helps/helps.component'
import { ViewCustomerDetailsComponent } from './helps/view-customer-details/view-customer-details.component';
import { CreateCustomerDetailsComponent } from './helps/create-customer-details/create-customer-details.component';
import { ToastrModule } from 'ngx-toastr';
import {environment} from 'src/environments/environment';
import {DialogModule} from 'primeng/dialog';
import { EnvironmentService } from './shared/util/environment.service';
import { DynamicTextboxComponent } from './helps/dynamic-textbox/dynamic-textbox.component';
import { AuthService } from './shared/services/common-service/auth.service';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { HttpUtilsService } from './shared/util/http-utils.service';
import { RouterModule } from '@angular/router';
import {MultiSelectModule} from 'primeng/multiselect';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

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

const load = (http: HttpClient) => {
  return () => {
    let url = 'api/Environment';
    if (!environment.production) {
      // use for local environment
      // url = 'http://localhost:39141/' + url;
      // for temporary use
      return http.get('assets/config/config.json').toPromise()
          .then((data: any) => {
            // console.log(data);
            return EnvironmentService.environment = data;
          });
    } else {
      url = window.origin + window.location.pathname + '/api/Environment';
    }
    return http.get(url, { responseType: 'text' }).toPromise()
      .then((Aspenvironment: string) => {
        if (Aspenvironment !== undefined && Aspenvironment !== null) {
          EnvironmentService.hostenvironment = Aspenvironment;
          let appUrl = '/assets/config/';
          if (!environment.production) {
            // use for local environment
            appUrl = appUrl + 'config' + '.json';
          } else {
            appUrl = appUrl + Aspenvironment.toLocaleLowerCase() + '.json';
          }
          return http.get(window.origin + window.location.pathname + appUrl).toPromise()
            .then((data: any) => {
              return EnvironmentService.environment = data;
            },
              (error: any) => {
              });
        }
      }, (error: any) => {
        return http.get('assets/config/config.json').toPromise()
          .then((data: any) => {
            return EnvironmentService.environment = data;
          });
      });
  };
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
    DynamicTextboxComponent,
    ForgotPasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule,
    TableModule,
    DialogModule,
    MomentModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      autoDismiss: true,
      positionClass: 'toast-top-full-width',
      preventDuplicates: false,
     enableHtml : true
    }),
    NgxSkeletonLoaderModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    MultiSelectModule
  ],
  exports: [
    HttpClientModule,
    SharedModule,
    RouterModule,
    MultiSelectModule
  ],
  providers: [
    EnvironmentService,
    {
      provide: APP_INITIALIZER,
      useFactory: load,
      deps: [HttpClient
      ],
      multi: true
    },
    {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  },
  AuthService,
  HttpUtilsService
],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
