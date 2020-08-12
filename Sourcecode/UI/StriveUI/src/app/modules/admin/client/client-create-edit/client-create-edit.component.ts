import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import * as moment from 'moment';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';

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
  collectionSize: number = 0;
  Type: { id: number; Value: string; }[];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private client: ClientService,
    private confirmationService: ConfirmationUXBDialogService, private vehicle: VehicleService,private getCode: GetCodeService) { }

  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.Score = [{ id: 0, Value: "Score1" }, { id: 1, Value: "Score2" }];
    //this.Type = [{ id: 0, Value: "Type1" }, { id: 1, Value: "Type2" }];
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
    this.getClientType();
  }

  getClientType(){
    this.getCode.getCodeByCategory("CLIENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.Type = cType.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getClientVehicle(id) {
    this.vehicle.getVehicleByClientId(id).subscribe(data => {
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
    this.selectedStateId = this.selectedData.State;
    this.State = this.selectedStateId;
    this.clientForm.patchValue({
      fName: this.selectedData.Client.FirstName,
      lName: this.selectedData.Client.LastName,
      noEmail: this.selectedData.Client.NoEmail,
      status: this.selectedData.Client.IsActive ? 0 : 1,
      score: this.selectedData.Client.Score,      
      type: this.selectedData.Client.ClientType,
      notes: this.selectedData.Client.Notes,
      checkOut: this.selectedData.Client.RecNotes,
      address: this.selectedData.ClientAddress.Address1,
      phone1: this.selectedData.ClientAddress.PhoneNumber,
      zipcode: this.selectedData.ClientAddress.Zip,
      phone2: this.selectedData.ClientAddress.PhoneNumber2,
      email: this.selectedData.ClientAddress.Email,
      city: this.selectedData.ClientAddress.City
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
    this.address = {
      relationshipId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientForm.value.address,
      address2: "",
      phoneNumber2: this.clientForm.value.phone2,
      isActive: true,
      zip: this.clientForm.value.zipcode,
      state: this.State,
      city: this.clientForm.value.city !== "" ? this.clientForm.value.city : 0,
      country: 38,
      phoneNumber: this.clientForm.value.phone1,
      email: this.clientForm.value.email,
      isDeleted: false,
      createdBy: 0,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    }
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
      createdBy: 0,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 0,
      updatedDate: new Date(),
      notes: this.clientForm.value.notes,
      recNotes: this.clientForm.value.checkOut,
      score: (this.clientForm.value.score == "" || this.clientForm.value.score == null) ? 0 : this.clientForm.value.score,
      noEmail: false,
      clientType: (this.clientForm.value.type == "" || this.clientForm.value.type == null) ? 0 : this.clientForm.value.type
    };
    const myObj = {
      client: formObj,
      clientAddress: this.address
    }
    this.client.updateClient(myObj).subscribe(data => {
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
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.vehicleDetails.push(this.vehicle.addVehicle);
      this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
      this.showVehicleDialog = false;
    }
    this.showVehicleDialog = event.isOpenPopup;
  }
  delete(data){
    this.confirmationService.confirm('Delete Vehicle', `Are you sure you want to delete this vehicle? All related 
    information will be deleted and the vehicle cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }
  confirmDelete(data) {
    this.vehicleDetails = this.vehicleDetails.filter(item => item !== data);
    this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;    
  }
  add() {
    this.headerData = 'Add New vehicle';
    this.showVehicleDialog = true;
  }
}

