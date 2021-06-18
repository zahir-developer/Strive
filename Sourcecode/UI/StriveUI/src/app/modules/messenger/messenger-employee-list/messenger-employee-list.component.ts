import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LocalStorage } from '@ng-idle/core';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

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
  @Output() recentlyMsgSent = new EventEmitter();
  employeeId: number = +localStorage.getItem('empId');
  selectedClass: string;
  constructor(private msgService: MessengerService, private signalrService: SignalRService,
    private toastr: ToastrService) { }
  ngOnInit(): void {
    this.getRecentChatHistory(this.employeeId);
    if (localStorage.getItem('isAuthenticated') === 'true') {
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
      }, (err) => {
        //this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }

  }
  getRecentChatHistory(employeeId) {
    this.msgService.GetEmployeeList(employeeId).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        if (empList.EmployeeList.ChatEmployeeList !== null) {
          this.empList = empList?.EmployeeList?.ChatEmployeeList;
          this.originalEmpList = this.empList;
          this.setName();
          this.setCommunicationId();
          this.setUnreadMsgFlag();
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  setUnreadMsgFlag() {
    this.empList.forEach(item => {
      item.isRead = true;
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
    this.recentlyMsgSent.emit(this.empList);
    if (this.empList.length > 0) {
      this.empList[0].type = 'first Employee';
      this.emitLoadMessageChat.emit(this.empList[0]);
    }
  }
  SetUnreadMsgBool(empId, event, msg) {
    this.empList.forEach(item => {
      if (+item.Id === +empId && msg !== '') {
        item.isRead = event;
        item.RecentChatMessage = msg;
        item.createdDate = new Date();
      } else {
        if (+item.Id === +empId && msg === '') {
          item.isRead = event;
        }
      }
    });
  }
  loadChat(employeeObj) {
    employeeObj.Selected = true;
    this.empList.filter(s => s.Id !== employeeObj.Id).forEach(item => {
      if (item.RecentChatMessage !== null && item.RecentChatMessage !== undefined) {
        item.Selected = false
      }
    });
    employeeObj.type = 'selected Employee';
    this.SetUnreadMsgBool(employeeObj.Id, true, '');
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
        IsGroup: event[0].IsGroup,
        isRead: event[0].isRead,
        type: event[0].type ? event[0].type : ''
      };
      const duplicateEmp = this.empList.filter(item => item.Id === event[0].EmployeeId);
      if (duplicateEmp.length > 0) {
        this.emitLoadMessageChat.emit(empObj);
      } else {
        this.empList.unshift(empObj);
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
