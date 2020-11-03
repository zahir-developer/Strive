import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private http: HttpUtilsService) { }
  getMonthlySalesReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getMonthlySalesReport}`, obj);
  }
  getCustomerSummaryReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getCustomerSummaryReport}`, obj);
  }
  getCustomerMonthlyDetailReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getCustomerMonthlyDetailReport}`, obj);
  }
}
