import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { Router } from '@angular/router';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

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
  sort = { column: 'ClientName', descending: true };
  sortColumn: { column: string; descending: boolean; };
  pageSizeList: number[];
  constructor(
    private vehicle: VehicleService,
    private toastr: ToastrService,
    private router: Router,
    private confirmationService: ConfirmationUXBDialogService
  ) { }

  ngOnInit() {
    this.page= ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllVehicleDetails();
    this.getService();
  }
  paginate(event) {
    
    this.pageSize= +this.pageSize;
    this.page = event ;
    
    this.getAllVehicleDetails()
  }
  paginatedropdown(event) {
    this.pageSize= +event.target.value;
    this.page =  this.page;
    
    this.getAllVehicleDetails()
  }
  // Get All Vehicles
  getAllVehicleDetails() {
    const obj = {
      searchName: ""
    }
    this.vehicle.getVehicle(obj).subscribe(data => {
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
    });
  }

  navigateToClient(vehicle) {
    this.router.navigate(['/admin/client'], { queryParams: { clientId: vehicle.ClientId } });
  }

  vehicleSearch() {
    this.page = 1;
    const obj = {
      searchName: this.search
    }
    this.vehicle.getVehicle(obj).subscribe(data => {
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
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
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
        this.upchargeServices = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === 'Upcharges');
        this.additionalService = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === 'Additional Services');
      }
    });
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
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

