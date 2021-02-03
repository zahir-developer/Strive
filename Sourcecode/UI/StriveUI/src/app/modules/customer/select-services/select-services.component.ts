import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-select-services',
  templateUrl: './select-services.component.html',
  styleUrls: ['./select-services.component.css']
})
export class SelectServicesComponent implements OnInit {
  @Output() dashboardPage = new EventEmitter();
  @Output() selectLocation = new EventEmitter();
  detailService: any = [];
  selectedService: any = '';
  serviceForm: FormGroup;
  @Input() scheduleDetailObj?: any;
  @Input() selectedData?: any;
  constructor(
    private customerService: CustomerService,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    console.log(this.scheduleDetailObj, 'schedule');
    this.serviceForm = this.fb.group({
      serviceID: ['', Validators.required]
    });
    this.getService();
  }

  backToDashboard() {
    this.dashboardPage.emit();
  }

  nextPage() {
    console.log(this.serviceForm, 'selectedServcie');
    const services = this.detailService.filter(item => item.ServiceId === +this.serviceForm.value.serviceID);
    if (services.length > 0) {
      this.scheduleDetailObj.serviceobj = services[0];
      this.selectLocation.emit();
    }
  }

  getService() {
    const serviceObj = {
      locationId: 0,
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.spinner.show();
    this.customerService.getServices(serviceObj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const serviceDetails = JSON.parse(res.resultData);
        console.log(serviceDetails, 'service');
        if (serviceDetails.ServiceSetup.getAllServiceViewModel !== null) {
          this.detailService = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item => item.ServiceType === 'Details');
          this.patchServiceValue();
        }
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  patchServiceValue() {
    if (this.scheduleDetailObj.serviceobj !== undefined && !this.scheduleDetailObj.isEdit) {
      this.serviceForm.patchValue({ serviceID: this.scheduleDetailObj.serviceobj.ServiceId });
    }
    if (this.scheduleDetailObj.isEdit) {
      const serviceId = this.selectedData.DetailsItem[0].ServiceId;
      this.serviceForm.patchValue({ serviceID: serviceId });
      this.scheduleDetailObj.JobItemId = this.selectedData.DetailsItem[0].JobItemId;
    }
  }

}
