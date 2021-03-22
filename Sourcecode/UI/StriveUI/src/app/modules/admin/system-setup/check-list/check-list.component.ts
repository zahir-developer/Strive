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
  selectedData: boolean = false;
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
 
  constructor(
    private employeeService: EmployeeService,
    private checkListSetup: CheckListService,
    private httpClient: HttpClient,
    private confirmationService: ConfirmationUXBDialogService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,

  ) { }

  ngOnInit(): void {
    this.sortColumn ={
      sortBy: ApplicationConfig.Sorting.SortBy.checklistSetup,
      sortOrder: ApplicationConfig.Sorting.SortOrder.checklistSetup.order
     }
    this.isLoading = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllRoles();
    this.getAllcheckListDetails();
  }
  checlist() {
    this.checklistAdd = true;
    this.selectedData = false;
  }
  checklistcancel() {
    this.checkListName = '';
    this.RoleId = [];
    this.checklistAdd = false;
  }
  // Get All Services
  getAllcheckListDetails() {
    this.isLoading = true;
    this.checkListSetup.getCheckListSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.checkListDetails = serviceDetails.GetChecklist;
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
    },  (err) => {
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
        this.rollList = roles.EmployeeRoles
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
    ,  (err) => {
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
    },  (err) => {
      this.spinner.hide();
              this.toastr.error(MessageConfig.CommunicationError, 'Error!');
            });
  }
  add(data, serviceDetails?) {
    if (data === 'add') {
      this.isEdit = false;
      this.submit(serviceDetails);
    } else {
      this.selectedData = serviceDetails.ChecklistId;
      this.isEdit = true;
      this.checklistAdd = false;
    }
  }
  cancel() {
    this.selectedData = false;

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

    const formObj = {
      checkList: {
        ChecklistId: data.ChecklistId ? data.ChecklistId : 0,
        Name: data.Name ? data.Name : this.checkListName,
        RoleId: data.RoleId ? data.RoleId : this.RoleId,
        IsDeleted: false,
        IsActive: true,
      }


    };
    if (data.ChecklistId) {
      this.spinner.show();
      this.checkListSetup.addCheckListSetup(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.CheckList.Update, 'Success!');
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
      },  (err) => {
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
      },  (err) => {
        this.spinner.hide();
                this.toastr.error(MessageConfig.CommunicationError, 'Error!');
              });
    }
  }
  sort(property) {
    this.sortColumn ={
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.checklistSetup.order
     }
     this.sorting(this.sortColumn)
     this.selectedCls(this.sortColumn)
   
  }
  sorting(sortColumn){
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
      this.sortColumn ={
        sortBy: property,
        sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
       }
   
       this.selectedCls(this.sortColumn)
  this.sorting(this.sortColumn)
      
    }
    selectedCls(column) {
      if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'DESC') {
        return 'fa-sort-desc';
      } else if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'ASC') {
        return 'fa-sort-asc';
      }
      return '';
    }


}
