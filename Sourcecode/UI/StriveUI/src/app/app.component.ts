import { Component, OnInit } from '@angular/core';
import { UserDataService } from './shared/util/user-data.service';
import { AuthenticateObservableService } from './shared/observable-service/authenticate-observable.service';
import { WhiteLabelService } from './shared/services/data-service/white-label.service';
import { LogoService } from './shared/services/common-service/logo.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
// import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'StriveUI';
  isUserAuthenticated = false;
  navData: any;
  constructor(private user: UserDataService, 
    private router : Router,
    private authService: AuthenticateObservableService,
    private whiteLabelService: WhiteLabelService, private logoService: LogoService, private userService: UserDataService) {
    this.isUserAuthenticated = this.user.isAuthenticated;
    // console.log(this.isUserAuthenticated);
    // console.log(this.authService.getIsAuthenticate);
  }
  ngOnInit() {
  
 
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.getTheme();
      this.setHeaderName();
      this.setNavList();
   
    }

  }
  setNavList(){
   this.userService.navName.subscribe(data => 
    {
      this.navData = data;

    }) 
     
      
      
    
  }
  setHeaderName() {
    if (localStorage.getItem('employeeName') !== undefined) {
      const headerName = localStorage.getItem('employeeName');
      this.userService.setHeaderName(headerName);
    }
  }
  getTheme() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
      if (res.status === 'Success') {
        const label = JSON.parse(res.resultData);
        if (label?.WhiteLabelling?.WhiteLabel !== undefined) {
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          if (label.WhiteLabelling.Theme !== null) {
            label.WhiteLabelling.Theme.forEach(item => {
              if (label.WhiteLabelling.WhiteLabel?.ThemeId === item.ThemeId) {
                document.documentElement.style.setProperty(`--primary-color`, item.PrimaryColor);
                document.documentElement.style.setProperty(`--navigation-color`, item.NavigationColor);
                document.documentElement.style.setProperty(`--secondary-color`, item.SecondaryColor);
                document.documentElement.style.setProperty(`--tertiary-color`, item.TertiaryColor);
                document.documentElement.style.setProperty(`--body-color`, item.BodyColor);
              }
            });
          }
          document.documentElement.style.setProperty(`--text-font`, label.WhiteLabelling.WhiteLabel?.FontFace);
        }
      }
    });
  }
}
