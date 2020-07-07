import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { LocationSetupComponent } from '../location-setup/location-setup.component';
import { LocationCreateEditComponent } from '../location-setup/location-create-edit/location-create-edit.component';
import { LocationSetupListComponent } from '../location-setup/location-setup-list/location-setup-list.component';
import { ProductSetupComponent } from '../product-setup/product-setup.component';
import { ProductSetupListComponent } from '../product-setup/product-setup-list/product-setup-list.component';
import { ProductCreateEditComponent } from '../product-setup/product-create-edit/product-create-edit.component';
import { VendorSetupComponent } from '../vendor-setup/vendor-setup.component';
import { VendorCreateEditComponent } from '../vendor-setup/vendor-create-edit/vendor-create-edit.component';
import { VendorSetupListComponent } from '../vendor-setup/vendor-setup-list/vendor-setup-list.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TableModule, DialogModule, SharedModule, ConfirmationService } from 'primeng';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { TokenInterceptor } from 'src/app/shared/interceptor/token.interceptor';
import { SetupRoutingModule } from './setup.routing';
import { OnlynumberDirective } from 'src/app/shared/Directive/only-number.directive';
import { SetupComponent } from './setup.component';
import { ServiceSetupComponent } from '../service-setup/service-setup.component';
import { ServiceCreateEditComponent } from '../service-setup/service-create-edit/service-create-edit.component';
import { ServiceSetupListComponent } from '../service-setup/service-setup-list/service-setup-list.component';

@NgModule({
    declarations: [LocationSetupComponent,LocationCreateEditComponent,LocationSetupListComponent,
    ServiceCreateEditComponent,ServiceSetupListComponent,ServiceSetupComponent,ProductSetupComponent,
    ProductSetupListComponent,ProductCreateEditComponent,VendorSetupComponent,VendorCreateEditComponent,
    VendorSetupListComponent,OnlynumberDirective,SetupComponent],
    imports: [
      CommonModule,
      SetupRoutingModule,
      ReactiveFormsModule,
      HttpClientModule,
      FormsModule,
      TableModule,
      DialogModule,
      SharedModule
    ],
    providers: [
      ConfirmationService,
      ThemeService,
      {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
    schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
  })
  export class SetupModule { }
  