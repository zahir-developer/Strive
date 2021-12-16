import { Component, OnInit, Input,Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-add-activity-additional',
  templateUrl: './add-activity-additional.component.html'
})
export class AddActivityAdditionalComponent implements OnInit {
  giftCardForm: FormGroup;
  @Output() userActivity: EventEmitter<any> = new EventEmitter();
  @Input() CreditAccountHistoryId?: any;
  @Input() clientId?: any;
  @Input() amount?: any;
  @Input() comments?: any;
  @Input() header?: any;
  
  submitted: boolean;
  symbol: string;
  amountValidation: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    // private giftCardService: GiftCardService,
    private toastr: ToastrService,
    private spinner :NgxSpinnerService,
    private messageService: MessageServiceToastr
    ) { }

  ngOnInit(): void {
    this.symbol = 'plus';
    this.amountValidation = false;
    this.submitted = false;
    this.giftCardForm = this.fb.group({
      amount: ['', Validators.required],
      txtNotes: ['', Validators.required],
      type: ['']
    });
    this.giftCardForm.patchValue({
      amount: this.amount,
      txtNotes: this.comments     
    });
  }
 

  closeModal() {
    this.activeModal.close();
  }

  get f() {
    return this.giftCardForm.controls;
  }

  settingType(event) {
    const type = event.target.value;
    if (type === 'plus') {
      this.symbol = 'plus';
    } else {
      this.symbol = 'minus';
    }
  }

  addActivity() {
    this.submitted = true;
    this.amountValidation = false;
    if (this.giftCardForm.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }
    if (this.symbol === 'plus') {
      this.giftCardForm.value.amount = this.giftCardForm.value.amount <0? (-1 * this.giftCardForm.value.amount) : this.giftCardForm.value.amount;
    } else  if (this.symbol === 'minus'){
      this.giftCardForm.value.amount = this.giftCardForm.value.amount > 0 ? (-1 * this.giftCardForm.value.amount) : this.giftCardForm.value.amount
    }

    const activityObj = {
      CreditAccountHistoryId: this.CreditAccountHistoryId,
      clientId: this.clientId,
      amount:this.giftCardForm.value.amount,
      notes:this.giftCardForm.value.txtNotes,
      type:this.symbol
    }

    this.userActivity.emit(activityObj);
    

    // if (this.symbol === 'minus') {
    //   if (+this.totalAmount < Number(this.giftCardForm.value.amount)) {
    //     this.amountValidation = true;
    //     this.toastr.warning(MessageConfig.Admin.GiftCard.insuffBalnce, 'Warning');
    //     return;
    //   }
    // }
    /*
    const activityObj = {
      giftCardHistoryId: 0,
      giftCardId: this.giftCardId,
      locationId: +localStorage.getItem('empLocationId'),
      transactionType: null,
      transactionAmount: this.symbol === 'plus' ? this.giftCardForm.value.amount : -(this.giftCardForm.value.amount),
      transactionDate: moment(new Date()),
      comments: null,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: moment(new Date()),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date())
    };
    const finalObj = {
      giftCardHistory: activityObj
    };
    this.spinner.show();
    this.giftCardService.addCardHistory(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.GiftCard.ActivityAdd, 'Success!');
        this.activeModal.close(true);
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });*/
  }


}
