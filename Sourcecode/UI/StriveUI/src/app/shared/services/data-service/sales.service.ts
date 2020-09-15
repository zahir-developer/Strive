import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private http: HttpUtilsService) { }
  getItemByTicketNumber(ticketNo): Observable<any> {
   return this.http.get(`${UrlConfig.totalUrl.getItemByTicketNumber}`, {params: {ticketNumber: ticketNo}});
 }
 deleteItemById(id){
  return this.http.delete(`${UrlConfig.totalUrl.deleteItemById}`, {params: {jobId: id}});
 }
 addItem(addObj) {
  return this.http.post(`${UrlConfig.totalUrl.addItem}`, addObj);
 }
 getService() {
   return this.http.get(`${UrlConfig.totalUrl.getServicewithPrice}`);
 }
 getTicketNumber() {
  return this.http.get(`${UrlConfig.totalUrl.getTicketNumberforItem}`);
 }
 updateListItem(updateObj) {
   return this.http.post(`${UrlConfig.totalUrl.updateListItem}`, updateObj);
 }
 updateItem(updateObj) {
  return this.http.put(`${UrlConfig.totalUrl.updateItem}`,
  {params: {JobItemId: updateObj.jobid, Quantity: +updateObj.quantity, Price: +updateObj.price}});
 }
 addPayemnt(paymentObj) {
  return this.http.post(`${UrlConfig.totalUrl.addPayment}`, paymentObj);
 }
 deleteTransaction(ticketNo) {
  return this.http.delete(`${UrlConfig.totalUrl.deleteTransaction}`, {params: {TicketNumber: ticketNo}});
 }
}
