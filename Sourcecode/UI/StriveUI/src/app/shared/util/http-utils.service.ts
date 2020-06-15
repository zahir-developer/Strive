

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from '../util/environment.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpUtilsService {

  constructor(private http: HttpClient) { }

  public post(url: string, data?: any, optional?: any): Observable<any> {
    // EnvironmentService.environment.api.epmsApi
    return this.http.post('http://localhost:55208/' + url, data, optional);
  }
  public get(url: string, optional?: any): Observable<any> {
    return this.http.get(EnvironmentService.environment.api.epmsApi + url, optional);
  }
  public put(url: string, data?: any): Observable<any> {
    return this.http.put(EnvironmentService.environment.api.epmsApi + url, data);
  }
  public delete(url: string, optional?: any): Observable<any> {
    return this.http.delete(EnvironmentService.environment.api.epmsApi  + url, optional);
  }
}
