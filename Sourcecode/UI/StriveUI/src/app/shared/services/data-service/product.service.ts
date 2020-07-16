import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpUtilsService) { }
   getProduct(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getProduct}`);
  }
  updateProduct(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateProduct}`, obj);
  } 
  deleteProduct(id : number){
    return this.http.delete(`${UrlConfig.totalUrl.deleteProduct}`+ id);
  }
  getProductById(id : number){
    return this.http.get(`${UrlConfig.totalUrl.getProductById}`+ id);
  } 
  getVendor(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getVendor}`);
  }
}