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
  isTableEmpty: boolean;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.isTableEmpty = true;
    this.getAllEmployeeDetails();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        this.isTableEmpty = false;
        if (employees.Employee.length > 0) {
          const employeeDetail = employees.Employee[0];
          this.employeeDetails = employeeDetail.EmployeeDetail;
          this.employeeDetails = this.employeeDetails.filter( item => item.IsActive === true );
          console.log(this.employeeDetails, 'detail');
        }
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
    if (event.status === 'saved') {
      this.getAllEmployeeDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, empDetails?) {
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

  employeeDetail(employeeDetail) {
    const id = employeeDetail.EmployeeId;
    this.employeeService.getEmployeeDetail(id).subscribe( res => {
      console.log(res, 'getEmployeById');
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        console.log(employees, 'employeDeatil');
      }
    });
  }

  deleteEmployee(employeeDetail) {
    const id = employeeDetail.EmployeeId;
    this.employeeService.deleteEmployee(id).subscribe( res => {
      console.log(res,'deleteEmployee');
      if (res.status === 'Success') {
        this.getAllEmployeeDetails();
      }
    });
  }
}
