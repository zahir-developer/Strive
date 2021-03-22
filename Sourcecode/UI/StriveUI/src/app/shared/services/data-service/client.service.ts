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
    return this.http.post(`${UrlConfig.client.getClient}`,obj);
  }
  getClientName(): Observable<any> {
    return this.http.get(`${UrlConfig.client.getClientName}`);
  }
  ClientSameName(obj) {
    return this.http.post(`${UrlConfig.client.sameClientName}`, obj);
  }
  addClient(obj) {
    return this.http.post(`${UrlConfig.client.addClient}`, obj);
  }
  updateClient(obj) {
    return this.http.post(`${UrlConfig.client.updateClient}`, obj);
  }
  deleteClient(id: number) {
    return this.http.delete(`${UrlConfig.client.deleteClient}` + id);
  }
  ClientEmailCheck(email) {
    return this.http.get(`${UrlConfig.client.clientEmailCheck}` , { params: { email: email } });
  }

  getClientById(id: number) {
    return this.http.get(`${UrlConfig.client.getClientById}` + id);
  }
  ClientSearch(obj) {
    return this.http.post(`${UrlConfig.client.getClientByName}`, obj);
  }
  getClientScore(): Observable<any> {
    return this.http.get(`${UrlConfig.client.getClientScore}`);
  }
  getStatementByClientId(id) {
    return this.http.get(`${UrlConfig.client.getStatementByClientId}` + id);
  }
  getHistoryByClientId(id) {
    return this.http.get(`${UrlConfig.client.getHistoryByClientId}` + id);
  }
}