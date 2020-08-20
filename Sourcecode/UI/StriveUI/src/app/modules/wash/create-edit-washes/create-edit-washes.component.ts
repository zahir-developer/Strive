import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-create-edit-washes',
  templateUrl: './create-edit-washes.component.html',
  styleUrls: ['./create-edit-washes.component.css']
})
export class CreateEditWashesComponent implements OnInit {

  washForm: FormGroup;
  timeIn: any;
  timeOut: any;
  minutes: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  Score: any;
  ticketNumber: any;
  barcodeDetails: any;
  vehicle: any;
  color: any;
  additionalService: any = [];
  serviceEnum: any;
  additional: any;
  washes: any;
  upcharges: any;
  airFreshner: any;
  UpchargeType: any;
  jobItems: any;

  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr, private wash: WashService) { }

  ngOnInit() {
    this.formInitialize();
    this.Score = [{ CodeId: 1, CodeValue: "None" }, { CodeId: 2, CodeValue: "Option1" }, { CodeId: 3, CodeValue: "Option2" }];
    if (this.isView === true) {
      this.viewWash();
    }
  }

  formInitialize() {
    this.washForm = this.fb.group({
      client: ['',],
      vehicle: ['',],
      type: ['',],
      barcode: ['',],
      washes: ['',],
      model: ['',],
      color: ['',],
      upcharges: ['',],
      upchargeType: ['',],
      airFreshners: ['',],
      notes: ['',],
      pastNotes: ['',]
    });
    this.getServiceType();
    this.getVehicle();
    this.getColor();
  }

  getWashById() {
    console.log(this.selectedData);
    this.washForm.patchValue({
      barcode: this.selectedData.BarCode,
    });
    this.ticketNumber = this.selectedData.TicketNumber;
    //this.additionalService = this.additional.filter(item => item.ServiceId === this.selectedData.JobItems.ServiceId);
  }

  getServiceType() {
    this.wash.getServiceType("SERVICETYPE").subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.serviceEnum = sType.Codes;
        this.getAllServices();
        console.log(this.serviceEnum);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllServices() {
    this.wash.getServices().subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.additional = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[2].CodeValue);
        this.washes = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[0].CodeValue);
        this.upcharges = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[3].CodeValue);
        this.airFreshner = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[4].CodeValue);
        this.UpchargeType = this.upcharges.filter(item => Number(item.ParentServiceId) === 0);
        this.upcharges = this.upcharges.filter(item => Number(item.ParentServiceId) !== 0);
        if (this.isEdit === true) {
          this.washForm.reset();
          this.getWashById();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  change(data) {
    const check = this.additionalService.filter(item => item === data);
    if (check.length === 0) {
      this.additionalService.push(data);
    } else {
      if(this.isEdit){
        const temp = this.jobItems.filter(item => item.ServiceId === data.ServiceId)[0];        
        this.jobItems = this.jobItems.filter(item => item.ServiceId !== data.ServiceId);
        temp.isDeleted = true;
        this.jobItems.push(temp);
        console.log(this.jobItems);
      }
      this.additionalService = this.additionalService.filter(item => item !== data);
    }
  }

  getVehicle() {
    this.wash.getVehicle().subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.vehicle = wash.Vehicle;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.CategoryId === 30);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  viewWash() {
    this.washForm.disable();
  }

  // Get Client And Vehicle Details By Barcode
  getByBarcode(barcode) {
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.barcodeDetails = wash.ClientAndVehicleDetail[0];
        console.log(this.barcodeDetails);
        this.washForm.patchValue({
          client: this.barcodeDetails.FirstName + this.barcodeDetails.LastName,
          vehicle: this.barcodeDetails.VehicleId,
          model: this.barcodeDetails.VehicleModel,
          color: this.barcodeDetails.VehicleColor
        });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  // Add/Update Wash
  submit() {
    const serviceWash = this.washes.filter(item => item.ServiceId === Number(this.washForm.value.washes));
    if (serviceWash.length !== 0) {
      this.additionalService.push(serviceWash[0]);
    }
    const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(this.washForm.value.upcharges));
    if (serviceUpcharge.length !== 0) {
      this.additionalService.push(serviceUpcharge[0]);
    }
    const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(this.washForm.value.airFreshners));
    if (serviceAir.length !== 0) {
      this.additionalService.push(serviceAir[0]);
    }
    console.log(this.additionalService);
    const job = {
      jobId: this.isEdit ? this.selectedData.JobId : 0,
      ticketNumber: this.isEdit ? this.selectedData.TicketNumber : "",
      locationId: 1,
      clientId: 3,// this.barcodeDetails.ClientId,
      vehicleId: 1,// this.barcodeDetails.VehicleId,
      jobType: 15,
      jobDate: new Date(),
      timeIn: new Date(),
      estimatedTimeOut: new Date(),
      actualTimeOut: new Date(),
      jobStatus: "",
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    this.jobItems = this.additionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: this.isEdit ? this.selectedData.JobId : 0,
        serviceId: item.ServiceId,
        commission: 0,
        price: item.Cost,
        quantity: 1,
        reviewNote: "",
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
    });
    const formObj = {
      job: job,
      jobItem: this.jobItems
    };
    if (this.isEdit === true) {
      this.wash.updateWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Wash Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.washForm.reset();
        }
      });
    } else {
      this.wash.addWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.washForm.reset();
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

