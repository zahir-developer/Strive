import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpUtilsService) { }
   getEmployees(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getEmployees}`);
  }
  updateEmployee(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateEmployees}`, obj);
  }
  getEmployeeDetail(id) {
    return this.http.get(`${UrlConfig.totalUrl.getEmployeeDetail}` + id);
  }
  deleteEmployee(id) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteEmployee}` + id);
  }
  getAllRoles() {
    return this.http.get(`${UrlConfig.totalUrl.getAllRoles}`);
  }
  getDropdownValue(code) {
    return this.http.get(`${UrlConfig.totalUrl.getDropdownValue}` + code);
  }
  getStateList() {
    return this.http.get(`${UrlConfig.totalUrl.stateList}`);
  }
  getCountryList() {
    return this.http.get(`${UrlConfig.totalUrl.countryList}`);
  }
  getAllCollision() {
    return this.http.get(`${UrlConfig.totalUrl.getAllCollision}`);
  }
  getCollisionById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getCollisionById}` + id);
  }
  deleteCollision(id) {
    return this.http.get(`${UrlConfig.totalUrl.deleteCollision}` + id);
  }
  saveCollision(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveCollision}`, obj);
  }
}
