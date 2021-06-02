import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-add-activity',
  templateUrl: './add-activity.component.html'
})
export class AddActivityComponent implements OnInit {
  giftCardForm: FormGroup;
  @Input() giftCardNumber?: any;
  @Input() activeDate?: any;
  @Input() totalAmount?: any;
  @Input() giftCardId?: any;
  submitted: boolean;
  symbol: string;
  amountValidation: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private giftCardService: GiftCardService,
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
      type: ['']
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
    if (this.symbol === 'minus') {
      if (+this.totalAmount < Number(this.giftCardForm.value.amount)) {
        this.amountValidation = true;
        this.toastr.warning(MessageConfig.Admin.GiftCard.insuffBalnce, 'Warning');
        return;
      }
    }
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
    });
  }

  updateBalance() {
    this.giftCardService.updateBalance(this.giftCardId).subscribe( res => {});
  }

}
