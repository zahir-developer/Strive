import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.css']
})
export class SetupComponent implements OnInit {

  setupForm: FormGroup;
  sysSetup: any;
  subModule: string = 'Basic Setup';

  constructor(private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {

    this.loadLocationSetup();
  }



  loadLocationSetup(): void {
    this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });
  }

  setupEvent(event) {
    if (event.target.value == 1) {
      this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });
    } else if (event.target.value == 2) {
      this.router.navigate([`/admin/setup/service`], { relativeTo: this.route });
    } else if (event.target.value == 3) {
      this.router.navigate([`/admin/setup/product`], { relativeTo: this.route });
    } else if (event.target.value == 4) {
      this.router.navigate([`/admin/setup/vendor`], { relativeTo: this.route });
    } else if (event.target.value == 5) {
      this.router.navigate([`/admin/setup/membership`], { relativeTo: this.route });
    } else if (event.target.value == 6) {
      this.router.navigate([`/admin/setup/adSetup`], { relativeTo: this.route });
    } else if (event.target.value == 7) {
      this.router.navigate([`/admin/setup/bonus`], { relativeTo: this.route });
    } else if (event.target.value == 8) {
      this.router.navigate([`/admin/setup/checkList`], { relativeTo: this.route });
    }
    else if (event.target.value == 9) {
      this.router.navigate([`/admin/setup/dealSetup`], { relativeTo: this.route });
    }
    else if (event.target.value == 10) {
      this.router.navigate([`/admin/setup/empHandBook`], { relativeTo: this.route });
    }
    else if (event.target.value == 11) {
      this.router.navigate([`/admin/setup/emailBlast`], { relativeTo: this.route });
    }
    else if (event.target.value == 12) {
      this.router.navigate([`/admin/setup/terms&condition`], { relativeTo: this.route });
    }
    else if (event.target.value == 13) {
      this.router.navigate([`/admin/setup/paymentGateway`], { relativeTo: this.route });
    }

  }

  subMenuClick(menu) {
    this.subModule = menu + ' ' + 'Setup';
  }
}
