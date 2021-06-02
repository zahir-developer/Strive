import { Component, OnInit, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { MessengerEmployeeSearchComponent } from './messenger-employee-search/messenger-employee-search.component';
import { MessengerEmployeeListComponent } from './messenger-employee-list/messenger-employee-list.component';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

declare var $: any;
@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  providers: [SignalRService]
})
export class MessengerComponent implements OnInit, AfterViewChecked {
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;
  @ViewChild(MessengerEmployeeSearchComponent) messengerEmployeeSearchComponent: MessengerEmployeeSearchComponent;
  @ViewChild(MessengerEmployeeListComponent) messengerEmployeeListComponent: MessengerEmployeeListComponent;
  msgList = [];

  employeeId: number = +localStorage.getItem('empId');

  recipientId: number = 0;

  recipientCommunicationId: string;

  messageBody = '';

  chatInitial: string;
  selectedEmployee: any;
  FirstName: string;

  LastName: string;

  chatFullName: string;

  isGroupChat: boolean;
  groupChatId: number;
  senderFirstName: string;
  senderLastName: string;
  currentEmployeeId: number;
  popupType: any;
  isUserOnline: boolean = false;
  groupEmpList: any;
  selectedChatId: number;
  previouslyMessaged: boolean;
  constructor(public signalRService: SignalRService, private msgService: MessengerService, private messageNotification: MessageServiceToastr, private http: HttpClient,
    private toastrService: ToastrService, private confirmationService: ConfirmationUXBDialogService, private spinner: NgxSpinnerService) { }



  ngOnInit() {
    this.previouslyMessaged = false;
    this.getSenderName();
    this.scrollToBottom();
    this.signalRService.startConnection();
    this.signalRService.SubscribeChatEvents();
    this.signalRService.ReceivedMsg.subscribe(data => {
      this.selectedChatId = this.selectedEmployee?.Id;
      if (data !== null && this.selectedEmployee !== null) {
        if (this.selectedChatId === data?.chatMessageRecipient?.senderId) {
          const receivedMsg = {
            SenderId: 0,
            SenderFirstName: this.selectedEmployee.FirstName,
            SenderLastName: this.selectedEmployee.LastName,
            ReceipientId: data.chatMessageRecipient.senderId,
            RecipientFirstName: '',
            RecipientLastName: '',
            MessageBody: data.chatMessage.messagebody,
            CreatedDate: data.chatMessage.createdDate,
            CommunicationId: this.selectedEmployee.CommunicationId
          };
          this.msgList.push(receivedMsg);
        } else {
          this.showUnreadMsg(data?.chatMessageRecipient?.senderId, data?.chatMessage?.messagebody);
        }
        this.LoadMessageChat(this.selectedEmployee);
      }
    });

    this.signalRService.ReceiveGrpMsg.subscribe(data => {
      this.selectedChatId = this.selectedEmployee?.Id;
      if (data !== null && this.selectedEmployee !== null) {
        if (this.selectedChatId === data?.chatMessageRecipient?.recipientGroupId && this.employeeId !== data.chatMessageRecipient.senderId) {
          const receivedMsg = {
            SenderId: 0,
            SenderFirstName: data.firstName,
            SenderLastName: data.lastName,
            ReceipientId: data.chatMessageRecipient.senderId,
            RecipientFirstName: '',
            RecipientLastName: '',
            MessageBody: data.chatMessage.messagebody,
            CreatedDate: data.chatMessage.createdDate,
            CommunicationId: this.selectedEmployee.CommunicationId
          };
          this.msgList.push(receivedMsg);
        } else {
          this.showUnreadMsg(data?.chatMessageRecipient?.senderId, data?.chatMessage?.messagebody);
        }
        this.LoadMessageChat(this.selectedEmployee);
      }
    });

    this.signalRService.communicationId.subscribe(data => {
      if (data !== null) {
        const commObj = {
          EmployeeId: +data[0],
          CommunicationId: data[1]
        };

        if (!this.selectedEmployee?.IsGroup) {
          if (this.selectedEmployee?.Id === commObj.EmployeeId) {
            this.isUserOnline = true;
          }
        }
      }
    });
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }
  getSenderName() {
    this.senderFirstName = localStorage.getItem('employeeFirstName');
    this.senderLastName = localStorage.getItem('employeeLastName');
  }
  showUnreadMsg(data, msg) {
    this.messengerEmployeeListComponent.SetUnreadMsgBool(data, false, msg);
  }
  openemp(event) {
    this.popupType = event;
    this.messengerEmployeeSearchComponent.getAllEmployees();
    $('#show-search-emp').show();
    $('.internal-employee').removeClass('col-xl-9');
    $('.internal-employee').addClass('col-xl-6');
    $('.view-msg').removeClass('Message-box-slide');
    $('.view-msg').addClass('Message-box');
    $('.plus-icon').addClass('opacity-16');
    $('.chat-box-slide').addClass('chatslide');
  }


  LoadMessageChat(employeeObj) {
    this.spinner.show();
    this.messengerEmployeeSearchComponent.closeemp();
    this.selectedEmployee = employeeObj;
    this.isGroupChat = employeeObj.IsGroup;
    this.groupChatId = employeeObj.IsGroup ? employeeObj.Id : 0;
    const chatObj = {
      senderId: employeeObj.IsGroup ? 0 : this.employeeId,
      recipientId: employeeObj.IsGroup ? 0 : employeeObj.Id,
      groupId: employeeObj.IsGroup ? employeeObj.Id : 0,
    };
    this.recipientId = employeeObj.Id;
    this.FirstName = employeeObj.FirstName;
    this.LastName = employeeObj.LastName;
    this.chatFullName = employeeObj.FirstName + ' ' + (employeeObj.LastName ? employeeObj.LastName : '');
    this.chatInitial = employeeObj?.FirstName?.charAt(0).toUpperCase() +
      (employeeObj?.LastName !== null ? employeeObj?.LastName?.charAt(0).toUpperCase() : '');
    this.recipientCommunicationId = employeeObj?.CommunicationId !== null ? employeeObj?.CommunicationId : '0';
    this.msgService.GetChatMessage(chatObj).subscribe(data => {
      if (data.status === 'Success') {
        const msgData = JSON.parse(data.resultData);
        this.msgList = msgData.ChatMessage.ChatMessageDetail !== null ? msgData.ChatMessage.ChatMessageDetail : [];
        this.scrollToBottom();
        this.spinner.hide();
        if (!this.selectedEmployee.IsGroup) {
          const chatDetail =
          {
            senderId: !this.isGroupChat ? this.employeeId : 0,
            recipientId: !this.isGroupChat ? this.selectedEmployee.Id : this.employeeId,
            groupId: this.isGroupChat ? this.groupChatId : null
          }
          this.changeUnreadMessageState(chatDetail);
        }
      }
      else {
        this.spinner.hide();
        this.toastrService.error(MessageConfig.CommunicationError, 'Error getting chat messages!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });

    if (this.isGroupChat) {
      this.getGroupMembers(this.groupChatId);
    }
  }

  recentlyMsgSent(event) { 
    if (event.length === 0) {
      this.previouslyMessaged = false;
    } else {
      this.previouslyMessaged = true;
    }
  }

  sendMessage(override = false) {
    if (this.messageBody.trim() === '' && !override) {
      this.messageNotification.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Messenger.Message });
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
        createdDate: new Date()
      },
      chatMessageRecipient: {
        chatRecipientId: 0,
        chatMessageId: 0,
        senderId: this.employeeId,
        recipientId: this.isGroupChat ? null : this.recipientId,
        recipientGroupId: this.isGroupChat ? this.groupChatId : null,
        isRead: false
      },
      connectionId: this.selectedEmployee.CommunicationId,
      fullName: null,
      groupName: null,
      groupId: this.selectedEmployee.CommunicationId,
      firstName: localStorage.getItem('employeeFirstName'),
      lastName: localStorage.getItem('employeeLastName'),
      chatGroupRecipient: []
    };

    if (this.selectedEmployee.IsGroup) {
      if (this.groupEmpList?.length > 0) {
        this.groupEmpList.forEach(s => {
          const grpRecp =
          {
            chatGroupRecipientId: 0,
            chatGroupId: s.ChatGroupId,
            recipientId: s.Id,
            isRead: false
          }
          msg.chatGroupRecipient.push(grpRecp);
        });

      }
    }

    this.spinner.show();
    this.msgService.SendMessage(msg).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        const sendObj = {
          SenderId: this.employeeId,
          SenderFirstName: this.senderFirstName,
          SenderLastName: this.senderLastName,
          ReceipientId: 0,
          RecipientFirstName: '',
          RecipientLastName: '',
          MessageBody: this.messageBody.trim(),
          CreatedDate: new Date()
        };
        this.msgList.push(sendObj);
        this.scrollToBottom();
        this.messengerEmployeeListComponent.SetUnreadMsgBool(this.selectedEmployee?.Id, true, this.messageBody);
        this.LoadMessageChat(this.selectedEmployee);
        this.messageBody = '';
      }
      else {
        this.spinner.hide();
        this.toastrService.error(MessageConfig.CommunicationError, 'Error sending message!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } catch (err) { }
  }
  openpopup(event) {
    this.currentEmployeeId = 0;
    this.openemp(event);
  }

  getGroupMembers(groupId) {
    this.msgService.getGroupMembers(groupId).subscribe(data => {
      if (data.status === 'Success') {
        const employeeListData = JSON.parse(data.resultData);
        this.groupEmpList = employeeListData?.EmployeeList?.ChatEmployeeList;
      }
      else {
        this.toastrService.error(MessageConfig.CommunicationError, 'Error getting Group Members!');
      }
    }
    , (err) => {
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  refreshGroupMembers(groupId) {
    this.spinner.show();
    this.msgService.getGroupMembers(groupId).subscribe(data => {
      if (data.status === 'Success') {
        const employeeListData = JSON.parse(data.resultData);
        this.groupEmpList = employeeListData?.EmployeeList?.ChatEmployeeList;
        this.spinner.hide();
        this.toastrService.success(MessageConfig.Messenger.add);
      }
      else {
        this.spinner.hide();
        this.toastrService.error(MessageConfig.CommunicationError, 'Error getting Group Members!');
      }
    },
     (err) => {
      this.spinner.hide();
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  sendFirstMessage(selectedEmployee) {
    this.sendMessage(true);
  }

  confirmDeleteGroupUser(groupChatUserId, chatGroupId) {
    this.confirmationService.confirm('Delete Group user', `Are you sure you want to delete this user from Group?`, 'Yes', 'No').then((confirmed) => {
      if (confirmed === true) {
        this.deleteGroupUser(groupChatUserId, chatGroupId);
      }
    })
      .catch(() => { });
  }

  deleteGroupUser(groupChatUserId, chatGroupId) {
    this.spinner.show();
    this.msgService.removeGroupUser(groupChatUserId).subscribe(data => {
      const result = JSON.parse(data.resultData);
      if (result.ChatGroupUserDelete === true) {
        this.spinner.hide();

        this.getGroupMembers(chatGroupId);
        this.toastrService.success('User removed from group successfully..!');
      }
      else {
        this.spinner.hide();

        this.toastrService.error(MessageConfig.CommunicationError, 'Error removing user from Group!');
      }

    }, (err) => {
      this.spinner.hide();
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  changeUnreadMessageState(chatDetail) {
    this.msgService.changeUnreadMessageState(chatDetail).subscribe(data => {
      if (data.status === 'Success') {
        const result = JSON.parse(data.resultData);
      }
      else {
      }
    }, (err) => {
      this.toastrService.error(MessageConfig.CommunicationError, 'Error!');
    });

  }
}
