import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserDataService } from '../../util/user-data.service';
import { AuthService } from './auth.service';
import { ApplicationConfig } from '../ApplicationConfig';
import { MessageConfig } from '../messageConfig';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class LandingService {
  dashBoardModule: boolean;

  constructor(private toastr: ToastrService,
    private authService: AuthService, private user: UserDataService, private router: Router, private route: ActivatedRoute,) { }
  loadTheLandingPage(isFromLogin?: boolean): void {
    const location = JSON.parse(localStorage.getItem('empLocation'));
    if (location?.length > 1 && isFromLogin) {

      this.router.navigate([`/location`], { relativeTo: this.route });

    }
    else if (location !== null ? location !== undefined : false) {
      localStorage.setItem('empLocationName', location[0].LocationName);
      localStorage.setItem('employeeCityName', location[0].CityName);
      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      this.user.navName.subscribe((data) => {

        const newparsedData = JSON.parse(data);
        if (newparsedData) {

          for (let i = 0; i < newparsedData?.length; i++) {
            const ModuleName = newparsedData[i].ModuleName;

            //DashBoard Module
            if (ModuleName === "Dashboard") {
              this.dashBoardModule = true;
            }
            else {
              this.routingPage();
            }
          }
        }
        else {
          this.routingPage();
        }

      });

      if (this.dashBoardModule === true) {
        this.router.navigate([`/dashboard`], { relativeTo: this.route });
      }
      else if (this.dashBoardModule === false) {
        this.routingPage();
      }
    }
    else
    {
      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      this.routingPage();
    }
  }
  routingPage() {
    const Roles = localStorage.getItem('empRoles');
    if (Roles) {
      if (Roles === ApplicationConfig.Roles.Admin) {
        this.router.navigate([`/dashboard`], { relativeTo: this.route });
      } else if (Roles === ApplicationConfig.Roles.Manager) {
        this.router.navigate([`/reports/eod`], { relativeTo: this.route });
      }
      else if (Roles === ApplicationConfig.Roles.Operator) {
        this.router.navigate([`/reports/eod`], { relativeTo: this.route });
      }
      else if (Roles === ApplicationConfig.Roles.Cashier) {
        this.router.navigate([`/sales`], { relativeTo: this.route });
      }
      else if (Roles === ApplicationConfig.Roles.Detailer) {
        this.router.navigate([`/detail`], { relativeTo: this.route });
      }
      else if (Roles === ApplicationConfig.Roles.Wash) {
        this.router.navigate([`/wash`], { relativeTo: this.route });
      }
      else if (Roles === ApplicationConfig.Roles.Customer) {
        const clientId = localStorage.getItem('clientId');
        this.router.navigate([`/customer`], { relativeTo: this.route, queryParams: { clientId: clientId } });
      }
      else if(Roles === ApplicationConfig.Roles.Greeter)
      {
        this.router.navigate([`/checkout`], { relativeTo: this.route });
      }
    }
  }
}
