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
  search : any = '';
  isEdit: boolean;
  isTableEmpty: boolean;
  selectedLocation: any;
  isLoading = true;
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  constructor(private locationService: LocationService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, private uiLoaderService: NgxUiLoaderService) { }

  ngOnInit() {
    this.getAllLocationSetupDetails();

  }

  // get all location
  getAllLocationSetupDetails() {
    this.isLoading =  true;
    this.locationService.getLocation().subscribe(data => {
      this.isLoading =  false;
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
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
  
  // Get Location Search
  locationSearch(){
    const obj ={
       locationSearch: this.search
    }
    this.locationService.LocationSearch(obj).subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationSetupDetails = location.Search;
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

  // Delete location
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


  // get location detail by locationId
  getLocationById(data) {
    this.locationService.getLocationById(data.LocationId).subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
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
