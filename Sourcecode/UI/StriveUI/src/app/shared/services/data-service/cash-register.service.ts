import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class CashRegisterService {

  constructor(private http: HttpUtilsService) { }
   getCashRegisterByDate(date : any){
    return this.http.get(`${UrlConfig.totalUrl.getCashRegister}`+ date);
  }
  
}
