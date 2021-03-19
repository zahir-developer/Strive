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
    return this.http.post(`${UrlConfig.details.addDetail}`, obj);
  }

  getDetailById(id) {
    return this.http.get(`${UrlConfig.details.getDetailById}` + id);
  }
  
  updateDetail(obj) {
    return this.http.post(`${UrlConfig.details.updateDetail}`, obj);
  }

  getAllBayById(id) {
    return this.http.get(`${UrlConfig.details.getAllBayById}` + id);
  }

  getScheduleDetailsByDate(obj) {
    return this.http.post(`${UrlConfig.details.getScheduleDetailsByDate}`, obj);
  }

  deleteDetail(id) {
    return this.http.delete(`${UrlConfig.details.deleteDetail}`, { params : { id } } );
  }

  getJobType() {
    return this.http.get(`${UrlConfig.details.getJobType}`);
  }

  getTodayDateScheduleList(JobDate, LocationId, ClientId) {
    return this.http.get(`${UrlConfig.details.getTodayDateScheduleList}`, { params: { JobDate, LocationId, ClientId}});
  }

  getAllEmployeeList() {
    return this.http.get(`${UrlConfig.employee.getEmployees}`);
  }

  getWashTimeByLocationId(washTimeDto) {
    return this.http.post(`${UrlConfig.washes.getWashTimeByLocationId}`, washTimeDto);
  }

  getPastClientNotesById(id) {
    return this.http.get(`${UrlConfig.details.getPastClientNotesById}` + id);
  }

  getAllEmplloyeeList() {
    return this.http.get(`${UrlConfig.employee.getEmployees}`);
  }

  saveEmployeeWithService(obj) {
    return this.http.post(`${UrlConfig.details.saveEmployeeWithService}`, obj);
  }

  getJobStatus(code) {
    return this.http.get(`${UrlConfig.common.getJobStatus}` + code);
  }

  getDetailCount(obj) {
    return this.http.post(`${UrlConfig.washes.getDashBoardCount}`, obj);
  }

  getDetailScheduleStatus(LocationId, date) {
    return this.http.get(`${UrlConfig.details.getDetailScheduleStatus}`, { params: { LocationId , Date: date } });
  }

  getClockedInDetailer(obj) {
    return this.http.get(`${UrlConfig.timeClock.getClockedInDetailer}` , obj);
  }

}
