import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { UserDataService } from './shared/util/user-data.service';
import { AuthenticateObservableService } from './shared/observable-service/authenticate-observable.service';
import { WhiteLabelService } from './shared/services/data-service/white-label.service';
import { LogoService } from './shared/services/common-service/logo.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';
import { Keepalive } from '@ng-idle/keepalive';
import { IdleLockoutComponent } from './shared/components/idle-lockout/idle-lockout.component';
import { Subscription } from 'rxjs';
import { AuthService } from './shared/services/common-service/auth.service';
import { SessionLogoutComponent } from './shared/components/session-logout/session-logout.component';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  @ViewChild(IdleLockoutComponent) idleLockoutComponent: IdleLockoutComponent;
  @ViewChild(SessionLogoutComponent) sessionLogoutComponent: SessionLogoutComponent;
  title = 'StriveUI';
  isUserAuthenticated = false;
  navData: any;
  localStorageUpdation = 'false';
  updation: boolean;
  showViewMaster: boolean;
  dialogDisplay = false;
  header: string;
  dialogType: string;
  countdown?: number;
  TimeoutPeriod = 30;
  intervalId: any;
  subscriptionAuthenticate: Subscription;
  constructor(
    private user: UserDataService,
    private router: Router,
    private authenticate: AuthenticateObservableService,
    private whiteLabelService: WhiteLabelService, private logoService: LogoService,
    private userService: UserDataService, private authService: AuthService,
    private idle: Idle) {
    this.isUserAuthenticated = this.user.isAuthenticated;
    this.subscriptionAuthenticate = this.authenticate.getIsAuthenticate().subscribe(isAuthenticate => {
      if (isAuthenticate)  {
       this.initializeTimeOut();
      } else {
        this.idle.stop();
      }
    });
  }
  ngOnInit() {
    this.initializeTimeOut();
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.getTheme();
      this.setHeaderName();
      this.setNavList();
    }

  }
  setNavList() {
    this.userService.navName.subscribe(data => {
      this.navData = data;
      if (data) {
        localStorage.setItem('views', data);
        this.localStorageUpdation = 'false';
      }
    });
  }
  closePopup(e) {
    this.showViewMaster = false;
  }
  setHeaderName() {
    if (localStorage.getItem('employeeName') !== undefined) {
      const headerName = localStorage.getItem('employeeName');
      this.userService.setHeaderName(headerName);
    }
  }

  initializeTimeOut() {
    if (this.user.isAuthenticated) {
      const seconds = 10 * 60;    // 60
      this.subscribeTheIdle(this.idle, seconds);
    }
  }

  subscribeTheIdle(idle, seconds) {
    // console.log(seconds);
    //  const idleTimeoutPeriod = seconds - this.TimeoutPeriod;
    const idleTimeoutPeriod = seconds;
    if (idleTimeoutPeriod < 0) {
      return false;
    }
    // sets an idle timeout of 5 seconds, for testing purposes.
    idle.setIdle(seconds);
    // sets a timeout period of 5 seconds. after 10 seconds of inactivity, the user will be considered timed out.
    idle.setTimeout(60);  // 60
    // sets the default interrupts, in this case, things like clicks, scrolls, touches to the document
    idle.setInterrupts(DEFAULT_INTERRUPTSOURCES);
    idle.onIdleEnd.subscribe(() => {
      // console.log('onIdle');
      this.sessionLogoutComponent.idleClear();
      this.sessionLogoutComponent.dialogDisplay = false;
      this.dialogDisplay = false;
      this.sessionLogoutComponent.header = '';
      this.header = '';
      this.sessionLogoutComponent.dialogType = 'noIdle';
      clearInterval(this.intervalId);
    });
    idle.onTimeoutWarning.subscribe((countdown) => {
      // console.log('onTimeoutWarning');
      this.dialogDisplay = true;
      this.sessionLogoutComponent.dialogDisplay = true;
      // this.idleState = 'You will time out in ' + countdown + ' seconds!'
      this.sessionLogoutComponent.countdown = countdown;
      this.sessionLogoutComponent.dialogType = 'idle';
      this.sessionLogoutComponent.header = 'Session Timeout';
      this.header = 'Session Timeout Warning!';
    }
    );
    idle.onTimeout.subscribe(() => {
      // console.log('onTimeout');
      this.dialogDisplay = true;
      this.sessionLogoutComponent.dialogType = 'timeout';
      this.sessionLogoutComponent.dialogDisplay = true;
      this.sessionLogoutComponent.header = 'Locked Out';
      this.header = 'Hey..! Session expired.. Please login to continue..!';
      this.authService.refreshLogout();
      clearInterval(this.intervalId);
    });
    idle.onIdleStart.subscribe(() => {
      //  console.log('onIdleStart');
      clearInterval(this.intervalId);
      this.timeCounter();
    }
    );
    this.reset();
  }

  /*
  * Idle watch start here
  */
  reset() {
    this.idle.watch();
  }

  /*
  * countdown starter
  */
  timeCounter(counter = this.TimeoutPeriod) {
    this.intervalId = setInterval(() => {
      counter = counter - 1;
      this.sessionLogoutComponent.countdown = counter;
      this.sessionLogoutComponent.dialogType = 'idle';
      this.dialogDisplay = true;
      this.sessionLogoutComponent.dialogDisplay = true;
      this.sessionLogoutComponent.header = 'Idle Warning.';
      this.header = 'Idle Warning.';
      if (counter <= 0) {
        clearInterval(this.intervalId);
      }
    }, 1000);
  }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscriptionAuthenticate.unsubscribe();
  }

  closeDialog() {
    this.dialogDisplay = false;
  }

  getTheme() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
      if (res.status === 'Success') {
        const label = JSON.parse(res.resultData);
        if (label?.WhiteLabelling?.WhiteLabel !== undefined) {
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          this.logoService.setTitle(label.WhiteLabelling.WhiteLabel?.Title);
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
