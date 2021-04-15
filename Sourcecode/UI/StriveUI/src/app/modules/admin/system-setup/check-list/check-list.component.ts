import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { CheckListService } from 'src/app/shared/services/data-service/check-list.service';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EditChecklistComponent } from './edit-checklist/edit-checklist.component';

@Component({
  selector: 'app-check-list',
  templateUrl: './check-list.component.html',
  styleUrls: ['./check-list.component.css']
})
export class CheckListComponent implements OnInit {
  dropdownSettings: IDropdownSettings = {};
  employeeRoles: any;
  isLoading: boolean;
  checkListDetails: any = [];
  isTableEmpty: boolean;
  collectionSize: number = 0;
  selectedData: any;
  isEdit: boolean;
  checkListName: any;
  RoleId = [];
  Roles: any;
  roles: any;
  employeeRole: any;
  employeeRoleId = [];
  rollList: any;
  checklistAdd: boolean;
  page: any;
  pageSize: any;
  pageSizeList: any;
  sortColumn: { sortBy: string; sortOrder: string; };
  notificationTimeList: any = [];
  notificationTime = '';
  isNotificationTimeLimit: boolean;
  NotificationList = [];
  constructor(
    private employeeService: EmployeeService,
    private checkListSetup: CheckListService,
    private httpClient: HttpClient,
    private confirmationService: ConfirmationUXBDialogService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private datePipe: DatePipe,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.checklistSetup,
      sortOrder: ApplicationConfig.Sorting.SortOrder.checklistSetup.order
    };
    this.isLoading = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.isNotificationTimeLimit = false;
    this.getAllRoles();
    this.getAllcheckListDetails();
  }
  checlist() {
    this.checklistAdd = true;
  }
  checklistcancel() {
    this.checkListName = '';
    this.RoleId = [];
    this.checklistAdd = false;
  }
  // Get All Services
  getAllcheckListDetails() {
    this.isLoading = true;
    this.notificationTime = '';
    this.notificationTimeList = [];
    this.NotificationList = [];
    this.checkListSetup.getCheckListSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.checkListDetails = serviceDetails.GetChecklist;
        // this.checkListDetails.forEach(item => {
        //   const time = item.NotificationTime.split(':');
        //   const hours = time[0];
        //   const min = time[1];
        //   const todayDate: any = new Date();
        //   todayDate.setHours(hours);
        //   todayDate.setMinutes(min);
        //   todayDate.setSeconds('00');
        // });
        if (this.checkListDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort(ApplicationConfig.Sorting.SortBy.checklistSetup);
          this.collectionSize = Math.ceil(this.checkListDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllcheckListDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllcheckListDetails();
  }
  onRoleDeSelect(event) {
    if (this.RoleId) {
      this.employeeRole = this.employeeRole.filter(item => item.item_id !== event.item_id);
      this.employeeRole.push(event);

      this.roles = this.employeeRole;

    }

  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.rollList = roles.EmployeeRoles;
        this.employeeRoles = roles.EmployeeRoles.map(item => {
          return {
            item_id: item.RoleMasterId,
            item_text: item.RoleName
          };
        });
        this.dropdownSettings = {
          singleSelection: false,
          defaultOpen: false,
          idField: 'item_id',
          textField: 'item_text',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: false
        };
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  delete(data) {
    this.confirmationService.confirm('Delete Service', `Are you sure you want to delete this Check List? All related 
  information will be deleted and the Check List cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Service
  confirmDelete(data) {
    this.spinner.show();
    this.checkListSetup.deleteCheckListSetup(data.ChecklistId).subscribe(res => {
      if (res.status === "Success") {
        this.spinner.hide()

        this.toastr.success(MessageConfig.Admin.SystemSetup.CheckList.Delete, 'Success!');
        this.getAllcheckListDetails();
      } else {
        this.spinner.hide()

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  add(data, serviceDetails?) {
    if (data === 'add') {
      this.isEdit = false;
      this.submit(serviceDetails);
    } else {
      this.checkListSetup.getById(serviceDetails.ChecklistId).subscribe(res => {
        if (res.status === "Success") {
          // this.selectedData = serviceDetails.ChecklistId;
          this.isEdit = true;
          this.checklistAdd = false;
          const sType = JSON.parse(res.resultData);
          // this.selectedData = sType.ChecklistById;
          this.selectedData = sType.ChecklistById;
          this.NotificationList = sType.ChecklistById.ChecklistNotificationTime;
          this.NotificationList.forEach(item => {
            const date = item.NotificationTime.split(':');
            const hours = date[0];
            const min = date[1];
            const todayDate: any = new Date();
            todayDate.setHours(hours);
            todayDate.setMinutes(min);
            todayDate.setSeconds('00');
            item.NotificationTime = this.datePipe.transform(todayDate, 'HH:mm');
          });
          const ngbModalOptions: NgbModalOptions = {
            backdrop: 'static',
            keyboard: false,
            size: 'lg'
          };
          const modalRef = this.modalService.open(EditChecklistComponent, ngbModalOptions);
          modalRef.componentInstance.NotificationList = this.NotificationList;
          modalRef.componentInstance.selectedData = this.selectedData;
          modalRef.componentInstance.rollList = this.rollList;
          modalRef.result.then((result) => {
            if (result) {
              this.isNotificationTimeLimit = false;
              this.getAllcheckListDetails();
            }
          });
        } else {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
        , (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
    }
  }
  cancel() {
    this.selectedData = false;
  }

  addTime() {
    if (this.notificationTimeList.length >= 10) {
      this.isNotificationTimeLimit = true;
      this.notificationTime = '';
      return;
    }
    this.notificationTimeList.push({
      time: this.notificationTime
    });
    this.notificationTimeList.forEach((item, index) => {
      item.id = index;
    });
    this.notificationTime = '';
  }

  editTime(checkList) {
    if (this.NotificationList.length >= 10) {
      this.isNotificationTimeLimit = true;
      this.notificationTime = '';
      return;
    }
    this.NotificationList.push({
      checkListNotificationId: 0,
      checkListId: checkList.ChecklistId,
      NotificationTime: this.notificationTime,
      isActive: true,
      isDeleted: false,
    });
    this.notificationTime = '';
  }

  removeTime(time) {
    this.notificationTimeList = this.notificationTimeList.filter(item => item.id !== time.id);
    if (this.notificationTimeList.length < 5) {
      this.isNotificationTimeLimit = false;
    }
  }

  inTime(event) {
    console.log(event, 'event');
  }

  submit(data) {
    if (data.RoleId === undefined && this.RoleId.length === 0) {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.CheckList.roleNameValidation, 'Warning!');
      return;
    }
    const pattern = /[a-zA-Z~`\d!@#$%^&*()-_=+][a-zA-Z~`\d!@#$%^&*()-_=+\d\\s]*/;

    if (data.Name !== undefined) {
      if (!pattern.test(data.Name) || data.Name === undefined) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.CheckList.CheckListNameValidation, 'Warning!');
        return;
      };
    } else {
      if (!pattern.test(this.checkListName) || this.checkListName === undefined) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.CheckList.CheckListNameValidation, 'Warning!');
        return;
      }
    }
    this.notificationTimeList.forEach(element => {
      this.NotificationList.push({
        checkListNotificationId: 0,
        checkListId: data.ChecklistId ? data.ChecklistId : 0,
        notificationTime: element.time,
        isActive: true,
        isDeleted: false,
      });
    });
    const formObj = {
      checkList: {
        ChecklistId: data.ChecklistId ? data.ChecklistId : 0,
        Name: data.Name ? data.Name : this.checkListName,
        RoleId: data.RoleId ? data.RoleId : this.RoleId,
        IsDeleted: false,
        IsActive: true,
      },
      checkListNotification: this.NotificationList
    };
    if (data.ChecklistId) {
      this.spinner.show();
      this.checkListSetup.addCheckListSetup(formObj).subscribe(res => {
        if (res.status === 'Success') {
          if (res.status === 'Success') {
            this.spinner.hide();
            this.toastr.success(MessageConfig.Admin.SystemSetup.CheckList.Update, 'Success!');
            this.getAllcheckListDetails();
            this.selectedData = false;
          } else {
            this.spinner.hide();
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          }
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.checkListSetup.addCheckListSetup(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();
          this.toastr.success(MessageConfig.Admin.SystemSetup.CheckList.Add, 'Success!');
          this.getAllcheckListDetails();
          this.checklistcancel();
          this.checkListName = '';
          this.RoleId = [];
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }
  sort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.checklistSetup.order
    }
    this.sorting(this.sortColumn)
    this.selectedCls(this.sortColumn)

  }
  sorting(sortColumn) {
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
    let property = sortColumn.sortBy;
    this.checkListDetails.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  }
  changesort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.sorting(this.sortColumn)

  }
  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }


}
