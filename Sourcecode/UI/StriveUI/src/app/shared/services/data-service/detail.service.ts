import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class DetailService {

  constructor(private http: HttpUtilsService) { }

  addDetail(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addDetail}`, obj);
  }

  getDetailById(id) {
    return this.http.get(`${UrlConfig.totalUrl.getDetailById}` + id);
  }
}
