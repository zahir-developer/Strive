import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CrudOperationService {
  customerdetails = [];
  basicsetupdetails = [];
  constructor() { }
  getCustomerDetails() {
    return this.customerdetails;
  }
  getBasicSetupDetails(){
    return this.basicsetupdetails;
  }
}
