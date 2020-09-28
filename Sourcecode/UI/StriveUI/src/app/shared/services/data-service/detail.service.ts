import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class DetailService {

  constructor(private http: HttpUtilsService) { }

  addDetail(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addDetail}`, obj);
  }

  getDetailById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getDetailById}` + id);
  }
  
  updateDetail(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateDetail}`, obj);
  }

  getAllBayById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getAllBayById}` + id);
  }

  getScheduleDetailsByDate(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getScheduleDetailsByDate}`, obj);
  }

  deleteDetail(id) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteDetail}`, { params : { id } } );
  }

  getJobType() {
    return this.http.get(`${UrlConfig.totalUrl.getJobType}`);
  }

  getTodayDateScheduleList(JobDate, LocationId) {
    return this.http.get(`${UrlConfig.totalUrl.getTodayDateScheduleList}`, { params: { JobDate, LocationId}});
  }

  getAllEmployeeList() {
    return this.http.get(`${UrlConfig.totalUrl.getEmployees}`);
  }

  getWashTimeByLocationId(id) {
    return this.http.get(`${UrlConfig.totalUrl.getLocationById}`, { params: { id }});
  }

  getPastClientNotesById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getPastClientNotesById}` + id);
  }

  getAllEmplloyeeList() {
    return this.http.get(`${UrlConfig.totalUrl.getEmployees}`);
  }

  saveEmployeeWithService(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveEmployeeWithService}`, obj);
  }

  getJobStatus(code) {
    return this.http.get(`${UrlConfig.totalUrl.getJobStatus}` + code);
  }
}
