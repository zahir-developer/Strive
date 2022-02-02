import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaymentService } from 'src/app/shared/services/common-service/payment.service';

@Component({
  selector: 'app-payment-gateway-create-edit',
  templateUrl: './payment-gateway-create-edit.component.html'
})
export class PaymentGatewayCreateEditComponent implements OnInit {
  paymentGatewaySetupForm: FormGroup;
  @Output() paymentGatewayDetails: EventEmitter<any> = new EventEmitter();
  @Input() PaymentGatewayId?: any;
  @Input() PaymentGatewayName?: any;
  @Input() BaseURL?: any;
  @Input() APIKey?:any;
  @Input() header?: any;
  
  submitted: boolean;  
  PaymentGatewayNameValidation: boolean;
  constructor(
    private payment: PaymentService,
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private locationService: LocationService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    debugger;
    this.paymentGatewaySetupForm = this.fb.group({
      PaymentGatewayName: ['', Validators.required],
      BaseURL: ['', Validators.required],
      APIKey: ['',Validators.required]
    });
    this.paymentGatewaySetupForm.patchValue({
      PaymentGatewayName: this.PaymentGatewayName,
      BaseURL: this.BaseURL  ,
      APIKey : this.APIKey   
    });
  }


  // Add / Update 
  submit() {
    const myObj = {
      PaymentGatewayId: this.PaymentGatewayId ==null ? 0 : this.PaymentGatewayId,
      PaymentGatewayName: this.PaymentGatewayName,
      BaseURL: this.BaseURL,
      APIKey:  this.APIKey,
      IsActive:true,
      IsDeleted:false,
      CreatedDate: this.PaymentGatewayId ==null ? new Date() : new Date()
    }
    this.spinner.show();
    const finalObj = {
      paymentGateway: myObj
    };
      this.payment.updatePayment(finalObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success('Payment Gateway updated successfully!', 'Success!');
          this.cancel();
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          // this.paymentGatewaySetupForm.clientForm.reset();
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
  }
  cancel() {
    this.activeModal.close();
  }
 
  get f() {
    return this.paymentGatewaySetupForm.controls;
  }
}

