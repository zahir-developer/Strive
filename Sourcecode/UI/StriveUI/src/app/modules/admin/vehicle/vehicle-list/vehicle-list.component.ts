import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicleDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isView: boolean;
  selectedVehicle: any;
  constructor(private vehicle: VehicleService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.getAllVehicleDetails();

  }
  getAllVehicleDetails() {
    this.vehicle.getVehicle().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Vehicle;
        console.log(this.vehicleDetails);
        if (this.vehicleDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
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
    this.confirmationService.confirm('Delete Vehicle', `Are you sure you want to delete this vehicle? All related 
    information will be deleted and the vehicle cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          //this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }
  // confirmDelete(data) {
  //   this.vehicle.deletevehicle(data.vehicleId).subscribe(res => {
  //     if (res.status === 'Success') {
  //       this.toastr.success('Record Deleted Successfully!!', 'Success!');
  //       this.getAllvehicleDetails();
  //     } else {
  //       this.toastr.error('Communication Error', 'Error!');
  //     }
  //   });
  // }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllVehicleDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, vehicleDet?) {
    if (data === 'add') {
      this.headerData = 'Add New vehicle';
      this.showDialog = true;
      this.selectedData = vehicleDet;
      this.isEdit = false;
      this.isView = false;
    } else {
      //this.getVehicleById(data, vehicleDet);
    }
  }

  // getVehicleById(data, vehicle) {
  //   this.vehicle.getvehicleById(vehicle.vehicleId).subscribe(res => {
  //     if (res.status === 'Success') {
  //       const vehicle = JSON.parse(res.resultData);
  //       this.selectedVehicle = vehicle.vehicleDetail[0];
  //       if (data === 'edit') {
  //         this.headerData = 'Edit vehicle';
  //         this.selectedData = this.selectedVehicle;
  //         this.isEdit = true;
  //         this.isView = false;
  //         this.showDialog = true;
  //       } else {
  //         this.headerData = 'View vehicle';
  //         this.selectedData = this.selectedVehicle;
  //         this.isEdit = true;
  //         this.isView = true;
  //         this.showDialog = true;
  //       }
  //     } else {
  //       this.toastr.error('Communication Error', 'Error!');
  //     }
  //   });
  // }
}

