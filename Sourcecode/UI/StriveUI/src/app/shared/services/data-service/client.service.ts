import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpUtilsService) { }
  getClient(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getClient}`,obj);
  }
  ClientSameName(obj) {
    return this.http.post(`${UrlConfig.totalUrl.sameClientName}`, obj);
  }
  addClient(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addClient}`, obj);
  }
  updateClient(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateClient}`, obj);
  }
  deleteClient(id: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteClient}` + id);
  }
  getClientById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getClientById}` + id);
  }
  ClientSearch(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getClientByName}`, obj);
  }
  getClientScore(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClientScore}`);
  }
  getStatementByClientId(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getStatementByClientId}`,obj);
  }
  getHistoryByClientId(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getHistoryByClientId}` ,obj);
  }
}