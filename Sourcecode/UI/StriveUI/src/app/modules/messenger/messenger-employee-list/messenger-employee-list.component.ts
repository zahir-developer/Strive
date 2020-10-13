import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LocalStorage } from '@ng-idle/core';
import { Subscription } from 'rxjs';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';

@Component({
  selector: 'app-messenger-employee-list',
  templateUrl: './messenger-employee-list.component.html',
  styleUrls: ['./messenger-employee-list.component.css']
})
export class MessengerEmployeeListComponent implements OnInit {
  search = '';
  empList = [];
  @Output() emitLoadMessageChat = new EventEmitter();



  constructor(private msgService: MessengerService) { }

  ngOnInit(): void {
    this.getRecentChatHistory();
  }
  getRecentChatHistory() {
    this.msgService.GetEmployeeList().subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList.EmployeeList.ChatEmployeeList;
        this.setName();
      }
    });
  }
  setName() {
    this.empList.map(item => {
      const intial = item.FirstName.charAt(0).toUpperCase() + item.LastName.charAt(0).toUpperCase();
      item.Initial = intial;
    });
  }
  

  loadChat(employeeObj) {
    this.emitLoadMessageChat.emit(employeeObj);
  }

}
