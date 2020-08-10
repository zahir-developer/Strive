import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

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
  constructor(private vehicle: VehicleService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.getAllWashDetails();

  }
  getAllWashDetails() {
    this.vehicle.getVehicle().subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.washDetails = wash.Vehicle;
        if (this.washDetails.length === 0) {
          this.isTableEmpty = true;
        } else {      
          this.collectionSize = Math.ceil(this.washDetails.length/this.pageSize) * 10;
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
          //this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }
  confirmDelete(data) {
    this.vehicle.deleteVehicle(data.ClientVehicleId).subscribe(res => {
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
    } else if (data === 'edit') {
      this.headerData = 'Edit Service';
      this.selectedData = washDetails;
      this.isEdit = true;
      this.isView = false;
      this.showDialog = true;
    } else {
      this.headerData = 'View Service';
      this.selectedData = washDetails;
      this.isEdit = true;
      this.isView = true;
      this.showDialog = true;
    }
  }

}
