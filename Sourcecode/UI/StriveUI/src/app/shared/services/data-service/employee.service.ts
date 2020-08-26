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
  saveEmployee(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveEmployees}`, obj);
  }
  getEmployeeDetail(empId) {
    return this.http.get(`${UrlConfig.totalUrl.getEmployeeDetail}`, { params : { id: empId }});
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
  uploadDocument(obj) {
    return this.http.post(`${UrlConfig.totalUrl.uploadDocument}`, obj);
  }
  getAllDocument(employeeId) {
    return this.http.get(`${UrlConfig.totalUrl.getAllDocument}` + employeeId);
  }
  getDocumentById(documentId, password) {
    return this.http.get(`${UrlConfig.totalUrl.getDocumentById}` + documentId +  ',' + password);
  }
  getLocation() {
    return this.http.get(`${UrlConfig.totalUrl.getLocation}`);
  }
  getAllCollision(empId) {
    return this.http.get(`${UrlConfig.totalUrl.getAllCollision}` + empId);
  }
  deleteDocument(docId) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteDocument}` + docId);
  }
  deleteCollision(collisionId) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteCollision}` , { params : { id: collisionId }});
  }
  getDetailCollision(collisionId) {
    return this.http.get(`${UrlConfig.totalUrl.getDetailCollision}` + collisionId);
  }
  saveCollision(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveCollision}`, obj);
  }
  searchEmployee(obj) {
    const search = obj.trim() === '' ? '%20' : obj;
    return this.http.get(`${UrlConfig.totalUrl.searchEmployee}` + search);
  }
  updateCollision(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateCollision}`, obj);
  }
  updateEmployee(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateEmployee}`, obj);
  }
  getAllClient() {
    return this.http.get(`${UrlConfig.totalUrl.getClient}`);
  }
  getVehicleByClientId(id) {
    return this.http.get(`${UrlConfig.totalUrl.getVehicleByClientId}`, { params : { id }});
  }
}
