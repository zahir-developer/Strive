import { Component, OnInit, ViewChild } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { Router } from '@angular/router';
import { HomeNavService } from 'src/app/shared/common-service/home-nav.service';

@Component({
  selector: 'app-washes-list',
  templateUrl: './washes-list.component.html',
  styleUrls: ['./washes-list.component.css']
})
export class WashesListComponent implements OnInit {  
  washDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isView: boolean;
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  dashboardDetails: any;
  locationId = +localStorage.getItem('empLocationId');
  constructor(private washes: WashService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, private router: Router, private homeNavigation: HomeNavService) { }

  ngOnInit() {

    const obj = {
      id: this.locationId,
      date: new Date()
    };
    this.washes.getDashBoard(obj);
    this.getAllWashDetails();
  }


  // Get All Washes
  getAllWashDetails() {
    this.washes.getAllWashes(this.locationId).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.washDetails = wash.Washes;
        console.log(wash);
        if (this.washDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.washDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    console.log(data);
    this.confirmationService.confirm('Delete Wash', `Are you sure you want to delete this Wash? All related 
    information will be deleted and the Wash cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Wash
  confirmDelete(data) {
    this.washes.deleteWash(data.JobId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllWashDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      const obj = {
        id: this.locationId,
        date: new Date()
      };
      this.washes.getDashBoard(obj);
      this.getAllWashDetails();
    }
    this.showDialog = event.isOpenPopup;
  }

  add(data, washDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Service';
      this.selectedData = washDetails;
      this.isEdit = false;
      this.isView = false;
      this.showDialog = true;
    } else {
      this.getWashById(data, washDetails);
    }
  }

  // Get Wash By Id
  getWashById(label, washDet) {
    this.washes.getWashById(washDet.JobId).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        if (label === 'edit') {
          this.headerData = 'Edit Service';
          this.selectedData = wash.WashesDetail;
          this.isEdit = true;
          this.isView = false;
          this.showDialog = true;
        } else {
          this.headerData = 'View Service';
          this.selectedData = wash.WashesDetail;
          this.isEdit = true;
          this.isView = true;
          this.showDialog = true;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  pay(wash) {
    this.router.navigate(['/sales'], { queryParams: { ticketNumber: wash.TicketNumber } });
  }  

  loadLandingPage(){
    this.homeNavigation.loadLandingPage();
  }
}
