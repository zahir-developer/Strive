import { Component, Output, OnInit, EventEmitter } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from '../shared/services/common-service/auth.service';
declare var $: any;
@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})

export class UnauthorizedComponent implements OnInit {

  @Output() authorizeStatusEmitter = new EventEmitter();
  constructor(private spinner: NgxSpinnerService, private authService: AuthService) { }

  ngOnInit(): void {
    this.spinner.hide();
    this.sessionClear();
    this.authorizeStatusEmitter.emit();
    this.sidenavsHide();
    this.authService.userloggedIn.next(false);
  }

  sessionClear()
  {
    localStorage.setItem('isAuthenticated', 'false');
    localStorage.removeItem('authorizationToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('empLocation');
    localStorage.removeItem('tokenExpiry');
    localStorage.removeItem('refreshTokenExpiry');
    localStorage.removeItem('tokenExpiryMinutes');
    localStorage.removeItem('refreshTokenCalled');
  }

  sidenavsHide() {
    $(document).ready(function(){ 
      $('#sidenavSec').css('display','none');
      $('#footerSec').css('display','none');
      $('#headerSec').css('display','none');
     });
  }
}
