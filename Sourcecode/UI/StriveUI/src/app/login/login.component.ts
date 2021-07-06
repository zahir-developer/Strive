import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService } from '../shared/services/login.service';
import { Router, ActivatedRoute } from '@angular/router'
import { AuthService } from '../shared/services/common-service/auth.service';
import { WhiteLabelService } from '../shared/services/data-service/white-label.service';
import { MessengerService } from '../shared/services/data-service/messenger.service';
import { UserDataService } from '../shared/util/user-data.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LandingService } from '../shared/services/common-service/landing.service';
import { GetCodeService } from '../shared/services/data-service/getcode.service';
import { CodeValueService } from '../shared/common-service/code-value.service';
import { tap, mapTo, share } from 'rxjs/operators';
import { ApplicationConfig } from '../shared/services/ApplicationConfig';
import { WeatherService } from '../shared/services/common-service/weather.service';
import { LogoService } from '../shared/services/common-service/logo.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errorFlag = false;
  loginForm: FormGroup;
  submitted = false;
  display = false;
  loginDetail: string;
  isLoginLoading: boolean;
  whiteLabelDetail: any;
  colorTheme: any;
  favIcon: HTMLLinkElement = document.querySelector('#appIcon');
  dashBoardModule: boolean;
  isRememberMe: boolean;
  showAccount = false;
  emailregex: RegExp = /^[ A-Za-z0-9@.+]*$/;

  constructor(
    private loginService: LoginService, private router: Router, private route: ActivatedRoute,
    private authService: AuthService, private whiteLabelService: WhiteLabelService, private getCodeService: GetCodeService,
    private msgService: MessengerService, private user: UserDataService,
    private logoService: LogoService,
    private spinner: NgxSpinnerService, private weatherService: WeatherService,
    private landing: LandingService, private codeValueService: CodeValueService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(data => {
    });
    this.getQueryToken();
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      isRemember: new FormControl(false)
    });
    this.bindValue();
  }

  bindValue() {
    if (localStorage.getItem('isRemember') !== null) {
      if (localStorage.getItem('isRemember') === 'true') {
        this.loginForm.patchValue({
          isRemember: localStorage.getItem('isRemember'),
          username: localStorage.getItem('username'),
          password: localStorage.getItem('password')
        });
      } else {
        this.loginForm.patchValue({
          isRemember: false,
          username: '',
          password: ''
        });
      }
    }
  }
  get f() { return this.loginForm.controls; }
  LoginSubmit(): void {
    this.submitted = true;
    this.errorFlag = false;
    if (this.loginForm.invalid) {
      this.errorFlag = true;
      return;
    }
    const loginObj = {
      email: this.loginForm.value.username,
      passwordHash: this.loginForm.value.password
    };
    // console.log(localStorage.getItem('isRemember'));
    if (localStorage.getItem('isRemember') === 'true') {
      localStorage.setItem('username', this.loginForm.value.username);
      localStorage.setItem('password', this.loginForm.value.password);
    } else {
      localStorage.removeItem('username');
      localStorage.removeItem('password');
    }
    this.isLoginLoading = true;
    this.spinner.show();
    this.authService.login(loginObj).subscribe(data => {
      this.isLoginLoading = false;
      if (data) {
        if (data.status === 'Success') {
          this.spinner.hide();
          const token = JSON.parse(data.resultData);
          this.landing.loadTheLandingPage(true);
          this.getCodeValue();
          this.getThemeColor();
          this.msgService.startConnection();
        } else {
          this.errorFlag = true;
          this.isLoginLoading = false;
          this.spinner.hide();
        }
      }
    }, (err) => {
      this.spinner.hide();
      this.isLoginLoading = false;
    });
  }

  rememberMe(event) {
    console.log(event);
    localStorage.setItem('isRemember', event.target.checked);
  }

  forgotPassword() {
    this.router.navigate([`/forgot-password`], { relativeTo: this.route });
  }

  getThemeColor() {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
        if (res.status === 'Success') {
          const label = JSON.parse(res.resultData);
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          const base64 = 'data:image/png;base64,';
          const logoBase64 = base64 + label.WhiteLabelling.WhiteLabel?.Base64;
          this.favIcon.href = logoBase64;
          this.colorTheme = label.WhiteLabelling.Theme;
          this.whiteLabelDetail = label.WhiteLabelling.WhiteLabel;
          this.colorTheme.forEach(item => {
            if (this.whiteLabelDetail.ThemeId === item.ThemeId) {
              document.documentElement.style.setProperty(`--primary-color`, item.PrimaryColor);
              document.documentElement.style.setProperty(`--navigation-color`, item.NavigationColor);
              document.documentElement.style.setProperty(`--secondary-color`, item.SecondaryColor);
              document.documentElement.style.setProperty(`--tertiary-color`, item.TertiaryColor);
              document.documentElement.style.setProperty(`--body-color`, item.BodyColor);
            }
          });
          document.documentElement.style.setProperty(`--text-font`, this.whiteLabelDetail.FontFace);
        }
      });
    }
  }

  getCodeValue() {
    this.getCodeService.getCodeByCategory(ApplicationConfig.Category.all).subscribe(res => {
      if (res.status === 'Success') {
        const value = JSON.parse(res.resultData);
        localStorage.setItem('codeValue', JSON.stringify(value.Codes));
      }
    });
  }


  createAccount() {
    this.router.navigate(['/signup'], { queryParams: { token: '12345' } });
  }

  getQueryToken() {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params.token) {
        this.showAccount = true;
        this.authService.logoutSecond();
      } else {
        this.showAccount = false;
        this.authService.logout();
      }
      localStorage.setItem('GUIDCODE', params.token);
    });
  }



}
