import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WashService {
  public data: BehaviorSubject<string> = new BehaviorSubject('');
  dashBoardData: any;  

  constructor(private http: HttpUtilsService) {
    setTimeout(() => {
      this.getDashBoard();
     }, 1000);
   }
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
  getByBarcode(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getByBarcode}` + id);
  }
  deleteWash(id : number){
    return this.http.get(`${UrlConfig.totalUrl.deleteWash}` + id);
  }
  // getDashboard(obj) {
  //   return this.http.post(`${UrlConfig.totalUrl.getDashBoardCount}`, obj)
  // } 
  getDashBoard() {
    const obj = {
      id: 1,
      date: new Date()
    };
    this.http.post(`${UrlConfig.totalUrl.getDashBoardCount}` ,obj).subscribe((data: any) => {
      const wash = JSON.parse(data.resultData);
      this.dashBoardData = wash.Dashboard;
      this.data.next(this.dashBoardData);
      console.log(this.data);
    });
  }   
}
