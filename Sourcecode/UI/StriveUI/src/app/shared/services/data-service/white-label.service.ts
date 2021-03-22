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
    return this.http.get(`${UrlConfig.whiteLabelling.getAllWhiteLabelDetail}`);
  }

  addWhiteLabelDetail(obj) {
    return this.http.post(`${UrlConfig.whiteLabelling.addWhiteLabelDetail}`, obj);
  }

  updateWhiteLabelDetail(obj) {
    return this.http.post(`${UrlConfig.whiteLabelling.updateWhiteLabelDetail}`, obj);
  }
  saveCustomColor(obj) {
    return this.http.post(`${UrlConfig.whiteLabelling.saveCustomColor}`, obj);
  }
  uploadWhiteLabel(uploadObj) {
    return this.http.post(`${UrlConfig.whiteLabelling.uploadWhiteLabel}`, uploadObj);
  }
}
