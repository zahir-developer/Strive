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
  getSchedule(date): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getSchedule}` + date);
  }
  deleteSchedule(id) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteSchedule}` + id);
  }
  getScheduleById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getScheduleById}` + id);
  }
}

