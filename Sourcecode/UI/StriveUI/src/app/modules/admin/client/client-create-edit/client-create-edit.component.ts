import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientStatementComponent } from '../client-statement/client-statement.component';
import { ClientHistoryComponent } from '../client-history/client-history.component';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';

@Component({
  selector: 'app-client-create-edit',
  templateUrl: './client-create-edit.component.html',
  styleUrls: ['./client-create-edit.component.css']
})
export class ClientCreateEditComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  address: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  vehicleDetails = [];
  vehicleDet = [];
  isTableEmpty: boolean;
  headerData: string;
  selectedVehicle: any;
  showVehicleDialog: boolean;
  clientId: number = 0;
  page = 1;
  pageSize = 3;
  collectionSize: number = 0;
  deleteIds = [];
  additionalService: any = [];
  vehicleNumber: number;
  sort = { column: 'VehicleNumber', descending: true };
  sortColumn: { column: string; descending: boolean; };
  employeeId: number;
  constructor(private toastr: ToastrService, private client: ClientService,
    private confirmationService: ConfirmationUXBDialogService,
    private modalService: NgbModal, private vehicle: VehicleService) { }

  ngOnInit() {    
    this.employeeId = +localStorage.getItem('empId');

    if (this.isEdit === true) {
      this.getClientVehicle(this.selectedData.ClientId);
    }
    else{
      this.vehicleNumber = 1;
    }
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.vehicle.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Status;
        if (this.vehicleDetails.length === 0) {
          this.isTableEmpty = true;
          this.vehicleNumber = 1;
        } else {
          let len = this.vehicleDetails.length;
          this.vehicleNumber = Number(this.vehicleDetails[len-1].VehicleNumber) + 1;
          console.log(this.vehicleNumber);
          this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  
  // Add/Update Client
  submit() {
    this.clientFormComponent.submitted = true;
    this.clientFormComponent.stateDropdownComponent.submitted = true;
    if (this.clientFormComponent.clientForm.invalid) {
      return;
    }
    if (this.clientFormComponent.ClientNameAvailable == true) {
      this.toastr.error('Client Name is Already Entered', 'Error!');

      return;
    }
    this.address = [{
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientFormComponent.clientForm.value.address,
      address2: "",
      phoneNumber2: this.clientFormComponent.clientForm.value.phone2,
      isActive: true,
      zip: this.clientFormComponent.clientForm.value.zipcode,
      state: this.clientFormComponent.State,
      city: this.clientFormComponent.city == 0 ? null : this.clientFormComponent.city,
      country: 38,
      phoneNumber: this.clientFormComponent.clientForm.value.phone1,
      email: this.clientFormComponent.clientForm.value.email,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date()
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: "",
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: 1,
      maritalStatus: 1,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: Number(this.clientFormComponent.clientForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score == "" || this.clientFormComponent.clientForm.value.score == null) ? 0 : this.clientFormComponent.clientForm.value.score,
      noEmail: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type == "" || this.clientFormComponent.clientForm.value.type == null) ? 0 : this.clientFormComponent.clientForm.value.type,
      amount : this.clientFormComponent.clientForm.value.amount
    };
    const myObj = {
      client: formObj,
      clientVehicle: this.vehicleDet.length === 0 ? null : this.vehicleDet,
      clientAddress: this.address
    }
    if (this.isEdit === true) {
      this.client.updateClient(myObj).subscribe(data => {
        if (data.status === 'Success') {
          this.deleteIds.forEach(element => {
            this.vehicle.deleteVehicle(element.VehicleId).subscribe(res => {
              if (res.status === 'Success') {
                this.toastr.success('Vehicle Deleted Successfully!!', 'Success!');
              } else {
                this.toastr.error('Communication Error', 'Error!');
              }
            });
          })
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.clientFormComponent.clientForm.reset();
        }
      });
    } else {
      this.client.addClient(myObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.clientFormComponent.clientForm.reset();
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.vehicleDetails.push(this.vehicle.vehicleValue);
      let len = this.vehicleDetails.length;
      this.vehicleNumber = Number(this.vehicleDetails[len-1].VehicleNumber) + 1;
      console.log(this.vehicleDetails,'vedel');
      this.vehicleDet.push(this.vehicle.addVehicle);
      this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
      this.showVehicleDialog = false;
    }
    this.showVehicleDialog = event.isOpenPopup;
  }
  delete(data) {
    if (this.isView) {
      return;
    }
    this.confirmationService.confirm('Delete Vehicle', `Are you sure you want to delete this vehicle? All related 
    information will be deleted and the vehicle cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Vehicle 
  confirmDelete(data) {
    this.vehicleDetails = this.vehicleDetails.filter(item => item !== data);
    let len = this.vehicleDetails.length;
    this.vehicleNumber = Number(this.vehicleDetails[len-1].VehicleNumber) + 1;
    this.vehicleDet = this.vehicleDet.filter(item => item.Barcode !== data.Barcode);
    this.toastr.success('Record Deleted Successfully!!', 'Success!');
    this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
    if (data.ClientVehicleId !== 0) {
      this.deleteIds.push(data);
    }
  }

  // Add New Vehicle
  add() {
    this.headerData = 'Add New vehicle';
    this.showVehicleDialog = true;
    this.clientId = this.selectedData.ClientId;
  }

  openStatement() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(ClientStatementComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
  }

  openHistory() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(ClientHistoryComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
  }

  getService() {
    this.vehicle.getMembershipService().subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
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

