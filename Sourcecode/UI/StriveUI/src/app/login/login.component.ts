import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService } from '../shared/services/login.service';
import { Router, ActivatedRoute } from '@angular/router'
import { AuthService } from '../shared/services/common-service/auth.service';
import { WhiteLabelService } from '../shared/services/data-service/white-label.service';
import { MessengerService } from '../shared/services/data-service/messenger.service';

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
  constructor(private loginService: LoginService, private router: Router, private route: ActivatedRoute,
    private authService: AuthService, private whiteLabelService: WhiteLabelService,
    private msgService: MessengerService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(data => {
      // console.log(data, 'isloggedIn value');
    });
    this.authService.logout();
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });
    this.msgService.closeConnection();
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
    this.isLoginLoading = true;
    this.authService.login(loginObj).subscribe(data => {
      this.isLoginLoading = false;
      if (data) {
        if (data.status === 'Success') {
          const token = JSON.parse(data.resultData);
          this.getThemeColor();
          this.loadTheLandingPage();
          this.msgService.startConnection();
        } else {
          this.errorFlag = true;
          this.isLoginLoading = false;
        }
      }
    }, (err) => {
      this.isLoginLoading = false;
    });
  }
  loadTheLandingPage(): void {
    const location = localStorage.getItem('empLocationId');
    if (!Array.isArray(JSON.parse(location))) {
      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      this.router.navigate([`/dashboard`], { relativeTo: this.route });
    } else {
      this.router.navigate([`/location`], { relativeTo: this.route });
    }
  }

  forgotPassword() {
    this.router.navigate([`/forgot-password`], { relativeTo: this.route });
  }

  getThemeColor() {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
        if (res.status === 'Success') {
          const label = JSON.parse(res.resultData);
          console.log(label, 'white');
          this.colorTheme = label.WhiteLabelling.Theme;
          this.whiteLabelDetail = label.WhiteLabelling.WhiteLabel;
          // this.fontName = this.whiteLabelDetail.FontFace;
          // this.themeId = this.whiteLabelDetail.ThemeId;
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
}
