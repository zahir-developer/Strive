import { Component, OnInit, EventEmitter, Output, Input, ViewChild, ViewEncapsulation } from '@angular/core';
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
import { ConfirmationService } from 'primeng/api';
import { Router } from '@angular/router';
import * as _ from 'underscore';
import { PrintCustomerCopyComponent } from '../print-customer-copy/print-customer-copy.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { GetUpchargeService } from 'src/app/shared/services/common-service/get-upcharge.service';
import { MakeService } from 'src/app/shared/services/common-service/make.service';
import { ModelService } from 'src/app/shared/services/common-service/model.service';

@Component({
  selector: 'app-create-edit-detail-schedule',
  templateUrl: './create-edit-detail-schedule.component.html',
  styleUrls: ['./create-edit-detail-schedule.component.css'],
  providers: [ConfirmationService],
  encapsulation: ViewEncapsulation.None
})
export class CreateEditDetailScheduleComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  @ViewChild(PrintWashComponent) printWashComponent: PrintWashComponent;
  @ViewChild(PrintCustomerCopyComponent) printCustomerCopyComponent: PrintCustomerCopyComponent;
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
  upcharges = [];
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
  details: any;
  outsideServices: any = [];
  @Output() closeDialog = new EventEmitter();
  @Output() refreshDetailGrid = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() bayScheduleObj?: any;
  @Input() jobTypeId?: any;
  baylist: any = [];
  // jobTypeId: any;
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
  outsideServiceId: any;
  @Input() isView?: any;
  detailItems: any = [];
  detailsJobServiceEmployee: any = [];
  isSaveClick: boolean;
  isStart: boolean;
  jobStatus: any = [];
  isCompleted: boolean;
  jobStatusID: any;
  jobID: any;
  clientName = '';
  vehicleNumber: number;
  detailId: any;
  upchargeId: any;
  airFreshenerId: any;
  additionalId: any;
  deleteDetailList: boolean;
  body: string;
  header: string;
  title: string;
  generatedClientId: any;
  selectclient: { id: any; name: string; };
  paidLabel: string = 'Pay';
  upchargeList: any;
  ceramicUpchargeId: any;
  isCeramic: boolean;
  ceramicUpcharges: any = [];
  isDetails: boolean;
  upcharge: any;
  noCeramicUpcharges: any[];
  constructor(
    private fb: FormBuilder,
    private wash: WashService,
    private message: MessageServiceToastr,
    private toastr: ToastrService,
    private detailService: DetailService,
    private spinner: NgxSpinnerService,
    private datePipe: DatePipe,
    private makeService: MakeService,
    private modelService: ModelService,
    private client: ClientService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private GetUpchargeService: GetUpchargeService,
    private codeValueService: CodeValueService,
    private serviceSetupService: ServiceSetupService
  ) { }

  ngOnInit(): void {
    this.isSaveClick = false;
    this.showDialog = false;
    this.submitted = false;
    this.isAssign = false;
    this.isViewPastNotes = false;
    this.viewNotesDialog = false;
    this.isStart = false;
    this.isCompleted = false;
    this.formInitialize();
    this.getJobStatus();
    this.getAllMake();
    this.getEmployeeList();
    this.getAllBayById();
    this.getTicketNumber();
    // this.getJobType();
  }

  formInitialize() {
    this.detailForm = this.fb.group({
      client: ['', Validators.required],
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
    if (this.isView) {
      this.detailForm.disable();
    }
  }

  getTicketNumber() {
    if (!this.isEdit) {
      this.wash.getTicketNumber().subscribe(data => {
        const ticket = JSON.parse(data.resultData);
        if (data.status === 'Success') {
          const ticket = JSON.parse(data.resultData);
          this.ticketNumber = ticket.GetTicketNumber.TicketNumber;
          this.jobID = ticket.GetTicketNumber.JobId
        }
        else {
          this.toastr.error(MessageConfig.TicketNumber, 'Error!');
        }
      });
    }
    this.assignDate();
    this.getColor();
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
      const inTime = this.datePipe.transform(this.bayScheduleObj.date, 'MM/dd/yyyy HH:mm');
      this.addDueTime();
      this.detailForm.patchValue({
        bay: this.bayScheduleObj.bayId,
        inTime
      });
      this.detailForm.controls.bay.disable();
      this.detailForm.controls.inTime.disable();
      this.getEmployeeList();
    }
  }

  addDueTime() {
    let outTime = this.bayScheduleObj.date.setMinutes(this.bayScheduleObj.date.getMinutes() + 30);
    outTime = this.datePipe.transform(outTime, 'MM/dd/yyyy HH:mm');
    this.detailForm.patchValue({
      dueTime: outTime
    });
    this.detailForm.controls.dueTime.disable();
  }

  checkValue(type) {
    if (type === 'make') {
      if (!this.detailForm.value.type.hasOwnProperty('id')) {
        this.detailForm.patchValue({ type: '' });
      }
    } else if (type === 'model') {
      if (!this.detailForm.value.model.hasOwnProperty('id')) {
        this.detailForm.patchValue({ model: '' });
      }
    } else if (type === 'color') {
      if (!this.detailForm.value.color.hasOwnProperty('id')) {
        this.detailForm.patchValue({ color: '' });
      }
    }
  }


  getByBarcode(barcode) {
    if (barcode === '') {
      this.toastr.warning(MessageConfig.Detail.BarCode, 'Warning!');
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
            });
            this.getMembership(this.barcodeDetails.VehicleId);
          }, 200);
        } else {
          const barCode = this.detailForm.value.barcode;
          this.detailForm.reset();
          this.detailForm.patchValue({ barcode: barCode });
          this.toastr.warning(MessageConfig.Detail.InvalidBarCode, 'Warning');
          this.additional.forEach(element => {
            element.IsChecked = false;
          });
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  washService(data) {
    console.log(data, 'washservice')
    this.isDetails = true;
    if (this.isEdit) {
      this.washItem.filter(i => i.ServiceTypeId === this.detailId)[0].IsDeleted = true;
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.detailId);
        this.washItem.filter(i => i.ServiceTypeId === this.detailId)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.detailId);
        const serviceWash = this.details.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
          if (serviceWash[0].IsCeramic === false) {
            console.log(this.additionalService[0], 'this.additionalService[0]')
            this.isCeramic = false;
            this.upcharges = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
            this.UpchargeType = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
          }
          else {
            this.isCeramic = true;
            this.upcharges = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
            this.UpchargeType = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
          }
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.detailId);
      const serviceWash = this.details.filter(item => item.ServiceId === Number(data));
      if (serviceWash.length !== 0) {
        this.additionalService.push(serviceWash[0]);
        if (serviceWash[0].IsCeramic === false) {
          console.log(this.additionalService[0], 'this.additionalService[0]')
          this.isCeramic = false;
          this.upcharges = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
          this.UpchargeType = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
        }
        else {
          this.isCeramic = true;
          this.upcharges = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
          this.UpchargeType = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
        }
      }
    }

    this.detailForm.patchValue({ upcharges: +data ? +data : '' });
    this.detailForm.patchValue({ upchargeType: +data ? +data : '' });
    this.getUpcharge();
  }

  outSideService(data) {
    if (this.isEdit) {
      if (this.washItem.filter(i => Number(i.ServiceTypeId) === this.outsideServiceId)[0] !== undefined) {
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.outsideServiceId)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.outsideServiceId);
        this.washItem.filter(i => Number(i.ServiceTypeId) === this.outsideServiceId)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.outsideServiceId);
        const serviceAir = this.outsideServices.filter(item => item.ServiceId === Number(data));
        if (serviceAir.length !== 0) {
          this.additionalService.push(serviceAir[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.outsideServiceId);
      const serviceAir = this.outsideServices.filter(item => item.ServiceId === Number(data));
      if (serviceAir.length !== 0) {
        this.additionalService.push(serviceAir[0]);
      }
    }
    this.detailForm.patchValue({ outsideServie: +data ? +data : '' });
  }

  upchargeService(data) {
    if (this.isCeramic === false) {
      this.upcharges = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
      this.UpchargeType = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId));
    }
    else {
      this.upcharges = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
      this.UpchargeType = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId));
    }
    if (this.isEdit) {
      if (this.washItem.filter(i => i.ServiceTypeId === this.upchargeId)[0] !== undefined) {
        this.washItem.filter(i => i.ServiceTypeId === this.upchargeId)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => i.ServiceId === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== this.upchargeId);
        this.washItem.filter(i => i.ServiceTypeId === this.upchargeId)[0].IsDeleted = false;
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
    this.detailForm.patchValue({ upcharges: +data ? +data : '' });
    this.detailForm.patchValue({ upchargeType: +data ? +data : '' });
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
    } else {
      data.IsChecked = data.IsChecked ? false : true;
    }
  }

  getAllClient() {
    this.wash.getAllClientName().subscribe(res => {
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
      }
    });
  }

  getServiceType() {
    const serviceTypeValue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceEnum = serviceTypeValue;
      this.detailId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.DetailPackage)[0]?.CodeId;
      this.upchargeId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.DetailUpcharge)[0]?.CodeId;
      this.ceramicUpchargeId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.DetailCeramicUpcharge)[0]?.CodeId;

      this.airFreshenerId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.AirFresheners)[0]?.CodeId;
      this.additionalId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices)[0]?.CodeId;
      this.outsideServiceId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.OutsideServices)[0]?.CodeId;
      this.getAllServices();
    }

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
    this.serviceSetupService.getAllServiceDetail(+localStorage.getItem('empLocationId')).subscribe(res => {
      if (res.status === 'Success') {
        const serviceDetails = JSON.parse(res.resultData);
        if (serviceDetails.AllServiceDetail !== null) {
          this.outsideServices = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.outsideServiceId);
          this.details = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.detailId);
          this.additional = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.additionalId);
          this.noCeramicUpcharges = []
          this.ceramicUpcharges = []
          this.noCeramicUpcharges = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.upchargeId);
          this.ceramicUpcharges = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.ceramicUpchargeId);



          this.airFreshner = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.airFreshenerId);

          this.additional.forEach(element => {
            element.IsChecked = false;
          });
          if (this.isEdit === true) {
            this.detailForm.reset();
            this.getWashById();
          }
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });

  }

  getWashById() {
    const isJobStatus = _.where(this.jobStatus, { CodeId: this.selectedData?.Details?.JobStatus });
    if (isJobStatus.length > 0) {
      if (isJobStatus[0].CodeValue === ApplicationConfig.CodeValue.inProgress) {
        this.isCompleted = true;
        this.jobStatusID = isJobStatus[0].CodeId;
      } else if (isJobStatus[0].CodeValue === ApplicationConfig.CodeValue.Completed) {
        this.isCompleted = true;
        this.isStart = true;
        this.jobStatusID = isJobStatus[0].CodeId;
      } else if (isJobStatus[0].CodeValue === ApplicationConfig.CodeValue.Waiting) {
        this.isStart = true;
        this.isCompleted = false;
        this.jobStatusID = isJobStatus[0].CodeId;
      }
    }
    this.getVehicleList(this.selectedData?.Details?.ClientId);
    this.getPastClientNotesById(this.selectedData?.Details?.ClientId);
    this.note = this.selectedData?.Details?.Notes;
    this.detailItems = this.selectedData?.DetailsItem;
    this.jobID = this.selectedData?.Details?.JobId;
    this.detailsJobServiceEmployee = this.selectedData.DetailsJobServiceEmployee !== null ?
      this.selectedData.DetailsJobServiceEmployee : [];
    if (this.selectedData?.Details?.IsPaid == "True") {
      this.paidLabel = 'Paid'
    }
    else {
      this.paidLabel = 'Pay'
    }
    const washes = this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.detailId)[0]?.ServiceId ?
      this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.detailId) : '';
    if (washes[0].IsCeramic == false) {

      this.isCeramic = false;
      this.upcharges = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId))

      this.UpchargeType = this.noCeramicUpcharges.filter(item => item.ServiceTypeId === Number(this.upchargeId))
    }
    else {
      this.isCeramic = true;
      this.upcharges = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId))

      this.UpchargeType = this.ceramicUpcharges.filter(item => item.ServiceTypeId === Number(this.ceramicUpchargeId))

    }

    this.detailForm.patchValue({
      barcode: this.selectedData?.Details?.Barcode,
      bay: this.selectedData?.Details?.BayId,
      inTime: this.datePipe.transform(this.selectedData?.Details?.TimeIn, 'MM/dd/yyyy HH:mm'),
      dueTime: this.datePipe.transform(this.selectedData?.Details?.EstimatedTimeOut, 'MM/dd/yyyy HH:mm'),
      client: { id: this.selectedData?.Details?.ClientId, name: this.selectedData?.Details?.ClientName },
      vehicle: this.selectedData?.Details?.VehicleId,
      type: { id: this.selectedData?.Details?.Make, name: this.selectedData?.Details?.VehicleMake },
      model: { id: this.selectedData?.Details?.Model, name: this.selectedData?.Details?.VehicleModel },
      color: { id: this.selectedData?.Details?.Color, name: this.selectedData?.Details?.VehicleColor },
      washes: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.detailId)[0]?.ServiceId ?
        this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.detailId)[0]?.ServiceId : '',
      upchargeType: washes[0].IsCeramic === false ?


        this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.upchargeId)[0]?.ServiceId ?
          this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.upchargeId)[0]?.ServiceId : '' : this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.ceramicUpchargeId)[0]?.ServiceId ?
          this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.ceramicUpchargeId)[0]?.ServiceId : '',


      upcharges: washes[0].IsCeramic === false ?


        this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.upchargeId)[0]?.ServiceId ?
          this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.upchargeId)[0]?.ServiceId : '' : this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.ceramicUpchargeId)[0]?.ServiceId ?
          this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.ceramicUpchargeId)[0]?.ServiceId : '',
      airFreshners: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.airFreshenerId)[0]?.ServiceId ?
        this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.airFreshenerId)[0]?.ServiceId : '',
      outsideServie: this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.outsideServiceId)[0]?.ServiceId ?
        this.selectedData.DetailsItem.filter(i => +i.ServiceTypeId === this.outsideServiceId)[0]?.ServiceId : ''
    });
    this.clientId = this.selectedData?.Details?.ClientId;
    this.getModel(this.selectedData?.Details?.Make)
    this.detailForm.controls.bay.disable();
    this.detailForm.controls.inTime.disable();
    this.detailForm.controls.dueTime.disable();
    this.ticketNumber = this.selectedData?.Details?.TicketNumber;
    this.washItem = this.selectedData.DetailsItem;
    this.washItem.forEach(element => {
      if (this.additional.filter(item => item.ServiceId === element.ServiceId)[0] !== undefined) {
        this.additional.filter(item => item.ServiceId === element.ServiceId)[0].IsChecked = true;
      }
    });
    if (this.selectedData?.Details?.ClientName.toLowerCase().startsWith('drive')) {
      this.detailForm.get('vehicle').disable();
    } else if (!this.isView) {
      this.detailForm.get('vehicle').enable();
    }


  }

  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');

        if (this.isEdit) {
          vehicle.VehicleDetails.forEach(item => {
            if (this.selectedData?.Details?.Make === item.CodeId) {
              this.selectedData.Details.vehicleMake = item.CodeValue;
            } else if (this.selectedData?.Details?.Model === item.CodeId) {
              this.selectedData.Details.vehicleModel = item.CodeValue;
            } else if (this.selectedData?.Details?.Color === item.CodeId) {
              this.selectedData.Details.vehicleColor = item.CodeValue;
            }
          });
        }



        this.color = this.color.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });



      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  onKeyUp(event) {
    if (event.target.value === '') {
      this.detailForm.patchValue({ vehicle: '', barcode: '', type: '', model: '', color: '' });
    }
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
      this.detailForm.get('vehicle').disable();
      return;
    } else if (!this.isView) {
      this.detailForm.get('vehicle').enable();
      this.getClientVehicle(this.clientId);
      this.getPastClientNotesById(this.clientId);
    }
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
          type: { id: vData.VehicleMakeId, name: vData.VehicleMake },
          model: { id: vData.VehicleModelId, name: vData.ModelName },
          color: { id: vData.ColorId, name: vData.Color }
        });
        this.getModel(vData.VehicleMakeId)
        this.getUpcharge()
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
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
    this.detailForm.patchValue({ airfreshners: +data });
  }

  getVehicleList(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (this.vehicle.length !== 0) {
          this.detailForm.patchValue({ vehicle: this.vehicle[0].VehicleId });
          this.getVehicleById(+this.vehicle[0].VehicleId);
          this.getMembership(+this.vehicle[0].VehicleId);
        } else {
          this.detailForm.get('vehicle').reset();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  start() {
    const jobstatus = _.where(this.jobStatus, { CodeValue: ApplicationConfig.CodeValue.inProgress });
    let jobStatusId;
    if (jobstatus.length > 0) {
      jobStatusId = jobstatus[0].CodeId;
    }
    this.detailForm.controls.inTime.enable();
    this.detailForm.controls.dueTime.enable();
    if (this.isEdit) {
      this.detailForm.controls.bay.enable();
    }

    const job = {
      jobId: this.selectedData.Details.JobId,
      ticketNumber: this.ticketNumber,
      locationId: localStorage.getItem('empLocationId'),
      clientId: this.detailForm.value.client.id,
      vehicleId: this.detailForm.value.vehicle,
      make: this.detailForm.value.type.id,
      model: this.detailForm.value.model.id,
      color: this.detailForm.value.color.id,
      jobType: this.jobTypeId,
      jobDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      jobStatus: jobStatusId,
      timeIn: moment(this.detailForm.value.inTime).format(),
      estimatedTimeOut: moment(this.detailForm.value.dueTime).format(),
      actualTimeOut: new Date(),
      isActive: true,
      isDeleted: false,
      checkOut: false,
      createdBy: 0,
      updatedBy: 0,
      notes: this.note
    };
    const formObj = {
      job,
      jobItem: null,
      jobDetail: null,
      baySchedule: null
    };
    this.spinner.show();
    this.detailService.updateDetail(formObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.isStart = false;
        this.isCompleted = true;
        this.detailForm.controls.inTime.disable();
        this.detailForm.controls.dueTime.disable();
        this.detailForm.controls.bay.disable();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  completed() {
    const jobstatus = _.where(this.jobStatus, { CodeValue: ApplicationConfig.CodeValue.Completed });
    let jobStatusId;
    if (jobstatus.length > 0) {
      jobStatusId = jobstatus[0].CodeId;
    }
    this.detailForm.controls.inTime.enable();
    this.detailForm.controls.dueTime.enable();
    if (this.isEdit) {
      this.detailForm.controls.bay.enable();
    }

    const job = {
      jobId: this.selectedData.Details.JobId,
      ticketNumber: this.ticketNumber,
      locationId: localStorage.getItem('empLocationId'),
      clientId: this.detailForm.value.client.id,
      vehicleId: this.detailForm.value.vehicle,
      make: this.detailForm.value.type.id,
      model: this.detailForm.value.model.id,
      color: this.detailForm.value.color.id,
      jobType: this.jobTypeId,
      jobDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      jobStatus: jobStatusId,
      timeIn: moment(this.detailForm.value.inTime).format(),
      estimatedTimeOut: moment(this.detailForm.value.dueTime).format(),
      actualTimeOut: new Date(),
      isActive: true,
      isDeleted: false,
      checkOut: true,
      createdBy: 0,
      updatedBy: 0,
      notes: this.note
    };
    const formObj = {
      job,
      jobItem: null,
      jobDetail: null,
      baySchedule: null
    };
    this.spinner.show();
    this.detailService.updateDetail(formObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.isCompleted = false;
        this.isStart = false;
        this.detailForm.controls.inTime.disable();
        this.detailForm.controls.dueTime.disable();
        this.detailForm.controls.bay.disable();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  saveDetail() {
    this.submitted = true;
    if (this.detailForm.invalid) {
      return;
    }
    if (!this.ticketNumber) {
      this.toastr.error(MessageConfig.TicketNumber, 'Error!');
      return;

    }
    this.detailForm.controls.inTime.enable();
    this.detailForm.controls.dueTime.enable();
    this.detailForm.controls.bay.enable();
    this.additional.forEach(element => {
      if (element.IsChecked) {
        this.additionalService.push(element);
      }
    });
    const jobstatus = _.where(this.jobStatus, { CodeValue: ApplicationConfig.CodeValue.Waiting });
    let jobStatusId;
    if (jobstatus.length > 0) {
      jobStatusId = jobstatus[0].CodeId;
    }
    const job = {
      jobId: this.isEdit ? this.selectedData.Details.JobId : this.jobID,
      ticketNumber: this.ticketNumber,
      locationId: localStorage.getItem('empLocationId'),
      clientId: this.detailForm.value.client.id,
      vehicleId: this.clientName.toLowerCase().startsWith('drive') ? null : this.detailForm.value.vehicle,
      make: this.detailForm.value.type.id,
      model: this.detailForm.value.model.id,
      color: this.detailForm.value.color.id,
      jobType: this.jobTypeId,
      jobDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      jobStatus: this.isEdit ? this.jobStatusID : jobStatusId,
      timeIn: moment(this.detailForm.value.inTime).format(),
      estimatedTimeOut: moment(this.detailForm.value.dueTime).format(),
      actualTimeOut: new Date(),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
      notes: this.note
    };
    const jobDetail = {
      jobDetailId: 0,
      jobId: this.isEdit ? this.selectedData.Details.JobId : this.jobID,
      bayId: this.detailForm.value.bay,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0
    };
    const baySchedule = {
      bayScheduleID: 0,
      bayId: this.detailForm.value.bay,
      jobId: this.isEdit ? this.selectedData.Details.JobId : this.jobID,
      scheduleDate: this.datePipe.transform(this.detailForm.value.inTime, 'yyyy-MM-dd'),
      scheduleInTime: this.datePipe.transform(this.detailForm.value.inTime, 'HH:mm'),
      scheduleOutTime: this.datePipe.transform(this.detailForm.value.dueTime, 'HH:mm'),
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
        jobId: this.isEdit ? this.selectedData.Details.JobId : this.jobID,
        serviceId: item.ServiceId,
        isActive: true,
        isDeleted: false,
        commission: 0,
        price: item.Price,
        quantity: 1,
        createdBy: 0,
        updatedBy: 0
      };
    });
    this.washItem.forEach(element => {
      this.jobItems.push({
        jobItemId: element.JobItemId,
        jobId: element.JobId ? element.JobId : this.jobID,
        serviceId: element.ServiceId,
        isActive: true,
        isDeleted: element.IsDeleted ? element.IsDeleted : false,
        commission: 0,
        price: element.Price,
        quantity: 1,
        createdBy: 0,
        updatedBy: 0
      });
    });
    this.assignedDetailService.forEach(item => {
      this.jobItems.push({
        jobItemId: 0,
        jobId: this.isEdit ? this.selectedData.Details.JobId : this.jobID,
        serviceId: item.ServiceId,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        updatedBy: 0,
        commission: 0,
        price: item.Price,
        quantity: 1,
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
      this.spinner.show();
      this.detailService.updateDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Detail.Update, 'Success!');
          this.detailForm.controls.inTime.disable();
          this.detailForm.controls.dueTime.disable();
          this.detailForm.controls.bay.disable();
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.detailService.addDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.isAssign = true;
          this.isStart = true;
          const jobID = JSON.parse(res.resultData);
          this.getDetailByID(jobID.Status);
          this.jobID = jobID.Status;
          this.detailForm.controls.inTime.disable();
          this.detailForm.controls.dueTime.disable();
          this.detailForm.controls.bay.disable();
          this.toastr.success(MessageConfig.Detail.Add, 'Success!');
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (error) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  getDetailByID(jobID) {
    this.detailService.getDetailById(jobID).subscribe(res => {
      if (res.status === 'Success') {
        const details = JSON.parse(res.resultData);
        this.selectedData = details.DetailsForDetailId;
        this.isEdit = true;
        this.washItem = this.selectedData.DetailsItem;
        this.detailItems = this.selectedData.DetailsItem;
        this.detailsJobServiceEmployee = this.selectedData.DetailsJobServiceEmployee !== null ?
          this.selectedData.DetailsJobServiceEmployee : [];
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
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
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  deleteDetail() {
    this.deleteDetailList = true
    this.body = 'Are you sure you want to Detail this Detail? All related information will be deleted and the Detail cannot be retrieved?',
      this.title = 'Delete Detail'
  }

  confirmDelete() {
    this.spinner.show();
    this.detailService.deleteDetail(this.selectedData.Details.JobId).subscribe(res => {
      // need to change
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Detail.Delete, 'Success');
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.refreshDetailGrid.emit();
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  membershipChange(data) {
    this.memberService = [];
    this.wash.getMembershipById(Number(data)).subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.memberService = membership.MembershipAndServiceDetail.MembershipService;
        if (this.memberService !== null) {
          const washService = this.memberService.filter(i => Number(i.ServiceTypeId) === this.detailId);
          if (washService.length !== 0) {
            this.washService(washService[0].ServiceId);
          }
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
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
      createdBy: null,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: null,
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
      isActive: true,
      isDeleted: false,
      createdBy: null,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: null,
      updatedDate: new Date(),
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score === '' ||
        this.clientFormComponent.clientForm.value.score == null) ? 0 : this.clientFormComponent.clientForm.value.score,
      noEmail: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type === '' ||
        this.clientFormComponent.clientForm.value.type == null) ? 0 : this.clientFormComponent.clientForm.value.type
    };
    const myObj = {
      client: formObj,
      clientVehicle: null,
      clientAddress: this.address
    };
    this.spinner.show();
    this.client.addClient(myObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const id = JSON.parse(data.resultData)
        this.generatedClientId = id?.Status[0];
        this.getClientById(this.generatedClientId)
        this.toastr.success(MessageConfig.Client.Add, 'Success');
        this.closePopupEmitClient();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.clientFormComponent.clientForm.reset();
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  getClientById(id) {
    this.spinner.show();
    this.client.getClientById(id).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const clientDetail = JSON.parse(res.resultData);
        const selectedclient = clientDetail.Status[0];
        this.selectclient =
        {
          id: selectedclient.ClientId,
          name: selectedclient.FirstName + ' ' + selectedclient.LastName
        }

        this.detailForm.patchValue({

          client: this.selectclient

        })

        this.selectedClient(this.selectclient)


      }
      else {
        this.spinner.hide();

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    })
  }
  assignEmployee() {
    this.showDialog = true;
  }

  storedService(event) {
    this.assignedDetailService = event.service;
    this.showDialog = false;
  }

  closeAssignModel() {
    this.showDialog = false;
  }

  cancelAssignModel() {
    this.showDialog = false;
    this.getDetailByID(this.jobID);
  }

  getEmployeeList() {
    const timeclock = {
      date: moment(new Date()).format(),
      locationId: +localStorage.getItem('empLocationId')
    };
    this.detailService.getClockedInDetailer(timeclock).subscribe(res => {
      if (res.status === 'Success') {
        const employee = JSON.parse(res.resultData);
        this.employeeList = employee.result;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getJobStatus() {
    const jobStatus = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.JobStatus);
    if (jobStatus.length > 0) {
      this.jobStatus = jobStatus;
    }
  }

  getPastClientNotesById(clientID) {
    this.viewNotes = [];
    this.detailService.getPastClientNotesById(clientID).subscribe(res => {
      if (res.status === 'Success') {
        const viewPast = JSON.parse(res.resultData);
        if (viewPast.PastClientNotesByClientId.length > 0) {
          this.isViewPastNotes = true;
          this.viewNotes = viewPast.PastClientNotesByClientId;
        } else {
          this.isViewPastNotes = false;
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  pastNotes() {
    this.viewNotesDialog = true;
  }

  print() {
    this.printWashComponent.print();
  }

  pay() {
    this.router.navigate(['/sales'], { queryParams: { ticketNumber: this.ticketNumber } });
  }

  printCustomerCopy() {
    this.printCustomerCopyComponent.print();
  }
  getModel(id) {
    this.modelService.getModelByMakeId(id).subscribe(res => {
      if (res.status === 'Success') {
        const makeModel = JSON.parse(res.resultData);
        this.model = makeModel.Model;
        this.model = this.model.map(item => {
          return {
            id: item.ModelId,
            name: item.ModelValue
          };
        });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getAllMake() {
    this.makeService.getMake().subscribe(res => {
      if (res.status === 'Success') {
        const make = JSON.parse(res.resultData);
        const makes = make.Make;
        this.type = makes.map(item => {
          return {
            id: item.MakeId,
            name: item.MakeValue
          };
        });
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  selectedModel(event) {
    const id = event.id;
    if (id !== null) {
      this.getModel(id);
    }
  }
  // To get upcharge
  getUpcharge() {
    if ((!this.ceramicUpchargeId || !this.upchargeId) && !this.detailForm.value.model?.id) {
      return;
    }
    const obj = {
      "upchargeServiceType": this.isCeramic === false ? this.upchargeId : this.ceramicUpchargeId,
      "modelId": this.detailForm.value.model?.id
    };

    this.GetUpchargeService.getUpcharge(obj).subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        this.upchargeList = jobtype.upcharge;
        if (this.upchargeList?.length > 0) {
          this.detailForm.patchValue({
            upcharges: this.upchargeList[this.upchargeList.length - 1].ServiceId,
            upchargeType: this.upchargeList[this.upchargeList.length - 1].ServiceId
          });
          this.additionalService.push(this.upchargeList[this.upchargeList.length - 1]);
        }
        else {
          this.detailForm.patchValue({
            upcharges: '',
            upchargeType: ''
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}

