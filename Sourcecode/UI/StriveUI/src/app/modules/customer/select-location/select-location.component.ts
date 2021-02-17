import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html',
  styleUrls: ['./select-location.component.css']
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
    private spinner: NgxSpinnerService
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
      this.spinner.hide();
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.locationList = location.Location;
        this.patchLocationValue();
      }
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
