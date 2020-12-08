import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { StateDropdownComponent } from '../state-dropdown/state-dropdown.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from '../../services/data-service/client.service';
import { GetCodeService } from '../../services/data-service/getcode.service';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
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
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private client: ClientService, private getCode: GetCodeService) { }


  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "Inactive" }];
    this.formInitialize();
    if (this.isView === true) {
      this.viewClient();
    }
    if (this.isEdit === true) {
      this.clientForm.reset();
      this.clientForm.controls.status.enable();
      this.getClientById();
    } else {
      this.clientForm.controls.status.disable();
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
      phone1: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
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
  }

  selectCity(id) {
    this.city = id;
  }

}
