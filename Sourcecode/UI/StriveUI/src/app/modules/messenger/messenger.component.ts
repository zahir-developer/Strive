import { Component, OnInit } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';

declare var $: any;
@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {

  constructor(public signalRService: SignalRService, private http: HttpClient) { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.UpdateCarCheckIn();   
    //this.startHttpRequest();
  }

  openemp() {
    $('#show-search-emp').show();
    $('.internal-employee').removeClass('col-xl-9');
    $('.internal-employee').addClass('col-xl-6');
    $('.view-msg').removeClass('Message-box-slide');
    $('.view-msg').addClass('Message-box');
    $('.plus-icon').addClass('opacity-16');
}

private startHttpRequest = () => {
  this.http.get('https://localhost:5001/api/chart')
    .subscribe(res => {
      console.log(res);
    })
}

}
