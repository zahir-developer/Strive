import { Component, OnInit } from '@angular/core';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
  selector: 'app-location-setup-list',
  templateUrl: './location-setup-list.component.html',
  styleUrls: ['./location-setup-list.component.css']
})
export class LocationSetupListComponent implements OnInit {
  locationSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  searchByName = '';
  searchById = '';
  isEdit: boolean;
  isTableEmpty: boolean;
  selectedLocation: any;
  isLoading = true;
  page = 1;
  pageSize = 5;
  collectionSize: number;
  constructor(private locationService: LocationService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, private uiLoaderService: NgxUiLoaderService) { }

  ngOnInit() {
    this.getAllLocationSetupDetails();

  }
  getAllLocationSetupDetails() {
    this.isLoading =  true;
    this.locationService.getLocation().subscribe(data => {
      this.isLoading =  false;
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        console.log(location, 'location');
        // this.locationSetupDetails = location.Location.filter(item => item.IsActive === true);
        this.locationSetupDetails = location.Location;
        if (this.locationSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.locationSetupDetails.length / this.pageSize) * 10;
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
    this.confirmationService.confirm('Delete Location', `Are you sure you want to delete this location? All related 
    information will be deleted and the location cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }
  confirmDelete(data) {
    this.locationService.deleteLocation(data.LocationId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllLocationSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
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
      this.getLocationById(locationDetails);
    }
  }

  getLocationById(data) {
    this.locationService.getLocationById(data.LocationId).subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        console.log(location, 'locationByid');
        this.selectedLocation = location.Location;
        this.headerData = 'Edit Location';
        this.selectedData = this.selectedLocation;
        this.isEdit = true;
        this.showDialog = true;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

}
