import { Component, OnInit, EventEmitter, Output, ViewChild, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { DealsService } from 'src/app/shared/services/data-service/deals.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-deals-add',
  templateUrl: './deals-add.component.html'
})
export class DealsAddComponent implements OnInit {

  dealSetupForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Output() getDeals = new EventEmitter();
  submitted: boolean;
  dealList: { name: string; value: string; }[];
  DealsOn: boolean;
  Custom: boolean;
  @Input() actionType?: string;
  @Input() selectedData?: any;
  @Input() DealsDetails: any;
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  date = new Date();
  maxDate = new Date();
  isLoading: boolean;
  startDate: any;
  endDate: any;
  timePeriods: { name: string; value: number; }[];
  dealId: any;
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private deals: DealsService,
    private getCode: GetCodeService,
    private datePipe: DatePipe
  ) { }

  ngOnInit() {
    this.submitted = false;
    this.formInitialize();
    this.timePeriods = ApplicationConfig.dealTimePeriods;
    this.dealList = ApplicationConfig.dealList;

    if (this.actionType === 'Edit') {
      const startDate = new Date(this.selectedData.StartDate); // this.datePipe.transform(this.selectedData.StartDate);
      const endDate = new Date(this.selectedData.EndDate); // this.datePipe.transform(this.selectedData.EndDate);
      const selectedDate = [startDate, endDate];
      this.dealSetupForm.patchValue({
        timePeriod: this.selectedData.TimePeriod,
        dealsName: this.selectedData.DealName,
        startDate: selectedDate,
        deals: this.selectedData.Deals
      });
      this.dealId = this.selectedData.DealId;
      this.dealSetupForm.controls.dealsName.disable();
      if (this.selectedData.TimePeriod === 3) {
        this.Custom = true;
        this.dealSetupForm.controls.startDate.enable();
      }
      else {
        this.Custom = false;
        this.dealSetupForm.controls.startDate.disable();
      }
    }

    if (this.actionType === 'Add') {
      if (this.DealsDetails.length > 0) {
        this.dealList = ApplicationConfig.dealList.filter(item => item.name !== this.DealsDetails[0].DealName);
      }
    }
  }

  formInitialize() {
    this.dealSetupForm = this.fb.group({
      timePeriod: ['', Validators.required],
      dealsName: ['', Validators.required],
      startDate: null,
      deals: [false]
    });
  }


  get f() {
    return this.dealSetupForm.controls;
  }


  Timeperiod(event) {
    if (+event.target.value === 3) {
      this.Custom = true;
      this.dealSetupForm.controls.startDate.enable();
    }
    else {
      this.Custom = false;
      this.dealSetupForm.controls.startDate.disable();
    }
  }


  dealChange(event) {
    if (event.checked === true) {
      this.dealSetupForm.controls.dealsName.enable()
      this.dealSetupForm.controls.timePeriod.enable()

    }
    else {
      this.dealSetupForm.controls.dealsName.disable()
      this.dealSetupForm.controls.timePeriod.disable()
      this.dealSetupForm.controls.dealsName.reset()
      this.dealSetupForm.controls.timePeriod.reset()
      this.dealSetupForm.controls.startDate.reset();
    }
  }


  onValueChange(event) {
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
    }
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  submit() {
    this.submitted = true;
    if (this.dealSetupForm.invalid) {
      return;
    }

    this.dealSetupForm.controls.dealsName.enable();
    const obj = {
      deal: {
        dealId: this.selectedData ? this.selectedData.DealId : 0,
        dealName: this.dealSetupForm.value.dealsName,
        timePeriod: this.dealSetupForm.value.timePeriod,
        deals: true,
        startDate: this.startDate === undefined ? null : this.startDate,
        endDate: this.endDate === undefined ? null : this.endDate,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      }

    };

    this.spinner.show();
    this.deals.addDealsSetup(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.Deal.Add, 'Success!');
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.getDeals.emit();
        this.isLoading = false;
      } else {
        this.spinner.hide();

        this.isLoading = false;
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.submitted = false;
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
