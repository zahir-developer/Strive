import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { isEmpty } from 'rxjs/operators';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { Router } from '@angular/router';
import { PrintWashComponent } from 'src/app/shared/components/print-wash/print-wash.component';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
declare var $: any;

@Component({
  selector: 'app-create-edit-washes',
  templateUrl: './create-edit-washes.component.html',
  styleUrls: ['./create-edit-washes.component.css']
})
export class CreateEditWashesComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  @ViewChild(PrintWashComponent) printWashComponent: PrintWashComponent;
  washForm: FormGroup;
  timeIn: any;
  timeOut: any;
  minutes: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  Score: any;
  ShowVehicle = false;
  ticketNumber: any;
  barcodeDetails: any;
  vehicle: any;
  color: any;
  type: any;
  jobTypeId: any;
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
  filteredModel: any = [];
  filteredcolor: any = [];
  filteredMake: any = [];
  memberService: any[];
  submitted: boolean;
  isBarcode: boolean = false;
  headerData: string;
  showVehicleDialog: boolean;
  showClientDialog: boolean;
  clientId: any;
  address: any;
  closeclientDialog: any;
  printData: any;
  isPrint: boolean;
  jobStatus: any = [];
  jobStatusId: number;
  clientName = '';
  washTime: any;
  vehicleNumber: number;
  washId: any;
  upchargeId: any;
  airFreshenerId: any;
  additionalId: any;
  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr,
    private wash: WashService, private client: ClientService, private router: Router, private detailService: DetailService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.getJobStatus();
    this.isPrint = false;
    this.formInitialize();
    this.timeInDate = new Date();
    this.Score = [{ CodeId: 1, CodeValue: "None" }, { CodeId: 2, CodeValue: "Option1" }, { CodeId: 3, CodeValue: "Option2" }];
    if (this.isView === true) {
      this.viewWash();
    }
    this.getJobType();
  }

  formInitialize() {
    this.washForm = this.fb.group({
      client: ['',],
      vehicle: ['', Validators.required],
      type: ['',],
      barcode: ['',],
      washes: ['', Validators.required],
      model: ['',],
      color: ['',],
      upcharges: ['',],
      upchargeType: ['',],
      airFreshners: ['',],
      notes: ['',],
      pastNotes: ['',]
    });
    this.getTicketNumber();
  }

  get f() {
    return this.washForm.controls;
  }

  getTicketNumber() {
    if (!this.isEdit) {
      this.ticketNumber = Math.floor(100000 + Math.random() * 900000);
    }
   
    this.getWashTimeByLocationID();
    this.getServiceType();
    this.getColor();
  }

  getWashTimeByLocationID() {
    const locationId = localStorage.getItem('empLocationId');
    this.detailService.getWashTimeByLocationId(locationId).subscribe(res => {
      if (res.status === 'Success') {
        const washTime = JSON.parse(res.resultData);
        const WashTimeMinutes = washTime.Location.Location.WashTimeMinutes;
        this.washTime = WashTimeMinutes;
        const dt = new Date();
        this.timeOutDate = dt.setMinutes(dt.getMinutes() + this.washTime);
      }
    });
  }

  getWashById() {
    this.getVehicleList(this.selectedData?.Washes[0]?.ClientId);
    this.washForm.patchValue({
      barcode: this.selectedData?.Washes[0]?.Barcode,
      client: { id: this.selectedData?.Washes[0]?.ClientId, name: this.selectedData?.Washes[0]?.ClientName },
      vehicle: this.selectedData.Washes[0].VehicleId,
      type: { id: this.selectedData.Washes[0].Make, name: this.selectedData?.Washes[0]?.vehicleMake },
      model: { id: this.selectedData?.Washes[0]?.Model, name: this.selectedData?.Washes[0]?.vehicleModel },
      color: { id: this.selectedData.Washes[0].Color, name: this.selectedData?.Washes[0]?.vehicleColor },
      notes: this.selectedData.Washes[0].ReviewNote,
      pastNotes: this.selectedData.Washes[0].PastHistoryNote,
      washes: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0]?.ServiceId,
      upchargeType: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId,
      upcharges: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId,
      airFreshners: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0]?.ServiceId,
    });
    this.clientId = this.selectedData?.Washes[0]?.ClientId;
    if (this.selectedData?.Washes[0]?.ClientName.toLowerCase().startsWith('drive')) {
      this.washForm.get('vehicle').disable();
    } else if (!this.isView) {
      this.washForm.get('vehicle').enable();
    }
    this.ticketNumber = this.selectedData.Washes[0].TicketNumber;
    this.washItem = this.selectedData.WashItem;
    this.washItem.forEach(element => {
      if (this.additional.filter(item => item.ServiceId === element.ServiceId)[0] !== undefined) {
        this.additional.filter(item => item.ServiceId === element.ServiceId)[0].IsChecked = true;
      }
    });
  }

  vehicleChange(id) {
    this.additional.forEach(element => {
      element.IsChecked = false;
    });
    this.getMembership(id);
    this.getVehicleById(id);
  }

  getMembership(id) {
    this.wash.getMembership(+id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
        if (this.membership !== null) {
          this.membershipChange(+vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId);
          this.membership.forEach(element => {
            const additionalService = this.additional.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (additionalService !== undefined && additionalService.length !== 0) {
              additionalService.forEach(item => {
                this.change(item);
              });
            }
          });
        } else {
          this.washForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  membershipChange(data) {
    this.memberService = [];
    this.wash.getMembershipById(Number(data)).subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.memberService = membership.MembershipAndServiceDetail.MembershipService;
        if (this.memberService !== null) {
          const washService = this.memberService.filter(i => Number(i.ServiceTypeId) === this.washId);
          if (washService.length !== 0) {
            this.washService(washService[0].ServiceId);
          } else {
            this.washForm.get('washes').reset();
          }
        } else {
          this.washForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }


  getServiceType() {
    this.wash.getServiceType("SERVICETYPE").subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.serviceEnum = sType.Codes;
        this.washId = this.serviceEnum.filter(i => i.CodeValue === 'Wash Package')[0]?.CodeId;
        this.upchargeId = this.serviceEnum.filter(i => i.CodeValue === 'Wash-Upcharge')[0]?.CodeId;
        this.airFreshenerId = this.serviceEnum.filter(i => i.CodeValue === 'Air Fresheners')[0]?.CodeId;
        this.additionalId = this.serviceEnum.filter(i => i.CodeValue === 'Additonal Services')[0]?.CodeId;
        this.getAllServices();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  //To get JobType
  getJobType() {
    this.wash.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Wash') {
              this.jobTypeId = item.valueid;
            }
          });
        }
      }
    });
  }
  getVehicleById(data) {
    this.wash.getVehicleById(data).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        const vData = vehicle.Status;
        this.washForm.patchValue({
          vehicle: vData.ClientVehicleId,
          barcode: vData.Barcode,
          type: { id: vData.VehicleMakeId, name: vData.VehicleMake },
          model: { id: vData.VehicleModelId, name: vData.ModelName },
          color: { id: vData.ColorId, name: vData.Color }
        });
        this.upchargeService(vData.Upcharge);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllServices() {
    const serviceObj = {
      locationId: +localStorage.getItem('empLocationId'),
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.wash.getServices(serviceObj).subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.additional = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item =>
          item.IsActive === true && Number(item.ServiceTypeId) === this.additionalId);
        this.washes = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item =>
          item.IsActive === true && Number(item.ServiceTypeId) === this.washId);
        this.upcharges = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item =>
          item.IsActive === true && Number(item.ServiceTypeId) === this.upchargeId);
        this.airFreshner = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item =>
          item.IsActive === true && Number(item.ServiceTypeId) === this.airFreshenerId);
        this.UpchargeType = this.upcharges;
        this.additional.forEach(element => {
          element.IsChecked = false;
        });
        if (this.isEdit === true) {
          this.washForm.reset();
          this.getWashById();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }



  filterClient(event) {
    const filtered: any[] = [];
    const query = event.query;
    this.wash.getAllClients(query).subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.ClientName.forEach(item => {
          item.fullName = item.FirstName + ' ' + item.LastName;
        });
        this.clientList = client.ClientName.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  filterModel(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.model) {
      const model = i;
      if (model.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(model);
      }
    }
    this.filteredModel = filtered;
  }

  filterColor(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.color) {
      const color = i;
      if (color.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(color);
      }
    }
    this.filteredcolor = filtered;
  }

  filterMake(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.type) {
      const make = i;
      if (make.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(make);
      }
    }
    this.filteredMake = filtered;
  }

  selectedClient(event) {
    this.clientId = event.id;
    this.clientName = event.name;
    const name = event.name.toLowerCase();
    if (name.startsWith('drive')) {
      this.washForm.get('vehicle').disable();
      return;
    } else if (!this.isView) {
      this.washForm.get('vehicle').enable();
      this.getClientVehicle(this.clientId);
    }
  }

  clientChange() {
    this.clientId = this.washForm.value.client.id;
  }

  change(data) {
    const temp = this.washItem.filter(item => item.ServiceId === data.ServiceId);
    if (temp.length !== 0) {
      this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted = this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted ? false : true;
    } else {
      data.IsChecked = data.IsChecked ? false : true;
    }
  }



  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');
        this.type = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleManufacturer');
        this.model = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleModel');
        if (this.isEdit) {
          vehicle.VehicleDetails.forEach(item => {
            if (+this.selectedData.Washes[0].Make === item.CodeId) {
              this.selectedData.Washes[0].vehicleMake = item.CodeValue;
            } else if (+this.selectedData.Washes[0].Model === item.CodeId) {
              this.selectedData.Washes[0].vehicleModel = item.CodeValue;
            } else if (+this.selectedData.Washes[0].Color === item.CodeId) {
              this.selectedData.Washes[0].vehicleColor = item.CodeValue;
            }
          });
          if (this.selectedData.Washes !== null && this.selectedData.WashItem !== null) {
            this.printData = {
              Details: this.selectedData.Washes[0],
              DetailsItem: this.selectedData.WashItem
            };
          }
        }
        this.model = this.model.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });
        this.color = this.color.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });
        this.type = this.type.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  viewWash() {
    this.washForm.disable();
  }

  checkValue(type) {
    if (type === 'make' && this.washForm.value.type !== '') {
      if (!this.washForm.value.type.hasOwnProperty('id')) {
        this.washForm.patchValue({ type: '' });
        this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'Please select valid type' });
      }
    } else if (type === 'model' && this.washForm.value.model !== '') {
      if (!this.washForm.value.model.hasOwnProperty('id')) {
        this.washForm.patchValue({ model: '' });
        this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'Please select valid model' });
      }
    } else if (type === 'color' && this.washForm.value.color !== '') {
      if (!this.washForm.value.color.hasOwnProperty('id')) {
        this.washForm.patchValue({ color: '' });
        this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'Please select valid color' });
      }
    }
  }


  // Get Client And Vehicle Details By Barcode
  getByBarcode(barcode) {
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        this.isBarcode = true;
        const wash = JSON.parse(data.resultData);
        if (wash.ClientAndVehicleDetail !== null && wash.ClientAndVehicleDetail.length > 0) {
          this.barcodeDetails = wash.ClientAndVehicleDetail[0];
          this.getClientVehicle(this.barcodeDetails.ClientId);
          setTimeout(() => {
            this.washForm.patchValue({
              client: { id: this.barcodeDetails.ClientId, name: this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName },
              vehicle: this.barcodeDetails.VehicleId,
            });
            this.getMembership(this.barcodeDetails.VehicleId);
          }, 200);
        } else {
          const barCode = this.washForm.value.barcode;
          this.washForm.reset();
          this.washForm.patchValue({ barcode: barCode });
          this.additional.forEach(element => {
            element.IsChecked = false;
          });
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getVehicleList(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (this.vehicle.length !== 0) {
          this.washForm.patchValue({ vehicle: this.vehicle[0].VehicleId });
          this.getVehicleById(+this.vehicle[0].VehicleId);
          this.getMembership(+this.vehicle[0].VehicleId);
        } else {
          this.washForm.get('vehicle').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  washService(data) {
    if (this.isEdit) {
      this.washItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0].IsDeleted = true;
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.washId);
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.washId);
        const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.washId);
      const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
      if (serviceWash.length !== 0) {
        this.additionalService.push(serviceWash[0]);
      }
    }
    this.washForm.patchValue({ washes: +data });
  }

  upchargeService(data) {
    if (this.isEdit) {
      if (this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0] !== undefined) {
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.upchargeId);
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.upchargeId);
        const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
        if (serviceUpcharge.length !== 0) {
          this.additionalService.push(serviceUpcharge[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.upchargeId);
      const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
      if (serviceUpcharge.length !== 0) {
        this.additionalService.push(serviceUpcharge[0]);
      }
    }
    this.washForm.patchValue({ upcharges: +data });
    this.washForm.patchValue({ upchargeType: +data });
  }

  airService(data) {
    if (this.isEdit) {
      if (this.washItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0] !== undefined) {
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.airFreshenerId);
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.airFreshenerId);
        const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
        if (serviceAir.length !== 0) {
          this.additionalService.push(serviceAir[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.airFreshenerId);
      const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
      if (serviceAir.length !== 0) {
        this.additionalService.push(serviceAir[0]);
      }
    }
    this.washForm.patchValue({ airfreshners: +data });
  }

  // Add/Update Wash
  submit() {
    this.submitted = true;
    if (this.washForm.invalid) {
      return;
    }
    this.additional.forEach(element => {
      if (element.IsChecked) {
        this.additionalService.push(element);
      }
    });
    const currentTime = new Date();
    const outTime = currentTime.setMinutes(currentTime.getMinutes() + this.washTime);
    const job = {
      jobId: this.isEdit ? this.selectedData.Washes[0].JobId : 0,
      ticketNumber: this.ticketNumber,
      locationId: +localStorage.getItem('empLocationId'),
      clientId: this.washForm.value.client.id,
      vehicleId: this.clientName.toLowerCase().startsWith('drive') ? null : this.washForm.value.vehicle,
      make: this.washForm.value.type.id,
      model: this.washForm.value.model.id, 
      color: this.washForm.value.color.id,
      jobType: this.jobTypeId,
      jobDate: moment(this.timeInDate).format(),
      timeIn: moment(this.timeInDate).format(),
      estimatedTimeOut: moment(this.timeOutDate).format(),
      actualTimeOut: null,
      notes: this.washForm.value.notes,
      jobStatus: this.jobStatusId,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date()
    };
    this.washItem.forEach(element => {
      this.additionalService = this.additionalService.filter(item => item.ServiceId !== element.ServiceId);
    });
    this.jobItems = this.additionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: this.isEdit ? +this.selectedData.Washes[0].JobId : 0,
        serviceId: item.ServiceId,
        commission: 0,
        price: item.Cost,
        quantity: 1,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: new Date(),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: new Date()
      };
    });
    this.washItem.forEach(element => {
      this.jobItems.push(element);
    });
    const formObj = {
      job: job,
      jobItem: this.jobItems
    };
    if (this.isEdit === true) {
      this.spinner.show();
      this.wash.updateWashes(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Wash Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      }, (error) => {
        this.spinner.hide();
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      });
    } else {
      this.spinner.show();
      this.wash.addWashes(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.washForm.reset();
        }
      }, (error) => {
        this.spinner.hide();
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  addVehicle() {
    this.headerData = 'Add New Vehicle';
    let len = this.vehicle.length;
    if (len === 0) {
      this.vehicleNumber = 1;
    } else {
      this.vehicleNumber = this.vehicle.length + 1;
    }
    this.showVehicleDialog = true;
  }

  closePopupEmitVehicle(event) {
    if (event.status === 'saved') {
      this.showVehicleDialog = false;
      this.getClientVehicle(this.clientId);
    }
    this.showVehicleDialog = event.isOpenPopup;
  }

  addClient() {
    this.headerData = 'Add New Client';
    this.showClientDialog = true;
  }

  closePopupEmitClient() {
    this.showClientDialog = false;
  }

  saveClient() {
    this.clientFormComponent.submitted = true;
    this.clientFormComponent.stateDropdownComponent.submitted = true;
    if (this.clientFormComponent.clientForm.invalid) {
      return;
    }
    this.address = [{
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientFormComponent.clientForm.value.address,
      address2: null,
      phoneNumber2: this.clientFormComponent.clientForm.value.phone2,
      isActive: true,
      zip: this.clientFormComponent.clientForm.value.zipcode,
      state: this.clientFormComponent.State,
      city: this.clientFormComponent.city,
      country: null,
      phoneNumber: this.clientFormComponent.clientForm.value.phone1,
      email: this.clientFormComponent.clientForm.value.email,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date()
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: null,
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: null,
      maritalStatus: null,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: this.clientFormComponent.clientForm.value.status == 0 ? true : false,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score == "" || this.clientFormComponent.clientForm.value.score == null) ? 0 : this.clientFormComponent.clientForm.value.score,
      noEmail: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type == "" || this.clientFormComponent.clientForm.value.type == null) ? 0 : this.clientFormComponent.clientForm.value.type
    };
    const myObj = {
      client: formObj,
      clientVehicle: null,
      clientAddress: this.address
    }
    this.client.addClient(myObj).subscribe(data => {
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        this.closePopupEmitClient();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.clientFormComponent.clientForm.reset();
      }
    });
  }

  getJobStatus() {
    this.wash.getJobStatus('JOBSTATUS').subscribe(res => {
      if (res.status === 'Success') {
        const status = JSON.parse(res.resultData);
        this.jobStatus = status.Codes.filter(item => item.CodeValue === 'In Progress');
        this.jobStatusId = this.jobStatus[0].CodeId;
      }
    });
  }

  pay() {
    this.router.navigate(['/sales'], { queryParams: { ticketNumber: this.ticketNumber } });
    this.openNav('sales')
  }
  openNav(sales) {

    $('.menu li').on('click', function () {
      $('.menu li').removeClass('theme-secondary-background-color active');
      $(this).addClass('theme-secondary-background-color active');
    });
    $('.nav-slider-menu-items li a').on('click', function () {
      $('.nav-slider-menu-items li a').removeClass('theme-secondary-color text-underline');
      $(this).addClass('theme-secondary-color text-underline');
    });
  }
  print() {
    this.isPrint = true;
    this.printWashComponent.print();
  }
}

