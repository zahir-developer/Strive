import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpUtilsService) { }

  getTodayDateScheduleList(JobDate, LocationId, ClientId) {
    return this.http.get(`${UrlConfig.dashboard.getTodayScheduleList}`, { params: { JobDate, LocationId , ClientId}});
  }

  getDetailCount(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getDashBoardCount}`, obj);
  }

  getLocation() {
    return this.http.get(`${UrlConfig.dashboard.getDashboardLocation}`);
  }

  getDashboardStatistics(obj) {
    return this.http.post(`${UrlConfig.dashboard.getDashboardStatistics}`, obj );
  }
}
