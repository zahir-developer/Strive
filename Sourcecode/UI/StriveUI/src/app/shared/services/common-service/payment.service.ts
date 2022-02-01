import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpUtilsService) { }
  getPaymentList() {
    return this.http.get(`${UrlConfig.common.getPaymentGateway}`);
  }  
  updatePayment(paymentObj): Observable<any> {
    return this.http.post(`${UrlConfig.common.addPaymentGateway}`, paymentObj );
  }

}
