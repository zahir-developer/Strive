import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModal, NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeCollisionComponent } from '../../employees/employee-collision/employee-collision.component';
import { DocumentListComponent } from '../../employees/document-list/document-list.component';
import { CreateDocumentComponent } from '../../employees/create-document/create-document.component';
import { ToastrService } from 'ngx-toastr';
import { CollisionListComponent } from '../../employees/collision-list/collision-list.component';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { Router } from '@angular/router';

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
  page = 1;
  pageSize = 5;
  collectionSize: number;
  search = '';
  sort = { column: 'Status', descending: true };
  sortColumn: { column: string; descending: boolean; };
  constructor(
    private employeeService: EmployeeService,
    private confirmationService: ConfirmationUXBDialogService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
    private router: Router
  ) { }

  ngOnInit() {
    this.isTableEmpty = true;
    // this.getAllEmployeeDetails();
    this.seachEmployee();
    this.getCommisionDropdownValue();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        this.isTableEmpty = false;
        if (employees.EmployeeList.length > 0) {
          const employeeDetail = employees.EmployeeList;
          this.employeeDetails = employeeDetail;
          // this.employeeDetails = this.employeeDetails.filter(item => item.Status === true);
          this.collectionSize = Math.ceil(this.employeeDetails.length / this.pageSize) * 10;
        }
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
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
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
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
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Employee Deleted Successfully!' });
        this.seachEmployee();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  seachEmployee() {
    this.employeeService.searchEmployee(this.search).subscribe(res => {
      if (res.status === 'Success') {
        const seachList = JSON.parse(res.resultData);
        this.employeeDetails = seachList.EmployeeList;
        this.collectionSize = Math.ceil(this.employeeDetails.length / this.pageSize) * 10;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllRoles() {
    this.employeeService.getDropdownValue('EMPLOYEEROLE').subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.employeeRoles = roles.Codes;
      }
    });
  }

  getGenderDropdownValue() {
    this.employeeService.getDropdownValue('GENDER').subscribe(res => {
      console.log(res, 'gender');
      if (res.status === 'Success') {
        const gender = JSON.parse(res.resultData);
        this.gender = gender.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getMaritalStatusDropdownValue() {
    this.employeeService.getDropdownValue('MARITALSTATUS').subscribe(res => {
      if (res.status === 'Success') {
        const status = JSON.parse(res.resultData);
        this.maritalStatus = status.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getCommisionDropdownValue() {
    this.employeeService.getDropdownValue('DETAILCOMMISSION').subscribe(res => {
      if (res.status === 'Success') {
        const type = JSON.parse(res.resultData);
        this.commissionType = type.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getStateList() {
    this.employeeService.getStateList().subscribe(res => {
      if (res.status === 'Success') {
        const state = JSON.parse(res.resultData);
        this.state = state.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getCountry() {
    this.employeeService.getCountryList().subscribe(res => {
      if (res.status === 'Success') {
        const country = JSON.parse(res.resultData);
        this.country = country.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
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
    }

  }

  closeDialog(event) {
    this.showDialog = event.isOpenPopup;
    this.seachEmployee();
  }

  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
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
    const modalRef = this.modalService.open(EmployeeCollisionComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.mode = 'create';
    modalRef.result.then((result) => {
      if (result) {
        this.seachEmployee();
      }
    });
  }

  viewCollision(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(CollisionListComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.actionType = 'view';
    modalRef.componentInstance.isModal = true;
  }

  viewDocument(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(DocumentListComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.actionType = 'view';
    modalRef.componentInstance.isModal = true;
  }

  addDocument(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(CreateDocumentComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
    modalRef.componentInstance.mode = 'create';
    modalRef.result.then((result) => {
      if (result) {
        this.seachEmployee();
      }
    });
  }

  collapsed() {
    this.isCollapsed = !this.isCollapsed;
  }

  schedule() {
    this.router.navigate(['/admin/scheduling']);
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }

}
