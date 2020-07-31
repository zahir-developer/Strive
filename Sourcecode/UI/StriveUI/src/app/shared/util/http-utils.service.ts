

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { EnvironmentService } from '../util/environment.service';
import { HttpClient } from '@angular/common/http';
import { environment  } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpUtilsService {

  constructor(private http: HttpClient) { }

  public post(url: string, data?: any, optional?: any): Observable<any> {
    // EnvironmentService.environment.api.epmsApi
    return this.http.post(environment.api.striveUrl + url, data, optional);
  }
  public get(url: string, optional?: any): Observable<any> {
    // EnvironmentService.environment.api.epmsApi
    return this.http.get( environment.api.striveUrl + url, optional);
  }
  public put(url: string, data?: any): Observable<any> {
    return this.http.put(environment.api.striveUrl + url, data);
  }
  public delete(url: string, optional?: any): Observable<any> {
    return this.http.delete(environment.api.striveUrl  + url, optional);
  }
}
