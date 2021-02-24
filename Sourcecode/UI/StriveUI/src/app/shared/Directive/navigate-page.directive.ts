import { HostListener } from '@angular/core';
import { Directive } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/common-service/auth.service';
import { UserDataService } from '../util/user-data.service';

@Directive({
  selector: '[appNavigatePage]'
})
export class NavigatePageDirective {
  dashBoardModule: boolean;

  constructor(private router: Router, private route: ActivatedRoute,
    private authService:AuthService,
    private user: UserDataService,) { }

  @HostListener('click', ['$event']) onClick(event) {
    //this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });

      const location = localStorage.getItem('empLocationId');
      if (!Array.isArray(JSON.parse(location))) {
        localStorage.setItem('isAuthenticated', 'true');
        this.authService.loggedIn.next(true);
        this.user.navName.subscribe((data= [] ) => {
          setTimeout(() => {
  
            if (data) {
              const newparsedData = JSON.parse(data);
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
  
          }, 100);
        });
  
        if (this.dashBoardModule === true) {
          this.router.navigate([`/dashboard`], { relativeTo: this.route });
        }
        else if (this.dashBoardModule === false) {
          this.routingPage();
  
        }
      } else {
        this.router.navigate([`/location`], { relativeTo: this.route });
      }
    }
    routingPage() {
      const Roles = localStorage.getItem('empRoles');
      if (Roles) {
        if (Roles === 'Admin') {
          this.router.navigate([`/dashboard`], { relativeTo: this.route });
        } else if (Roles === 'Manager') {
          this.router.navigate([`/reports/eod`], { relativeTo: this.route });
        }
        else if (Roles === 'Operator') {
          this.router.navigate([`/reports/eod`], { relativeTo: this.route });
        }
        else if (Roles === 'Cashier') {
          this.router.navigate([`/sales`], { relativeTo: this.route });
        }
        else if (Roles === 'Detailer') {
          this.router.navigate([`/detail`], { relativeTo: this.route });
        }
        else if (Roles === 'Wash') {
          this.router.navigate([`/wash`], { relativeTo: this.route });
        }
        else if (Roles === 'Client') {
          const clientId = localStorage.getItem('clientId');
          this.router.navigate([`/customer`], { relativeTo: this.route, queryParams: { clientId: clientId } });
        }
      }
    }
  }


