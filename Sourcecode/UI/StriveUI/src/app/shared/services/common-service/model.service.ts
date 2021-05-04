import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  constructor(private http: HttpUtilsService) { }
  

  getModelByMakeId(Id) {
    return this.http.get(`${UrlConfig.common.modelByMakeId}`+  Id ,  { params: { makeId: Id } });
  }
}
