import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LocalStorage } from '@ng-idle/core';
import { Subscription } from 'rxjs';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { SignalRService } from 'src/app/shared/services/data-service/signal-r.service';

@Component({
  selector: 'app-messenger-employee-list',
  templateUrl: './messenger-employee-list.component.html',
  styleUrls: ['./messenger-employee-list.component.css']
})
export class MessengerEmployeeListComponent implements OnInit {
  search = '';
  empList = [];
  empOnlineStatus: any;
  @Output() emitLoadMessageChat = new EventEmitter();
  constructor(private msgService: MessengerService, private signalrService: SignalRService) { }
  ngOnInit(): void {
    this.getRecentChatHistory();
    this.signalrService.communicationId.subscribe(data => {
      this.empOnlineStatus = data;
      this.getRecentChatHistory();
    });
  }
  getRecentChatHistory() {
    this.msgService.GetEmployeeList().subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList?.EmployeeList?.ChatEmployeeList;
        this.setName();
        this.setCommunicationId();
      }
    });
  }
  setCommunicationId() {
    if (this.empOnlineStatus !== undefined && this.empOnlineStatus !== null) {
      this.empList.forEach(item => {
        if (+item.EmployeeId === +this.empOnlineStatus[0]) {
          item.communicationId = this.empOnlineStatus[1];
        }
      });
    }
  }
  setName() {
    if (this.empList.length > 0) {
      this.empList.map(item => {
        const intial = item.FirstName.charAt(0).toUpperCase() + item.LastName.charAt(0).toUpperCase();
        item.Initial = intial;
      });
      this.emitLoadMessageChat.emit(this.empList[0]);
    }
  }
  loadChat(employeeObj) {
    this.emitLoadMessageChat.emit(employeeObj);
  }
}