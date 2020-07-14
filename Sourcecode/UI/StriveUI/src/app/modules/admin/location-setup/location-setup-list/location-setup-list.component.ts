import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-location-setup-list',
  templateUrl: './location-setup-list.component.html',
  styleUrls: ['./location-setup-list.component.css'],
  providers: [ConfirmationService]
})
export class LocationSetupListComponent implements OnInit {
  locationSetupDetails = [];
  locationSetupForm: FormGroup;
  showDialog = false;
  selectedData: any;
  headerData: string;
  searchByName = '';
  searchById = '';
  isEdit: boolean;
  isTableEmpty: boolean;
  constructor(private locationService: LocationService, private toastr: ToastrService, private fb: FormBuilder,
              private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.locationSetupForm = this.fb.group({
      workHour: ['', Validators.required, Validators.maxLength(2)]
    });

    this.getAllLocationSetupDetails();

  }
  getAllLocationSetupDetails() {
    this.locationService.getLocation().subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationSetupDetails = location.Location;
        console.log(this.locationSetupDetails);
        if (this.locationSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.isTableEmpty = false;
        }
      }
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Yes',
      rejectLabel: 'No',
      accept: () => {
        this.locationService.deleteLocation(data.LocationId).subscribe(res => {
          if (res.status === 'Success') {
            this.toastr.success('Record Deleted Successfully!!', 'Success!');
          }else{
            this.toastr.error('Communication Error','Error!');
          }
        });
      },
 reject: () => {
      }
    });
    // const index = this.locationSetupDetails.map(x => x.id).indexOf(data.id);
    // this.confirmationService.confirm({
    //   header: 'Delete',
    //   message: 'Do you want to continue?',
    //   acceptLabel: 'Yes',
    //   rejectLabel: 'Cancel',
    //   accept: () => {
    //   },
    //   reject: () => {
    //   }
    // });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllLocationSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, locationDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Location';
      this.selectedData = locationDetails;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.headerData = 'Edit Location';
      this.selectedData = locationDetails;
      this.isEdit = true;
      this.showDialog = true;
    }
  }
}
