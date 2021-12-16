import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  constructor(private http: HttpUtilsService) { }
  
  getAuthModelByMakeId(Id) {
    return this.http.get(`${UrlConfig.Auth.ModelByMakeId}`+  Id ,  { params: { makeId: Id } });
  }  

  getModelByMakeId(Id) {
    return this.http.get(`${UrlConfig.common.ModelByMakeId}`+  Id ,  { params: { makeId: Id } });
  }  
}
