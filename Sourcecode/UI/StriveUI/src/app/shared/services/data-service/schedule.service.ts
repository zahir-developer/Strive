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
    return this.http.post(`${UrlConfig.schedule.addSchedule}`, scheduleObj);
  }
  getSchedule(getScheduleObj): Observable<any> {
    return this.http.post(`${UrlConfig.schedule.getSchedule}`, getScheduleObj);
  }
  deleteSchedule(scheduleId) {
    return this.http.delete(`${UrlConfig.schedule.deleteSchedule}` , {params: {id: scheduleId}});
  }
  getScheduleById(id) {
    return this.http.get(`${UrlConfig.schedule.getScheduleById}`, {params: {scheduleId: id}});
  }
}

