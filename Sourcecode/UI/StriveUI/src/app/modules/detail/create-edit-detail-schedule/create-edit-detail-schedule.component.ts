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
  details: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  baylist: any = [];
  jobTypeId: any;
  constructor(
    private fb: FormBuilder,
    private wash: WashService,
    private toastr: MessageServiceToastr,
    private detailService: DetailService
  ) { }

  ngOnInit(): void {
    this.formInitialize();
    this.getAllBayById();
    this.getJobType();
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
      airFreshners: ['',],
      bay: [''],
      inTime: [''],
      dueTime: ['']
    });
    this.getTicketNumber();
  }

  getTicketNumber() {
    this.wash.getTicketNumber().subscribe(data => {
      this.ticketNumber = data;
    });
    this.getColor();
    this.getAllClient();
    this.getServiceType();
  }

  getByBarcode(barcode) {
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        if (wash.ClientAndVehicleDetail !== null && wash.ClientAndVehicleDetail.length > 0) {
          this.barcodeDetails = wash.ClientAndVehicleDetail[0];
          this.getClientVehicle(this.barcodeDetails.ClientId);
          setTimeout(() => {
            this.detailForm.patchValue({
              client: { id: this.barcodeDetails.ClientId, name: this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName },
              vehicle: this.barcodeDetails.VehicleId,
              model: this.barcodeDetails.VehicleModelId,
              color: this.barcodeDetails.VehicleColor,
              type: this.barcodeDetails.VehicleMfr
            });
            this.getMembership(this.barcodeDetails.VehicleId);
          }, 200);
        } else {
          const barCode = this.detailForm.value.barcode;
          this.detailForm.reset();
          this.detailForm.patchValue({ barcode: barCode });
          this.additional.forEach(element => {
            element.IsChecked = false;
          });
        }
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
    if (this.isEdit) {
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
    if (this.isEdit) {
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
        this.details = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[1].CodeValue);
        this.additional = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[2].CodeValue);
        this.washes = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[0].CodeValue);
        this.upcharges = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[3].CodeValue);
        this.airFreshner = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[4].CodeValue);
        this.UpchargeType = this.upcharges;
        // this.upcharges = this.upcharges.filter(item => Number(item.ParentServiceId) !== 0);
        this.additional.forEach(element => {
          element.IsChecked = false;
        });
        if (this.isEdit === true) {
          this.detailForm.reset();
          this.getWashById();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getWashById() {
    console.log(this.additional);
    this.detailForm.patchValue({
      barcode: this.selectedData.Details.Barcode,
      client: { id: this.selectedData?.Details?.ClientId, name: this.selectedData?.Details.ClientName },
      vehicle: this.selectedData.Details.VehicleId,
      type: this.selectedData.Details.Make,
      model: this.selectedData.Details.Model,
      color: this.selectedData.Details.Color,
      washes: this.selectedData.DetailsItem.filter(i => i.ServiceTypeId === 16)[0]?.ServiceId,
      upchargeType: this.selectedData.DetailsItem.filter(i => i.ServiceTypeId === 18)[0]?.ServiceId,
      upcharges: this.selectedData.DetailsItem.filter(i => i.ServiceTypeId === 18)[0]?.ServiceId,
      airFreshners: this.selectedData.DetailsItem.filter(i => i.ServiceTypeId === 19)[0]?.ServiceId,
    });
    this.getByBarcode(this.selectedData?.Details?.Barcode);
    this.ticketNumber = this.selectedData.Details.TicketNumber;
    this.washItem = this.selectedData.DetailsItem;
    console.log(this.washItem);
    this.washItem.forEach(element => {
      if (this.additional.filter(item => item.ServiceId === element.ServiceId)[0] !== undefined) {
        this.additional.filter(item => item.ServiceId === element.ServiceId)[0].IsChecked = true;
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
    this.getVehicleById(id);
  }

  getVehicleById(data) {
    this.wash.getVehicleById(data).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        const vData = vehicle.Status;
        this.detailForm.patchValue({
          barcode: vData.Barcode,
          type: vData.VehicleMakeId,
          model: vData.VehicleModelId,
          color: vData.ColorId
        });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }


  airService(data) {
    if (this.isEdit) {
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
        if (!this.isEdit) {
          this.detailForm.patchValue({ vehicle: this.vehicle[0].VehicleId });
          this.getVehicleById(+this.vehicle[0].VehicleId);
          this.getMembership(+this.vehicle[0].VehicleId);
        }
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
      jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
      ticketNumber: this.ticketNumber,
      locationId: 1,
      clientId: this.detailForm.value.client.id,
      vehicleId: this.detailForm.value.vehicle,
      make: this.detailForm.value.type,
      model: this.detailForm.value.model,
      color: this.detailForm.value.color,
      jobType: this.jobTypeId,
      jobDate: new Date(),
      timeIn: new Date(),
      estimatedTimeOut: new Date(),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
      barcode: this.detailForm.value.barcode,
      notes: 'check'
    };
    this.washItem.forEach(element => {
      this.additionalService = this.additionalService.filter(item => item.ServiceId !== element.ServiceId);
    });
    this.jobItems = this.additionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
        serviceId: item.ServiceId,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        updatedBy: 0
      };
    });
    this.washItem.forEach(element => {
      this.jobItems.push(element);
    });
    const formObj = {
      job,
      jobItem: this.jobItems
    };
    if (this.isEdit === true) {
      this.detailService.updateDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Wash Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    } else {
      this.detailService.addDetail(formObj).subscribe(res => {
        console.log(res);
        if (res.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        }
      });
    }
  }

  getAllBayById() {
    this.detailService.getAllBayById(1).subscribe(res => {
      if (res.status === 'Success') {
        const bay = JSON.parse(res.resultData);
        if (bay.BayDetailsForLocationId.length > 0) {
          this.baylist = bay.BayDetailsForLocationId.map(item => {
            return {
              id: item.BayId,
              name: 'Bay - ' + item.BayId,
              bayName: item.BayName
            };
          });
        }
      }
    });
  }

  deleteDetail() {
    console.log(this.selectedData);
    this.detailService.deleteDetail(1).subscribe(res => {  // need to change
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record deleted Successfully!!' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
  }

  getJobType() {
    this.detailService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Detail') {
              this.jobTypeId = item.valueid;
            }
          });
        }
      }
    });
  }

}
