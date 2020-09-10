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
}
