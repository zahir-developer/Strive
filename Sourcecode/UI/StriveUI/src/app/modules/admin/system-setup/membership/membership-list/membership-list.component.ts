import { Component, OnInit } from '@angular/core';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-membership-list',
  templateUrl: './membership-list.component.html',
  styleUrls: ['./membership-list.component.css']
})
export class MembershipListComponent implements OnInit {
  membershipDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  Status:any;
  searchStatus:any;
 
  query = '';
  collectionSize: number = 0;
  page: any;
  pageSize: any;
  pageSizeList: any;
  isDesc: boolean = false;
  column: string = 'MembershipName';
  constructor(private toastr: MessageServiceToastr, 
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService, private member: MembershipService) { }

  ngOnInit() {
    this.page= ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.Status = [{id : 0,Value :"InActive"}, {id :1 , Value:"Active"}, {id :2 , Value:"All"}];
    this.searchStatus = "";
    this.getAllMembershipDetails();
  }

  // Get All Membership
  getAllMembershipDetails() {
this.spinner.show();
    this.member.getMembership().subscribe(data => {
      this.spinner.hide()
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.membershipDetails = membership.Membership;
        if (this.membershipDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort('MembershipName')
          this.collectionSize = Math.ceil(this.membershipDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  paginate(event) {
    
    this.pageSize= +this.pageSize;
    this.page = event ;
    
    this.getAllMembershipDetails()
  }
  paginatedropdown(event) {
    this.pageSize= +event.target.value;
    this.page =  this.page;
    
    this.getAllMembershipDetails()
  }
  sort(property) {
    this.isDesc = !this.isDesc; //change the direction    
    this.column = property;
    let direction = this.isDesc ? 1 : -1;
   
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
 
  

  membershipSearch(){
    this.page = 1;
    const obj ={
       membershipSearch: this.query
    }
    this.spinner.show();
    this.member.searchMembership(obj).subscribe(data => {
      this.spinner.hide()
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.membershipDetails = membership.MembershipSearch;
        if (this.membershipDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.membershipDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }


  delete(data) {
    this.confirmationService.confirm('Delete Membership', `Are you sure you want to delete this membership? All related 
  information will be deleted and the membership cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Membership
  confirmDelete(data) {
    this.member.deleteMembership(data.MembershipId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Membership Deleted Successfully!!' });
        this.getAllMembershipDetails();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
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
    this.member.getMembershipById(det.MembershipId).subscribe(data => {
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);        
        const details = membership.MembershipAndServiceDetail;
        this.headerData = 'Edit Membership';
        this.selectedData = details;
        this.isEdit = true;
        this.showDialog = true;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
}

