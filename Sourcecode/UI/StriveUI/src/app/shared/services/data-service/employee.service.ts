import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpUtilsService) { }
   getEmployees(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getEmployees}`);
  }
  updateEmployee(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateEmployees}`, obj);
  }
}
