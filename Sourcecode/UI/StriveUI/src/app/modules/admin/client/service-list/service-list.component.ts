import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.css']
})
export class ServiceListComponent implements OnInit {
  washForm: FormGroup;
  timeIn: any;
  timeOut: any;
  minutes: any;
  Score: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
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
  selectclient: any;
  generatedClientId: any;
  jobItem: any = [];
  constructor(
    private fb: FormBuilder,
    private wash: WashService,
    private detailService: DetailService,
    private codeValueService: CodeValueService,
    private serviceSetupService: ServiceSetupService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    this.timeInDate = new Date();
    this.formInitialize();
    this.getColor();
    this.getTicketNumber();
  }

  formInitialize() {
    this.washForm = this.fb.group({
      client: [''],
      vehicle: [''],
      type: [''],
      barcode: [''],
      washes: [''],
      model: [''],
      color: [''],
      upcharges: [''],
      upchargeType: [''],
      airFreshners: [''],
      notes: [''],
      // pastNotes: ['',]
    });
  }

  getTicketNumber() {
    this.getServiceType();
  }

  getServiceType() {
    const serviceTypeValue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceEnum = serviceTypeValue;
      this.washId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.WashPackage)[0]?.CodeId;
      this.upchargeId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.WashUpcharge)[0]?.CodeId;
      this.airFreshenerId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.AirFresheners)[0]?.CodeId;
      this.additionalId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices)[0]?.CodeId;
      this.getAllServices();
    }
  }

  getAllServices() {
    this.serviceSetupService.getAllServiceDetail(+localStorage.getItem('empLocationId')).subscribe(res => {
      if (res.status === 'Success') {
        const serviceDetails = JSON.parse(res.resultData);
        if (serviceDetails.AllServiceDetail !== null) {
          this.additional = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.additionalId);
          this.washes = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.washId);
          this.upcharges = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.upchargeId);
          this.airFreshner = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.airFreshenerId);
          this.UpchargeType = this.upcharges;
          this.additional.forEach(element => {
            element.IsChecked = false;
          });
          if (this.isEdit === true) {
            this.washForm.reset();
            this.getServiceListByJobid();
          }
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');
        this.type = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleManufacturer');
        this.model = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleModel');
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getServiceListByJobid() {
    this.spinner.show();
    this.wash.getWashById(205383).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        const wash = JSON.parse(data.resultData);
        this.headerData = 'View Service';
        this.selectedData = wash.WashesDetail;
        this.jobItem = this.selectedData.WashItem;
        this.getVehicleList(this.selectedData?.Washes[0]?.ClientId);
        this.bindData(this.selectedData?.Washes[0]);
        this.isEdit = true;
        this.isView = true;
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getVehicleList(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  bindData(data) {
    this.model.forEach( item => {
      if (+data.Model === +item.id) {
        data.vehicleModel = item.name;
      }
    });
    this.type.forEach( item => {
      if (+data.Make === +item.id) {
        data.vehicleMake = item.name;
      }
    });
    this.color.forEach( item => {
      if (+data.Color === item.id) {
        data.vehicleColor = item.name;
      }
    });
    this.ticketNumber = data.TicketNumber;
    this.timeOutDate = data?.EstimatedTimeOut;
    this.timeInDate = data?.TimeIn;
    const inTIme = new Date(data?.TimeIn);
    const outTime = new Date(data?.EstimatedTimeOut);
    let diff = (inTIme.getTime() - outTime.getTime()) / 1000;
    diff /= 60;
    this.washTime = Math.abs(Math.round(diff));
    this.washForm.patchValue({
      barcode: data?.Barcode,
      client: data?.ClientName,
      vehicle: data.VehicleId,
      type: data?.vehicleMake,
      model:  data?.vehicleModel,
      color: data?.vehicleColor,
      notes: data?.ReviewNote,
      washes: this.jobItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0]?.ServiceId,
      upchargeType: this.jobItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId,
      upcharges: this.jobItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId,
      airFreshners: this.jobItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0]?.ServiceId,
    });
    this.jobItem.forEach(element => {
      if (this.additional.filter(item => item.ServiceId === element.ServiceId)[0] !== undefined) {
        this.additional.filter(item => item.ServiceId === element.ServiceId)[0].IsChecked = true;
      }
    });
    this.washForm.disable();
  }

  closeHistoryModel() {
    this.activeModal.close();
  }

}
