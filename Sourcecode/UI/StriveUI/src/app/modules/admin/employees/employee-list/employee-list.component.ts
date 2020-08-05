import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModal, NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeCollisionComponent } from '../../employees/employee-collision/employee-collision.component';
import { DocumentListComponent } from '../../employees/document-list/document-list.component';
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
  commissionType: any;
  actionType: string;
  employeeId: any;
  location: any;
  public isCollapsed = false;
  constructor(
    private employeeService: EmployeeService,
    private confirmationService: ConfirmationUXBDialogService,
    private modalService: NgbModal
  ) { }

  ngOnInit() {
    this.isTableEmpty = true;
    this.getAllEmployeeDetails();
    this.getAllRoles();
    this.getGenderDropdownValue();
    this.getMaritalStatusDropdownValue();
    this.getCountry();
    this.getStateList();
    this.getCommisionDropdownValue();
    this.getLocation();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        this.isTableEmpty = false;
        if (employees.Employee.length > 0) {
          const employeeDetail = employees.Employee;
          this.employeeDetails = employeeDetail;
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
      this.actionType = data;
    } else {
      this.headerData = 'Edit Employees';
      this.isEdit = true;
      this.actionType = data;
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
    this.confirmationService.confirm('Delete Employee', `Are you sure you want to delete` + '\t' + employeeDetail.FirstName, 'Confirm', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(employeeDetail);
        }
      })
      .catch(() => { });
  }

  confirmDelete(employeeDetail) {
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

  getCommisionDropdownValue() {
    this.employeeService.getDropdownValue('COMMISIONTYPE').subscribe(res => {
      if (res.status === 'Success') {
        const type = JSON.parse(res.resultData);
        this.commissionType = type.Codes;
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

  addEmployee(mode: string, employee?: any) {
    if (mode === 'create') {
      this.headerData = 'Create Employees';
      this.isEdit = false;
      this.showDialog = true;
      this.actionType = mode;
    } else {
      this.isEdit = true;
      this.actionType = mode;
      this.employeeId = employee.EmployeeId;
      this.showDialog = true;
      // this.employeeDetail(employee);
    }

  }

  closeDialog(event) {
    this.showDialog = event.isOpenPopup;
    this.getAllEmployeeDetails();
  }

  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
        console.log(this.location, 'location');
      }
    });
  }

  openCollisionModal(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef =  this.modalService.open(EmployeeCollisionComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.mode = 'create';
  }

  viewDocument(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef =  this.modalService.open(DocumentListComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.actionType = 'view';
    modalRef.componentInstance.isModal = true;
  }

  collapsed() {
    this.isCollapsed = !this.isCollapsed;
  }

}
