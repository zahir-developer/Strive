import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserDataService } from 'src/app/shared/util/user-data.service';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SidenavComponent implements OnInit {
  isAuthenticated: boolean;
  isLoggedIn$: Observable<boolean>;
  constructor(private user: UserDataService, private authService: AuthService) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    console.log(this.isLoggedIn$, 'side Nae');
  }
}
