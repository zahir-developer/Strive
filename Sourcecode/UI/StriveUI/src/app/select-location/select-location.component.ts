import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/common-service/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html',
  styleUrls: ['./select-location.component.css']
})
export class SelectLocationComponent implements OnInit {
empName: any;
location: any;
locationId = '';
  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

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
this.router.navigate([`/dashboard`], { relativeTo: this.route });
  }
}
}
