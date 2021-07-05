import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { StateDropdownComponent } from '../state-dropdown/state-dropdown.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from '../../services/data-service/client.service';
import { GetCodeService } from '../../services/data-service/getcode.service';
import { CityComponent } from '../city/city.component';
import { MessageConfig } from '../../services/messageConfig';
import { ApplicationConfig } from '../../services/ApplicationConfig';
import { CodeValueService } from '../../common-service/code-value.service';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html'
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
  ClientEmailAvailable: boolean;
  isAmount: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private client: ClientService, private getCode: GetCodeService, private codeService: CodeValueService) { }


  ngOnInit() {
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "Inactive" }];
    this.isAmount = false;
    this.formInitialize();
    if (this.isView === true) {
      this.viewClient();
    }
    if (this.isEdit === true) {
      this.getClientById();
    }
    if (this.isEdit !== true || this.isView === true) {
      this.clientForm.controls.status.disable();
    } else {
      this.clientForm.controls.status.enable();
    }
  }

  formInitialize() {
    this.clientForm = this.fb.group({
      fName: ['', Validators.required],
      lName: ['', Validators.required],
      address: ['',],
      zipcode: ['', [Validators.minLength(5)]],
      state: ['',],
      city: ['',],
      phone1: ['', [Validators.required, Validators.minLength(14)]],
      email: ['', [Validators.required, Validators.email]],
      phone2: ['',],
      creditAccount: ['',],
      noEmail: ['',],
      score: ['',],
      status: ['', Validators.required],
      notes: ['',],
      checkOut: ['',],
      type: ['', Validators.required],
      amount: ['',]
    });
    this.clientForm.get('status').patchValue(0);
    this.clientForm.controls.amount.disable();
    this.getClientType();
    this.getScore();
  }

  get f() {
    return this.clientForm.controls;
  }


  sameClientName() {
    const clientNameDto = {
      FirstName: this.clientForm.value.fName,
      LastName: this.clientForm.value.lName,
      PhoneNumber: this.clientForm.value.phone1
    };
    if (this.clientForm.value.fName && this.clientForm.value.lName && this.clientForm.value.phone1) {
      this.client.ClientSameName(clientNameDto).subscribe(res => {
        if (res.status === 'Success') {
          const sameName = JSON.parse(res.resultData);
          if (sameName.IsClientNameAvailable === true) {
            this.ClientNameAvailable = true;
            this.toastr.warning(MessageConfig.Client.clientExist, 'Warning!');

          } else {
            this.ClientNameAvailable = false;
          }
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get ClientType
  getClientType() {
    const clientTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.clientType);
    if (clientTypeValue.length > 0) {
      this.Type = clientTypeValue;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.ClientType).subscribe(data => {
        if (data.status === "Success") {
          const cType = JSON.parse(data.resultData);
          this.Type = cType.Codes;
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
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
    if (this.selectedData.IsCreditAccount) {
      this.isAmount = true;
      this.clientForm.get('amount').setValidators([Validators.required]);
      this.clientForm.controls.amount.enable();
    } else {
      this.isAmount = false;
      this.clientForm.get('amount').clearValidators();
    }
  }

  viewClient() {
    this.clientForm.disable();
  }

  change(data) {
    this.clientForm.value.creditAccount = data;
    if (data) {
      this.isAmount = true;
      this.clientForm.get('amount').setValidators([Validators.required]);
      this.clientForm.controls.amount.enable();
    } else {
      this.isAmount = false;
      this.clientForm.get('amount').clearValidators();
      this.clientForm.controls.amount.disable();
    }
  }

  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  selectCity(event) {
    this.city = event;
  }
  clientEmailExist() {
    if (this.clientForm.controls.email.errors !== null) {
      return;
    }
    this.client.ClientEmailCheck(this.clientForm.controls.email.value).subscribe(res => {
      if (res.status === 'Success') {
        const sameEmail = JSON.parse(res.resultData);
        if (sameEmail.EmailIdExist === true) {
          this.ClientEmailAvailable = true;
          this.toastr.warning(MessageConfig.Client.emailExist, 'Warning!');
        } else {
          this.ClientEmailAvailable = false;
          this.toastr.info(MessageConfig.Client.emailNotExist, 'Information!');
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
