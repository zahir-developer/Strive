import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class CashRegisterService {

  constructor(private http: HttpUtilsService) { }
  getCashRegisterByDate(type: string, locId: number, date: string) {
    return this.http.get(`${UrlConfig.totalUrl.getCashRegister}`,
    { params: { cashRegisterType: type, locationId: locId, dateTime: date } });
  }

  saveCashRegister(obj, cashRegisterType: string) {
    return this.http.post(`${UrlConfig.totalUrl.saveCashRegister}`, obj, cashRegisterType);
  }
}
