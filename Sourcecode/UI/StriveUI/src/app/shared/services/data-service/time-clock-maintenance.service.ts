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
    return this.http.post(`${UrlConfig.timeClock.getTimeClockEmployeeDetails}`, obj)
  }

  getTimeClockWeekDetails(obj) {
    return this.http.post(`${UrlConfig.timeClock.getTimeClockWeekDetails}`, obj);
  }

  saveTimeClock(obj) {
    return this.http.post(`${UrlConfig.timeClock.saveTimeClock}`, obj);
  }

  getAllRoles() {
    return this.http.get(`${UrlConfig.employee.getAllRoles}`);
  }

  deleteTimeClockEmployee(obj)
  {
    return this.http.delete(`${UrlConfig.timeClock.deleteTimeClockEmployee}`, { params: { EmployeeId : obj.employeeId , LocationId : obj.locationId }})
  }

  getEmployeeList() {
    return this.http.get(`${UrlConfig.employee.getEmployees}`);
  }
  getRolesbyEmployeeId(id) {
    return this.http.get(`${UrlConfig.employee.getRoleByEmpId}`, { params: { id: id } });


  }
}
