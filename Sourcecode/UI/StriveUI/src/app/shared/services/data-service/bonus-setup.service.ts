import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class BonusSetupService {

  constructor(private http: HttpUtilsService) { }

  saveBonus(obj) {
    return this.http.post(`${UrlConfig.bonusSetup.saveBonus}`, obj);
  }
}
