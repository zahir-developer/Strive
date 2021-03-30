import { Component, OnInit, Output, Input, EventEmitter, ViewEncapsulation } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as _ from 'underscore';
import { MessageConfig } from '../../services/messageConfig';
import { ApplicationConfig } from '../../services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-vehicle-create-edit',
  templateUrl: './vehicle-create-edit.component.html',
  styleUrls: ['./vehicle-create-edit.component.css'],
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
  washesDropdown: any = [];
  submitted: boolean;
  filteredModel: any = [];
  filteredcolor: any = [];
  filteredMake: any = [];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService,
    private spinner : NgxSpinnerService) { }

  ngOnInit() {
    this.formInitialize();
    if (this.isView === true) {
      this.viewVehicle();
    }
    if (this.isEdit === true) {
      this.vehicleForm.reset();
      this.getVehicleById();
      this.getVehicleMembershipDetailsByVehicleId();
    }
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
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
      services: [[]]
    });
    this.vehicleForm.get('vehicleNumber').patchValue(this.vehicleNumber);
    this.vehicleForm.controls.vehicleNumber.disable();
    this.getVehicleCodes();
    this.getVehicleMembership();
    this.getMembershipService();
  }

  get f() {
    return this.vehicleForm.controls;
  }

  getVehicleById() {
    this.vehicleForm.patchValue({
      barcode: this.selectedData.Barcode,
      vehicleNumber: this.selectedData.VehicleNumber,
      color: { id: this.selectedData.ColorId, name: this.selectedData.Color },
      model: { id: this.selectedData.VehicleModelId, name: this.selectedData.ModelName },
      make: { id: this.selectedData.VehicleMakeId, name: this.selectedData.VehicleMake },
      upchargeType: this.selectedData.Upcharge,
      upcharge: this.selectedData.Upcharge,
      monthlyCharge: this.selectedData.MonthlyCharge.toFixed(2),
      membership: ''
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
          this.getMemberServices(this.memberServiceId);
          this.vehicleForm.patchValue({
            membership: vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId
          });
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
          this.dropdownSettings = {
            singleSelection: false,
            defaultOpen: false,
            idField: 'item_id',
            textField: 'item_text',
            itemsShowLimit: 1,
            enableCheckAll: false,
            allowSearchFilter: true
          };
          this.vehicleForm.patchValue({
            services: this.memberService
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get VehicleMembership
  getVehicleMembership() {
    this.vehicle.getVehicleMembership().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.Membership;
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
    this.dropdownSettings = {
      singleSelection: false,
      defaultOpen: false,
      idField: 'item_id',
      textField: 'item_text',
      itemsShowLimit: 2,
      enableCheckAll: false,
      allowSearchFilter: true
    };
  }

  membershipChange(data) {
    this.vehicleForm.get('monthlyCharge').reset();

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
        if (membership.MembershipAndServiceDetail.Membership !== null) {
          this.vehicleForm.patchValue({
            monthlyCharge: membership.MembershipAndServiceDetail.Membership?.Price?.toFixed(2)
          });
        }
        if (membership.MembershipAndServiceDetail.MembershipService !== null) {
          this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
          const washService = this.membershipServices.filter(item =>
            item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
          if (washService.length > 0) {
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }
          const upchargeServcie = this.membershipServices.filter(item =>
            item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);
          if (upchargeServcie.length > 0) {
            this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
          }
          if (this.membershipServices.filter(i => i.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices).length !== 0) { // Additonal Services
            this.memberOnchangePatchedService = this.membershipServices.filter(item =>
              (item.ServiceType) === ApplicationConfig.Enum.ServiceType.AdditonalServices);
          }
          this.memberOnchangePatchedService.forEach(element => {
            if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
              this.selectedservice.push(element);
            }
          });
          this.extraService.forEach(element => {
            if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
              this.selectedservice.push(element);
            }
          });
          const serviceIds = this.selectedservice.map(item => item.ServiceId);
          const memberService = serviceIds.map((e) => {
            const f = this.additionalService.find(a => a.ServiceId === e);
            return f ? f : 0;
          });
          this.memberService = memberService.map(item => {
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
            services: ''
          });
        }
        this.dropdownSettings = {
          singleSelection: false,
          defaultOpen: false,
          idField: 'item_id',
          textField: 'item_text',
          itemsShowLimit: 2,
          enableCheckAll: false,
          allowSearchFilter: true,
        };
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  onItemSelect(data) {
    this.extraService.push(this.additionalService.filter(i => i.ServiceId === data.item_id)[0]);
    this.memberService.push(data);
    this.vehicleForm.get('services').patchValue(this.memberService);
    let price = 0;
    price = +this.additionalService.filter(i => i.ServiceId === data.item_id)[0].Price;
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
    this.vehicle.getMembershipById(+data).subscribe(res => {
      if (res.status === 'Success') {
        this.memberOnchangePatchedService = [];
        const membership = JSON.parse(res.resultData);
        if (membership.MembershipAndServiceDetail.MembershipService !== null) {
          this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
          const washService = this.membershipServices.filter(item => item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.WashPackage);
          if (washService.length > 0) {
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }
          const upchargeServcie = this.membershipServices.filter(item => item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.WashUpcharge);
          if (upchargeServcie.length > 0) {
            this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
          }
          if (this.membershipServices.filter(i => (i.ServiceTypeName) === ApplicationConfig.Enum.ServiceType.AdditonalServices).length !== 0) {
            this.memberOnchangePatchedService = this.membershipServices.filter(item => (item.ServiceTypeName) === ApplicationConfig.Enum.ServiceType.AdditonalServices);
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

  // Get vehicleCodes
  getVehicleCodes() {
    this.vehicle.getVehicleCodes().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.make = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleManufacturer');
        this.model = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleModel');
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');
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
        this.make = this.make.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });
        this.upchargeService();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  upchargeService() {
    const serviceObj = {
      locationId: localStorage.getItem('empLocationId'),
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.vehicle.getUpchargeService(serviceObj).subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.upchargeType = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item => item.IsActive === true && item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge);
        this.washesDropdown = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(item =>
          item.IsActive === true && item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage);
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
        const r2 = this.patchedService.filter((elem) => !this.memberService.find(({ item_id }) => elem.ServiceId === item_id));
        r2.forEach(item => item.IsDeleted = true);
        memberService = r.concat(r1).concat(r2);
      } else {
        memberService = this.memberService;
      }
      const formObj = {
        vehicleId: this.selectedData.ClientVehicleId,
        clientId: this.selectedData.ClientId,
        locationId: localStorage.getItem('empLocationId'),
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
      const membership = {
        clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ?
          this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
        clientVehicleId: this.selectedData.ClientVehicleId,
        locationId: localStorage.getItem('empLocationId'),
        membershipId: this.vehicleForm.value.membership === '' ?
          this.vehicles?.ClientVehicleMembership?.MembershipId : this.vehicleForm.value.membership,
        startDate: new Date().toLocaleDateString(),
        endDate: new Date((new Date()).setDate((new Date()).getDate() + 30)).toLocaleDateString(),
        status: true,
        notes: null,
        isActive: this.vehicleForm.value.membership === '' ? false : true,
        isDeleted: this.vehicleForm.value.membership === '' ? true : false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: new Date(),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: new Date(),
        totalPrice: this.vehicleForm.value.monthlyCharge
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
        Upcharge: this.upchargeType !== null ? this.upchargeType.filter(item =>
          item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
        Barcode: this.vehicleForm.value.barcode,
        MembershipName: membershipName !== '' ? membershipName : 'No'
      };

      const model = {
        clientVehicleMembershipDetails: this.vehicleForm.value.membership === '' && membership.clientMembershipId === 0 ? null : membership,
        clientVehicleMembershipService: membershipServices.length !== 0 ? membershipServices : null
      };
      const sourceObj = {
        clientVehicle: { clientVehicle: formObj },
        clientVehicleMembershipModel: model
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
        UpdatedDate: new Date()
      };
      const value = {
        ClientId: this.isAdd ? 0 : this.clientId,
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: this.vehicleForm.value.make.name,
        VehicleModel: this.vehicleForm.value.model.name,
        VehicleColor: this.vehicleForm.value.color.name,
        MembershipName: 'NO',
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
      const oldUpchargeServcie = this.upchargeServices.filter(item => item.ServiceId === upchargeServcie[0].ServiceId);
      oldPrice = +this.vehicleForm.value.monthlyCharge - oldUpchargeServcie[0].Price;
      const newUpchargeServcie = this.upchargeServices.filter(item => item.ServiceId === +event.target.value);
      newPrice = oldPrice + newUpchargeServcie[0].Price;
      this.vehicleForm.patchValue({ monthlyCharge: newPrice });
    }
    if (value === 'type') {
      this.vehicleForm.patchValue({ upcharge: event.target.value });
    } else {
      this.vehicleForm.patchValue({ upchargeType: event.target.value });
    }
  }
}

