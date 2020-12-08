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
  getDailyTipReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getDailyTipReport}`, obj);
  }
  getMonthlyTipReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getMonthlyTipReport}`, obj);
  }
  getDailyStatusReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getDailyStatusReport}`, obj);
  }
  getDailyStatusWashReport(obj): Observable<any> {
    return this.http.post(`${UrlConfig.reports.getDailyStatusWashReport}`, obj);
  }
  getMonthlyDailyTipReport(obj) {
    return this.http.post(`${UrlConfig.reports.getMonthlyDailyTipReport}`, obj);
  }
  getDailyStatusDetailInfo(obj) {
    return this.http.post(`${UrlConfig.reports.getDetailStatusInfo}`, obj);
  }
  getDailyClockDetail(obj) {
    return this.http.post(`${UrlConfig.reports.getDailyClockDetail}`, obj);
  }
  getCashRegisterByDate(type: string, locId: number, date: string) {
    return this.http.get(`${UrlConfig.totalUrl.getCashRegister}`,
    { params: { cashRegisterType: type, locationId: locId, dateTime: date } });
  }
  getMonthlyMoneyOwnedReport(Date, LocationId) {
    return this.http.get(`${UrlConfig.reports.getMonthlyMoneyOwnedReport}`, {  params : { Date , LocationId } });
  }
  getEodSaleReport(obj) {
    return this.http.post(`${UrlConfig.reports.getEodSaleReport}`, obj);
  }
  getTimeClockEmpHoursDetail(obj) {
    return this.http.post(`${UrlConfig.reports.getTimeClockEmpHoursDetail}`, obj);
  }
  getDailySalesReport(obj){
    return this.http.post(`${UrlConfig.reports.getDailySalesReport}`, obj);
  }

  getHourlyWashReport(obj) {
    return this.http.post(`${UrlConfig.reports.getHourlyWashReport}`, obj);
  }
  getEODexcelReport(obj){
    return this.http.post(`${UrlConfig.reports.EODExcelReport}`, obj);
  }
  getDailyStatusExcelReport(obj){
    return this.http.post(`${UrlConfig.reports.dailyStatusExcelReport}`, obj);
  }
}
