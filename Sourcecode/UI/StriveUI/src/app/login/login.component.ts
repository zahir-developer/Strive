import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService } from '../shared/services/login.service';
import { Router, ActivatedRoute } from '@angular/router'
import { AuthService } from '../shared/services/common-service/auth.service';

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
  constructor(private loginService: LoginService, private router: Router, private route: ActivatedRoute,
              private authService: AuthService) { }

  ngOnInit(): void {

    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('')
    });

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
          // this.display = true;
          const token = JSON.parse(data.resultData);
          this.loginDetail = token.EmployeeDetails.FirstName + ' - ' + token.EmployeeDetails.EmployeeDetail.EmployeeCode + ' - ' +
            token.EmployeeDetails.EmployeeRole[0].RoleName;
          this.loadTheLandingPage();
        } else {
          this.errorFlag = true;
          this.isLoginLoading = false;
        }
      }
    }, (err) => {
      // this.isLoginLoading = false;
    });
  }
  loadTheLandingPage(): void {
    this.router.navigate([`/admin/setup`], { relativeTo: this.route });
  }
}
