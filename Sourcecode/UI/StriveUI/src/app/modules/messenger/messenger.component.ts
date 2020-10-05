import { Component, OnInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  openemp() {
    $('#show-search-emp').show();
    $('.internal-employee').removeClass('col-xl-9');
    $('.internal-employee').addClass('col-xl-6');
    $('.view-msg').removeClass('Message-box-slide');
    $('.view-msg').addClass('Message-box');
    $('.plus-icon').addClass('opacity-16');
}
}
