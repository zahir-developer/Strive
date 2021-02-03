import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { StateDropdownComponent } from '../state-dropdown/state-dropdown.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from '../../services/data-service/client.service';
import { GetCodeService } from '../../services/data-service/getcode.service';
import { CityComponent } from '../city/city.component';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;

  clientForm: FormGroup;
  Status: any;
  State: any;
  Score: any;
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  selectedStateId: any;
  selectedCountryId: any;
  clientId: number = 0;
  Type: any;
  submitted: boolean;
  city: any;
  selectedCityId: any;
  ClientNameAvailable: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private client: ClientService, private getCode: GetCodeService) { }


  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "Inactive" }];
    this.formInitialize();
    if (this.isView === true) {
      this.viewClient();
    }
    if (this.isEdit === true) {
      this.getClientById();
    } 
    if(this.isEdit !== true || this.isView === true){
      this.clientForm.controls.status.disable();
    }else{
      this.clientForm.controls.status.enable();
    }
  }
 
  formInitialize() {
    this.clientForm = this.fb.group({
      fName: ['', Validators.required],
      lName: ['', Validators.required],
      address: ['', Validators.required],
      zipcode: ['', [Validators.required, Validators.minLength(5)]],
      state: ['',],
      city: ['',],
      phone1: ['', [Validators.required, Validators.minLength(14)]],
      email: ['', Validators.email],
      phone2: ['',],
      creditAccount: ['',],
      noEmail: ['',],
      score: ['',],
      status: ['', Validators.required],
      notes: ['',],
      checkOut: ['',],
      type: ['', Validators.required],
      amount:['',]
    });
    this.clientForm.get('status').patchValue(0);
    this.getClientType();
    this.getScore();
  }

  get f() {
    return this.clientForm.controls;
  }
  sameClientName() {
    const clientNameDto = {
FirstName :this.clientForm.value.fName,
LastName : this.clientForm.value.lName,
PhoneNumber : this.clientForm.value.phone1

    }
    if(this.clientForm.value.fName && this.clientForm.value.lName && this.clientForm.value.phone1){
      this.client.ClientSameName(clientNameDto).subscribe(res => {
        if (res.status === 'Success') {
          const sameName = JSON.parse(res.resultData);
          if(sameName.IsClientNameAvailable === true){
            this.ClientNameAvailable = true;
            this.toastr.error('Client is Already Exists', 'Error!');
  
          } else{
            this.ClientNameAvailable = false;
   
          }
        }
      });
    }
   
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
  getClientById() {
    this.selectedStateId = this.selectedData.State;
    this.State = this.selectedStateId;
    this.selectedCityId = this.selectedData.City;
    this.city = this.selectedCityId;
    this.clientForm.patchValue({
      fName: this.selectedData.FirstName,
      lName: this.selectedData.LastName,
      creditAccount: this.selectedData.NoEmail,
      status: this.selectedData.IsActive ? 0 : 1,
      score: this.selectedData.Score,
      type: this.selectedData.ClientType,
      amount: this.selectedData.Amount,
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

  getSelectedStateId(event) {
    this.State = event.target.value;
    this.cityComponent.getCity(event.target.value);
  }

  selectCity(event) {
    this.city = event.target.value;
  }

}
