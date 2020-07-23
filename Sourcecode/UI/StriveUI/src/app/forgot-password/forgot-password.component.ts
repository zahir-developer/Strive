import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ForgotPasswordService } from '../shared/services/common-service/forgot-password.service';
import { MustMatch } from '../shared/Validator/must-match.validator';
import { MessageServiceToastr } from '../shared/services/common-service/message.service';

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
  isOTPScreen: boolean;
  userId = '';
  otpCode = '';
  isForgotPassword: boolean;
  isOtpCode: boolean;
  isResetPassword: boolean;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private forgotPasswordService: ForgotPasswordService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.isEmail = false;
    this.isMobileNumber = false;
    this.submitted = false;
    this.passwordValidation = false;
    this.isOTPScreen = false;
    this.isForgotPassword = true;
    this.isOtpCode = false;
    this.isResetPassword = false;
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
    if (this.isEmail) {
      this.userId = form.value.email;
    } else if (this.isMobileNumber) {
      this.userId = form.value.mobile;
    }

    this.forgotPasswordService.getOTPCode(this.userId).subscribe(res => {
      console.log(res, 'OTP');
      if (res.status === 'Success') {
        this.isOTPScreen = true;
        this.isForgotPassword = false;
        this.isResetPassword = false;
      }
    });

  }

  verifyOtp(form) {
    this.otpCode = form.value.otp;

    this.forgotPasswordService.verifyOtp(this.userId, this.otpCode).subscribe(res => {
      console.log(res, 'res');
      if (res.status === 'Success') {
        this.isOTPScreen = false;
        this.isForgotPassword = false;
        this.isResetPassword = true;
      }
    });
  }

  resendOtp() {
    this.forgotPasswordService.getOTPCode(this.userId).subscribe(res => {
      console.log(res, 'OTP');
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Your OTP has been Resent' });
      }
    });
  }

  resetPassword(newPasswordForm) {
    this.passwordValidation = true;
    if (this.newPasswordForm.invalid) {
      return;
    }

    const finalObj = {
      otp: this.otpCode,
      newPassword: newPasswordForm.value.confirm,
      userId: this.userId
    };

    this.forgotPasswordService.resetPassword(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Password is updated successfully' });
        this.router.navigate([`/login`], { relativeTo: this.route });
      }
    });
  }

  get g() { return this.newPasswordForm.controls; }
}
