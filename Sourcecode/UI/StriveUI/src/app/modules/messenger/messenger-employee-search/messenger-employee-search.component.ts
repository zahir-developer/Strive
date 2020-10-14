import { Component, OnInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-messenger-employee-search',
  templateUrl: './messenger-employee-search.component.html',
  styleUrls: ['./messenger-employee-search.component.css']
})
export class MessengerEmployeeSearchComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  closeemp(){
    $('#show-search-emp').hide();
    $('.internal-employee').addClass('col-xl-9');
    $('.internal-employee').removeClass('col-xl-6');
    $('.view-msg').addClass('Message-box-slide');
    $('.view-msg').removeClass('Message-box');
    $('.plus-icon').removeClass('opacity-16');
  }
}
