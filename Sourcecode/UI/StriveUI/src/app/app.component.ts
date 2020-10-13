import { Component, OnInit } from '@angular/core';
import { UserDataService } from './shared/util/user-data.service';
import { AuthenticateObservableService } from './shared/observable-service/authenticate-observable.service';
import { WhiteLabelService } from './shared/services/data-service/white-label.service';
import { LogoService } from './shared/services/common-service/logo.service';
// import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'StriveUI';
  isUserAuthenticated = false;
  constructor(private user: UserDataService, private authService: AuthenticateObservableService,
    private whiteLabelService: WhiteLabelService, private logoService: LogoService) {
    this.isUserAuthenticated = this.user.isAuthenticated;
    // console.log(this.isUserAuthenticated);
    // console.log(this.authService.getIsAuthenticate);
  }
  ngOnInit() {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.getTheme();
    }
  }
  getTheme() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
      if (res.status === 'Success') {
        const label = JSON.parse(res.resultData);
        if (label?.WhiteLabelling?.WhiteLabel !== undefined) {
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          document.documentElement.style.setProperty(`--primary-color`, label.WhiteLabelling.WhiteLabel?.PrimaryColor);
          document.documentElement.style.setProperty(`--navigation-color`, label.WhiteLabelling.WhiteLabel?.NavigationColor);
          document.documentElement.style.setProperty(`--secondary-color`, label.WhiteLabelling.WhiteLabel?.SecondaryColor);
          document.documentElement.style.setProperty(`--tertiary-color`, label.WhiteLabelling.WhiteLabel?.TertiaryColor);
          document.documentElement.style.setProperty(`--body-color`, label.WhiteLabelling.WhiteLabel?.BodyColor);
          document.documentElement.style.setProperty(`--text-font`, label.WhiteLabelling.WhiteLabel?.FontFace);
        }
      }
    });
  }
}
