import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import * as moment from 'moment';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';

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
  vehicleDetails: any =[];
  isTableEmpty: boolean;
  headerData: string;
  selectedVehicle: any;
  showVehicleDialog: boolean;
  page = 1;
  pageSize = 3;
  collectionSize: number;
  Type: { id: number; Value: string; }[];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private client: ClientService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.Score = [{ id: 0, Value: "Score1" }, { id: 1, Value: "Score2" }];
    this.Type = [{ id: 0, Value: "Type1" }, { id: 1, Value: "Type2" }];
    this.formInitialize();
    if (this.isView === true) {
      this.viewClient();
    }
    if (this.isEdit === true) {
      this.clientForm.reset();
      this.getClientById();
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
      email: ['',],
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
  }

  getClientVehicle(id) {
    this.vehicle.getVehicleById(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Status;
        console.log(this.vehicleDetails);
        this.vehicleDetails = this.vehicleDetails;
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
    const clientAddress = this.selectedData.ClientAddress[0];
    this.selectedStateId = clientAddress.State;
    this.State = this.selectedStateId;
    this.clientForm.patchValue({
      fName: this.selectedData.FirstName,
      lName: this.selectedData.LastName,
      noEmail: this.selectedData.NoEmail,
      status: this.selectedData.IsActive ? 0 : 1,
      score: this.selectedData.Score,
      notes: this.selectedData.Notes,
      checkOut: this.selectedData.RecNotes,
      address: clientAddress.Address1,
      phone1: clientAddress.PhoneNumber,
      zipcode: clientAddress.Zip,
      phone2: clientAddress.PhoneNumber2,
      email: clientAddress.Email,
      city: clientAddress.City
    });    
    this.getClientVehicle(this.selectedData.ClientId);
  }

  viewClient() {
    this.clientForm.disable();
  }

  change(data) {
    this.clientForm.value.creditAccount = data;
  }

  submit() {
    this.address = [{
      relationshipId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddress[0].ClientAddressId : 0,
      address1: this.clientForm.value.address,
      address2: "",
      phoneNumber2: this.clientForm.value.phone2,
      isActive: true,
      zip: this.clientForm.value.zipcode,
      state: this.State,
      city: this.clientForm.value.city !== "" ? this.clientForm.value.city : 0,
      country: 38,
      phoneNumber: this.clientForm.value.phone1,
      email: this.clientForm.value.email
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientForm.value.fName,
      middleName: "",
      lastName: this.clientForm.value.lName,
      gender: 0,
      maritalStatus: 0,
      birthDate: new Date(),
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      isActive: this.clientForm.value.status == 0 ? true : false,
      notes: this.clientForm.value.notes,
      recNotes: this.clientForm.value.checkOut,
      score: (this.clientForm.value.score == "" || this.clientForm.value.score == null) ? 0 : this.clientForm.value.score,
      noEmail: false,
      clientAddress: this.address,
      clientType: (this.clientForm.value.type == "" || this.clientForm.value.type == null) ? 0 : this.clientForm.value.type
    };
    this.client.updateClient(formObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.clientForm.reset();
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
    this.vehicle.addVehicle = [];
  }
  getSelectedStateId(event) {
    this.State = event.target.value;
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.vehicleDetails = this.vehicle.addVehicle;       
      this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
      console.log(this.vehicleDetails);
      this.showVehicleDialog = false;
    }
    this.showVehicleDialog = event.isOpenPopup;
  }
  add() {
    this.headerData = 'Add New vehicle';
    this.showVehicleDialog = true;
  }
}

