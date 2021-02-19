import { Component, OnInit, EventEmitter, Output, ViewChild, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { DealsService } from 'src/app/shared/services/data-service/deals.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';

@Component({
  selector: 'app-deals-add',
  templateUrl: './deals-add.component.html',
  styleUrls: ['./deals-add.component.css']
})
export class DealsAddComponent implements OnInit {

  dealSetupForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Output() getDeals = new EventEmitter();
  submitted: boolean;
  dealList: { name: string; value: string; }[];
  DealsOn: boolean;
  Custom: boolean;
  @Input() selectedData?: any;

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
    private toastr: MessageServiceToastr,
    private spinner: NgxSpinnerService,
    private deals: DealsService,
    private getCode: GetCodeService 
  ) { }

  ngOnInit() {
    this.submitted = false;
    this.formInitialize();
this.dealSetupForm.controls.dealsName.disable()
this.dealSetupForm.controls.timePeriod.disable()
this.dealSetupForm.controls.startDate.disable()    
this.timePeriods = [
  {
    name: 'Monthly', value: 1
  },
  {
    name: 'Yearly', value: 2
  },
  {
    name: 'Custom', value: 3
  }
]
this.dealList = [
  {
    name: 'Buy 10 get 1 Free', value: 'Buy 10 get 1 Free'
  },
  {
    name: '5/5/10/15', value: '5/5/10/15'
  }
]

 if(this.selectedData) {
  this.dealSetupForm.patchValue({
    timePeriod: this.selectedData.TimePeriod,
    dealsName: this.selectedData.DealName,
    startDate: this.selectedData.startDate + this.selectedData.endDate,
    deals: this.selectedData.Deals
  });
  this.dealId = this.selectedData.DealId
 }
 if (this.selectedData.TimePeriod == 3) {
   this.Custom = true;
  this.dealSetupForm.controls.startDate.enable()
    }
  else{
    this.Custom = false;
    this.dealSetupForm.controls.startDate.disable();
  
  
  }
  }

  formInitialize() {
    this.dealSetupForm = this.fb.group({
      timePeriod: ['', Validators.required],
      dealsName: ['', Validators.required],
      startDate: [''],
      deals: [false]
    });
  }


  get f() {
    return this.dealSetupForm.controls;
  }
  Timeperiod(event) {
    if (event.target.value == 3) {
      this.Custom = true;
this.dealSetupForm.controls.startDate.enable() 
   }
else{
  this.Custom = false;
  this.dealSetupForm.controls.startDate.disable();


}
  }
  dealChange(event) {
    if (event.checked == true) {
      this.dealSetupForm.controls.dealsName.enable()
      this.dealSetupForm.controls.timePeriod.enable()

        }
        else{
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


    const obj = {
      deal: {
        dealId: this.selectedData ?  this.selectedData.DealId:  0 ,
        dealName: this.dealSetupForm.controls['dealsName'].value,
        timePeriod:  this.dealSetupForm.controls['timePeriod'].value,
        deals: this.dealSetupForm.controls['deals'].value ,
        startDate: this.startDate == undefined ? null : this.startDate,
        endDate: this.endDate == undefined ? null : this.endDate,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate:new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      }
    
    };

    this.spinner.show();
    this.deals.addDealsSetup(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Deals Saved Successfully' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.getDeals.emit();
        this.isLoading = false;
      } else {
        this.isLoading = false;
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
        this.submitted = false;
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

}
