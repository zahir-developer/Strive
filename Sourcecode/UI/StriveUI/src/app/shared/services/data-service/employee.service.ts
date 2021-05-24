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
    return this.http.get(`${UrlConfig.employee.getEmployees}`);
  }
  saveEmployee(obj) {
    return this.http.post(`${UrlConfig.employee.saveEmployees}`, obj);
  }
  getEmployeeDetail(empId) {
    return this.http.get(`${UrlConfig.employee.getEmployeeDetail}`, { params : { id: empId }});
  }
  deleteEmployee(id) {
    return this.http.delete(`${UrlConfig.employee.deleteEmployee}` + id);
  }
  getAllRoles() {
    return this.http.get(`${UrlConfig.employee.getAllRoles}`);
  }
  getDropdownValue(code) {
    return this.http.get(`${UrlConfig.common.getDropdownValue}` + code);
  }
  getStateList() {
    return this.http.get(`${UrlConfig.common.stateList}`);
  }
  getCountryList() {
    return this.http.get(`${UrlConfig.common.countryList}`);
  }
  uploadDocument(obj) {
    return this.http.post(`${UrlConfig.document.uploadDocument}`, obj);
  }
  getAllDocument(employeeId) {
    return this.http.get(`${UrlConfig.employee.getAllEmployeeDocument}` + employeeId);
  }
  getDocumentById(documentId, password) {
    return this.http.get(`${UrlConfig.employee.getEmployeeDocumentById}` + documentId +  ',' + password);
  }
  getLocation() {
    return this.http.get(`${UrlConfig.location.getAllLocationName}`);
  }
  getAllCollision(empId) {
    return this.http.get(`${UrlConfig.collision.getAllCollision}` + empId);
  }
  deleteDocument(docId) {
    return this.http.delete(`${UrlConfig.employee.deleteEmployeeDocument}` + docId);
  }
  deleteCollision(collisionId) {
    return this.http.delete(`${UrlConfig.collision.deleteCollision}` , { params : { id: collisionId }});
  }
  getDetailCollision(collisionId) {
    return this.http.get(`${UrlConfig.collision.getDetailCollision}` + collisionId);
  }
  saveCollision(obj) {
    return this.http.post(`${UrlConfig.collision.saveCollision}`, obj);
  }
  searchEmployee(obj) {
    const search = obj.trim() === '' ? '%20' : obj;
    return this.http.get(`${UrlConfig.employee.searchEmployee}` + search);
  }
  updateCollision(obj) {
    return this.http.post(`${UrlConfig.collision.updateCollision}`, obj);
  }
  updateEmployee(obj) {
    return this.http.post(`${UrlConfig.employee.updateEmployee}`, obj);
  }
  getAllClient() {
    return this.http.get(`${UrlConfig.client.getClient}`);
  }
  getVehicleByClientId(id) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleByClientId}`, { params: { id } } );
  }

  getAllEmployeeList(obj) {
    return this.http.post(`${UrlConfig.employee.getAllEmployeeList}`, obj);
  }

  getAllEmployeeName(id) {
    return this.http.get(`${UrlConfig.employee.getAllEmployeeName}`, { params: { id } });
  }

  getEmployeeHourlyRateById(id) {
    return this.http.get(`${UrlConfig.employee.getEmployeeHourlyRateById}`, { params: { id } });
  }

  validateEmail(id) {
    return this.http.get(`${UrlConfig.employee.validateEmail}` + id);
  }
}
