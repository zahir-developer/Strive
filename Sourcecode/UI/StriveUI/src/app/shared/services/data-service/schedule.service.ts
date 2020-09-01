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
  getSchedule(getScheduleObj): Observable<any> {
    return this.http.post(`${UrlConfig.totalUrl.getSchedule}`, getScheduleObj);
  }
  deleteSchedule(scheduleId) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteSchedule}` , {params: {id: scheduleId}});
  }
  getScheduleById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getScheduleById}`, {params: {scheduleId: id}});
  }
}

