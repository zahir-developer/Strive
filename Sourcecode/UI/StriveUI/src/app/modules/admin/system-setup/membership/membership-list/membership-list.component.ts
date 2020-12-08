import { Component, OnInit } from '@angular/core';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';

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
  isLoading = true;
  page = 1;
  pageSize = 5;
  query = '';
  collectionSize: number = 0;
  constructor(private toastr: MessageServiceToastr, private confirmationService: ConfirmationUXBDialogService, private member: MembershipService) { }

  ngOnInit() {
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

