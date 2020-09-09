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

  getTodayDateScheduleList(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getTodayDateScheduleList}`, obj);
  }
}
