import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private http: HttpUtilsService) { }
   saveSchedule(scheduleObj): Observable<any> {
    return this.http.post(`${UrlConfig.totalUrl.addSchedule}`, scheduleObj);
  }
  getSchedule(fromDate, endDate): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getSchedule}`, {params: {StartDate: fromDate, EndDate: endDate}});
  }
  deleteSchedule(scheduleId) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteSchedule}` , {params: {id: scheduleId}});
  }
  getScheduleById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getScheduleById}`, {params: {scheduleId: id}});
  }
}

