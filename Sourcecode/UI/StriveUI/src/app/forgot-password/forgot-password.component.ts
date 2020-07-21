import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ForgotPasswordService } from '../shared/services/common-service/forgot-password.service';
import { MustMatch } from '../shared/Validator/must-match.validator';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  isEmail: boolean;
  isMobileNumber: boolean;
  forgotPasswordForm: FormGroup;
  submitted: boolean;
  otpForm: FormGroup;
  newPasswordForm: FormGroup;
  passwordValidation: boolean;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private forgotPasswordService: ForgotPasswordService) { }

  ngOnInit(): void {
    this.isEmail = false;
    this.isMobileNumber = false;
    this.submitted = false;
    this.passwordValidation = false;
    this.forgotPasswordForm = this.formBuilder.group({
      email: [''],
      mobile: ['']
    });
    this.otpForm = this.formBuilder.group({
      otp: ['']
    });
    this.newPasswordForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirm: ['', Validators.required]
    }, {
      validator: MustMatch('password', 'confirm')
    });
  }

  backToLoginPage() {
    this.router.navigate([`/login`], { relativeTo: this.route });
  }

  selectOption(event) {
    console.log(event);
    this.submitted = false;
    if (event.target.value === 'email') {
      this.isEmail = true;
      this.isMobileNumber = false;
      this.forgotPasswordForm.get('email').setValidators([Validators.required]);
      this.forgotPasswordForm.get('mobile').clearValidators();
    } else if (event.target.value === 'mobile') {
      this.isMobileNumber = true;
      this.isEmail = false;
      this.forgotPasswordForm.get('mobile').setValidators([Validators.required]);
      this.forgotPasswordForm.get('email').clearValidators();
    } else {
      this.isEmail = false;
      this.isMobileNumber = false;
    }

  }

  get f() { return this.forgotPasswordForm.controls; }

  submit(form) {
    console.log(form);
    if (this.forgotPasswordForm.invalid) {
      this.submitted = true;
      return;
    }
    let userId = '';
    if (this.isEmail) {
      userId = form.value.email;
    } else if (this.isMobileNumber) {
      userId = form.value.mobile;
    }
    
    this.forgotPasswordService.getOTPCode(userId).subscribe( res => {
      console.log(res, 'OTP');
    });

  }

  resetPassword(newPasswordForm) {
    this.passwordValidation = true;
    if (this.newPasswordForm.invalid) {
      return;
    }
  }

  get g() { return this.newPasswordForm.controls; }
}
