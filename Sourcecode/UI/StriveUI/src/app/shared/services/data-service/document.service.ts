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

  updateDocument(obj) {
    return this.http.post(`${UrlConfig.document.updateDocument}`, obj);
  }

  getDocument(id,type) {
    return this.http.get(`${UrlConfig.document.getDocument}` + id + '/' + type);
  }
  getAllDocument(id) {
    return this.http.get(`${UrlConfig.document.getAllDocument}` + id );
  }

  deleteDocumentById(id,type) {
    return this.http.delete(`${UrlConfig.document.deleteDocumentById}` + id + '/' + type );
  }

  deleteDocument(id,type) {
    return this.http.delete(`${UrlConfig.document.deleteDocument}` + id + '/' + type );
  }

  getDocumentById(id, type) {
    return this.http.get(`${UrlConfig.document.getDocumentById}` + id  + '/' + type);
  }

}
