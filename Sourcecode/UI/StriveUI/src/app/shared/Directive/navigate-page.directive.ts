import { HostListener } from '@angular/core';
import { Directive } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Directive({
  selector: '[appNavigatePage]'
})
export class NavigatePageDirective {

  constructor(private router: Router, private route: ActivatedRoute) { }

  @HostListener('click', ['$event']) onClick(event) {
    this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });
  }

}
