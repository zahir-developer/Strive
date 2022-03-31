import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/common-service/auth.service';
declare var $: any;

@Component({
  selector: 'app-session-expired',
  templateUrl: './session-expired.component.html',
  styleUrls: ['./session-expired.component.css']
})
export class SessionExpiredComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
    $(document).ready(function () {
      $('#sidenavSec').css('display', 'none');
      $('#footerSec').css('display', 'none');
      $('#headerSec').css('display', 'none');
    });
    this.authService.userloggedIn.next(false);
    this.clearCacheValue();

  }

  clearCacheValue() {
    localStorage.setItem('isAuthenticated', 'false');
    localStorage.removeItem('authorizationToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('empLocation');
    localStorage.removeItem('tokenExpiry');
    localStorage.removeItem('tokenExpiryMinutes');
    localStorage.removeItem('refreshTokenExpiry');
    localStorage.removeItem('refreshTokenExpiryMinutes');
    localStorage.removeItem('refreshTokenCalled');
  }

  ngOnDestroy() {
    $(document).ready(function () {
      $('#sidenavSec').css('display', 'flex');
      $('#footerSec').css('display', 'flex');
      $('#headerSec').css('display', 'flex');
    });
  }


  loginPage() {
    this.router.navigate([`/login`]);
  }


}
