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
  vehicleDetails: any;
  isTableEmpty: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private client: ClientService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.Score = [{ id: 0, Value: "Score1" }, { id: 1, Value: "Score2" }];
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
      checkOut: ['',]
    });
    this.clientForm.get('status').patchValue(0);
    this.getAllVehicle();
  }

  getAllVehicle() {
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
    this.changeEmail(this.selectedData.NoEmail);
  }

  viewClient() {
    this.clientForm.disable();
  }

  change(data) {
    this.clientForm.value.creditAccount = data;
  }
  changeEmail(data) {
    this.clientForm.value.noEmail = data;
    if (data) {
      this.clientForm.get('email').disable();
      //this.clientForm.get('email').reset();
    } else {
      this.clientForm.get('email').enable();
    }
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
      city: this.clientForm.value.city,
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
      birthDate: "",
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      isActive: true,
      notes: this.clientForm.value.notes,
      recNotes: this.clientForm.value.checkOut,
      score: this.clientForm.value.Score,
      noEmail: this.clientForm.value.noEmail == "" ? false : this.clientForm.value.noEmail,
      clientAddress: this.address,
      clientType: 0
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
  }
  getSelectedStateId(event) {
    this.State = event.target.value;
  }
}

