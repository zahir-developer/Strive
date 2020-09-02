import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';

@Component({
  selector: 'app-create-edit-detail-schedule',
  templateUrl: './create-edit-detail-schedule.component.html',
  styleUrls: ['./create-edit-detail-schedule.component.css']
})
export class CreateEditDetailScheduleComponent implements OnInit {
  detailForm: FormGroup;
  ticketNumber: any;
  barcodeDetails: any;
  vehicle: any;
  color: any;
  type: any;
  additionalService: any = [];
  serviceEnum: any;
  additional: any;
  washes: any;
  upcharges: any;
  airFreshner: any;
  UpchargeType: any;
  jobItems: any;
  washItem: any = [];
  membership: any;
  timeInDate: any;
  timeOutDate: any;
  model: any;
  clientList: any;
  filteredClient: any[];
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  constructor(
    private fb: FormBuilder,
    private wash: WashService,
    private toastr: MessageServiceToastr,
    private detailService: DetailService
  ) { }

  ngOnInit(): void {
    this.formInitialize();
  }

  formInitialize() {
    this.detailForm = this.fb.group({
      client: ['',],
      vehicle: ['',],
      type: ['',],
      barcode: ['',],
      washes: ['',],
      model: ['',],
      color: ['',],
      upcharges: ['',],
      upchargeType: ['',],
      airFreshners: ['',]
    });
    this.getTicketNumber();
  }

  getTicketNumber() {
    this.wash.getTicketNumber().subscribe(data => {
      this.ticketNumber = data;
    });
    this.getAllClient();
    this.getServiceType();
    this.getColor();
  }

  getByBarcode(barcode) {
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.barcodeDetails = wash.ClientAndVehicleDetail[0];
        this.detailForm.patchValue({
          client: { id: this.barcodeDetails.ClientId, name: this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName },
          vehicle: this.barcodeDetails.VehicleId,
          model: this.barcodeDetails.VehicleModelId,
          color: this.barcodeDetails.VehicleColor,
          type: this.barcodeDetails.VehicleMfr
        });
        this.getClientVehicle(this.barcodeDetails.ClientId);
        this.getMembership(this.barcodeDetails.VehicleId);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getMembership(id) {
    this.wash.getMembership(+id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
        if (this.membership !== null) {
          this.membership.forEach(element => {
            const washService = this.washes.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (washService.length !== 0) {
              this.washService(washService[0].ServiceId);
            }
            const upchargeService = this.upcharges.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (upchargeService.length !== 0) {
              this.upchargeService(upchargeService[0].ServiceId);
            }
            const additionalService = this.additional.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (additionalService !== undefined && additionalService.length !== 0) {
              additionalService.forEach(item => {
                this.change(item);
              });
            }
          });
        }
        console.log(this.membership, id);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  washService(data) {
    if (false) {
      this.washItem.filter(i => i.ServiceTypeId === 15)[0].IsDeleted = true;
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 15);
        this.washItem.filter(i => i.ServiceTypeId === 15)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 15);
        const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 15);
      const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
      if (serviceWash.length !== 0) {
        this.additionalService.push(serviceWash[0]);
      }
    }
    console.log(this.additionalService, this.washItem);
  }

  upchargeService(data) {
    if (false) {
      if (this.washItem.filter(i => i.ServiceTypeId === 18)[0] !== undefined) {
        this.washItem.filter(i => i.ServiceTypeId === 18)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
        this.washItem.filter(i => i.ServiceTypeId === 18)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 18);
        const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
        if (serviceUpcharge.length !== 0) {
          this.additionalService.push(serviceUpcharge[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 18);
      const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
      if (serviceUpcharge.length !== 0) {
        this.additionalService.push(serviceUpcharge[0]);
      }
    }
    this.detailForm.patchValue({ upcharges: +data });
    this.detailForm.patchValue({ upchargeType: +data });
    console.log(this.additionalService, this.washItem);
  }

  change(data) {
    const temp = this.washItem.filter(item => item.ServiceId === data.ServiceId);
    if (temp.length !== 0) {
      this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted = this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted ? false : true;
      console.log(this.washItem);
    } else {
      data.IsChecked = data.IsChecked ? false : true;
    }
  }

  getAllClient() {
    this.wash.getAllClient().subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.Client.forEach(item => {
          item.fullName = item.FirstName + '\t' + item.LastName;
        });
        console.log(client, 'client');
        this.clientList = client.Client.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      }
    });
  }

  getServiceType() {
    this.wash.getServiceType('SERVICETYPE').subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.serviceEnum = sType.Codes;
        this.getAllServices();
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
        this.UpchargeType = this.upcharges;
        // this.upcharges = this.upcharges.filter(item => Number(item.ParentServiceId) !== 0);
        this.additional.forEach(element => {
          element.IsChecked = false;
        });
        // if (this.isEdit === true) {
        //   this.washForm.reset();
        //   this.getWashById();
        // }
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
        this.type = vehicle.VehicleDetails.filter(item => item.CategoryId === 28);
        this.model = vehicle.VehicleDetails.filter(item => item.CategoryId === 29);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  filterClient(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.clientList) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredClient = filtered;
  }

  selectedClient(event) {
    const clientId = event.id;
    console.log(clientId);
    this.getClientVehicle(clientId);
  }

  vehicleChange(id) {
    this.additional.forEach(element => {
      element.IsChecked = false;
    });
    this.getMembership(id);
  }

  airService(data) {
    if (false) {
      this.washItem.filter(i => i.ServiceTypeId === 19)[0].IsDeleted = true;
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
        this.washItem.filter(i => i.ServiceTypeId === 19)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 19);
        const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
        if (serviceAir.length !== 0) {
          this.additionalService.push(serviceAir[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => i.ServiceTypeId !== 19);
      const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
      if (serviceAir.length !== 0) {
        this.additionalService.push(serviceAir[0]);
      }
    }
    console.log(this.additionalService, this.washItem);
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  saveDetail() {
    this.additional.forEach(element => {
      if (element.IsChecked) {
        this.additionalService.push(element);
      }
    });
    const job = {
      jobId: 0,
      ticketNumber: this.ticketNumber,
      locationId: 1,
      clientId: this.detailForm.value.client.id,
      vehicleId: this.detailForm.value.vehicle,
      make: this.detailForm.value.type,
      model: this.detailForm.value.model,
      color: this.detailForm.value.color,
      jobType: 15,
      jobDate: new Date(),
      timeIn: new Date(),
      estimatedTimeOut: new Date(),
      actualTimeOut: new Date(),
      jobStatus: 1,
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    this.washItem.forEach(element => {
      this.additionalService = this.additionalService.filter(item => item.ServiceId !== element.ServiceId);
    });
    this.jobItems = this.additionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: 0,
        serviceId: item.ServiceId,
        commission: 0,
        price: item.Cost,
        quantity: 1,
        reviewNote: '',
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
    });
    this.washItem.forEach(element => {
      this.jobItems.push(element);
    });
    const formObj = {
      job,
      jobItem: this.jobItems
    };
    this.detailService.addDetail(formObj).subscribe(res => {
      console.log(res);
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
  }

}
