import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WhiteLabelService {

  constructor(private http: HttpUtilsService) { }

  getAllWhiteLabelDetail() {
    return this.http.get(`${UrlConfig.totalUrl.getAllWhiteLabelDetail}`);
  }
}
