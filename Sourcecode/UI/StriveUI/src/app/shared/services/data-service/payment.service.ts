import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }

  post(body) {
    const headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Authorization', 'Bearer' + 'testing')
      .set('Access-Control-Allow-Origin', '*');
    console.log(headers, 'new header');
    return this.http.post('url', body, { headers });
  }

}
