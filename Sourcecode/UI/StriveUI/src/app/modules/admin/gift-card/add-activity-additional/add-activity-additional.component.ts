import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
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
  activityForm: FormGroup;
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
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    //this.symbol = 'plus';
    this.amountValidation = false;
    this.submitted = false;
    this.activityForm = this.fb.group({
      amount: ['', Validators.required],
      txtNotes: ['', Validators.required],
      type: ['']
    });
    this.activityForm.patchValue({
      amount: this.amount,
      txtNotes: this.comments
    });
  }


  closeModal() {
    this.activeModal.close();
  }

  get f() {
    return this.activityForm.controls;
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
    if (this.activityForm.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }

    if (this.symbol === undefined) {
      this.toastr.warning(MessageConfig.Client.ActivityPlusMinus, 'Warning!');
      return;
    }

    if (this.symbol === 'plus') {
      this.activityForm.value.amount = this.activityForm.value.amount < 0 ? (-1 * this.activityForm.value.amount) : this.activityForm.value.amount;
    } else if (this.symbol === 'minus') {
      this.activityForm.value.amount = this.activityForm.value.amount > 0 ? (-1 * this.activityForm.value.amount) : this.activityForm.value.amount
    }

    const activityObj = {
      CreditAccountHistoryId: this.CreditAccountHistoryId,
      clientId: this.clientId,
      amount: this.activityForm.value.amount,
      notes: this.activityForm.value.txtNotes,
      type: this.symbol
    }

    this.userActivity.emit(activityObj);


    // if (this.symbol === 'minus') {
    //   if (+this.totalAmount < Number(this.activityForm.value.amount)) {
    //     this.amountValidation = true;
    //     this.toastr.warning(MessageConfig.Admin.activity.insuffBalnce, 'Warning');
    //     return;
    //   }
    // }
    /*
    const activityObj = {
      activityHistoryId: 0,
      activityId: this.activityId,
      locationId: +localStorage.getItem('empLocationId'),
      transactionType: null,
      transactionAmount: this.symbol === 'plus' ? this.activityForm.value.amount : -(this.activityForm.value.amount),
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
      activityHistory: activityObj
    };
    this.spinner.show();
    this.activityService.addCardHistory(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.activity.ActivityAdd, 'Success!');
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
