import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employeeDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.getAllEmployeeDetails();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        this.employeeDetails = employees.Employee;
      }
    });
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {

}
closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllEmployeeDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, empDetails?) {
  if (data === 'add') {
    this.headerData = 'Create Employees';
    this.selectedData = empDetails;
    this.isEdit = false;
    this.showDialog = true;
  } else {
    this.headerData = 'Edit Employees';
    this.selectedData = empDetails;
    this.isEdit = true;
    this.showDialog = true;
  }
}
}
