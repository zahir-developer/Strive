import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CrudOperationService {
  customerdetails = [];
  basicsetupdetails = [];
  productsetupdetails = [];
  servicesetupdetails = [];
  vendorsetupdetails = [];
  constructor() { }
  getCustomerDetails() {
    return this.customerdetails;
  }
  getBasicSetupDetails(){
    return this.basicsetupdetails;
  }
  getProductSetupDetails(){
    return this.productsetupdetails;
  }
  getServiceSetupDetails(){
    return this.servicesetupdetails;
  }
  getVendorSetupDetails(){
    return this.vendorsetupdetails;
  }
}
