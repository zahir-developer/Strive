import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogoService {
private logo: BehaviorSubject<any> = new BehaviorSubject<any> (null);
public name: Observable<any> = this.logo.asObservable();
  constructor() { }
  setLogo(base64){
this.logo.next(base64);
  }
}
