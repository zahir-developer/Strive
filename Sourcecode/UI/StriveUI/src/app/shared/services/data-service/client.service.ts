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
  updateClient(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateClient}`, obj);
  }
  deleteClient(id: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteClient}` + id);
  }
  getClientById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getClientById}` + id);
  }
}