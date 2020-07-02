import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService } from '../shared/services/login.service';
import {Router, ActivatedRoute} from '@angular/router'

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

  constructor(private loginService: LoginService, private router: Router, private route: ActivatedRoute) { }

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
    this.loginService.userAuthentication(loginObj).subscribe(data => {
      if (data) {
        if (data.status === 'Success') {
          this.display = true;
          const token = JSON.parse(data.resultData);
          this.loginDetail = token.EmployeeDetails.FirstName + ' - ' + token.EmployeeDetails.EmployeeDetail.EmployeeCode + ' - ' + 
          token.EmployeeDetails.EmployeeRole[0].RoleName;
          localStorage.setItem('authorizationToken', token.Token);
          localStorage.setItem('refreshToken', token.RefreshToken);
          // this.loaddTheLandingPage();
        } else {
          this.errorFlag = true;
        }
      }
    });
  }
  loadTheLandingPage(): void {
      this.router.navigate([`/admin/employees`], { relativeTo: this.route });
    }
}
