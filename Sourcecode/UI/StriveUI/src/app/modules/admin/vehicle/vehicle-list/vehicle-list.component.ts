import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html'
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
  search: any = '';
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  additionalService: any = [];
  upchargeServices: any = [];

  pageSizeList: number[];
  memberServiceId: any;
  vehicleslist: any;
  imagePopup: boolean;
  imgData: any;
  linkSource: string;
  base64: any;
  imgbase64: any;
  imageList = [];
  isOpenImage: boolean;
  originalImage = '';
  sortColumn: { sortBy: string; sortOrder: string; };
  searchUpdate = new Subject<string>();

  constructor(
    private vehicle: VehicleService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService,
    private memberService: MembershipService,
    private route: ActivatedRoute
  ) { 
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.page = 1;
        this.getAllVehicleDetails();
      });
  }

  ngOnInit() {
    this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.Vehicle, sortOrder: ApplicationConfig.Sorting.SortOrder.Vehicle.order };

    this.imagePopup = false;
    this.isOpenImage = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    const paramsData = this.route.snapshot.queryParamMap.get('vehicleId');
    if (paramsData !== null) {
      const clientObj = {
        ClientVehicleId: paramsData
      };
      this.getVehicleById('view', clientObj);
    }
    this.getAllVehicleDetails();
    this.getService();
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllVehicleDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
    this.getAllVehicleDetails();
  }
  // Get All Vehicles
  getAllVehicleDetails() {
    const obj = {
      startDate: null,
      endDate: null,
      locationId: null,
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search !== '' ? this.search : null,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: true
    };
    this.spinner.show();
    this.vehicle.getVehicle(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        this.vehicleDetails = [];
        const vehicle = JSON.parse(data.resultData);
        let totalCount = 0;

        if (vehicle.Vehicle.clientViewModel !== null) {
          this.vehicleDetails = vehicle.Vehicle.clientViewModel;
          if (this.vehicleDetails?.length > 0) {
            for (let i = 0; i < this.vehicleDetails.length; i++) {
              this.vehicleDetails[i].ModelName == 'None' ? this.vehicleDetails[i].ModelName =  'Unk' : this.vehicleDetails[i].ModelName ;
            }
          }
          totalCount = vehicle.Vehicle.Count.Count;
          if (this.vehicleDetails.length === 0) {
            this.isTableEmpty = true;
          } else {
            this.collectionSize = Math.ceil(totalCount / this.pageSize) * 10;
            this.isTableEmpty = false;
          }
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  previewImage(vehicle) {
    this.vehicle.getAllVehicleThumbnail(vehicle.ClientVehicleId).subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        console.log(sType, 'image');
        this.imagePopup = true;
        if (sType.VehicleThumbnails.length > 0) {
          this.imageList = sType.VehicleThumbnails;
          this.imageList.forEach( item => {
            item.vehicleImage = 'data:image/png;base64,' + item.Base64Thumbnail;
          });
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  openImage(base64Value) {
    this.vehicle.getVehicleImageById(base64Value.VehicleImageId).subscribe( res => {
      if (res.status === 'Success') {
        const image = JSON.parse(res.resultData);
        this.originalImage = 'data:image/png;base64,' + image.VehicleThumbnails.Base64Thumbnail;
        this.isOpenImage = true;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  navigateToClient(vehicle) {
    this.router.navigate(['/admin/client'], { queryParams: { clientId: vehicle.ClientId } });
  }

  vehicleSearch() {
    this.page = 1;
    const obj = {
      searchName: this.search
    };
    this.spinner.show();
    this.vehicle.getVehicle(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Vehicle;
        if (this.vehicleDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }

  delete(data) {
    this.vehicle.getVehicleMembershipDetailsByVehicleId(data.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembership == null) {
          this.confirmationService.confirm('Delete Vehicle', `Are you sure you want to delete this vehicle? All related 
          information will be deleted and the vehicle cannot be retrieved?`, 'Yes', 'No')
            .then((confirmed) => {
              if (confirmed === true) {
                this.confirmDelete(data);
              }
            })
            .catch(() => { });
        }
        else {
          this.toastr.warning(MessageConfig.Admin.Vehicle.memberShip, 'Warning!');

        }
      }
    });
  }

  // Delete vehicle
  confirmDelete(data) {
    this.spinner.show();
    this.vehicle.deleteVehicle(data.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.Vehicle.Delete, 'Success!');
        this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.Vehicle, sortOrder: ApplicationConfig.Sorting.SortOrder.Vehicle.order };
        this.page = 1;
        this.getAllVehicleDetails();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved' || event.status === 'edit') {
      this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.Vehicle, sortOrder: ApplicationConfig.Sorting.SortOrder.Vehicle.order };

      this.getAllVehicleDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, vehicleDet?) {
    this.getVehicleById(data, vehicleDet);
  }

  // Get Vehicle By Id
  getVehicleById(data, vehicleDet) {
    this.vehicle.getVehicleById(vehicleDet.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        this.selectedVehicle = vehicle.Status;
        if (data === 'edit') {
          this.headerData = 'Edit Vehicle';
          this.selectedData = this.selectedVehicle;
          this.isEdit = true;
          this.isView = false;
          this.showDialog = true;
        } else {
          this.headerData = 'View Vehicle';
          this.selectedData = this.selectedVehicle;
          this.isEdit = true;
          this.isView = true;
          this.showDialog = true;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getService() {
    this.vehicle.getMembershipService().subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.upchargeServices = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.WashUpcharge);
        this.additionalService = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.AdditonalServices);
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  changeSorting(column) {
    this.sortColumn ={
     sortBy: column,
     sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
   this.getAllVehicleDetails();
 }

 
newgetAllVehicleDetails()
{
  this.page = 1;
  this.getAllVehicleDetails();
}

 selectedCls(column) {
   if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'DESC') {
     return 'fa-sort-desc';
   } else if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'ASC') {
     return 'fa-sort-asc';
   }
   return '';
 }

}

