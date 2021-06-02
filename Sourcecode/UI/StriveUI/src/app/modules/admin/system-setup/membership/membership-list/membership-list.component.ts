import { Component, OnInit } from '@angular/core';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-membership-list',
  templateUrl: './membership-list.component.html',
  styles: [`
  .table-ellipsis {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    max-width: 350px;
}
  `]
})
export class MembershipListComponent implements OnInit {
  membershipDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  Status: any;
  searchStatus: any;
  query = '';
  collectionSize: number = 0;
  page: any;
  pageSize: any;
  pageSizeList: any;
  isLoading: boolean;
  sortColumn: { sortBy: any; sortOrder: string; };
  searchUpdate = new Subject<string>();
  constructor(
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService, private member: MembershipService) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.membershipSearch();
      });
  }

  ngOnInit() {
    this.isLoading = false;
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.MemberShipSetup,
      sortOrder: ApplicationConfig.Sorting.SortOrder.MemberShipSetup.order
    }
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.Status = [{ id: 0, Value: "InActive" }, { id: 1, Value: "Active" }, { id: 2, Value: "All" }];
    this.searchStatus = "";
    this.getAllMembershipDetails();
  }

  // Get All Membership
  getAllMembershipDetails() {
    this.isLoading = true;
    this.member.getMembership().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.membershipDetails = membership.Membership;
        this.membershipDetails = this.membershipDetails.filter(item => item.IsActive === true);
        if (this.membershipDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort(ApplicationConfig.Sorting.SortBy.MemberShipSetup);

          this.collectionSize = Math.ceil(this.membershipDetails.length / this.pageSize) * 10;
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
    this.getAllMembershipDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllMembershipDetails();
  }

  sort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.MemberShipSetup.order
    }
    this.sorting(this.sortColumn)
    this.selectedCls(this.sortColumn)

  }
  sorting(sortColumn) {
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
    let property = sortColumn.sortBy;
    this.membershipDetails.sort(function (a, b) {
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


  membershipSearch() {
    this.page = 1;
    const obj = {
      membershipSearch: this.query
    };
    this.isLoading = true;
    this.member.searchMembership(obj).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.membershipDetails = membership.MembershipSearch;
        if (this.membershipDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort(ApplicationConfig.Sorting.SortBy.MemberShipSetup);

          this.collectionSize = Math.ceil(this.membershipDetails.length / this.pageSize) * 10;
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


  delete(data) {
    this.spinner.show();
    this.member.deleteRestrictionMembershipVehicle(data.MembershipId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const vehicle = JSON.parse(res.resultData);
        if (vehicle.VehicleMembershipByMembershipId == false) {
          this.confirmationService.confirm('Delete Membership', `Are you sure you want to delete this membership? All related 
  information will be deleted and the membership cannot be retrieved?`, 'Yes', 'No')
            .then((confirmed) => {
              if (confirmed === true) {
                this.confirmDelete(data);
              }

            })
            .catch(() => { });
        }

        else {
          this.spinner.hide();

          this.toastr.warning(MessageConfig.Admin.SystemSetup.MemberShipSetup.DeleteRestrict, 'Warning!');

        }

      }


    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });


  }

  // Delete Membership
  confirmDelete(data) {
    this.spinner.show();
    this.member.deleteMembership(data.MembershipId).subscribe(res => {
      if (res.status === "Success") {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.MemberShipSetup.Delete, 'Success');
        this.getAllMembershipDetails();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllMembershipDetails();
    }
    this.showDialog = event.isOpenPopup;
  }

  add(data, det?) {
    if (data === 'add') {
      this.headerData = 'Add New Membership';
      this.selectedData = det;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.getMembershipById(det);
    }
  }

  // Get Membership By Id
  getMembershipById(det) {
    this.spinner.show();
    this.member.getMembershipById(det.MembershipId).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const membership = JSON.parse(data.resultData);
        const details = membership.MembershipAndServiceDetail;
        this.headerData = 'Edit Membership';
        this.selectedData = details;
        this.isEdit = true;
        this.showDialog = true;
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

