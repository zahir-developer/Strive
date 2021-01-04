import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

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
    private fb: FormBuilder
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
    const services = this.detailService.filter( item => item.ServiceId === +this.serviceForm.value.serviceID);
    if (services.length > 0) {
      this.scheduleDetailObj.serviceobj = services[0];
      this.selectLocation.emit();
    }
  }

  getService() {
    this.customerService.getServices().subscribe(res => {
      if (res.status === 'Success') {
        const serviceDetails = JSON.parse(res.resultData);
        console.log(serviceDetails, 'service');
        this.detailService = serviceDetails.ServiceSetup.filter(item => item.ServiceType === 'Details');
        this.patchServiceValue();
      }
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
