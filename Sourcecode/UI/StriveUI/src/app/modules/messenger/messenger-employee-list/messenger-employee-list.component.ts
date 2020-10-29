import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LocalStorage } from '@ng-idle/core';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';

@Component({
  selector: 'app-messenger-employee-list',
  templateUrl: './messenger-employee-list.component.html',
  styleUrls: ['./messenger-employee-list.component.css']
})
export class MessengerEmployeeListComponent implements OnInit {
  query = '';
  empList = [];
  originalEmpList = [];
  empOnlineStatus: any;
  @Output() emitLoadMessageChat = new EventEmitter();
  @Output() popupEmit = new EventEmitter();
  employeeId: number = +localStorage.getItem('empId');
  constructor(private msgService: MessengerService, private signalrService: SignalRService) { }
  ngOnInit(): void {
    this.getRecentChatHistory(this.employeeId);
    this.signalrService.communicationId.subscribe(data => {
      if (data !== null) {
        this.empOnlineStatus = data;

        const commObj = {
          EmployeeId: +data[0],
          CommunicationId: data[1]
        };

        this.msgService.UpdateChatCommunication(commObj).subscribe(data => {
        });

        if (this.empList.length > 0) {
          this.setCommunicationId();
        }
      }
    });
  }
  getRecentChatHistory(employeeId) {
    this.msgService.GetEmployeeList(employeeId).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList?.EmployeeList?.ChatEmployeeList;
        this.originalEmpList = this.empList;
        this.setName();
        this.setCommunicationId();
      }
    });
  }
  setCommunicationId() {
    if (this.empOnlineStatus !== undefined && this.empOnlineStatus !== null) {
      this.empList.forEach(item => {
        if (+item.Id === +this.empOnlineStatus[0]) {
          item.CommunicationId = this.empOnlineStatus[1];
        }
      });
    }
  }
  setName() {
    if (this.empList.length > 0) {
      this.emitLoadMessageChat.emit(this.empList[0]);
      this.empList.forEach(item => {
        if (item.RecentChatMessage !== null && item.RecentChatMessage !== undefined) {
          const recentMsg = item.RecentChatMessage.split(',');
          item.RecentChatMessage = recentMsg[1];
          item.createdDate = recentMsg[0];
        }
      });
    }
  }
  loadChat(employeeObj) {
    this.emitLoadMessageChat.emit(employeeObj);
  }
  getEmpForNewChat(event) {
    if (event !== undefined) {
      const empObj = {
        Id: event[0].EmployeeId,
        FirstName: event[0].FirstName,
        LastName: event[0].LastName,
        CommunicationId: event[0]?.CommunicationId,
        ChatCommunicationId: '0',
        IsGroup: event[0].IsGroup
      };
      const duplicateEmp = this.empList.filter(item => item.Id === event[0].EmployeeId);
      if (duplicateEmp.length > 0) {
        this.emitLoadMessageChat.emit(empObj);
      } else {
        this.empList.unshift({
          Id: event[0].EmployeeId, FirstName: event[0].FirstName,
          LastName: event[0].LastName, CommunicationId: event[0]?.CommunicationId, ChatCommunicationId: '0', IsGroup: event[0].IsGroup
        });
        this.emitLoadMessageChat.emit(empObj);
      }
    }
  }
  addemp() {
    this.popupEmit.emit('newChat');
  }
  search() {
    this.empList = this.originalEmpList;
    const results: any = [];
    if (!this.query || this.query === '*') {
      this.query = '';
    } else {
      this.query = this.query.toLowerCase();
    }
    this.empList = this.empList.filter(item => {
      item.fullName = item.FirstName + ' ' + item.LastName;
      if (JSON.stringify(item.fullName).toLowerCase().includes(this.query)) {
        return item;
      }
    });
  }
}
