import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from '../../services/data-service/client.service';
import { PaymentService } from '../../services/data-service/payment.service';
import { CityComponent } from '../city/city.component';
import { StateDropdownComponent } from '../state-dropdown/state-dropdown.component';

@Component({
  selector: 'app-payment-process',
  templateUrl: './payment-process.component.html',
  styleUrls: ['./payment-process.component.css']
})
export class PaymentProcessComponent implements OnInit {
  @Input() clientId?: number;
  @Input() totalAmount?: any;
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  billingForm: FormGroup;
  selectedStateId: any;
  State: any;
  city: any;
  selectedCityId: any;
  isStateLoaded: boolean;
  selectedCountryId: any;
  tipAmount = '';
  submitted: boolean = false;
  constructor(
    private activeModal: NgbActiveModal,
    private paymentService: PaymentService,
    private client: ClientService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.isStateLoaded = false;
    this.formInitialize();
    this.getClientDetail();
  }

  closeModal() {
    this.isStateLoaded = false;
    const obj = {
      status: false,
      tipAmount: this.tipAmount
    };
    this.activeModal.close(obj);
  }

  formInitialize() {
    this.billingForm = this.fb.group({
      companyName: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address1: ['', Validators.required],
      address2: [''],
       city: ['', Validators.required],
       state: ['', Validators.required],
      zip: ['', Validators.required],
       country: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required]
    });
  }
  get f() {
    return this.billingForm.controls;
  }

  getClientDetail() {
    this.client.getClientById(this.clientId).subscribe(res => {
      if (res.status === 'Success') {
        const clientDetail = JSON.parse(res.resultData);
        console.log(clientDetail, 'client');
        if (clientDetail.Status) {
          const clientObj = clientDetail.Status[0];
          this.selectedStateId = clientObj.State;
          this.State = clientObj.State;
          this.selectedCityId = clientObj.City;
          this.selectedCountryId = clientObj.Country;
          this.isStateLoaded = true;
          this.billingForm.patchValue({
            firstName: clientObj.FirstName,
            lastName: clientObj.LastName,
            address1: clientObj.Address1,
            address2: clientObj.Address2,
            // city: clientObj.City,
            // state: clientObj.State,
            zip: clientObj.Zip,
            // country: clientObj.Country,
            email: clientObj.Email,
            phone: clientObj.PhoneNumber
          });
        }
      }
    });
  }

  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  selectCity(event) {
    this.city = event;
  }

  process() {
    this.submitted = true;
  if(this.billingForm.invalid){
    return;
  }
    const obj = {
      status: true,
      tipAmount: this.tipAmount
    };
    this.activeModal.close(obj);
  }

}
