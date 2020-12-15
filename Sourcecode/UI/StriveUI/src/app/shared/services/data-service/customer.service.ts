import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpUtilsService) { }

  getDailySalesReport(obj){
    return this.http.post(`${UrlConfig.reports.getDailySalesReport}`, obj);
  }
}
