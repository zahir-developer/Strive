import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html'
})
export class SelectLocationComponent implements OnInit {
  @Output() selectionPage = new EventEmitter();
  @Output() selectAppointment = new EventEmitter();
  locationList: any = [];
  locationForm: FormGroup;
  @Input() scheduleDetailObj?: any;
  @Input() selectedData?: any;
  constructor(
    private customerService: CustomerService,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.locationForm = this.fb.group({
      locationID: ['', Validators.required]
    });
    this.getLocation();
  }

  cancel() {
    this.selectionPage.emit();
  }

  next() {
    const location = this.locationList.filter( item => item.LocationId === +this.locationForm.value.locationID);
    if (location.length > 0) {
      this.scheduleDetailObj.locationObj = location[0];
      this.selectAppointment.emit();
    }
  }

  getLocation() {
    this.spinner.show();
    this.customerService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        const location = JSON.parse(res.resultData);
        this.locationList = location.Location;
        this.patchLocationValue();
      }
      else{
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        this.spinner.hide()
      }
    }
    , (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  patchLocationValue() {
    if (this.scheduleDetailObj.locationObj !== undefined) {
      this.locationForm.patchValue({ locationID: this.scheduleDetailObj.locationObj.LocationId });
    }
    if (this.scheduleDetailObj.isEdit) {
      const locationid = this.selectedData.Details.LocationId;
      this.locationForm.patchValue({ locationID: locationid });
    }
  }

}
