import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/shared/util/user-data.service';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAutheticated: boolean;
  isLoggedIn$: Observable<boolean>;
  constructor(private authService: AuthService, private userService: UserDataService, private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
  }
  logout() {
    this.authService.logout();
  }
}
