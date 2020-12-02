import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CheckListService {

  constructor(private http : HttpUtilsService) { }
  getCheckListSetup(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getCheckList}`);
  }
  addCheckListSetup(obj) {
    
    return this.http.post(`${UrlConfig.totalUrl.addCheckList}`,obj);
  }
  deleteCheckListSetup(id) {
    
    return this.http.delete(`${UrlConfig.totalUrl.DeleteCheckList}`, { params: { id: id } });
  }
}
