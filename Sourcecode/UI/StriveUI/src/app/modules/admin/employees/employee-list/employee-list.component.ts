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
  employeeRoles: any;
  gender: any;
  maritalStatus: any;
  state: any;
  country: any;
  employeeData: any;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.isTableEmpty = true;
    this.getAllEmployeeDetails();
    this.getAllRoles();
    this.getGenderDropdownValue();
    this.getMaritalStatusDropdownValue();
    this.getCountry();
    this.getStateList();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        this.isTableEmpty = false;
        if (employees.Employee.length > 0) {
          const employeeDetail = employees.Employee[0];
          this.employeeDetails = employeeDetail.EmployeeDetail;
          this.employeeDetails = this.employeeDetails.filter(item => item.IsActive === true);
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
      this.isEdit = true;
      // this.selectedData = empDetails;
      // this.isEdit = true;
      // this.showDialog = true;
      this.employeeDetail(empDetails);
    }
  }

  employeeDetail(employeeDetail) {
    const id = employeeDetail.EmployeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      console.log(res, 'getEmployeById');
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        console.log(employees, 'employeDeatil');
        if (employees.EmployeeDetail.length > 0) {
          this.employeeData = employees.EmployeeDetail[0];
          this.showDialog = true;
        }
      }
    });
  }

  deleteEmployee(employeeDetail) {
    const id = employeeDetail.EmployeeId;
    this.employeeService.deleteEmployee(id).subscribe(res => {
      console.log(res, 'deleteEmployee');
      if (res.status === 'Success') {
        this.getAllEmployeeDetails();
      }
    });
  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      console.log(res, 'getAllRoles');
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        console.log(roles, 'roles');
        this.employeeRoles = roles.EmployeeRoles;
      }
    });
  }

  getGenderDropdownValue() {
    this.employeeService.getDropdownValue('GENDER').subscribe(res => {
      console.log(res, 'gender');
      if (res.status === 'Success') {
        const gender = JSON.parse(res.resultData);
        this.gender = gender.Codes;
      }
    });
  }

  getMaritalStatusDropdownValue() {
    this.employeeService.getDropdownValue('MARITALSTATUS').subscribe(res => {
      if (res.status === 'Success') {
        const status = JSON.parse(res.resultData);
        this.maritalStatus = status.Codes;
      }
    });
  }

  getStateList() {
    this.employeeService.getStateList().subscribe(res => {
      if (res.status === 'Success') {
        const state = JSON.parse(res.resultData);
        this.state = state.Codes;
      }
    });
  }

  getCountry() {
    this.employeeService.getCountryList().subscribe(res => {
      if (res.status === 'Success') {
        const country = JSON.parse(res.resultData);
        this.country = country.Codes;
      }
    });
  }
}
