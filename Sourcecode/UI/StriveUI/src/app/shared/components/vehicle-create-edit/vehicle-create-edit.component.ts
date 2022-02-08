import { Component, OnInit, Output, Input, EventEmitter, ViewEncapsulation } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as _ from 'underscore';
import { MessageConfig } from '../../services/messageConfig';
import { ApplicationConfig } from '../../services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { ModelService } from '../../services/common-service/model.service';
import { EmployeeService } from '../../services/data-service/employee.service';
import { MakeService } from '../../services/common-service/make.service';
import { WashService } from '../../services/data-service/wash.service';
import { CodeValueService } from '../../common-service/code-value.service';
import { GetUpchargeService } from '../../services/common-service/get-upcharge.service';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DecimalPipe } from '@angular/common';
import { ClientService } from '../../services/data-service/client.service';

@Component({
  selector: 'app-vehicle-create-edit',
  templateUrl: './vehicle-create-edit.component.html',
  encapsulation: ViewEncapsulation.None
})
export class VehicleCreateEditComponent implements OnInit {
  vehicleForm: FormGroup;
  dropdownSettings: IDropdownSettings = {};
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() clientId?: any;
  @Input() vehicleNumber?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  @Input() additionalService?: any;
  @Input() isAdd?: any;
  @Input() upchargeServices?: any;
  make: any;
  model: any;
  color: any;
  washes: any;
  upchargeType: any;
  membership: any;
  additional: any;
  membershipServices: any = [];
  memberService: any = [];
  vehicles: any;
  patchedService: any;
  memberServiceId: any;
  memberOnchangePatchedService: any = [];
  selectedservice: any = [];
  extraService: any = [];
  washService: any = [];
  washServiceId: number = 0;
  washesDropdown: any = [];
  submitted: boolean;
  filteredModel: any = [];
  filteredcolor: any = [];
  filteredMake: any = [];
  models: any;
  isClientVehicle: boolean;
  clientList: any;
  upchargeTypeId: any;
  oldUpchargeId: number = 0;
  serviceEnum: any;
  upchargeList: any;
  MembershipDiscount: boolean = false;
  membershipId: number = 0;
  clientMembershipId: number = 0;
  locationId: number = 0;
  upchargePrice: number = 0;
  isEditLoad: boolean = false;
  isLoaded:boolean = false;
  ccRegex: RegExp = /[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$/;
  card: string;
  profileId: string;
  accountId: string;
  isMembership: boolean = false;
  billingAddress: any = [];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService,
    private spinner: NgxSpinnerService, private employeeService: EmployeeService,
    private modelService: ModelService, private payrollsService:PayrollsService, 
    private messageService: MessageServiceToastr,
    private decimalPipe: DecimalPipe, private client:ClientService,
    private makeService: MakeService, private wash: WashService, private codeValueService: CodeValueService, private GetUpchargeService: GetUpchargeService) { }

  ngOnInit() {
    this.formInitialize();
    if (this.isView === true) {
      this.viewVehicle();
    }
    this.getServiceType();
    if (this.isEdit === true) {
      this.isEditLoad = true;
      this.isClientVehicle = false;
      this.vehicleForm.reset();
      

      this.getVehicleById();
      this.getVehicleMembershipDetailsByVehicleId();
      this.getMembershipService();
      
    }
    else {
      this.isLoaded = true;
      this.locationId = +localStorage.getItem('empLocationId');

      this.getMembershipService();
      this.getVehicleMembership(this.locationId);

      this.getVehicleCodes();
    }
    
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
      client: [''],
      barcode: [''],
      vehicleNumber: ['',],
      make: ['', Validators.required],
      model: ['', Validators.required],
      color: ['', Validators.required],
      upcharge: ['',],
      upchargeType: ['',],
      monthlyCharge: ['',],
      membership: ['',],
      wash: [''],
      services: [[]],
      cardNumber: ['',] ,
      expiryDate: ['',]
    });
    this.vehicleForm.get('vehicleNumber').patchValue(this.vehicleNumber);
    this.vehicleForm.controls.vehicleNumber.disable();
    this.vehicleForm.patchValue({
      wash: ""
    });
    this.getAllMake();


  }

  get f() {
    return this.vehicleForm.controls;
  }

  dropDownSetting() {
    this.dropdownSettings = {
      singleSelection: ApplicationConfig.dropdownSettings.singleSelection,
      defaultOpen: ApplicationConfig.dropdownSettings.defaultOpen,
      idField: ApplicationConfig.dropdownSettings.idField,
      textField: ApplicationConfig.dropdownSettings.textField,
      itemsShowLimit: ApplicationConfig.dropdownSettings.itemsShowLimit,
      enableCheckAll: ApplicationConfig.dropdownSettings.enableCheckAll,
      allowSearchFilter: ApplicationConfig.dropdownSettings.allowSearchFilter
    };
  }

  getVehicleById() {
    if (this.selectedData.ClientId !== null) {
      this.vehicleForm.patchValue({
        client: { id: this.selectedData.ClientId, name: this.selectedData.ClientName },  // this.selectedData.ClientName
      });
      this.vehicleForm.controls.client.disable();
    } else {
      this.vehicleForm.controls.client.enable();
    }

    this.oldUpchargeId = this.selectedData.Upcharge === null ? 0 : this.selectedData.Upcharge;

    this.vehicleForm.patchValue({
      barcode: this.selectedData.Barcode,
      vehicleNumber: this.selectedData.VehicleNumber,
      color: { id: this.selectedData.ColorId, name: this.selectedData.Color },
      model: { id: this.selectedData.VehicleModelId, name: this.selectedData.ModelName },
      make: { id: this.selectedData.VehicleMakeId, name: this.selectedData.VehicleMake },
      upchargeType: this.selectedData.Upcharge === 0 ? "" : this.selectedData.Upcharge,
      upcharge: this.selectedData.Upcharge === 0 ? "" : this.selectedData.Upcharge,
      //monthlyCharge: this.selectedData.MonthlyCharge.toFixed(2),
      membership: '',
      wash: ''
    });

    this.locationId = this.selectedData.LocationId;

    this.getVehicleMembership(this.locationId);
    //this.getUpcharge();

    this.selectedData.LocationId

    //if (this.selectedData.Upcharge === null)
    //  this.getModel(this.selectedData.VehicleMakeId, true);
    //else
    this.getModel(this.selectedData.VehicleMakeId, true);

        this.getVehicleCodes();
  }

  selectedClient(event) {
    console.log(event, 'event');
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

  viewVehicle() {
    this.vehicleForm.disable();
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
    for (const i of this.make) {
      const make = i;
      if (make.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(make);
      }
    }
    this.filteredMake = filtered;
  }

  getVehicleMembershipDetailsByVehicleId() {
    this.vehicle.getVehicleMembershipDetailsByVehicleId(this.selectedData.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        this.vehicles = vehicle.VehicleMembershipDetails;
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembership !== null) {
          this.memberServiceId = vehicle?.VehicleMembershipDetails?.ClientVehicleMembership?.MembershipId;
          //this.getMemberServices(this.memberServiceId);
          this.isMembership = true;
          this.vehicleForm.get('cardNumber').setValidators([Validators.required]); 
          this.vehicleForm.get('expiryDate').setValidators([Validators.required]);
          
          this.vehicleForm.patchValue({
            membership: vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId,
            monthlyCharge: vehicle?.VehicleMembershipDetails?.ClientVehicleMembership.TotalPrice.toFixed(2),
            cardNumber:vehicle.VehicleMembershipDetails.ClientVehicleMembership.CardNumber,
            expiryDate:vehicle.VehicleMembershipDetails.ClientVehicleMembership.ExpiryDate
          });
         
          this.profileId = vehicle.VehicleMembershipDetails.ClientVehicleMembership.ProfileId;
          this.accountId = vehicle.VehicleMembershipDetails.ClientVehicleMembership.AccountId;

          this.clientMembershipId = vehicle.VehicleMembershipDetails?.ClientVehicleMembership?.ClientMembershipId;
          this.membershipId = vehicle.VehicleMembershipDetails?.ClientVehicleMembership?.MembershipId;
        }
        else {
          var clientId = vehicle.VehicleMembershipDetails?.ClientVehicle?.ClientId;
          var vehicleId = vehicle.VehicleMembershipDetails?.ClientVehicle?.VehicleId;
          if (clientId !== null || clientId !== undefined) {
            this.getMembershipDiscount(clientId, vehicleId);
          }
        }
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembershipService !== null) {
          this.patchedService = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
          this.selectedservice = this.patchedService;
          const serviceIds = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService.map(item => item.ServiceId);
          const memberService = serviceIds.map((e) => {
            const f = this.additional.find(a => a.item_id === e);
            return f ? f : 0;
          });
          this.memberService = memberService.map(item => {
            return {
              item_id: item.item_id,
              item_text: item.item_text
            };
          });
          this.memberService = this.memberService.filter(s => s.item_id !== undefined);
          this.dropDownSetting();
          this.vehicleForm.patchValue({
            services: this.memberService
          });

          const washService = this.patchedService.filter(item => item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
          if (washService.length > 0) {
            this.washServiceId = washService[0].ServiceId;
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }

        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get VehicleMembership
  getVehicleMembership(locId) {
    this.vehicle.getVehicleMembership(locId).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.MembershipName;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getMembershipService() {
    this.additional = this.additionalService?.map(item => {
      return {
        item_id: item.ServiceId,
        item_text: item.ServiceName
      };
    });
    this.dropDownSetting();
  }

  membershipChange(data) {
    this.selectedservice = [];
    this.extraService = [];
    if (data !== "") {
      this.vehicleForm.get('monthlyCharge').reset();
      this.isMembership = true;
     
      this.vehicleForm.get('cardNumber').setValidators([Validators.required]); 
      this.vehicleForm.get('expiryDate').setValidators([Validators.required]);
      if (this.memberOnchangePatchedService.length !== 0) {
        this.memberOnchangePatchedService.forEach(element => {
          this.selectedservice = this.selectedservice.filter(i => i.ServiceId !== element.ServiceId);
          this.memberService = this.memberService.filter(i => i.item_id !== element.ServiceId);
        });
      }
      this.memberService = [];
      this.vehicle.getMembershipById(Number(data)).subscribe(res => {
        if (res.status === 'Success') {
          this.memberOnchangePatchedService = [];
          const membership = JSON.parse(res.resultData);
          var monthlyCharge = 0;
          if (membership.MembershipAndServiceDetail.Membership !== null) {
            var upchargePrice = 0;
            if (this.MembershipDiscount) {
              if (membership.MembershipAndServiceDetail.Membership.DiscountedPrice !== null) {
                monthlyCharge = membership.MembershipAndServiceDetail.Membership?.DiscountedPrice;
              }
              else {
                this.toastr.warning(MessageConfig.Admin.Vehicle.membershipDiscountNotUpdated, 'Warning!');
              }
            }
            else {
              monthlyCharge = membership.MembershipAndServiceDetail.Membership?.Price;
            }

            if (this.vehicleForm.value.upcharge !== undefined) {
              if (this.upchargeList?.length > 0) {
                var service = this.upchargeList[this.upchargeList.length - 1];
                upchargePrice = +service.Price;
              }
            }
            monthlyCharge = +monthlyCharge + upchargePrice;

            this.vehicleForm.patchValue({
              monthlyCharge: monthlyCharge?.toFixed(2)
            });


          }

          if (membership.MembershipAndServiceDetail.MembershipService !== null) {
            this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
            this.washService = this.membershipServices.filter(item =>
              item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
            if (this.washService.length > 0) {
              this.vehicleForm.patchValue({ wash: this.washService[0].ServiceId });
              //this.washTypeChange(this.washService[0].ServiceId);
              this.vehicleForm.controls.wash.disable();
            }

            //Upcharge - Disabled when mebership change as per Business requirement.
            /*
            const upchargeServcie = this.membershipServices.filter(item =>
              item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);

            if (upchargeServcie.length > 0) {
              this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
            }
            */

            if (this.membershipServices.filter(i => i.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices || ApplicationConfig.Enum.ServiceType.WashPackage).length !== 0) { // Additonal Services
              this.memberOnchangePatchedService = this.membershipServices.filter(item =>
                (item.ServiceType) === ApplicationConfig.Enum.ServiceType.AdditonalServices || ApplicationConfig.Enum.ServiceType.WashPackage);
            }
            this.memberOnchangePatchedService.forEach(element => {
              if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                this.selectedservice.push(element);
              }
            });
            this.washService.forEach(element => {
              if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                this.selectedservice.push(element);
              }
            });
            this.extraService.forEach(element => {
              if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                this.selectedservice.push(element);
              }
            });

            //Upcharge
            var upcharge = this.membershipServices.filter(i => i.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);
            if (upcharge?.length > 0) {
              var upchargeService = this.selectedservice.filter(i => i.ServiceId === upcharge[0].ServiceId)[0] !== undefined
              if (upchargeService !== undefined) {
                console.log(upchargeService);
              }
            }

            const serviceIds = this.selectedservice.map(item => item.ServiceId);
            const memberService = serviceIds.map((e) => {
              const f = this.additionalService.find(a => a.ServiceId === e);
              return f ? f : 0;
            });

            this.memberService = memberService.filter(s => s.ServiceId !== undefined).map(item => {
              return {
                item_id: item.ServiceId,
                item_text: item.ServiceName
              };
            });
            this.vehicleForm.get('services').patchValue(this.memberService);
            if (this.patchedService !== undefined) {
              this.patchedService.forEach(element => {
                if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                  element.IsDeleted = true;
                }
              });
            }

          } else {
            this.vehicleForm.patchValue({
              upcharge: '',
              upchargeType: '',
              services: '',
              wash: ""
            });
          }
          this.dropDownSetting();
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
    else {
      this.vehicleForm.patchValue({ upcharge: "", upchargeType: "", wash: "", monthlyCharge: "0.00" });
      this.vehicleForm.get('services').patchValue(null);
      this.isMembership = false;
     
      this.vehicleForm.get('cardNumber').clearValidators(); 
      this.vehicleForm.get('cardNumber').reset(); 
      this.vehicleForm.get('expiryDate').clearValidators();
      this.vehicleForm.get('expiryDate').reset();
    }
  }

  getMembershipDiscount(clientId, vehicleId) {
    this.vehicle.GetMembershipDiscountStatus(clientId, vehicleId).subscribe(res => {
      if (res.status === 'Success') {
        const discount = JSON.parse(res.resultData);
        this.MembershipDiscount += discount.Status;
        if (discount.Status === true)
          this.toastr.success(MessageConfig.Admin.Vehicle.membershipDiscountAvailable, 'Membership');
        else
          this.toastr.info(MessageConfig.Admin.Vehicle.membershipDiscountNotAvailable, 'Membership');
      }
    });
  }

  onItemSelect(data) {
    this.extraService.push(this.additionalService.filter(i => i.ServiceId === data.item_id)[0]);
    this.memberService.push(data);
    this.vehicleForm.get('services').patchValue(this.memberService);
    let price = 0;
    price = +this.additionalService.filter(i => i.ServiceId === data.item_id)[0].Price;
    if (price === 0) {
      this.toastr.warning(MessageConfig.Admin.Vehicle.ServiceZeroPrice, 'Additional Service');
    }
    price += +this.vehicleForm.value.monthlyCharge;
    this.vehicleForm.get('monthlyCharge').patchValue(price.toFixed(2));
  }

  onItemDeselect(data) {
    this.memberService = this.memberService.filter(item => item.item_id !== data.item_id);
    let extra = [];
    extra = this.extraService.filter(i => +i.ServiceId === data.item_id);
    if (extra.length === 0) {
      this.memberService.push(data);
      this.vehicleForm.get('services').patchValue(this.memberService);
      this.toastr.warning('Membership Services cannot be removed', 'Warning!');
    } else {
      this.vehicleForm.get('services').patchValue(this.memberService);
      let price = 0;
      price = +this.additionalService.filter(i => i.ServiceId === data.item_id)[0].Price;
      price = +this.vehicleForm.value.monthlyCharge - price;
      this.vehicleForm.get('monthlyCharge').patchValue(price.toFixed(2));
    }
  }

  getMemberServices(data) {
    this.extraService = [];
    this.vehicle.getMembershipById(+data).subscribe(res => {
      if (res.status === 'Success') {
        this.memberOnchangePatchedService = [];
        const membership = JSON.parse(res.resultData);
        if (membership.MembershipAndServiceDetail.MembershipService !== null) {
          this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
          const washService = this.membershipServices.filter(item => item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
          if (washService.length > 0) {
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }
          const upchargeServcie = this.membershipServices.filter(item => item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);
          if (upchargeServcie.length > 0) {
            this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
          }
          if (this.membershipServices.filter(i => (i.ServiceType) === ApplicationConfig.Enum.ServiceType.AdditonalServices || ApplicationConfig.Enum.ServiceType.WashPackage).length !== 0) {
            this.memberOnchangePatchedService = this.membershipServices.filter(item => (item.ServiceType) === ApplicationConfig.Enum.ServiceType.AdditonalServices || ApplicationConfig.Enum.ServiceType.WashPackage);
            if (this.memberOnchangePatchedService.length !== 0) {
              this.patchedService.forEach(element => {
                if (this.memberOnchangePatchedService.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                  this.extraService.push(element);
                }
              });
            }
          }
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  selectedMake(event) {
    const id = event.id;
    if (id !== null) {
      this.getModel(id);
    }
  }

  selectedModel(event) {
    const id = event.id;
    if (id !== null) {
      this.getModel(id)
    }
  }

  getModel(id, applyUpcharge = true) {
    if (id !== null) {
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

          this.getUpcharge(applyUpcharge);
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  getAllMake() {
    this.makeService.getMake().subscribe(res => {
      if (res.status === 'Success') {
        const make = JSON.parse(res.resultData);
        const makes = make.Make;
        this.make = makes.map(item => {
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

  // Get vehicleCodes
  getVehicleCodes() {
    this.vehicle.getVehicleCodes().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');

        this.color = this.color.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });

        this.getAllServiceDetail();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getAllServiceDetail() {
    const serviceObj = {
      locationId: localStorage.getItem('empLocationId'),
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    const locationID = this.locationId;
    this.vehicle.getAllServiceDetail(locationID).subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.upchargeType = serviceDetails.AllServiceDetail.filter(item =>
          item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.WashUpcharge);
        this.washesDropdown = serviceDetails.AllServiceDetail.filter(item =>
          item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.WashPackage);
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  change(data) {
    this.vehicleForm.value.franchise = data;
  }

  checkValue(type) {
    if (type === 'make') {
      if (!this.vehicleForm.value.make.hasOwnProperty('id')) {
        this.vehicleForm.patchValue({ make: '' });
      }
    } else if (type === 'model') {
      if (!this.vehicleForm.value.model.hasOwnProperty('id')) {
        this.vehicleForm.patchValue({ model: '' });
      }
    } else if (type === 'color') {
      if (!this.vehicleForm.value.color.hasOwnProperty('id')) {
        this.vehicleForm.patchValue({ color: '' });
      }
    }
  }


  // Add/Update Vehicle
  submit() {
    this.submitted = true;
    if (this.vehicleForm.invalid) {
      return;
    }

    this.vehicleForm.controls.vehicleNumber.enable();
    this.vehicleForm.controls.client.enable();
    let memberService = [];
    let clientMembershipId = '';
    if (this.isEdit === true) {
      if (this.patchedService !== undefined) {
        clientMembershipId = this.patchedService[0].ClientMembershipId ? this.patchedService[0].ClientMembershipId :
          this.patchedService[0].MembershipId ? this.patchedService[0].MembershipId : 0;
        const r = this.patchedService.filter((elem) => this.memberService.find(({ item_id }) => elem.ServiceId === item_id));
        r.forEach(item => item.IsDeleted = false);
        const r1 = this.memberService.filter((elem) => !this.patchedService.find(({ ServiceId }) => elem.item_id === ServiceId));
        r1.forEach(item => {
          item.ClientVehicleMembershipServiceId = 0,
            item.IsDeleted = false,
            item.ClientMembershipId = clientMembershipId,
            item.ServiceId = item.item_id;
          item.IsActive = true;
        });
        /*const r2 = this.patchedService.filter((elem) => !this.memberService.find(({ item_id }) => elem.ServiceId === item_id));
        r2.forEach(item => item.IsDeleted = true);*/
        memberService = r.concat(r1);
      } else {
        memberService = this.memberService;
      }

      if (this.washService !== undefined) {
        if (this.washService[0] !== undefined) {
          const serviceId = this.washService[0].ServiceId;
          this.memberService.push(
            {
              ClientVehicleMembershipServiceId: 0,
              IsDeleted: false,
              ClientMembershipId: clientMembershipId,
              ServiceId: serviceId,
              IsActive: true,
            }
          )
        }
      }

      if (this.washServiceId === 0 && this.vehicleForm.value.wash !== "") {
        memberService.push(
          {
            ClientVehicleMembershipServiceId: 0,
            IsDeleted: false,
            ClientMembershipId: clientMembershipId,
            ServiceId: +this.vehicleForm.value.wash,
            IsActive: true,
          }
        )

      }

      if (this.patchedService !== undefined) {

        var washServiceTypeId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.WashPackage)[0]?.CodeId
        var washPackage = this.patchedService.filter(s => s.ServiceType === washServiceTypeId);

        var membershipWashPackage = this.membershipServices.filter(s => s.ServiceTypeId === washServiceTypeId);
        if (membershipWashPackage.length > 0 && washPackage.length === 0) {
          memberService.push(
            {
              ClientVehicleMembershipServiceId: 0,
              IsDeleted: false,
              ClientMembershipId: clientMembershipId,
              ServiceId: +membershipWashPackage[0].ServiceId,
              IsActive: true,
            })
        }
        else if (membershipWashPackage.length === 0 && washPackage.length > 0) {
          washPackage.forEach(item => {
            memberService.push(
              {
                ClientVehicleMembershipServiceId: item.ClientVehicleMembershipServiceId,
                IsDeleted: true,
                ClientMembershipId: clientMembershipId,
                ServiceId: item.ServiceId,
                IsActive: true,
              })
          });
        }
      }



      var insertUpcharge = 0;
      if (this.patchedService !== undefined && this.vehicleForm.value.upcharge !== "") {
        var upchargeService = this.patchedService.filter(s => s.ServiceId === +this.vehicleForm.value.upcharge);
        if (upchargeService.length === 0)
          insertUpcharge = 1;
      }
      if (insertUpcharge || (this.oldUpchargeId !== +this.vehicleForm.value.upcharge && this.oldUpchargeId === 0)) {
        memberService.push(
          {
            ClientVehicleMembershipServiceId: 0,
            IsDeleted: false,
            ClientMembershipId: clientMembershipId,
            ServiceId: +this.vehicleForm.value.upcharge,
            IsActive: true,
          }
        )
      }
      else {
        if (this.patchedService !== undefined) {
          var upchrg = this.patchedService.filter(s => s.ServiceTypeId === this.upchargeTypeId && s.ServiceId !== +this.vehicleForm.value.upcharge);
          if (upchrg?.length > 0 && this.vehicleForm.value.upcharge === "") {
            upchrg.forEach(item => {
              memberService.push(
                {
                  ClientVehicleMembershipServiceId: item.ClientVehicleMembershipServiceId,
                  IsDeleted: true,
                  ClientMembershipId: clientMembershipId,
                  ServiceId: item.ServiceId,
                  IsActive: true,
                }
              )
            });
          }
        }
        else if (this.vehicleForm.value.upcharge !== "" && upchrg?.length === 0) {
          memberService.push(
            {
              ClientVehicleMembershipServiceId: 0,
              IsDeleted: false,
              ClientMembershipId: clientMembershipId,
              ServiceId: +this.vehicleForm.value.upcharge,
              IsActive: true,
            }
          )
        }
      }

      const formObj = {
        vehicleId: this.selectedData.ClientVehicleId,
        clientId: this.vehicleForm.value.client?.id,
        locationId: this.selectedData.LocationId,
        vehicleNumber: this.vehicleForm.value.vehicleNumber,
        vehicleMfr: this.vehicleForm.value.make.id,
        vehicleModel: this.vehicleForm.value.model.id,
        vehicleModelNo: null,
        vehicleYear: null,
        vehicleColor: Number(this.vehicleForm.value.color.id),
        upcharge: Number(this.vehicleForm.value.upcharge),
        barcode: this.vehicleForm.value.barcode !== '' ? this.vehicleForm.value.barcode : 'None/UNK',
        monthlyCharge: this.vehicleForm.value.monthlyCharge,
        notes: null,
        isActive: true,
        isDeleted: false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: new Date(),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: new Date()
      };
      
       if(this.vehicleForm.value.cardNumber == null){
        this.vehicleForm.value.cardNumber = '';
       }
     if(this.vehicleForm.value.cardNumber != '' && !this.vehicleForm.value.cardNumber.includes("XXXX")){
        this.client.getClientById(this.vehicleForm.value.client?.id).subscribe(res => {
          if (res.status === 'Success') {
            const clientDetail = JSON.parse(res.resultData);
            console.log(clientDetail, 'client');
            if (clientDetail.Status.length > 0) {
              const clientObj = clientDetail.Status[0];
              
              const billingDetailObj = {
                name: clientObj.FirstName + '' + clientObj.LastName,
                address: clientObj.Address1 ? clientObj.Address1 : null,
                city: null,  // need too change
                country: null,  // need too change
                region: null,  // need too change
                postal: clientObj.Zip ? clientObj.Zip : null
              };
              
              const amount = this.decimalPipe.transform(this.vehicleForm.value.monthlyCharge, '.2-2');
              const paymentDetailObj = {
                account: this.vehicleForm.value.cardNumber,
                expiry: this.vehicleForm.value.expiryDate != null ? this.vehicleForm.value.expiryDate.replace("/","") : "", 
                amount: amount.toString().replace(",",""),
                orderId: "",  // need too change
              };
          
              const authObj = {
                cardConnect: {},
                paymentDetail: paymentDetailObj,
                billingDetail: billingDetailObj,
                locationId: parseInt(localStorage.getItem('empLocationId'))
              };
              this.paymentAuth(authObj,memberService,formObj);
            } 
          }
       });

     }else{
        const membership = {
          clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ?
            this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
          clientVehicleId: this.selectedData.ClientVehicleId,
          locationId: localStorage.getItem('empLocationId'),
          membershipId: this.vehicleForm.value.membership === '' ?
            this.vehicles?.ClientVehicleMembership?.MembershipId : this.vehicleForm.value.membership,
          startDate: new Date().toLocaleDateString(),
          // endDate: new Date((new Date()).setDate((new Date()).getDate() + 30)).toLocaleDateString(),
          endDate: null,
          status: true,
          notes: null,
          isActive: this.vehicleForm.value.membership === '' ? false : true,
          isDeleted: this.vehicleForm.value.membership === '' ? true : false,
          createdBy: +localStorage.getItem('empId'),
          createdDate: new Date(),
          updatedBy: +localStorage.getItem('empId'),
          updatedDate: new Date(),
          totalPrice: this.vehicleForm.value.monthlyCharge,
          isDiscount: this.MembershipDiscount,
          cardNumber: this.vehicleForm.value.cardNumber,
          expiryDate:this.vehicleForm.value.expiryDate != null ? this.vehicleForm.value.expiryDate.replace("/","") : "",
          profileId:this.profileId,
          accountId:this.accountId
        };
        let membershipServices = [];
        if (memberService !== undefined && memberService.length) {
          membershipServices = memberService.map(item => {
            return {
              clientVehicleMembershipServiceId: item.ClientVehicleMembershipServiceId ? item.ClientVehicleMembershipServiceId : 0,
              clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ?
                this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
              serviceId: item.ServiceId ? item.ServiceId : item.item_id,
              isActive: true,
              isDeleted: item.IsDeleted,
              createdBy: +localStorage.getItem('empId'),
              createdDate: new Date(),
              updatedBy: +localStorage.getItem('empId'),
              updatedDate: new Date()
            };
          });
        }
        let membershipName = '';
        if (this.vehicleForm.value.membership !== '') {
          const selectedMembership = this.membership.filter(item => item.MembershipId === +this.vehicleForm.value.membership);
          if (selectedMembership.length > 0) {
            membershipName = selectedMembership[0].MembershipName;
          }
        }
        const value: any = {
          ClientId: this.clientId,
          ClientVehicleId: this.selectedData.ClientVehicleId,
          VehicleNumber: this.vehicleForm.value.vehicleNumber,
          VehicleMfr: this.vehicleForm.value.make.name,
          VehicleModel: this.vehicleForm.value.model.name,
          VehicleColor: this.vehicleForm.value.color.name,
          Upcharge: this.upchargeType !== null && this.upchargeType !== undefined ? this.upchargeType.filter(item =>
            item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
          Barcode: this.vehicleForm.value.barcode,
          MembershipName: membershipName !== '' ? membershipName : 'No'
        };
  
  
        //Avoid duplicate of existing services which is not removed.
        membershipServices = membershipServices.filter(s => (s.clientVehicleMembershipServiceId === 0 && (s.serviceId !== null) && (s.serviceId !== undefined)) || (s.clientVehicleMembershipServiceId > 0 && s.isDeleted === true));
        var membershipServicefiltered = [];
        if (this.vehicleForm.value.membership !== '') {
          membershipServices.forEach(s => {
            var t = membershipServices.filter(s => s.serviceId === this.vehicleForm.value.wash && s.isDeleted === true);
  
            if (t.length === 0)
              membershipServicefiltered.push(s);
  
          })
        }
  
        membershipServices = membershipServicefiltered;

        const model = {
          clientVehicleMembershipDetails: this.vehicleForm.value.membership === '' && membership.clientMembershipId === 0 ? null : membership,
          clientVehicleMembershipService: membershipServices.length !== 0 ? membershipServices : null
        };
  
        var deleteClientMembershipId = 0;
        if (this.vehicleForm.value.membership === "" || ((this.vehicleForm.value.membership !== this.membershipId) && this.clientMembershipId !== 0)) {
          deleteClientMembershipId = this.clientMembershipId !== 0 ? this.clientMembershipId : null;
        }
  
        const sourceObj = {
          clientVehicle: { clientVehicle: formObj },
          clientVehicleMembershipModel: (deleteClientMembershipId === 0) ?
            ((this.vehicleForm.value.membership !== "") ? model : null) : ((this.vehicleForm.value.membership !== "") ? model : null),
          deletedClientMembershipId: deleteClientMembershipId
        };
  
        this.vehicle.vehicleValue = value;
        this.spinner.show();
        this.vehicle.updateVehicle(sourceObj).subscribe(data => {
          if (data.status === 'Success') {
            this.spinner.hide();
            this.toastr.success(MessageConfig.Admin.Vehicle.Update, 'Success!');
            this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          } else {
            this.spinner.hide()
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          }
        }, (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
      }
     
    } else {
      const add = {
        VehicleId: 0,
        ClientId: this.clientId,
        LocationId: localStorage.getItem('empLocationId'),
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: Number(this.vehicleForm.value.make.id),
        VehicleModel: Number(this.vehicleForm.value.model.id),
        VehicleColor: Number(this.vehicleForm.value.color.id),
        Upcharge: Number(this.vehicleForm.value.upcharge),
        Barcode: this.vehicleForm.value.barcode !== '' ? this.vehicleForm.value.barcode : 'None/UNK',
        VehicleModelNo: null,
        VehicleYear: null,
        Notes: null,
        IsActive: true,
        IsDeleted: false,
        CreatedBy: +localStorage.getItem('empId'),
        CreatedDate: new Date(),
        UpdatedBy: +localStorage.getItem('empId'),
        UpdatedDate: new Date(),
      };
      const value = {
        ClientId: this.isAdd ? 0 : this.clientId,
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: this.vehicleForm.value.make.name,
        VehicleModel: this.vehicleForm.value.model.name,
        VehicleColor: this.vehicleForm.value.color.name,
        MembershipName: 'No',
        Upcharge: this.upchargeType !== null ? this.upchargeType.filter(item =>
          item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
        Barcode: this.vehicleForm.value.barcode !== '' ? this.vehicleForm.value.barcode : 'None/UNK',
      };
      const formObj = {
        clientVehicle: add,
        vehicleImage: []
      };
      if (this.isAdd === true) {
        this.spinner.show();
        this.vehicle.saveVehicle(formObj).subscribe(data => {
          if (data.status === 'Success') {
            this.spinner.hide();
            this.toastr.success(MessageConfig.Admin.Vehicle.Add, 'Success!');
            this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          } else {
            this.spinner.hide();
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          }
        }, (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
      } else {
        this.vehicle.addVehicle = add;
        this.vehicle.vehicleValue = value;
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.toastr.success(MessageConfig.Admin.Vehicle.Save, 'Success!');
      }
    }
  }

  onAllItemSelect(event) {
    console.log(event, 'event');
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  upchargeTypeChange(event, value) {

    const upchargeServcie = this.membershipServices.filter(item =>
      item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);
    let oldPrice = 0;
    let newPrice = 0;
    if (upchargeServcie.length > 0) {

      const oldUpchargeServcie = this.upchargeServices.filter(item => item.ServiceId === this.oldUpchargeId);

      if (this.oldUpchargeId === 0) {
        oldPrice = 0;
      }
      else {
        oldPrice = oldUpchargeServcie[0].Price !== undefined ? oldUpchargeServcie[0].Price : 0;
      }
      var monthlycharge = Number(this.vehicleForm.value.monthlyCharge);
      var newUpcharge = 0;
      if (event.target.value !== "") {
        const newUpchargeServcie = this.upchargeServices.filter(item => item.ServiceId === +event.target.value);
        newUpcharge = newUpchargeServcie[0].Price;
        this.oldUpchargeId = +event.target.value;
      }
      else {
        newUpcharge = 0;
        this.oldUpchargeId = 0;
      }
      newPrice = monthlycharge - oldPrice + newUpcharge;
      this.vehicleForm.patchValue({ monthlyCharge: newPrice });

    }
    if (value === 'type') {
      this.vehicleForm.patchValue({ upcharge: event.target.value });
    } else {
      this.vehicleForm.patchValue({ upchargeType: event.target.value });
    }
  }

  washTypeChange(serviceId) {
    const washService = this.membershipServices.filter(item =>
      item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
    let oldPrice = 0;
    let newPrice = 0;
    if (washService.length > 0) {
      const oldUpchargeServcie = this.washService.filter(item => item.ServiceId === washService[0].ServiceId);
      oldPrice = +this.vehicleForm.value.monthlyCharge - oldUpchargeServcie[0].Price !== undefined ? oldUpchargeServcie[0].Price : 0;
      const newUpchargeServcie = this.washService.filter(item => item.ServiceId === serviceId);
      newPrice = oldPrice + newUpchargeServcie[0].Price;
      this.vehicleForm.patchValue({ monthlyCharge: newPrice });
    }
    this.vehicleForm.patchValue({ wash: serviceId });
  }

  getServiceType() {
    const serviceTypeValue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceEnum = serviceTypeValue;
      this.upchargeTypeId = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.WashUpcharge)[0]?.CodeId;
    }
  }

  getCardType(number) {
    // visa
    let re = new RegExp('^4');
    if (number.match(re) != null) {
      this.card = 'Visa';
      return this.card;
    }


    // Mastercard 
    // Updated for Mastercard 2017 BINs expansion
    if (/^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$/.test(number)) {
      this.card = 'Mastercard';
      return this.card;
    }

    // AMEX
    re = new RegExp('^3[47]');
    if (number.match(re) != null) {
      this.card = 'AMEX';
      return this.card;
    }
    // Discover
    re = new RegExp('^(6011|622(12[6-9]|1[3-9][0-9]|[2-8][0-9]{2}|9[0-1][0-9]|92[0-5]|64[4-9])|65)');
    if (number.match(re) != null) {
      this.card = 'Discover';
      return this.card;
    }
    // Diners
    re = new RegExp('^36');
    if (number.match(re) != null) {
      this.card = 'Diners';
      return this.card;
    }
    // Diners - Carte Blanche
    re = new RegExp('^30[0-5]');
    if (number.match(re) != null) {
      this.card = 'Diners - Carte Blanche';
      return this.card;
    }
    // JCB
    re = new RegExp('^35(2[89]|[3-8][0-9])');
    if (number.match(re) != null) {
      this.card = 'JCB';
      return this.card;
    }
    // Visa Electron
    re = new RegExp('^(4026|417500|4508|4844|491(3|7))');
    if (number.match(re) != null) {
      this.card = 'Visa Electron';
      return this.card;
    }

    console.log(this.card);
  }

  getUpcharge(applyUpcharge = true) {
    if (this.isEdit && applyUpcharge) {
      if (!this.upchargeTypeId || !this.vehicleForm.value.model?.id) {
        return;
      }
      const obj = {
        upchargeServiceType: this.upchargeTypeId,
        modelId: this.vehicleForm.value.model?.id,
        locationId: this.locationId
      };

      this.GetUpchargeService.getUpcharge(obj).subscribe(res => {
        if (res.status === 'Success') {
          const jobtype = JSON.parse(res.resultData);
          this.upchargeList = jobtype.upcharge;
          var serviceId = 0
          var membership = this.vehicleForm.value.membership;

          if (this.upchargeList?.length > 0) {
            var service = this.upchargeList[this.upchargeList.length - 1];
            serviceId = service?.ServiceId;
            this.vehicleForm.patchValue({
              "upcharge": serviceId,
              "upchargeType": serviceId
            });
            
            var newCharge = 0;
            
            if (this.isLoaded && applyUpcharge && (membership !== "" && membership !== undefined)) {              
              newCharge = parseFloat(this.vehicleForm.value.monthlyCharge) + parseFloat(service?.Price) - this.upchargePrice;
              this.upchargePrice = service?.Price;

              this.vehicleForm.patchValue({ monthlyCharge: newCharge.toFixed(2) });
            }
            else {
              this.upchargePrice = service?.Price;
            }

            if(this.isEdit&& !this.isLoaded)
            {
              this.isLoaded = true;
            }

            this.toastr.info(MessageConfig.Admin.Vehicle.UpchargeApplied, 'Upcharge!');
          }
          else {

            var newCharge = 0;
            if(membership !== undefined && membership !== "")
            {
              newCharge = parseFloat(this.vehicleForm.value.monthlyCharge) - this.upchargePrice;
            }

            this.upchargePrice = 0;

            this.vehicleForm.patchValue({
              "upcharge": '',
              "upchargeType": '',
              "monthlyCharge": newCharge.toFixed(2)
            });

            this.toastr.info(MessageConfig.Admin.Vehicle.UpchargeNotAvailable, 'Upcharge!');
          }
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }
  

  paymentAuth(authObj,memberService,formObj) {
    this.spinner.show();
    
    this.payrollsService.authProfile(authObj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const auth = JSON.parse(res.resultData);        
       this.profileId = auth.profileid;
       this.accountId = auth.acctid;
       const membership = {
        clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ?
          this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
        clientVehicleId: this.selectedData.ClientVehicleId,
        locationId: localStorage.getItem('empLocationId'),
        membershipId: this.vehicleForm.value.membership === '' ?
          this.vehicles?.ClientVehicleMembership?.MembershipId : this.vehicleForm.value.membership,
        startDate: new Date().toLocaleDateString(),
        //endDate: new Date((new Date()).setDate((new Date()).getDate() + 30)).toLocaleDateString(),
        endDate:null,
        status: true,
        notes: null,
        isActive: this.vehicleForm.value.membership === '' ? false : true,
        isDeleted: this.vehicleForm.value.membership === '' ? true : false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: new Date(),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: new Date(),
        totalPrice: this.vehicleForm.value.monthlyCharge,
        isDiscount: this.MembershipDiscount,
        cardNumber: this.vehicleForm.value.cardNumber,
        expiryDate:this.vehicleForm.value.expiryDate != null ? this.vehicleForm.value.expiryDate.replace("/","") : "",
        profileId:this.profileId,
        accountId:this.accountId
      };
      let membershipServices = [];
      if (memberService !== undefined && memberService.length) {
        membershipServices = memberService.map(item => {
          return {
            clientVehicleMembershipServiceId: item.ClientVehicleMembershipServiceId ? item.ClientVehicleMembershipServiceId : 0,
            clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ?
              this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
            serviceId: item.ServiceId ? item.ServiceId : item.item_id,
            isActive: true,
            isDeleted: item.IsDeleted,
            createdBy: +localStorage.getItem('empId'),
            createdDate: new Date(),
            updatedBy: +localStorage.getItem('empId'),
            updatedDate: new Date()
          };
        });
      }
      let membershipName = '';
      if (this.vehicleForm.value.membership !== '') {
        const selectedMembership = this.membership.filter(item => item.MembershipId === +this.vehicleForm.value.membership);
        if (selectedMembership.length > 0) {
          membershipName = selectedMembership[0].MembershipName;
        }
      }
      const value: any = {
        ClientId: this.clientId,
        ClientVehicleId: this.selectedData.ClientVehicleId,
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: this.vehicleForm.value.make.name,
        VehicleModel: this.vehicleForm.value.model.name,
        VehicleColor: this.vehicleForm.value.color.name,
        Upcharge: this.upchargeType !== null && this.upchargeType !== undefined ? this.upchargeType.filter(item =>
          item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
        Barcode: this.vehicleForm.value.barcode,
        MembershipName: membershipName !== '' ? membershipName : 'No'
      };


      //Avoid duplicate of existing services which is not removed.
      membershipServices = membershipServices.filter(s => (s.clientVehicleMembershipServiceId === 0 && (s.serviceId !== null) && (s.serviceId !== undefined)) || (s.clientVehicleMembershipServiceId > 0 && s.isDeleted === true));
      var membershipServicefiltered = [];
      if (this.vehicleForm.value.membership !== '') {
        membershipServices.forEach(s => {
          var t = membershipServices.filter(s => s.serviceId === this.vehicleForm.value.wash && s.isDeleted === true);

          if (t.length === 0)
            membershipServicefiltered.push(s);

        })
      }

      membershipServices = membershipServicefiltered;
      const model = {
        clientVehicleMembershipDetails: this.vehicleForm.value.membership === '' && membership.clientMembershipId === 0 ? null : membership,
        clientVehicleMembershipService: membershipServices.length !== 0 ? membershipServices : null
      };

      var deleteClientMembershipId = 0;
      if (this.vehicleForm.value.membership === "" || ((this.vehicleForm.value.membership !== this.membershipId) && this.clientMembershipId !== 0)) {
        deleteClientMembershipId = this.clientMembershipId !== 0 ? this.clientMembershipId : null;
      }

      const sourceObj = {
        clientVehicle: { clientVehicle: formObj },
        clientVehicleMembershipModel: (deleteClientMembershipId === 0) ?
          ((this.vehicleForm.value.membership !== "") ? model : null) : ((this.vehicleForm.value.membership !== "") ? model : null),
        deletedClientMembershipId: deleteClientMembershipId
      };

      this.vehicle.vehicleValue = value;
      this.spinner.show();
      this.vehicle.updateVehicle(sourceObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();
          this.toastr.success(MessageConfig.Admin.Vehicle.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide()
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
      } else {
        
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: res.errorMessage });
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}

