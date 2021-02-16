import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../shared/services/common-service/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { UserDataService } from '../shared/util/user-data.service';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html',
  styleUrls: ['./select-location.component.css']
})
export class SelectLocationComponent implements OnInit {
  empName: any;
  location: any;
  locationId = '';
  @ViewChild(LoginComponent) login: LoginComponent;
  roleAccess = [];
  dashBoardModule: boolean = false;

  constructor(private user: UserDataService,
    private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.empName = localStorage.getItem('employeeName');
    this.location = JSON.parse(localStorage.getItem('empLocationId'));
    this.locationId = JSON.parse(localStorage.getItem('empLocationId'))[0].LocationId;

  }
  proceed() {
    if (this.locationId !== '') {
      localStorage.setItem('empLocationId', this.locationId);
      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      //this.router.navigate([`/dashboard`], { relativeTo: this.route });

      this.user.navName.subscribe((data = []) => {
        setTimeout(() => {

          if (data) {
            const newparsedData = JSON.parse(data);
            for (let i = 0; i < newparsedData?.length; i++) {
              const ModuleName = newparsedData[i].ModuleName;

              //DashBoard Module
              if (ModuleName === "Dashboard") {
                this.dashBoardModule = true;
              }
            }

          }

        }, 100)
      })

      if (this.dashBoardModule === true) {
        this.router.navigate([`/dashboard`], { relativeTo: this.route });
      }
      else if (this.dashBoardModule === false) {
        this.routingPage();

      }


    }


  }
  routingPage() {
    const Roles = localStorage.getItem('empRoles');
    if (Roles) {
      if (Roles == 'Admin') {
        this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });
      } else if (Roles == 'Manager') {
        this.router.navigate([`/reports/eod`], { relativeTo: this.route });
      }
      else if (Roles == 'Operator') {
        this.router.navigate([`/reports/eod`], { relativeTo: this.route });
      }
      else if (Roles == 'Cashier') {
        this.router.navigate([`/sales`], { relativeTo: this.route });
      }
      else if (Roles == 'Detailer') {
        this.router.navigate([`/detail`], { relativeTo: this.route });
      }
      else if (Roles == 'Washer') {
        this.router.navigate([`/wash`], { relativeTo: this.route });
      }
    }
  }
}
