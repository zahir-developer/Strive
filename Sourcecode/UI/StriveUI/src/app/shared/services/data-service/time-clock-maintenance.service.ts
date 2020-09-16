import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class TimeClockMaintenanceService {

  constructor(private http: HttpUtilsService) { }

  getTimeClockEmployeeDetails(obj)
  {
    return this.http.post(`${UrlConfig.totalUrl.getTimeClockEmployeeDetails}`, obj)
  }

  getTimeClockWeekDetails(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getTimeClockWeekDetails}`, obj);
  }

  saveTimeClock(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveTimeClock}`, obj);
  }

  getAllRoles(type) {
    return this.http.get(`${UrlConfig.totalUrl.getDropdownValue}` + type);
  }

  deleteTimeClockEmployee(obj)
  {
    return this.http.delete(`${UrlConfig.totalUrl.deleteTimeClockEmployee}`, { params: { EmployeeId : obj.employeeId , LocationId : obj.locationId }})
  }
}
