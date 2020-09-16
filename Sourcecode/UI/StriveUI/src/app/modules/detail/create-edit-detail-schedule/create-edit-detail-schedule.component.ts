import { Component, OnInit, EventEmitter, Output, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import * as moment from 'moment';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import { PrintWashComponent } from 'src/app/shared/components/print-wash/print-wash.component';

@Component({
  selector: 'app-create-edit-detail-schedule',
  templateUrl: './create-edit-detail-schedule.component.html',
  styleUrls: ['./create-edit-detail-schedule.component.css']
})
export class CreateEditDetailScheduleComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  @ViewChild(PrintWashComponent) printWashComponent: PrintWashComponent;
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
  outsideServices: any;
  @Output() closeDialog = new EventEmitter();
  @Output() refreshDetailGrid = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() bayScheduleObj?: any;
  baylist: any = [];
  jobTypeId: any;
  isBarcode = false;
  memberService: any[];
  note = '';
  headerData: string;
  showVehicleDialog: boolean;
  showClientDialog: boolean;
  address: any;
  clientId: any;
  showDialog: boolean;
  employeeList: any = [];
  isAssign: boolean;
  assignedDetailService = [];
  submitted: boolean;
  isViewPastNotes: boolean;
  viewNotes: any = [];
  viewNotesDialog: boolean;
  constructor(
    private fb: FormBuilder,
    private wash: WashService,
    private toastr: MessageServiceToastr,
    private detailService: DetailService,
    private spinner: NgxSpinnerService,
    private datePipe: DatePipe,
    private client: ClientService
  ) { }

  ngOnInit(): void {
    console.log(localStorage.getItem('empLocationId'), 'locationId');
    this.showDialog = false;
    this.submitted = false;
    this.isAssign = false;
    this.isViewPastNotes = false;
    this.viewNotesDialog = false;
    this.getEmployeeList();
    this.formInitialize();
    this.getAllBayById();
    this.getJobType();
  }

  formInitialize() {
    this.detailForm = this.fb.group({
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
      bay: [''],
      inTime: [''],
      dueTime: [''],
      outsideServie: ['']
    });
    if (!this.isEdit) {
      this.getTicketNumber();
    } else {
      this.getAllList();
    }
  }

  getAllList() {
    this.assignDate();
    this.getColor();
    this.getAllClient();
    this.getServiceType();
  }

  getTicketNumber() {
    this.wash.getTicketNumber().subscribe(data => {
      this.ticketNumber = data;
    });
    this.assignDate();
    this.getColor();
    this.getAllClient();
    this.getServiceType();
  }

  get f() {
    return this.detailForm.controls;
  }

  assignDate() {
    if (!this.isEdit) {
      const time = this.bayScheduleObj.time.split(':');
      const hours = time[0];
      const minutes = time[1];
      this.bayScheduleObj.date.setHours(hours);
      this.bayScheduleObj.date.setMinutes(minutes);
      this.bayScheduleObj.date.setSeconds('00');
      const inTime = this.datePipe.transform(this.bayScheduleObj.date, 'yyyy-MM-dd hh:mm');
      this.getWashTimeByLocationID();
      this.detailForm.patchValue({
        bay: this.bayScheduleObj.bayId,
        inTime
      });
      console.log(this.bayScheduleObj, 'bayScheduleObj');
    }
  }

  getWashTimeByLocationID() {
    const locationId = localStorage.getItem('empLocationId');
    this.detailService.getWashTimeByLocationId(locationId).subscribe(res => {
      if (res.status === 'Success') {
        const washTime = JSON.parse(res.resultData);
        const WashTimeMinutes = washTime.Location.Location.WashTimeMinutes;
        // dt.setMinutes(dt.getMinutes() + 30);
        let outTime = this.bayScheduleObj.date.setMinutes(this.bayScheduleObj.date.getMinutes() + WashTimeMinutes);
        outTime = this.datePipe.transform(outTime, 'MM-dd-yyyy, HH:mm');
        this.detailForm.patchValue({
          dueTime: outTime
        });
        console.log(washTime, 'washTime');
      }
    });
  }

  getByBarcode(barcode) {
    if (barcode === '') {
      this.toastr.showMessage({ severity: 'warning', title: 'Warning', body: 'Please enter Barcode' });
      return;
    }
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        this.isBarcode = true;
        const wash = JSON.parse(data.resultData);
        if (wash.ClientAndVehicleDetail !== null && wash.ClientAndVehicleDetail.length > 0) {
          this.barcodeDetails = wash.ClientAndVehicleDetail[0];
          this.getClientVehicle(this.barcodeDetails.ClientId);
          this.getPastClientNotesById(this.barcodeDetails.ClientId);
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
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Invalid Barcode' });
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
          // this.detailForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  washService(data) {
    if (this.isEdit) {
      this.washItem.filter(i => i.ServiceTypeId === 16)[0].IsDeleted = true;
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 16);
        this.washItem.filter(i => i.ServiceTypeId === 16)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 16);
        const serviceWash = this.details.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 16);
      const serviceWash = this.details.filter(item => item.ServiceId === Number(data));
      if (serviceWash.length !== 0) {
        this.additionalService.push(serviceWash[0]);
      }
    }
    console.log(this.additionalService, this.washItem);
  }

  outSideService(data) {
    if (this.isEdit) {
      this.washItem.filter(i => i.ServiceTypeId === 648)[0].IsDeleted = true;
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 648);
        this.washItem.filter(i => i.ServiceTypeId === 648)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 648);
        const serviceWash = this.outsideServices.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 648);
      const serviceWash = this.outsideServices.filter(item => item.ServiceId === Number(data));
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
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
        const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
        if (serviceUpcharge.length !== 0) {
          this.additionalService.push(serviceUpcharge[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
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
      if (data.IsChecked) {
        this.washItem.forEach(item => {
          if (item.ServiceId === data.ServiceId) {
            item.IsDeleted = true;
          } else {
            item.IsDeleted = false;
          }
        });
      }
      // this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted = this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted ? false : true;
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
        this.outsideServices = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[6].CodeValue);
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
    this.getClientVehicle(this.selectedData?.Details?.ClientId);
    this.note = this.selectedData.Details.Notes;
    this.detailForm.patchValue({
      barcode: this.selectedData.Details.Barcode,
      bay: this.selectedData.Details.BayId,
      inTime: this.datePipe.transform(this.selectedData.Details.TimeIn, 'MM-dd-yyyy, HH:mm'),
      dueTime: this.datePipe.transform(this.selectedData.Details.EstimatedTimeOut, 'MM-dd-yyyy, HH:mm'),
      client: { id: this.selectedData?.Details?.ClientId, name: this.selectedData?.Details.ClientName },
      vehicle: this.selectedData.Details.VehicleId,
      type: this.selectedData.Details.Make,
      model: this.selectedData.Details.Model,
      color: this.selectedData.Details.Color,
      washes: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === 16)[0]?.ServiceId,
      upchargeType: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === 18)[0]?.ServiceId,
      upcharges: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === 18)[0]?.ServiceId,
      airFreshners: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === 19)[0]?.ServiceId,
      outsideServie: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === 648)[0]?.ServiceId
    });
    // this.getByBarcode(this.selectedData?.Details?.Barcode);
    this.ticketNumber = this.selectedData?.Details?.TicketNumber;
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
        if (this.isEdit) {
          vehicle.VehicleDetails.forEach( item  => {
            if (this.selectedData.Details.Make === item.CodeId ) {
                this.selectedData.Details.vehicleMake = item.CodeValue;
            } else if (this.selectedData.Details.Model === item.CodeId) {
              this.selectedData.Details.vehicleModel = item.CodeValue;
            } else if (this.selectedData.Details.Color === item.CodeId) {
              this.selectedData.Details.vehicleColor = item.CodeValue;
            }
        });
        }
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
    this.clientId = event.id;
    this.getClientVehicle(this.clientId);
    this.getPastClientNotesById(this.clientId);
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
          vehicle: vData.ClientVehicleId,
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
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
        const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
        if (serviceAir.length !== 0) {
          this.additionalService.push(serviceAir[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
      const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
      if (serviceAir.length !== 0) {
        this.additionalService.push(serviceAir[0]);
      }
    }
    console.log(this.additionalService, this.washItem);
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.detailForm.patchValue({ vehicle: '' });
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (!this.isEdit && !this.isBarcode) {
          this.detailForm.patchValue({ vehicle: this.vehicle[0].VehicleId });
          this.getVehicleById(+this.vehicle[0].VehicleId);
          this.getMembership(+this.vehicle[0].VehicleId);
        }
        if (this.isEdit && this.selectedData.Details !== null) {
          this.detailForm.patchValue({ vehicle: this.selectedData.Details.VehicleId });
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  saveDetail() {
    this.submitted = true;
    if (this.detailForm.invalid) {
      return;
    }
    this.additional.forEach(element => {
      if (element.IsChecked) {
        this.additionalService.push(element);
      }
    });
    const job = {
      jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
      ticketNumber: this.ticketNumber,
      locationId: localStorage.getItem('empLocationId'),
      clientId: this.detailForm.value.client.id,
      vehicleId: this.detailForm.value.vehicle,
      make: this.detailForm.value.type,
      model: this.detailForm.value.model,
      color: this.detailForm.value.color,
      jobType: this.jobTypeId,
      jobDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      timeIn: moment(this.detailForm.value.inTime).format(),
      estimatedTimeOut: moment(this.detailForm.value.dueTime).format(),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
      // barcode: this.detailForm.value.barcode,
      notes: this.note
    };
    const jobDetail = {
      jobDetailId: 0,
      jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
      bayId: this.detailForm.value.bay,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0
    };
    const baySchedule = {
      bayScheduleID: 0,
      bayId: this.detailForm.value.bay,
      jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
      scheduleDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      scheduleInTime: this.datePipe.transform(this.detailForm.value.inTime, 'hh:mm'),
      scheduleOutTime: this.datePipe.transform(this.detailForm.value.dueTime, 'hh:mm'),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
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
    this.washItem.forEach(item => {
      item.isActive = true;
    });
    this.washItem.forEach(element => {
      this.jobItems.push(element);
    });
    this.assignedDetailService.forEach(item => {
      this.jobItems.push({
        jobItemId: 0,
        jobId: this.isEdit ? this.selectedData.Details.JobId : 0,
        serviceId: item.ServiceId,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        updatedBy: 0,
        employeeId: item.EmployeeId
      });
    });
    const formObj = {
      job,
      jobItem: this.jobItems,
      jobDetail,
      baySchedule
    };
    if (this.isEdit === true) {
      this.detailService.updateDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Wash Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          this.refreshDetailGrid.emit();
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    } else {
      this.spinner.show();
      this.detailService.addDetail(formObj).subscribe(res => {
        this.spinner.hide();
        console.log(res);
        if (res.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          this.refreshDetailGrid.emit();
        }
      });
    }
  }

  getAllBayById() {
    const locationId = localStorage.getItem('empLocationId');
    this.detailService.getAllBayById(locationId).subscribe(res => {
      if (res.status === 'Success') {
        const bay = JSON.parse(res.resultData);
        if (bay.BayDetailsForLocationId.length > 0) {
          this.baylist = bay.BayDetailsForLocationId.map((item, index) => {
            return {
              id: item.BayId,
              name: 'Bay - ' + index,
              bayName: item.BayName
            };
          });
        }
      }
    });
  }

  deleteDetail() {
    console.log(this.selectedData);
    this.detailService.deleteDetail(this.selectedData.Details.JobId).subscribe(res => {  // need to change
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record deleted Successfully!!' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.refreshDetailGrid.emit();
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
          const washService = this.memberService.filter(i => Number(i.ServiceTypeId) === 15);
          if (washService.length !== 0) {
            this.washService(washService[0].ServiceId);
          } else {
            // this.detailForm.get('washes').reset();
          }
        } else {
          // this.detailForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
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

  addVehicle() {
    this.headerData = 'Add New Vehicle';
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
      address2: "",
      phoneNumber2: this.clientFormComponent.clientForm.value.phone2,
      isActive: true,
      zip: this.clientFormComponent.clientForm.value.zipcode,
      state: this.clientFormComponent.State,
      city: this.clientFormComponent.city,
      country: 38,
      phoneNumber: this.clientFormComponent.clientForm.value.phone1,
      email: this.clientFormComponent.clientForm.value.email,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    }];
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: "",
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: 1,
      maritalStatus: 1,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: this.clientFormComponent.clientForm.value.status == 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
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
    };
    this.client.addClient(myObj).subscribe(data => {
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        this.closePopupEmitClient();
        this.getAllClient();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.clientFormComponent.clientForm.reset();
      }
    });
  }

  assignEmployee() {
    this.showDialog = true;
  }

  storedService(event) {
    this.assignedDetailService = event.service;
    this.isAssign = true;
    this.showDialog = false;
  }

  closeAssignModel() {
    if (!this.isAssign) {
      this.assignedDetailService = [];
    }
    this.showDialog = false;
  }

  getEmployeeList() {
    this.detailService.getAllEmployeeList().subscribe(res => {
      if (res.status === 'Success') {
        const employee = JSON.parse(res.resultData);
        this.employeeList = employee.EmployeeList.Employee;
      }
    });
  }

  getPastClientNotesById(clientID) {
    this.viewNotes = [];
    this.detailService.getPastClientNotesById(clientID).subscribe( res => {
      if (res.status === 'Success') {
        const viewPast = JSON.parse(res.resultData);
        if (viewPast.PastClientNotesByClientId.length > 0) {
          this.isViewPastNotes = true;
          this.viewNotes = viewPast.PastClientNotesByClientId;
        } else {
          this.isViewPastNotes = false;
        }
      }
    });
  }

  pastNotes() {
    this.viewNotesDialog = true;
  }

  print() {
    this.printWashComponent.print();
  }
}
