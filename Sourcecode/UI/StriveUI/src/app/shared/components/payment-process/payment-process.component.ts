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
  paymentForm: FormGroup;
  selectedStateId: any;
  State: any;
  city: any;
  selectedCityId: any;
  isStateLoaded: boolean;
  selectedCountryId: any;
  tipAmount = '';
  ccRegex: RegExp = /[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$/;
  submitted: boolean = false;
  card: string;
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
      // city: ['', Validators.required],
      // state: ['', Validators.required],
      zip: ['', Validators.required],
      // country: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required]
    });
    this.paymentForm = this.fb.group({
      customerName: [''],
      tipAmount: ['', Validators.required],
      cardNumber: ['', Validators.required],
      expiryDate: ['', Validators.required],
      ccv: ['', Validators.required],

    });
  }
  get payment() {
    return this.paymentForm.controls;
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
            city: clientObj.City,
            state: clientObj.State,
            zip: clientObj.Zip,
            country: clientObj.Country,
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

    if (this.billingForm.invalid && this.paymentForm.invalid) {
      this.stateDropdownComponent.submitted = true;
      this.cityComponent.submitted = true;
      if (this.stateDropdownComponent.stateValueSelection === false) {
        return;
      }
      if (this.cityComponent.selectValueCity === false) {
        return;
      }
    }
    if (this.billingForm.invalid && this.paymentForm.invalid) {
      return;
    }
    const obj = {
      status: true,
      tipAmount: this.paymentForm.value.tipAmount
    };
    this.activeModal.close(obj);
  }


  getCardType(number) {
    // visa
    var re = new RegExp("^4");
    if (number.match(re) != null) {
      this.card = "Visa";
      return this.card;
    }


    // Mastercard 
    // Updated for Mastercard 2017 BINs expansion
    if (/^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$/.test(number)) {
      this.card = "Mastercard";
      return this.card;
    }

    // AMEX
    re = new RegExp("^3[47]");
    if (number.match(re) != null) {
      this.card = "AMEX";
      return this.card;
    }
    // Discover
    re = new RegExp("^(6011|622(12[6-9]|1[3-9][0-9]|[2-8][0-9]{2}|9[0-1][0-9]|92[0-5]|64[4-9])|65)");
    if (number.match(re) != null) {
      this.card = "Discover";
      return this.card;
    }
    // Diners
    re = new RegExp("^36");
    if (number.match(re) != null) {
      this.card = "Diners";
      return this.card;
    }
    // Diners - Carte Blanche
    re = new RegExp("^30[0-5]");
    if (number.match(re) != null) {
      this.card = "Diners - Carte Blanche";
      return this.card;
    }
    // JCB
    re = new RegExp("^35(2[89]|[3-8][0-9])");
    if (number.match(re) != null) {
      this.card = "JCB";
      return this.card;
    }
    // Visa Electron
    re = new RegExp("^(4026|417500|4508|4844|491(3|7))");
    if (number.match(re) != null) {
      this.card = "Visa Electron";
      return this.card;
    }

    console.log(this.card)
  }


}
