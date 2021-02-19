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
    return this.http.get(`${UrlConfig.product.getProduct}`);
  }
  addProduct(obj) {
    return this.http.post(`${UrlConfig.product.addProduct}`, obj);
  }
  updateProduct(obj) {
    return this.http.post(`${UrlConfig.product.updateProduct}`, obj);
  }
  deleteProduct(id: number) {
    return this.http.delete(`${UrlConfig.product.deleteProduct}`, { params: { productId: id } });
  }
  getProductById(id: number) {
    return this.http.get(`${UrlConfig.product.getProductById}`, { params: { productId: id } });
  }
  getVendor(): Observable<any> {
    return this.http.get(`${UrlConfig.vendor.getALLVendorName}`);
  }
  ProductSearch(obj) {
    return this.http.post(`${UrlConfig.product.getProductSearch}`, obj);
  }
  getAllLocationName() {
    return this.http.get(`${UrlConfig.location.getAllLocationName}`);
  }
}
