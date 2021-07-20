import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-session-expired',
  templateUrl: './session-expired.component.html',
  styleUrls: ['./session-expired.component.css']
})
export class SessionExpiredComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
    $(document).ready(function(){ 
     $('#sidenavSec').css('display','none');
     $('#footerSec').css('display','none');
     $('#headerSec').css('display','none');
    });
  }

  ngOnDestroy() { 
    $(document).ready(function(){ 
      $('#sidenavSec').css('display','flex');
      $('#footerSec').css('display','flex');
      $('#headerSec').css('display','flex');
     });
  }


  loginPage() {
    this.router.navigate([`/login`]);
  }


}
