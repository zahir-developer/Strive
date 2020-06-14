import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CrudOperationService {
  customerdetails = [];
  constructor() { }
  getCustomerDetails() {
    return this.customerdetails;
  }
}
