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
  addProduct(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addProduct}`, obj);
  }
  updateProduct(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateProduct}`, obj);
  }
  deleteProduct(id: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteProduct}`, { params: { productId: id } });
  }
  getProductById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getProductById}`, { params: { productId: id } });
  }
  getVendor(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getALLVendorName}`);
  }
  ProductSearch(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getProductSearch}`, obj);
  }
  getAllLocationName() {
    return this.http.get(`${UrlConfig.totalUrl.getAllLocationName}`);
  }
}
