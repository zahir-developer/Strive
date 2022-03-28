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
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { EmployeeHourlyRateComponent } from '../employee-hourly-rate/employee-hourly-rate.component';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html'
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
  collectionSize: number;
  search = '';
  page: any;
  pageSize: any;
  pageSizeList: any[];
  sortColumn: { sortBy: string; sortOrder: string; };
  searchUpdate = new Subject<string>();
  statusGroup = true;

  constructor(
    private employeeService: EmployeeService,
    private confirmationService: ConfirmationUXBDialogService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private messageService: MessageServiceToastr,
    private router: Router
  ) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.newSeachEmployee();
      });
   }

  ngOnInit() {
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Employee, sortOrder: ApplicationConfig.Sorting.SortOrder.Employee.order };

    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.isTableEmpty = true;
    this.seachEmployee();
    this.getCommisionDropdownValue();
  }
  getAllEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        const employees = JSON.parse(data.resultData);
        const employeeDetail = employees.EmployeeList;
        this.employeeDetails = employeeDetail;
        if (this.employeeDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.employeeDetails.length / this.pageSize) * 10;

          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.seachEmployee();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
    this.seachEmployee();
  }
  employeeDetail(employeeDetail) {
    const id = employeeDetail.EmployeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        if (employees.EmployeeDetail.length > 0) {
          this.employeeData = employees.EmployeeDetail[0];
          this.showDialog = true;
        }
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    this.spinner.show();
    this.employeeService.deleteEmployee(id).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Employee.Delete, 'Success!');
        this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Employee, sortOrder: ApplicationConfig.Sorting.SortOrder.Employee.order };

        this.seachEmployee();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  newSeachEmployee()
  {
    this.page = 1;
    this.seachEmployee();
  }

  seachEmployee() {
    this.spinner.show();
    const empObj = {
      startDate: null,
      endDate: null,
      locationId: null,
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: this.statusGroup
    };
    this.employeeDetails = [];
    this.employeeService.getAllEmployeeList(empObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const seachList = JSON.parse(res.resultData);
        if (seachList.EmployeeList.Employee !== null) {
          this.employeeDetails = seachList.EmployeeList.Employee;
          const totalCount = seachList.EmployeeList.Count.Count;
          this.collectionSize = Math.ceil(totalCount / this.pageSize) * 10;
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  getAllRoles() {
    this.employeeService.getDropdownValue('EMPLOYEEROLE').subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.employeeRoles = roles.Codes;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getGenderDropdownValue() {
    this.employeeService.getDropdownValue('GENDER').subscribe(res => {
      if (res.status === 'Success') {
        const gender = JSON.parse(res.resultData);
        this.gender = gender.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getMaritalStatusDropdownValue() {
    this.employeeService.getDropdownValue('MARITALSTATUS').subscribe(res => {
      if (res.status === 'Success') {
        const status = JSON.parse(res.resultData);
        this.maritalStatus = status.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getCommisionDropdownValue() {
    this.employeeService.getDropdownValue('DETAILCOMMISSION').subscribe(res => {
      if (res.status === 'Success') {
        const type = JSON.parse(res.resultData);
        this.commissionType = type.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getStateList() {
    this.employeeService.getStateList().subscribe(res => {
      if (res.status === 'Success') {
        const state = JSON.parse(res.resultData);
        this.state = state.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getCountry() {
    this.employeeService.getCountryList().subscribe(res => {
      if (res.status === 'Success') {
        const country = JSON.parse(res.resultData);
        this.country = country.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Employee, sortOrder: ApplicationConfig.Sorting.SortOrder.Employee.order };

    this.seachEmployee();
  }

  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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

  openHourlyRateModal(employee) {
    const empId = employee.EmployeeId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(EmployeeHourlyRateComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = empId;
  }

  schedule() {
    this.router.navigate(['/admin/scheduling']);
  }
  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder === 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn);
    this.seachEmployee();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }


  selectOption(event) {
    if(event === 'null') {
      this.statusGroup = null
    } else {
    this.statusGroup = event === 'true' ? true: false;
    }
  }

  sendEmployeeEmail() {
    this.spinner.show();
    this.employeeService.sendEmployeeEmail().subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

}
