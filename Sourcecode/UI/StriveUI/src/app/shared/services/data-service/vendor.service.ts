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
    return this.http.get(`${UrlConfig.vendor.getVendor}`);
  }
  saveVendor(obj) {
    return this.http.post(`${UrlConfig.vendor.saveVendor}`, obj);
  }
  deleteVendor(id: number) {
    return this.http.delete(`${UrlConfig.vendor.deleteVendor}` + id);
  }
  getVendorById(id: number) {
    return this.http.get(`${UrlConfig.vendor.getVendorById}` + id);
  }
  updateVendor(obj) {
    return this.http.post(`${UrlConfig.vendor.updateVendor}`, obj);
  }
  VendorSearch(obj) {
    return this.http.post(`${UrlConfig.vendor.getVendorSearch}`, obj);
  }v
}