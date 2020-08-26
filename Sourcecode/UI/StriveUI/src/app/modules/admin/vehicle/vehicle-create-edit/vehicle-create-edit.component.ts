import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

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
  make: any;
  model: any;
  color: any;
  upcharge: any;
  upchargeType: any;
  membership: any;
  additional: any;
  membershipServices: any = [];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
    this.upcharge = [{ CodeId: 0, CodeValue: "None" }, { CodeId: 1, CodeValue: "Upcharge1" }, { CodeId: 2, CodeValue: "Upcharge2" }];
    if (this.isView === true) {
      this.viewVehicle();
    }
    if (this.isEdit === true) {
      this.vehicleForm.reset();
      this.getVehicleById();
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
      service: ['',]
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
      upcharge: this.selectedData.Upcharge
    });
  }

  viewVehicle() {
    this.vehicleForm.disable();
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
    this.vehicle.getMembershipService().subscribe(data => {
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.additional = membership.ServicesWithPrice.filter(item => item.ServiceTypeName === "Additional Services");
        console.log(membership);
        this.additional = this.additional.map(item => {
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
          itemsShowLimit: 3,
          allowSearchFilter: false
        };
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
        this.upchargeType = vehicle.VehicleDetails.filter(item => item.CategoryId === 34);
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
    const formObj = {
      vehicleId: this.selectedData.ClientVehicleId,
      clientId: this.selectedData.ClientId,
      locationId: 1,
      vehicleNumber: this.vehicleForm.value.vehicleNumber,
      vehicleMfr: this.vehicleForm.value.make,
      vehicleModel: this.vehicleForm.value.model,
      vehicleModelNo: 0,
      vehicleYear: "",
      vehicleColor: Number(this.vehicleForm.value.color),
      upcharge: Number(this.vehicleForm.value.upcharge),
      barcode: this.vehicleForm.value.barcode,
      notes: "",
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const membership = {
      clientMembershipId: 0,
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
    const membershipServices = this.vehicleForm.value.service.map(item => {
      return {
        clientVehicleMembershipServiceId: 0,
        clientMembershipId: 0,
        serviceId: item.item_id,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      }
    });
    const sourceObj = {
      clientVehicle: formObj,
      clientVehicleMembershipModel:{
        clientVehicleMembershipDetails: membership,
        clientVehicleMembershipServices: membershipServices
      }
    }
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
      VehicleMake: this.make !== null ? this.make.filter(item => item.CodeId === Number(this.vehicleForm.value.make))[0].CodeValue : 0,
      ModelName: this.model !== null ? this.model.filter(item => item.CodeId === Number(this.vehicleForm.value.model))[0].CodeValue : 0,
      Color: this.color !== null ? this.color.filter(item => item.CodeId === Number(this.vehicleForm.value.color))[0].CodeValue : 0,
      Upcharge: this.upcharge !== null ? this.upcharge.filter(item => item.CodeId === Number(this.vehicleForm.value.upcharge))[0].CodeValue : 0,
      Barcode: this.vehicleForm.value.barcode,
    };
    if (this.isEdit === true) {
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
      this.vehicle.addVehicle = add;
      this.vehicle.vehicleValue = value;
      this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      this.toastr.success('Vehicle Saved Successfully!!', 'Success!');
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

