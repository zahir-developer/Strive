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
    return this.http.get(`${UrlConfig.Checklist.getCheckList}`);
  }
  addCheckListSetup(obj) {
    
    return this.http.post(`${UrlConfig.Checklist.addCheckList}`,obj);
  }
  deleteCheckListSetup(id) {
    
    return this.http.delete(`${UrlConfig.Checklist.DeleteCheckList}`, { params: { id: id } });
  }
  getById(id) {
    
    return this.http.get(`${UrlConfig.Checklist.getById}`, { params: { id: id } });
  }
}
