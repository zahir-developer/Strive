import { Component, OnInit } from '@angular/core';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-messenger-employee-list',
  templateUrl: './messenger-employee-list.component.html',
  styleUrls: ['./messenger-employee-list.component.css']
})
export class MessengerEmployeeListComponent implements OnInit {
search = '';
empList = [];
  constructor(private empService: EmployeeService) { }

  ngOnInit(): void {
    this.getRecentChatHistory();
  }
  getRecentChatHistory() {
    this.empService.searchEmployee(this.search).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList.EmployeeList;
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
}
