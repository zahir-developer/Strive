import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import * as moment from 'moment';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientStatementComponent } from '../client-statement/client-statement.component';
import { ClientHistoryComponent } from '../client-history/client-history.component';

@Component({
  selector: 'app-client-create-edit',
  templateUrl: './client-create-edit.component.html',
  styleUrls: ['./client-create-edit.component.css']
})
export class ClientCreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  clientForm: FormGroup;
  Status: any;
  State: any;
  Score: any;
  address: any;
  selectedLocation: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  selectedStateId: any;
  selectedCountryId: any;
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
  Type: { id: number; Value: string; }[];
  deleteIds = [];
  submitted: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private client: ClientService,
    private confirmationService: ConfirmationUXBDialogService,
    private modalService: NgbModal, private vehicle: VehicleService, private getCode: GetCodeService) { }

  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.formInitialize();
    if (this.isView === true) {
      this.viewClient();
    }
    if (this.isEdit === true) {
      this.clientForm.reset();
      this.getClientById();
      this.getClientVehicle(this.selectedData.ClientId);
    }
  }

  formInitialize() {
    this.clientForm = this.fb.group({
      fName: ['',],
      lName: ['',],
      address: ['',],
      zipcode: ['',],
      state: ['',],
      city: ['',],
      phone1: ['',],
      email: ['', Validators.email],
      phone2: ['',],
      creditAccount: ['',],
      noEmail: ['',],
      score: ['',],
      status: ['',],
      notes: ['',],
      checkOut: ['',],
      type: ['',]
    });
    this.clientForm.get('status').patchValue(0);
    this.getClientType();
    this.getScore();
  }

  get f() {
    return this.clientForm.controls;
  }

  // Get Score
  getScore() {
    this.client.getClientScore().subscribe(data => {
      if (data.status === 'Success') {
        const client = JSON.parse(data.resultData);
        this.Score = client.ClientDetails;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  // Get ClientType
  getClientType() {
    this.getCode.getCodeByCategory("CLIENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.Type = cType.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.vehicle.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Status;
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

  getClientById() {
    this.selectedStateId = this.selectedData.State;
    this.State = this.selectedStateId;
    this.clientForm.patchValue({
      fName: this.selectedData.FirstName,
      lName: this.selectedData.LastName,
      noEmail: this.selectedData.NoEmail,
      status: this.selectedData.IsActive ? 0 : 1,
      score: this.selectedData.Score,
      type: this.selectedData.ClientType,
      notes: this.selectedData.Notes,
      checkOut: this.selectedData.RecNotes,
      address: this.selectedData.Address1,
      phone1: this.selectedData.PhoneNumber,
      zipcode: this.selectedData.Zip,
      phone2: this.selectedData.PhoneNumber2,
      email: this.selectedData.Email
    });
    this.clientId = this.selectedData.ClientId;
  }

  viewClient() {
    this.clientForm.disable();
  }

  change(data) {
    this.clientForm.value.creditAccount = data;
  }

  // Add/Update Client
  submit() {
    this.submitted = true;
    if (this.clientForm.invalid) {
      return;
    }
    this.address = [{
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientForm.value.address,
      address2: "",
      phoneNumber2: this.clientForm.value.phone2,
      isActive: true,
      zip: this.clientForm.value.zipcode,
      state: this.State,
      city: 1,
      country: 38,
      phoneNumber: this.clientForm.value.phone1,
      email: this.clientForm.value.email,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientForm.value.fName,
      middleName: "",
      lastName: this.clientForm.value.lName,
      gender: 1,
      maritalStatus: 1,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: this.clientForm.value.status == 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
      notes: this.clientForm.value.notes,
      recNotes: this.clientForm.value.checkOut,
      score: (this.clientForm.value.score == "" || this.clientForm.value.score == null) ? 0 : this.clientForm.value.score,
      noEmail: false,
      clientType: (this.clientForm.value.type == "" || this.clientForm.value.type == null) ? 0 : this.clientForm.value.type
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
            this.vehicle.deleteVehicle(element.ClientVehicleId).subscribe(res => {
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
          this.clientForm.reset();
        }
      });
    } else {
      this.client.addClient(myObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.clientForm.reset();
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  getSelectedStateId(event) {
    this.State = event.target.value;
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.vehicleDetails.push(this.vehicle.vehicleValue);
      this.vehicleDet.push(this.vehicle.addVehicle);
      this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
      this.showVehicleDialog = false;
    }
    this.showVehicleDialog = event.isOpenPopup;
  }
  delete(data){
    if(this.isView){
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
}

