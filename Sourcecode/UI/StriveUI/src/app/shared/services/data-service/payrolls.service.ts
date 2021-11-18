import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class PayrollsService {

  constructor(private http: HttpUtilsService) { }

  getPayroll(LocationId, StartDate, EndDate) {
    return this.http.get(`${UrlConfig.payRoll.getPayroll}`, { params: { LocationId , StartDate ,  EndDate } });
  }
  addPayRoll(obj) {
    return this.http.post(`${UrlConfig.payRoll.addPayrollProcess}`, obj);
  }
  updateAdjustment(obj) {
    return this.http.post(`${UrlConfig.payRoll.updateAdjustment}`, obj);
  }
  editRestriction(empId,StartDate,EndDate, LocationId) {
    return this.http.get(`${UrlConfig.payRoll.editRestrict}`, { params: { empId , StartDate ,  EndDate, LocationId } });
  }
}
