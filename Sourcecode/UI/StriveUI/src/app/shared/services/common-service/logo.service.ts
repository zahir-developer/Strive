import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogoService {
private logo: BehaviorSubject<any> = new BehaviorSubject<any> (null);
public name: Observable<any> = this.logo.asObservable();
private logoName: BehaviorSubject<any> = new BehaviorSubject<any> (null);
public title: Observable<any> = this.logoName.asObservable();
  constructor() { }
  setLogo(base64){
this.logo.next(base64);
  }
  setTitle(name){
    this.logoName.next(name);
  }
}
