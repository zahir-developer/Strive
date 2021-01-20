import { Component, OnInit, Output, Input, EventEmitter, ViewEncapsulation } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as _ from 'underscore';

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
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
    if (this.isView === true) {
      this.viewVehicle();
    }
    if (this.isEdit === true) {
      this.vehicleForm.reset();
      this.getVehicleById();
      this.getVehicleMembershipDetailsByVehicleId();
      console.log(this.additionalService, 'data');
    }
    console.log(this.selectedData, 'selectedData');
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
      barcode: ['',Validators.required],
      vehicleNumber: ['',],
      make: ['',Validators.required],
      model: ['',Validators.required],
      color: ['',Validators.required],
      upcharge: ['',],
      upchargeType: ['',],
      monthlyCharge: ['',],
      membership: ['',],
      wash: [''],
      services: [[]]
    });
    this.vehicleForm.controls.vehicleNumber.disable();
    this.vehicleForm.get('vehicleNumber').patchValue(this.vehicleNumber);
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
      make: this.selectedData.VehicleMakeId,
      model: this.selectedData.VehicleModelId,
      color: this.selectedData.ColorId,
      upchargeType: this.selectedData.Upcharge,
      upcharge: this.selectedData.Upcharge,
      monthlyCharge: this.selectedData.MonthlyCharge.toFixed(2),
      membership: ''
    });
  }

  viewVehicle() {
    this.vehicleForm.disable();
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
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            itemsShowLimit: 1,
            allowSearchFilter: false
          };
          this.vehicleForm.patchValue({
            services: this.memberService
          });
        }
      }
    });
  }

  // Get VehicleMembership
  getVehicleMembership() {
    this.vehicle.getVehicleMembership().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.Membership;
        console.log(this.membership);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
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
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: false
    };
  }

  membershipChange(data) {
    if (this.memberOnchangePatchedService.length !== 0) {
      this.memberOnchangePatchedService.forEach(element => {
        this.selectedservice = this.selectedservice.filter(i => i.ServiceId !== element.ServiceId);
        this.memberService = this.memberService.filter(i => i.item_id !== element.ServiceId);
      });
    }
    this.memberService = [];
    // this.patchedService = [];
    this.vehicle.getMembershipById(Number(data)).subscribe(res => {
      if (res.status === 'Success') {
        this.memberOnchangePatchedService = [];
        const membership = JSON.parse(res.resultData);
        this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
        this.vehicleForm.patchValue({
          monthlyCharge: membership.MembershipAndServiceDetail.Membership?.Price?.toFixed(2)
        });
        if (this.membershipServices !== null) {
          const washService = this.membershipServices.filter(item => item.ServiceTypeName === 'Washes');
          if (washService.length > 0) {
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }
          const upchargeServcie = this.membershipServices.filter(item => item.ServiceTypeName === 'Upcharges');
          if (upchargeServcie.length > 0) {
            this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
          }
          if (this.membershipServices.filter(i => Number(i.ServiceTypeId) === 17).length !== 0) {
            this.memberOnchangePatchedService = this.membershipServices.filter(item => Number(item.ServiceTypeId) === 17);
          }
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
        this.dropdownSettings = {
          singleSelection: false,
          defaultOpen: false,
          idField: 'item_id',
          textField: 'item_text',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 2,
          allowSearchFilter: false
        };
        this.vehicleForm.get('services').patchValue(this.memberService);
        if (this.patchedService !== undefined) {
          this.patchedService.forEach(element => {
            if (this.selectedservice.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
              element.IsDeleted = true;
            }
          });
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
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
        this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
        if (this.membershipServices !== null) {
          const washService = this.membershipServices.filter(item => item.ServiceTypeName === 'Washes');
          if (washService.length > 0) {
            this.vehicleForm.patchValue({ wash: washService[0].ServiceId });
            this.vehicleForm.controls.wash.disable();
          }
          const upchargeServcie = this.membershipServices.filter(item => item.ServiceTypeName === 'Upcharges');
          if (upchargeServcie.length > 0) {
            this.vehicleForm.patchValue({ upcharge: upchargeServcie[0].ServiceId, upchargeType: upchargeServcie[0].ServiceId });
          }
        }
        if (this.membershipServices.filter(i => Number(i.ServiceTypeId) === 17).length !== 0) {
          this.memberOnchangePatchedService = this.membershipServices.filter(item => Number(item.ServiceTypeId) === 17);
          if (this.memberOnchangePatchedService.length !== 0) {
            this.patchedService.forEach(element => {
              if (this.memberOnchangePatchedService.filter(i => i.ServiceId === element.ServiceId)[0] === undefined) {
                this.extraService.push(element);
              }
            });
          }
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  // Get vehicleCodes
  getVehicleCodes() {
    this.vehicle.getVehicleCodes().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.make = vehicle.VehicleDetails.filter(item => item.CategoryId === 28);
        this.model = vehicle.VehicleDetails.filter(item => item.CategoryId === 29);
        this.color = vehicle.VehicleDetails.filter(item => item.CategoryId === 30);
        this.upchargeService();
        //this.upchargeType = vehicle.VehicleDetails.filter(item => item.CategoryId === 34);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  upchargeService() {
    this.vehicle.getUpchargeService().subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        console.log(serviceDetails, 'service');
        this.upchargeType = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceTypeId === '18');
        this.washesDropdown = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === 'Washes');
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  change(data) {
    this.vehicleForm.value.franchise = data;
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
        vehicleMfr: this.vehicleForm.value.make,
        vehicleModel: this.vehicleForm.value.model,
        vehicleModelNo: 0,
        vehicleYear: '',
        vehicleColor: Number(this.vehicleForm.value.color),
        upcharge: Number(this.vehicleForm.value.upcharge),
        barcode: this.vehicleForm.value.barcode,
        monthlyCharge: this.vehicleForm.value.monthlyCharge,
        notes: '',
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
      const membership = {
        clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ? this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
        // clientMembershipId: this.,
        clientVehicleId: this.selectedData.ClientVehicleId,
        locationId: localStorage.getItem('empLocationId'),
        membershipId: this.vehicleForm.value.membership === "" ? this.vehicles?.ClientVehicleMembership?.MembershipId : this.vehicleForm.value.membership,
        startDate: new Date().toLocaleDateString(),
        endDate: new Date((new Date()).setDate((new Date).getDate() + 30)).toLocaleDateString(),
        status: true,
        notes: '',
        isActive: this.vehicleForm.value.membership === "" ? false : true,
        isDeleted: this.vehicleForm.value.membership === "" ? true : false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        totalPrice: this.vehicleForm.value.monthlyCharge
      };
      let membershipServices = [];
      if (memberService !== undefined && memberService.length) {
        membershipServices = memberService.map(item => {
          return {
            clientVehicleMembershipServiceId: item.ClientVehicleMembershipServiceId ? item.ClientVehicleMembershipServiceId : 0,
            clientMembershipId: this.vehicles?.ClientVehicleMembership?.ClientMembershipId ? this.vehicles?.ClientVehicleMembership?.ClientMembershipId : 0,
            serviceId: item.ServiceId ? item.ServiceId : item.item_id,
            isActive: true,
            isDeleted: item.IsDeleted,
            createdBy: 1,
            createdDate: new Date(),
            updatedBy: 1,
            updatedDate: new Date()
          };
        });
      }



      const model = {
        clientVehicleMembershipDetails: this.vehicleForm.value.membership === "" && membership.clientMembershipId === 0 ? null : membership,
        clientVehicleMembershipService: membershipServices.length !== 0 ? membershipServices : null
      };
      const sourceObj = {
        clientVehicle: { clientVehicle: formObj },
        clientVehicleMembershipModel: model
      };
      this.vehicle.updateVehicle(sourceObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
        }
      });
    } else {
      const add = {
        VehicleId: 0,
        ClientId: this.clientId,
        LocationId: localStorage.getItem('empLocationId'),
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: Number(this.vehicleForm.value.make),
        VehicleModel: Number(this.vehicleForm.value.model),
        VehicleColor: Number(this.vehicleForm.value.color),
        Upcharge: Number(this.vehicleForm.value.upcharge),
        Barcode: this.vehicleForm.value.barcode,
        VehicleModelNo: 0,
        VehicleYear: "",
        Notes: "",
        IsActive: true,
        IsDeleted: false,
        CreatedBy: 1,
        CreatedDate: new Date(),
        UpdatedBy: 1,
        UpdatedDate: new Date()
      };
      const value = {
        ClientVehicleId: this.isAdd ? 0 : this.clientId,
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: this.make !== null ? this.make.filter(item => item.CodeId === Number(this.vehicleForm.value.make))[0].CodeValue : 0,
        VehicleModel: this.model !== null ? this.model.filter(item => item.CodeId === Number(this.vehicleForm.value.model))[0].CodeValue : 0,
        VehicleColor: this.color !== null ? this.color.filter(item => item.CodeId === Number(this.vehicleForm.value.color))[0].CodeValue : 0,
        Upcharge: this.upchargeType !== null ? this.upchargeType.filter(item =>
          item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
        Barcode: this.vehicleForm.value.barcode,
      };
      const formObj = {
        clientVehicle: [add]
      };
      if (this.isAdd === true) {
        this.vehicle.saveVehicle(formObj).subscribe(data => {
          if (data.status === 'Success') {
            this.toastr.success('Vehicle Added Successfully!!', 'Success!');
            this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          } else {
            this.toastr.error('Communication Error', 'Error!');
          }
        });
      } else {
        this.vehicle.addVehicle = add;
        this.vehicle.vehicleValue = value;
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.toastr.success('Vehicle Saved Successfully!!', 'Success!');
      }
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  upchargeTypeChange(event, value) {
    console.log(event.target.value, value);
    const upchargeServcie = this.membershipServices.filter(item => item.ServiceTypeName === 'Upcharges');
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

