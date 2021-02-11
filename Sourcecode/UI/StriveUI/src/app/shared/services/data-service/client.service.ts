import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpUtilsService) { }
  getClient(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClient}`);
  }
  getClientName(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClientName}`);
  }
  ClientEmailCheck(email) {
    return this.http.get(`${UrlConfig.totalUrl.clientEmailCheck}` , { params: { email: email } });
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
  getStatementByClientId(id) {
    return this.http.get(`${UrlConfig.totalUrl.getStatementByClientId}` + id);
  }
  getHistoryByClientId(id) {
    return this.http.get(`${UrlConfig.totalUrl.getHistoryByClientId}` + id);
  }
}