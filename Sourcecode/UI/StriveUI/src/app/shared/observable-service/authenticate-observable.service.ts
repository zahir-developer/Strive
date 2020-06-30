import { Injectable } from '@angular/core';
import {Subject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateObservableService {
  private subject = new Subject<any>();
  constructor() { }
  setIsAuthenticate(isAuthenticate: boolean) {
  this.subject.next(isAuthenticate);
  }
  clearIsAuthenticate() {
    this.subject.next();
  }
  getIsAuthenticate(): Observable<any> {
return this.subject.asObservable();
  }
}
