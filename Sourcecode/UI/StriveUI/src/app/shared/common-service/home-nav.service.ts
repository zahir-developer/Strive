import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
    providedIn: 'root'
  })

  export class HomeNavService {

    constructor( private router: Router, private route: ActivatedRoute) { }

    loadLandingPage() {
        if (localStorage.getItem('isDashboard') === 'true') {
          this.router.navigate([`/dashboard`], { relativeTo: this.route });
        }
        else {
          this.routingPage();
    
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
          else if (Roles == 'Wash') {
            this.router.navigate([`/wash`], { relativeTo: this.route });
          }
        }
      }
  }