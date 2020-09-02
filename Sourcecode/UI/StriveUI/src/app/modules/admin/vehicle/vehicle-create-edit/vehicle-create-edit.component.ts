import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as _ from 'underscore';

@Component({
  selector: 'app-vehicle-create-edit',
  templateUrl: './vehicle-create-edit.component.html',
  styleUrls: ['./vehicle-create-edit.component.css']
})
export class VehicleCreateEditComponent implements OnInit {
  vehicleForm: FormGroup;
  dropdownSettings: IDropdownSettings = {};
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() clientId?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  @Input() additionalService?: any;
  make: any;
  model: any;
  color: any;
  upchargeType: any;
  membership: any;
  additional: any;
  membershipServices: any = [];
  memberService: any;
  vehicles: any;
  patchedService: any;
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
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
      barcode: ['',],
      vehicleNumber: ['',],
      make: ['',],
      model: ['',],
      color: ['',],
      upcharge: ['',],
      upchargeType: ['',],
      monthlyCharge: ['',],
      membership: ['',],
      service: [[]]
    });
    this.getVehicleCodes();
    this.getVehicleMembership();
    this.getMembershipService();
  }

  getVehicleById() {
    this.vehicleForm.patchValue({
      barcode: this.selectedData.Barcode,
      vehicleNumber: this.selectedData.VehicleNumber,
      make: this.selectedData.VehicleMakeId,
      model: this.selectedData.VehicleModelId,
      color: this.selectedData.ColorId,
      upcharge: this.selectedData.Upcharge,
      monthlyCharge: this.selectedData.MonthlyCharge
    });
  }

  viewVehicle() {
    this.vehicleForm.disable();
  }

  getVehicleMembershipDetailsByVehicleId() {
    this.vehicle.getVehicleMembershipDetailsByVehicleId(this.selectedData.ClientVehicleId).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        console.log(vehicle, 'vec');
        this.vehicles = vehicle.VehicleMembershipDetails;
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembership !== null) {
          this.membershipChange(vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId);
          this.vehicleForm.patchValue({
            membership: vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId
          });
        }
        if (vehicle.VehicleMembershipDetails.ClientVehicleMembershipService !== null) {
          this.patchedService = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
          const service = [];
          vehicle.VehicleMembershipDetails.ClientVehicleMembershipService.forEach(item => {
            const additionalService = _.where(this.additional, { item_id: item.ServiceId });
            if (additionalService.length > 0) {
              // service.push(additionalService[0]);
            }
          });
          this.vehicleForm.patchValue({
            service
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
    this.additional = this.additionalService.map(item => {
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
    this.memberService = [];
    this.vehicle.getMembershipById(Number(data)).subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.membershipServices = membership.MembershipAndServiceDetail.MembershipService;
        if (this.membershipServices.filter(i => Number(i.ServiceTypeId) === 18)[0] !== undefined) {
          this.vehicleForm.get('upchargeType').patchValue(this.membershipServices.filter(i => Number(i.ServiceTypeId) === 18)[0].ServiceId);
          this.vehicleForm.get('upcharge').patchValue(this.membershipServices.filter(i => Number(i.ServiceTypeId) === 18)[0].ServiceId);
        }
        // if (this.membershipServices.filter(i => Number(i.ServiceTypeId) === 17).length !== 0) {
        //    const memberService = this.additionalService.filter(i => Number(i.ServiceType) === 17);
        //    this.memberService = memberService.map (item => {
        //     return {
        //       item_id: item.ServiceId,
        //       item_text: item.ServiceName
        //     };
        //    });
        // }
        if (this.membershipServices.filter(i => Number(i.ServiceTypeId) === 17).length !== 0) {
          const serviceIds = this.membershipServices.filter(item => Number(item.ServiceTypeId) === 17).map(item => item.ServiceId);
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
        this.upchargeType = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceTypeId === '18');
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
    if (this.isEdit === true) {
        const formObj = {
          vehicleId: this.selectedData.ClientVehicleId,
          clientId: this.selectedData.ClientId,
          locationId: 1,
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
          clientMembershipId: this.vehicles.ClientVehicleMembership !== undefined ? this.vehicles.ClientVehicleMembership.ClientMembershipId : 0,
          clientVehicleId: this.selectedData.ClientVehicleId,
          locationId: 1,
          membershipId: this.vehicleForm.value.membership,
          startDate: "2020-08-26T14:04:54.988Z",
          endDate: "2020-08-26T14:04:54.988Z",
          status: true,
          notes: "string",
          isActive: true,
          isDeleted: false,
          createdBy: 1,
          createdDate: new Date(),
          updatedBy: 1,
          updatedDate: new Date()
        };
        let membershipServices = [];
        membershipServices = this.vehicleForm.value.service.map(item => {
          return {
            clientVehicleMembershipServiceId: 0,
            clientMembershipId: this.vehicles.ClientVehicleMembership !== undefined ? this.vehicles.ClientVehicleMembership.ClientMembershipId : 0,
            serviceId: item.item_id,
            isActive: true,
            isDeleted: false,
            createdBy: 1,
            createdDate: new Date(),
            updatedBy: 1,
            updatedDate: new Date()
          };
        });
  
  
        const model = {
          clientVehicleMembershipDetails: membership,
          clientVehicleMembershipService: membershipServices
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
          this.vehicleForm.reset();
        }
      });
    } else {
      const add = {
        VehicleId: 0,
        ClientId: this.clientId,
        LocationId: 1,
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
        ClientVehicleId: 0,
        VehicleNumber: this.vehicleForm.value.vehicleNumber,
        VehicleMfr: this.make !== null ? this.make.filter(item => item.CodeId === Number(this.vehicleForm.value.make))[0].CodeValue : 0,
        VehicleModel: this.model !== null ? this.model.filter(item => item.CodeId === Number(this.vehicleForm.value.model))[0].CodeValue : 0,
        VehicleColor: this.color !== null ? this.color.filter(item => item.CodeId === Number(this.vehicleForm.value.color))[0].CodeValue : 0,
        Upcharge: this.upchargeType !== null ? this.upchargeType.filter(item =>
          item.ServiceId === Number(this.vehicleForm.value.upcharge))[0]?.Upcharges : 0,
        Barcode: this.vehicleForm.value.barcode,
      };
      this.vehicle.addVehicle = add;
      this.vehicle.vehicleValue = value;
      this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      this.toastr.success('Vehicle Saved Successfully!!', 'Success!');
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  upchargeTypeChange(event, value) {
    console.log(event.target.value, value);
    if (value === 'type') {
      this.vehicleForm.patchValue({ upcharge: event.target.value });
    } else {
      this.vehicleForm.patchValue({ upchargeType: event.target.value });
    }
  }
}

