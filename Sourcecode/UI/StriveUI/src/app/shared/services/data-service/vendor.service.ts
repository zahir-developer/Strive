import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class VendorService {

  constructor(private http: HttpUtilsService) { }
  getVendor(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getVendor}`);
  }
  saveVendor(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveVendor}`, obj);
  }
  deleteVendor(id: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteVendor}` + id);
  }
  getVendorById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getVendorById}` + id);
  }
  updateVendor(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateVendor}`, obj);
  }
}