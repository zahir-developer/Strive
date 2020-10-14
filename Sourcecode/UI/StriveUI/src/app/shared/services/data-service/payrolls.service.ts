import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class PayrollsService {

  constructor(private http: HttpUtilsService) { }

  getPayroll(LocationId, StartDate, EndDate) {
    return this.http.get(`${UrlConfig.totalUrl.getPayroll}`, { params: { LocationId , StartDate ,  EndDate } });
  }
}
