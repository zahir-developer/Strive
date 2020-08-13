import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WashService {  
public data: BehaviorSubject<string> = new BehaviorSubject('');
  dashboardData: any;

  constructor(private http: HttpUtilsService) { }
  getAllWashes(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getAllWash}`);
  }
  updateWashes(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateWash}`, obj);
  }
  addWashes(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addWash}`, obj);
  }
  getWashById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getWashById}` + id);
  }
  deleteWash(id : number){
    return this.http.get(`${UrlConfig.totalUrl.deleteWash}` + id);
  }
  getDashboard() {
    const locationId = 1;
    this.http.get(`${UrlConfig.totalUrl.getDashBoardCount}` + locationId).subscribe((data: any) => {
      this.dashboardData = data.currentWeather;
      this.data.next(this.dashboardData);
    });
  }
}
