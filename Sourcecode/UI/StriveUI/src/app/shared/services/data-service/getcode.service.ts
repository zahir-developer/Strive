import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class GetCodeService {

  constructor(private http: HttpUtilsService) { }
   
  getCodeByCategory(obj : string){
    return this.http.get(`${UrlConfig.totalUrl.getCode}`, obj);
  } 
}