import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { Router } from '@angular/router';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { DomSanitizer } from '@angular/platform-browser';

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
  search: any = '';
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  additionalService: any = [];
  upchargeServices: any = [];
  sort = { column: 'VehicleNumber', descending: true };
  sortColumn: { column: string; descending: boolean; };
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
  constructor(
    private vehicle: VehicleService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService,
    private memberService: MembershipService,
    private adSetup: AdSetupService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.imagePopup = false;
    this.isOpenImage = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
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
      sortOrder: this.sort.descending ? 'DESC' : 'ASC',
      sortBy: this.sort.column,
      status: true
    };
    this.spinner.show();
    this.vehicle.getVehicle(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.vehicleDetails = [];
        const vehicle = JSON.parse(data.resultData);
        let totalCount = 0;
        if (vehicle.Vehicle.clientViewModel !== null) {
          this.vehicleDetails = vehicle.Vehicle.clientViewModel;
          totalCount = vehicle.Vehicle.Count.Count;
          if (this.vehicleDetails.length === 0) {
            this.isTableEmpty = true;
          } else {
            this.collectionSize = Math.ceil(totalCount / this.pageSize) * 10;
            this.isTableEmpty = false;
          }
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  previewImage() {
    this.vehicle.getAllVehicleThumbnail(49233).subscribe(data => {
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
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  openImage(base64Value) {
    this.isOpenImage = true;
    this.originalImage = base64Value.vehicleImage;
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
      this.spinner.hide();
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Vehicle;
        if (this.vehicleDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    }, (err) => {
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
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembership) {
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
          this.toastr.error('Could not Delete the Vehicle due to  Assigned the Membership', 'Error!')
        }
      }
    });
  }

  // Delete vehicle
  confirmDelete(data) {
    this.vehicle.deleteVehicle(data.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllVehicleDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
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
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getService() {
    this.vehicle.getMembershipService().subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.upchargeServices = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === 'Wash-Upcharge');
        this.additionalService = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === 'Additonal Services');
      }
    });
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
    this.getAllVehicleDetails();
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }
}

