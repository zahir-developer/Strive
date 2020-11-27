import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpUtilsService) { }

  addDocument(obj) {
    return this.http.post(`${UrlConfig.document.addDocument}`, obj);
  }

  getDocument(id,type) {
    return this.http.get(`${UrlConfig.document.getDocument}` + id + '/' + type);
  }

  deleteDocument(id,type) {
    return this.http.delete(`${UrlConfig.document.deleteDocument}` + id + '/' + type );
  }

}
