import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-vehicle-create-edit',
  templateUrl: './vehicle-create-edit.component.html',
  styleUrls: ['./vehicle-create-edit.component.css']
})
export class VehicleCreateEditComponent implements OnInit {
  vehicleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  make:any;
  model:any;
  color:any;
  upcharge:any;
  upchargeType: { id: number; Value: string; }[];
  membership: { id: number; Value: string; }[];
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
    this.make = [{ id: 0, Value: "Make1" }, { id: 1, Value: "Make2" }];
    this.model = [{ id: 0, Value: "Model1" }, { id: 1, Value: "Model2" }];
    this.color = [{ id: 0, Value: "Color1" }, { id: 1, Value: "Color2" }];
    this.upcharge = [{ id: 0, Value: "None" }, { id: 1, Value: "Upcharge1" }, { id: 2, Value: "Upcharge2" }];
    this.upchargeType = [{ id: 0, Value: "None" }, { id: 1, Value: "UpchargeType1" }, { id: 2, Value: "UpchargeType2" }];
    this.membership = [{ id: 0, Value: "Member1" }, { id: 1, Value: "Member2" }, { id: 2, Value: "Member3" }];
    if (this.isEdit === true) {
      this.vehicleForm.reset();
      this.getVehicleById();
    }
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
      barcode: ['',],
      make: ['',],
      model: ['',],
      color: ['',],
      upcharge: ['',],
      upchargeType: ['',],
      monthlyCharge: ['',],
      membership: ['',]
    });
  }

  getVehicleById() {
    this.vehicleForm.patchValue({
      barcode: this.selectedData.Barcode,
      //tag: this.selectedData.VehicleNumber,
      make: this.selectedData.VehicleMake,
      model: this.selectedData.VehicleModel,
      color: this.selectedData.VehicleColor,
      upcharge: this.selectedData.Upcharge
    });
  }

  change(data) {
    this.vehicleForm.value.franchise = data;
  }

  submit() {   
    const formObj = [{
      clientVehicleId: 0,
      clientId: 2,
      locationId: 1,
      vehicleNumber: this.vehicleForm.value.tag,
      vehicleMake: this.vehicleForm.value.make,
      vehicleModel: this.vehicleForm.value.model,
      vehicleModelNo:0,
      vehicleYear:"",
      vehicleColor: this.vehicleForm.value.color,
      upcharge: this.vehicleForm.value.upcharge,
      barcode: this.vehicleForm.value.barcode,
      notes: "",
      createdDate: new Date()
    }];
    this.vehicle.updateVehicle(formObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.vehicleForm.reset();
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved'});
  }
}

