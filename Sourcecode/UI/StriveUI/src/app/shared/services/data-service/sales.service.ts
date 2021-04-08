import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private http: HttpUtilsService) { }
  getItemByTicketNumber(salesObj) {
   return this.http.post(`${UrlConfig.sales.getItemByTicketNumber}`, salesObj);
 }
 deleteItemById(deleteItem){
  return this.http.delete(`${UrlConfig.sales.deleteItemById}`, {params: {ItemId: deleteItem.ItemId, isJobItem: deleteItem.IsJobItem}});
 }
 addItem(addObj) {
  return this.http.post(`${UrlConfig.sales.addItem}`, addObj);
 }
 getService() {
   return this.http.get(`${UrlConfig.ServiceSetup.getService}`);
 }
 getTicketNumber() {
  const locationId = localStorage.getItem('empLocationId');

  return this.http.get(`${UrlConfig.common.getTicketNumber}`+ locationId);
 }
 updateListItem(updateObj) {
   return this.http.post(`${UrlConfig.sales.updateListItem}`, updateObj);
 }
 updateItem(updateObj) {
  return this.http.post(`${UrlConfig.sales.updateItem}`, updateObj);
 }
 addPayemnt(paymentObj) {
  return this.http.post(`${UrlConfig.sales.addPayment}`, paymentObj);
 }
 deleteJob(ticketNo) {
  return this.http.delete(`${UrlConfig.sales.deleteJob}`, {params: {TicketNumber: ticketNo}});
 }
 rollback(rollBack) {
  return this.http.post(`${UrlConfig.sales.rollbackTransaction}`, rollBack);
 }
 getServiceAndProduct(locID) {
  return this.http.get(`${UrlConfig.sales.getServiceAndProduct}` + locID);
 }
 updateProductItem(updateObj) {
  return this.http.post(`${UrlConfig.sales.updateProductObj}`, updateObj);
 }
 getAccountDetails(obj) {
  return this.http.post(`${UrlConfig.sales.getAccountDetails}`, obj);
 } 
 updateAccountBalance(obj) {
  return this.http.post(`${UrlConfig.client.updateAccountBalance}`, obj);
 }
 getPaymentStatus(code) {
  return this.http.get(`${UrlConfig.common.getPaymentStatus}` + code);
}
}
