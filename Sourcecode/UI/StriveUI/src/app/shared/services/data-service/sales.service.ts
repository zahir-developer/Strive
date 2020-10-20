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
  getItemByTicketNumber(ticketNo): Observable<any> {
   return this.http.get(`${UrlConfig.totalUrl.getItemByTicketNumber}`, {params: {ticketNumber: ticketNo}});
 }
 deleteItemById(deleteItem){
  return this.http.delete(`${UrlConfig.totalUrl.deleteItemById}`, {params: {ItemId: deleteItem.ItemId, isJobItem: deleteItem.IsJobItem}});
 }
 addItem(addObj) {
  return this.http.post(`${UrlConfig.totalUrl.addItem}`, addObj);
 }
 getService() {
   return this.http.get(`${UrlConfig.totalUrl.getService}`);
 }
 getTicketNumber() {
  return this.http.get(`${UrlConfig.totalUrl.getTicketNumberforItem}`);
 }
 updateListItem(updateObj) {
   return this.http.post(`${UrlConfig.totalUrl.updateListItem}`, updateObj);
 }
 updateItem(updateObj) {
  return this.http.post(`${UrlConfig.totalUrl.updateItem}`, updateObj);
 }
 addPayemnt(paymentObj) {
  return this.http.post(`${UrlConfig.totalUrl.addPayment}`, paymentObj);
 }
 deleteJob(ticketNo) {
  return this.http.delete(`${UrlConfig.totalUrl.deleteJob}`, {params: {TicketNumber: ticketNo}});
 }
 rollback(ticketNo) {
  return this.http.delete(`${UrlConfig.totalUrl.rollbackTransaction}`, {params: {TicketNumber: ticketNo}});
 }
 getServiceAndProduct() {
  return this.http.get(`${UrlConfig.totalUrl.getServiceAndProduct}`);
 }
 updateProductItem(updateObj) {
  return this.http.post(`${UrlConfig.totalUrl.updateProductObj}`, updateObj);
 }
 getPaymentStatus(code) {
  return this.http.get(`${UrlConfig.totalUrl.getPaymentStatus}` + code);
}
}
