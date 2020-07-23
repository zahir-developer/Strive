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
    return this.http.put(`${UrlConfig.totalUrl.getOtpCode}`  + obj);
  }
}
