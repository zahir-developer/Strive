import { Component, OnInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {
  sampleJson = [];
  message = '';
  constructor() { }

  ngOnInit(): void {
    this.sampleJson = [{ userId: 'me', FirstName: 'Mathu', LastName: 'Priya', body: 'hai', date: new Date() },
    { userId: 'others', FirstName: 'karthi', LastName: 'sri', body: 'hw r u', date: new Date() },
    { userId: 'me', FirstName: 'Mathu', LastName: 'Priya', body: 'gm', date: new Date() }];
    this.setInitial();
  }
  setInitial() {
    this.sampleJson.forEach(item => {
      const intial = item.FirstName.charAt(0).toUpperCase() + item.LastName.charAt(0).toUpperCase();
      item.Initial = intial;
    });
  }
  openemp() {
    $('#show-search-emp').show();
    $('.internal-employee').removeClass('col-xl-9');
    $('.internal-employee').addClass('col-xl-6');
    $('.view-msg').removeClass('Message-box-slide');
    $('.view-msg').addClass('Message-box');
    $('.plus-icon').addClass('opacity-16');
  }
  sendMessage() {
if (this.message !== '' && this.message !== undefined) {
console.log(this.message);
}
  }
}
