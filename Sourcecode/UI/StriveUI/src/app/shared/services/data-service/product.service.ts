import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpUtilsService) { }
  getProduct(obj) {
    return this.http.post(`${UrlConfig.product.getProduct}`, obj);
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
  getProductDetailById(id: number) {
    return this.http.get(`${UrlConfig.product.getProductDetailById}`, { params: { productId: id } });
  }
  getVendor(): Observable<any> {
    return this.http.get(`${UrlConfig.vendor.getALLVendorName}`);
  }
  getAllLocationName() {
    return this.http.get(`${UrlConfig.location.getAllLocationName}`);
  }
}
