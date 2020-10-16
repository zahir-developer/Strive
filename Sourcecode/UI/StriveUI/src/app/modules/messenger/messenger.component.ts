import { Component, OnInit } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { nullSafeIsEquivalent } from '@angular/compiler/src/output/output_ast';
import { LocalStorage } from '@ng-idle/core';

declare var $: any;
@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {
  msgList = [];

  employeeId: number = +localStorage.getItem('empId');

  recipientId: number = 0;

  recipientCommunicationId: string;

  messageBody: string;

  chatInitial: string;
  selectedEmployee: any;
  FirstName: string;

  LastName: string;

  chatFullName: string;

  IsGroupChat: boolean;
  constructor(public signalRService: SignalRService, private msgService: MessengerService, private messageNotification: MessageServiceToastr, private http: HttpClient) { }



  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.SubscribeChatEvents();
    this.signalRService.ReceivedMsg.subscribe(data => {
      if (data !== null) {
        this.LoadMessageChat(this.selectedEmployee);
      }
    });
    //this.startHttpRequest();
  }
  openemp() {
    $('#show-search-emp').show();
    $('.internal-employee').removeClass('col-xl-9');
    $('.internal-employee').addClass('col-xl-6');
    $('.view-msg').removeClass('Message-box-slide');
    $('.view-msg').addClass('Message-box');
    $('.plus-icon').addClass('opacity-16');
    $('.chat-box-slide').addClass('chatslide');
  }


  LoadMessageChat(employeeObj) {
    this.selectedEmployee = employeeObj;
    const chatObj = {
      senderId: this.employeeId,
      recipientId: employeeObj.Id,
      groupId: 0
    };
    this.recipientId = employeeObj.Id;
    this.FirstName = employeeObj.FirstName;
    this.LastName = employeeObj.LastName;
    this.chatFullName = employeeObj.FirstName + ' ' + employeeObj.LastName;
    this.chatInitial = employeeObj?.FirstName?.charAt(0).toUpperCase() +
    (employeeObj?.LastName ? employeeObj?.LastName?.charAt(0).toUpperCase() : '');
    this.recipientCommunicationId = employeeObj?.CommunicationId !== null ? employeeObj?.CommunicationId : '0';
    this.msgService.GetChatMessage(chatObj).subscribe(data => {
      if (data.status === 'Success') {
        const msgData = JSON.parse(data.resultData);
        this.msgList = msgData.ChatMessage.ChatMessageDetail;
      }
    });
  }

  sendMessage() {

    if (this.messageBody.trim() === '') {
      this.messageNotification.showMessage({ severity: 'warning', title: 'Warning', body: 'Please enter a message..!!!' });
      return;
    }

    const msg = {
      chatMessage: {
        chatMessageId: 0,
        subject: null,
        messagebody: this.messageBody.trim(),
        parentChatMessageId: null,
        expiryDate: null,
        isReminder: true,
        nextRemindDate: null,
        reminderFrequencyId: null,
        createdBy: 0,
        createdDate: null
      },
      chatMessageRecipient: {
        chatRecipientId: 0,
        chatMessageId: 0,
        senderId: this.employeeId,
        recipientId: this.recipientId,
        recipientGroupId: null,
        isRead: true
      },
      connectionId: this.recipientCommunicationId,
      fullName: null,
      groupName: null
    };

    const objmsg: string[] = [this.recipientCommunicationId,
    this.employeeId.toString(),
    this.FirstName,
    this.LastName,
    this.chatInitial,
    this.messageBody.trim()
    ]

    this.msgService.SendMessage(msg).subscribe(data => {
      if (data.status === 'Success') {
        this.signalRService.SendPrivateMessage(objmsg);
        this.LoadMessageChat(this.selectedEmployee);
        this.messageBody = '';
      }
    });
  }
}
