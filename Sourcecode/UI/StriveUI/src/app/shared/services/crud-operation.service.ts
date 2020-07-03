import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CrudOperationService {
  customerdetails = [];
  locationsetupdetails = [];
  productsetupdetails = [];
  servicesetupdetails = [];
  vendorsetupdetails = [];
  constructor() { }
  getCustomerDetails() {
    return this.customerdetails;
  }
  getLocationSetupDetails(){
    return this.locationsetupdetails;
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
