import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { Router } from '@angular/router';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';

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
  TimeInFormat: any;
  washListDetails = [];
  constructor(private washes: WashService, private toastr: ToastrService,
    private datePipe: DatePipe,

    private confirmationService: ConfirmationUXBDialogService, private router: Router) { }

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
       
        for(let i = 0; i < this.washDetails.length; i ++){
  let hh = this.washDetails[i].TimeIn.substring(13, 11);
  let m = this.washDetails[i].TimeIn.substring(16, 14);
  var s = this.washDetails[i].TimeIn.substring(19, 17);
 let min = m ;

  let sec = s ;
  var dd = "AM";
  var h = hh;
  if (h >= 12) {
    h = hh - 12;
    dd = "PM";
  }
  if (h == 0) {
    h = 12;
  }
 let inTimeFormat =   hh + ":" + min + ":" + sec + dd;
 let outhh = this.washDetails[i].EstimatedTimeOut.substring(13, 11);
  let outm = this.washDetails[i].EstimatedTimeOut.substring(16, 14);
  var outs = this.washDetails[i].EstimatedTimeOut.substring(19, 17);
 let outmin = outm ;

  let outsec = s ;
  var outdd = "AM";
  var outh = outhh;
  if (outh >= 12) {
    outh = outhh - 12;
    outdd = "PM";
  }
  if (h == 0) {
    h = 12;
  }
 let outTimeFormat =   outhh + ":" + outmin + ":" + outsec + outdd;
          this.washListDetails.push({
            'TicketNumber' : this.washDetails[i].TicketNumber,
            'ClientName' : this.washDetails[i].ClientName,
            'PhoneNumber' : this.washDetails[i].PhoneNumber,
             'VehicleName' : this.washDetails[i].VehicleName,
             'ServiceName' : this.washDetails[i].ServiceName,
             'EstimatedTimeOut' : outTimeFormat,
            'TimeIn' :inTimeFormat

          
          })
        }
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
}
