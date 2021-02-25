import { HostListener } from '@angular/core';
import { Directive } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/common-service/auth.service';
import { UserDataService } from '../util/user-data.service';

@Directive({
  selector: '[appNavigatePage]'
})
export class NavigatePageDirective {
  dashBoardModule: boolean;

  constructor(private router: Router, private route: ActivatedRoute,
    private authService:AuthService,
    private user: UserDataService,) { }

  @HostListener('click', ['$event']) onClick(event) {
   this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });

  }

}
