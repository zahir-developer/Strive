import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ForgotPasswordService {

  constructor(private http: HttpUtilsService) { }

  getOTPCode(obj) {
    return this.http.put(`${UrlConfig.Auth.getOtpCode}`  + obj);
  }

  verifyOtp(userId, otp) {
    return this.http.get(`${UrlConfig.Auth.verifyOtp}` + userId + '/' + otp);
  }

  resetPassword(obj) {
    return this.http.post(`${UrlConfig.Auth.resetPassword}` , obj);
  }
}
