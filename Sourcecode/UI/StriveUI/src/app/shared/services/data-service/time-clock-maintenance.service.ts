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

  getAllRoles() {
    return this.http.get(`${UrlConfig.totalUrl.getAllRoles}`);
  }

  deleteTimeClockEmployee(obj)
  {
    return this.http.delete(`${UrlConfig.totalUrl.deleteTimeClockEmployee}`, { params: { EmployeeId : obj.employeeId , LocationId : obj.locationId }})
  }

  getEmployeeList() {
    return this.http.get(`${UrlConfig.totalUrl.getEmployees}`);
  }
  getRolesbyEmployeeId() {
    const id = + localStorage.getItem('empId')
    return this.http.get(`${UrlConfig.totalUrl.getRoleByEmpId}`, { params: { id: id } });


  }
}
