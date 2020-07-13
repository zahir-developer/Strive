import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class CashRegisterService {

  constructor(private http: HttpUtilsService) { }
   getCashRegisterByDate(cashRegisterType : string , locationId : number , date : string){
    return this.http.get(`${UrlConfig.totalUrl.getCashRegister}`+cashRegisterType + '&' + locationId + '&' + date);
  }
  saveCashRegister(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveCashRegister}`, obj);
  }
}
