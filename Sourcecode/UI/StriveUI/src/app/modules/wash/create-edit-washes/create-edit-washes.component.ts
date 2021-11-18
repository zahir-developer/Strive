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
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { GetUpchargeService } from 'src/app/shared/services/common-service/get-upcharge.service';
import { MakeService } from 'src/app/shared/services/common-service/make.service';
import { ModelService } from 'src/app/shared/services/common-service/model.service';
import { DatePipe } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-create-edit-washes',
  templateUrl: './create-edit-washes.component.html'
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
  additionalServiceRemoved: any = [];
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
  paidLabel: string = 'Pay';
  upchargeList: any;
  jobID: any;
  selectedUpcharge: number;
  getDetailByBarcode: boolean = false;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private message: MessageServiceToastr,
    private landingservice: LandingService,
    private makeService: MakeService,
    private modelService: ModelService,
    private wash: WashService, private client: ClientService, private router: Router, private detailService: DetailService,
    private spinner: NgxSpinnerService, private codeValueService: CodeValueService, private serviceSetupService: ServiceSetupService
    , private GetUpchargeService: GetUpchargeService,    
    private datePipe: DatePipe, 
  ) { }

  ngOnInit() {


    this.getTicketNumber();
    this.getAllMake();
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
  landing() {
    this.landingservice.loadTheLandingPage();
  }
  formInitialize() {

    this.washForm = this.fb.group({
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
      notes: ['',],
      pastNotes: ['',]
    });
  }

  get f() {
    return this.washForm.controls;
  }

  getTicketNumber() {
    if (!this.isEdit) {
      this.wash.getTicketNumber().subscribe(data => {
        if (data.status === 'Success') {
          const ticket = JSON.parse(data.resultData);
          this.ticketNumber = ticket.GetTicketNumber.JobId;
          this.jobID = ticket.GetTicketNumber.JobId;
        }
        else {
          this.toastr.error(MessageConfig.TicketNumber, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }

    this.getWashTimeByLocationID();
    this.getServiceType();
    this.getColor();
  }

  getWashTimeByLocationID() {
        this.detailService.getWashTimeByLocationId(localStorage.getItem('empLocationId'),this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss').toString()).subscribe(res => {
      if (res.status === 'Success') {
        const washTime = JSON.parse(res.resultData);
        if (washTime.Washes.length > 0) {
          const WashTimeMinutes = washTime.Washes[0].WashtimeMinutes;
          this.washTime = WashTimeMinutes;
          const dt = new Date();
          this.timeOutDate = dt.setMinutes(dt.getMinutes() + this.washTime);
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getWashById() {
    if (this.selectedData?.Washes[0].IsPaid == "True") {
      this.paidLabel = 'Paid'
    }
    else {
      this.paidLabel = 'Pay'
    }

    this.getVehicleList(this.selectedData?.Washes[0]?.ClientId);
    this.getClientPastNotes(this.selectedData?.Washes[0]?.ClientId);

    this.washForm.patchValue({
      barcode: this.selectedData?.Washes[0]?.Barcode,
      client: { id: this.selectedData?.Washes[0]?.ClientId, name: this.selectedData?.Washes[0]?.ClientName },
      vehicle: this.selectedData.Washes[0].VehicleId,
      type: { id: this.selectedData.Washes[0].Make, name: this.selectedData?.Washes[0]?.VehicleMake },
      model: { id: this.selectedData?.Washes[0]?.Model, name: this.selectedData?.Washes[0]?.VehicleModel },
      color: { id: this.selectedData.Washes[0].Color, name: this.selectedData?.Washes[0]?.VehicleColor },
      notes: this.selectedData.Washes[0].ReviewNote,
      washes: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0]?.ServiceId ?
        this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.washId)[0]?.ServiceId : '',
      upchargeType: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId ?
        this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId : '',
      upcharges: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId ?
        this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0]?.ServiceId : '',
      airFreshners: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0]?.ServiceId ?
        this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === this.airFreshenerId)[0]?.ServiceId : '',
    });
    this.getModel(this.selectedData.Washes[0].Make);
    this.timeInDate = this.selectedData.Washes[0].TimeIn;
    this.timeOutDate = this.selectedData.Washes[0].EstimatedTimeOut;
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
  getClientPastNotes(Id) {
    this.detailService.getPastClientNotesById(Id).subscribe(data => {
      if (data.status === 'Success') {
        const pastNote = JSON.parse(data.resultData);
        if (pastNote.PastClientNotesByClientId.length > 0) {
          const pastClientNotes = pastNote.PastClientNotesByClientId[0]?.Notes;
          if (pastClientNotes) {
            this.washForm.controls.pastNotes.disable();

            this.washForm.patchValue({
              pastNotes: pastClientNotes

            })

          }
          else {
            this.washForm.controls.pastNotes.enable();

          }
        }


      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    })
  }
  vehicleChange(id) {
    this.additional.forEach(element => {
      element.IsChecked = false;
    });
    this.getMembership(id);
    this.getVehicleById(id);
  }

  getMembership(id) {
    var loggedLocId = +localStorage.getItem('empLocationId');
    this.wash.getMembership(+id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
        if (this.membership !== null) {

          var mlocationId = vehicle.VehicleMembershipDetails?.ClientVehicleMembership?.LocationId;
          var membershipId = +vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId;
          if(mlocationId !== undefined)
          {
            if(mlocationId !== loggedLocId)
            {
              this.getAllServices(mlocationId, membershipId);

            }
            else
            {
              this.getAllServices(loggedLocId, membershipId);
            }
          }
          this.toastr.warning(MessageConfig.Wash.DifferentLocationServiceLoaded, 'Different Location Services Loaded!');
          this.membershipChange(membershipId);
          this.membership.forEach(element => {
            const additionalService = this.additional.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (additionalService !== undefined && additionalService.length !== 0) {
              additionalService.forEach(item => {
                item.IsChecked = true;
              });
            }
          });
          console.log(this.additional);
        } else {
          this.washForm.get('washes').reset();
          this.getAllServices(loggedLocId);
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
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

  // To get JobType
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
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  getVehicleById(data) {
    this.wash.getVehicleById(data).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        const vData = vehicle.Status;

        if (this.barcodeDetails?.ClientId === 0) {
          var vehicles = [];
          var v  = 
          {
            VehicleId : vData.ClientVehicleId,
            VehicleModel: vData.ModelName === null ? 'Unk' : vData.ModelName,
            VehicleMfr: vData.VehicleMake === null ? 'Unk' : vData.VehicleMake,
            VehicleColor: vData.Color === null ? 'Unk' : vData.Color
          };

          vehicles.push(v);
          this.vehicle = vehicles;
        }
        

        this.washForm.patchValue({
          vehicle: vData.ClientVehicleId,
          barcode: vData.Barcode,
          type: { id: vData.VehicleMakeId, name: vData.VehicleMake },
          model: { id: vData.VehicleModelId, name: vData.ModelName },
          color: { id: vData.ColorId, name: vData.Color }
        });
        this.getModel(vData.VehicleMakeId);
        this.upchargeService(vData.Upcharge);
        this.getUpcharge();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getAllServices( locationId = 0, membershipId = 0) {
    var locId = locationId == 0 ? +localStorage.getItem('empLocationId'): locationId;
    const serviceObj = {
      locationId: locId,
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.serviceSetupService.getAllServiceDetail(locId).subscribe(res => {
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

          if(membershipId !== 0)
          {
            this.membershipChange(membershipId);
            this.membership.forEach(element => {
              const additionalService = this.additional.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
              if (additionalService !== undefined && additionalService.length !== 0) {
                additionalService.forEach(item => {
                  item.IsChecked = true;
                });
              }
            });
          }
          if (this.isEdit === true) {
            this.washForm.reset();
            this.getWashById();
          }
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
      this.washForm.patchValue({ vehicle: '', type: '', model: '', color: '', pastNotes: '' });
      this.washForm.get('pastNotes').enable();
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

    this.washForm.patchValue({ vehicle: '', type: '', model: '', color: '', pastNotes: '' });

    this.clientId = event.id;
    this.clientName = event.name;
    this.getClientPastNotes(this.clientId);
    const name = event.name.toLowerCase();
    if (name.startsWith('drive')) {
      this.washForm.get('vehicle').disable();
      return;
    } else if (!this.isView) {
      this.washForm.patchValue({ barcode: '' });
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
        // const models = this.model.filter( item =>  item.id === id.VehicleModelId);
        // if (models.length > 0) {
        //   this.washForm.patchValue({
        //     type: models[0]
        //   });
        // }
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
    this.washForm.patchValue({
      model: ''
    });
    if (id !== null) {
      this.getModel(id);
    }
  }
  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');

        if (this.isEdit) {
          vehicle.VehicleDetails.forEach(item => {
            if (+this.selectedData.Washes[0].Make === item.CodeId) {
              this.selectedData.Washes[0].vehicleMake = item.CodeValue;
            } else if (+this.selectedData.Washes[0].Model === item.CodeId) {
              this.selectedData.Washes[0].vehicleModel = item.CodeValue;
            }

            else if (+this.selectedData.Washes[0].Color === item.CodeId) {
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


        this.color = this.color.map(item => {
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

  viewWash() {
    this.washForm.disable();
  }

  checkValue(type) {
    if (type === 'make') {
      if (!this.washForm.value.type.hasOwnProperty('id')) {
        this.washForm.patchValue({ type: '' });
      }
    } else if (type === 'model') {
      if (!this.washForm.value.model.hasOwnProperty('id')) {
        this.washForm.patchValue({ model: '' });
      }
    } else if (type === 'color') {
      if (!this.washForm.value.color.hasOwnProperty('id')) {
        this.washForm.patchValue({ color: '' });
      }
    }
  }


  // Get Client And Vehicle Details By Barcode
  getByBarcode(barcode) {
    this.getDetailByBarcode = true;
    if (barcode === '') {
      return;
    }
    this.washForm.patchValue({
      barcode
    });
    this.spinner.show();
    this.wash.getByBarcode(barcode).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.isBarcode = true;
        const wash = JSON.parse(data.resultData);
        if (wash.ClientAndVehicleDetail !== null && wash.ClientAndVehicleDetail.length > 0) {
          this.barcodeDetails = wash.ClientAndVehicleDetail[0];
          if (this.barcodeDetails?.ClientId !== 0) {
              this.getClientVehicle(this.barcodeDetails.ClientId, this.barcodeDetails.VehicleId, 1);
          }
          else
          {
            this.getVehicleById(this.barcodeDetails.VehicleId);
          }

          
          this.clientName = this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName;
          
          setTimeout(() => {
            this.washForm.patchValue({
              client: { id: this.barcodeDetails.ClientId, name: this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName },
              vehicle: this.barcodeDetails.VehicleId
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getVehicleList(id, vehicleId = 0) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (vehicleId != 0) {
          this.getVehicleById(vehicleId);
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get Vehicle By ClientId
  getClientVehicle(id, vehicleId = 0, getByBarcode = 0) {

    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (this.vehicle.length !== 0) {
          var vehId = 0;
          if (vehicleId !== 0)
            vehId = vehicleId;
          else
            vehId = +this.vehicle[this.vehicle.length - 1].VehicleId;

          this.washForm.patchValue({ vehicle: vehId });

          this.getVehicleById(vehId);
          if (getByBarcode !== 1) {
            this.getMembership(vehId);
          }
        } else {
          this.washForm.get('vehicle').reset();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    this.washForm.patchValue({ washes: +data ? +data : '' });
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
    this.washForm.patchValue({ upcharges: +data ? +data : '' });
    this.washForm.patchValue({ upchargeType: +data ? +data : '' });
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

    if (!this.ticketNumber) {
      this.toastr.warning(MessageConfig.TicketNumber, 'Warning!');
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
      jobId: this.isEdit ? this.selectedData.Washes[0].JobId : this.jobID,
      ticketNumber: this.ticketNumber,
      barcode: this.washForm.value.barcode,
      locationId: +localStorage.getItem('empLocationId'),
      clientId: (this.clientName.toLowerCase().trim().replace(' ', '')) == 'driveup' ? null : this.washForm.value.client.id,
      vehicleId: this.clientName.toLowerCase().startsWith('drive') ? null : this.washForm.value.vehicle,
      make: this.washForm.value.type?.id?.toString(),
      model: this.washForm.value.model?.id?.toString(),
      color: this.washForm.value.color?.id?.toString(),
      jobType: this.jobTypeId,
      jobDate: new Date().toDateString().split('T')[0] === "" ? new Date().toDateString() : new Date().toDateString().split('T')[0],
      timeIn: moment(this.timeInDate).format(),
      estimatedTimeOut: this.timeOutDate ? moment(this.timeOutDate).format() : null,
      actualTimeOut: null,
      notes: this.washForm.value.notes,
      jobStatus: this.jobStatusId,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: moment(new Date()).format(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: this.isEdit ? moment(new Date()).format() : null
    };
    this.washItem.forEach(element => {
      this.additionalService = this.additionalService.filter(item => item.ServiceId !== element.ServiceId);
    });


    const finalAdditionalService = [];
    this.additionalService.forEach(service => {
      if (this.additionalServiceRemoved.filter(s => s === service.ServiceId).length === 0) {
        finalAdditionalService.push(service);
      }
    });

    this.jobItems = finalAdditionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: this.isEdit ? +this.selectedData.Washes[0].JobId : this.jobID,
        serviceId: item.ServiceId,
        commission: 0,
        price: item.Price,
        quantity: 1,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: moment(new Date()).format(),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: this.isEdit ? moment(new Date()).format() : null
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
      this.spinner.show();
      this.wash.updateWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Wash.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (error) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.wash.updateWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Wash.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.washForm.reset();
        }
      }, (error) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
      updatedDate: this.isEdit ? moment(new Date()).format() : null
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: null,
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: null,
      maritalStatus: null,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: this.isEdit ? moment(new Date()).format() : null,
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score == "" || this.clientFormComponent.clientForm.value.score == null) ? 0 : this.clientFormComponent.clientForm.value.score,
      isCreditAccount: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type == "" || this.clientFormComponent.clientForm.value.type == null) ? 0 : this.clientFormComponent.clientForm.value.type
    };
    const myObj = {
      client: formObj,
      clientVehicle: null,
      clientAddress: this.address,
      token: null,
      password: ''
    }
    this.spinner.show();
    this.client.addClient(myObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        const id = JSON.parse(data.resultData)
        this.generatedClientId = id?.Status[0];
        this.getClientById(this.generatedClientId)
        this.toastr.success(MessageConfig.Client.Add, 'Success!');
        this.closePopupEmitClient();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.clientFormComponent.clientForm.reset();
        this.spinner.hide();
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
        };
        this.washForm.patchValue({
          client: this.selectclient
        });
        this.selectedClient(this.selectclient);
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
  getJobStatus() {
    const jobStatus = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.JobStatus);
    if (jobStatus.length > 0) {
      this.jobStatus = jobStatus.filter(item => item.CodeValue === ApplicationConfig.CodeValue.inProgress);
      if (this.jobStatus.length > 0) {
        this.jobStatusId = this.jobStatus[0].CodeId;
      }
    }

  }

  pay() {
    this.router.navigate(['/sales'], { queryParams: { ticketNumber: this.ticketNumber } });
    this.openNav('sales');
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
  // To get upcharge
  getUpcharge() {
    if (!this.upchargeId || !this.washForm.value.model?.id) {
      return;
    }
    const obj = {
      "upchargeServiceType": this.upchargeId,
      "modelId": this.washForm.value.model?.id
    };

    this.GetUpchargeService.getUpcharge(obj).subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        this.upchargeList = jobtype.upcharge;
        var serviceId = 0
        if (this.upchargeList?.length > 0) {
          serviceId = this.upchargeList[this.upchargeList.length - 1].ServiceId;

          if (this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0] !== undefined) {
            this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0].IsDeleted = true;
          }

          this.washForm.patchValue({
            upcharges: serviceId,
            upchargeType: serviceId
          });
          this.upchargeService(serviceId);
          this.selectedUpcharge = serviceId;
        }
        else {
          this.additionalServiceRemoved.push(this.selectedUpcharge);
          this.washForm.patchValue({
            upcharges: '',
            upchargeType: ''
          });

          if (this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0] !== undefined) {
            this.washItem.filter(i => Number(i.ServiceTypeId) === this.upchargeId)[0].IsDeleted = true;
          }

        }

        // if(this.upcharges){
        //   this.upcharges.forEach(element => {
        //     if(this.upchargeList.length > 0){
        //       this.upchargeList.forEach(item => {
        //         if(element.ServiceId == item.ServiceId){



        //         }
        //       });
        //     }
        //     else{
        //       this.washForm.patchValue({
        //         upcharges : '',

        //         upchargeType: ''

        //       })
        //     } 
        // });
        //  }
        //  else{
        //   this.washForm.patchValue({
        //     upcharges : '',

        //     upchargeType: ''

        //   })
        // }

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
