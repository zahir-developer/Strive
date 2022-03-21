import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { environment } from '../../../environments/environment';
    
@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})

export class FooterComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;
  appVersion: any;
  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.appVersion = environment.appVersion;
  }

}
