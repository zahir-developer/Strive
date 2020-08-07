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
  upchargeType: any;
  membership: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
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
    this.getVehicleColor();
    this.getVehicleMake();
    this.getVehicleModel();
    this.getVehicleUpcharge();
    this.getVehicleMembership();
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

  getVehicleMembership(){
    this.vehicle.getVehicleMembership().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.VehicleMembership;
      }else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getVehicleColor(){
    this.vehicle.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.CodeType;
      }else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getVehicleUpcharge(){
    this.vehicle.getVehicleUpcharge().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.upcharge = vehicle.CodeType;
      }else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getVehicleModel(){
    this.vehicle.getVehicleModel().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.model = vehicle.CodeType;
      }else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getVehicleMake(){
    this.vehicle.getVehicleMake().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.make = vehicle.CodeType;
      }else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  change(data) {
    this.vehicleForm.value.franchise = data;
  }

  submit() {  
    const sourceObj=[]; 
    const formObj = {
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
    };
    const add = {
      VehicleNumber: null,
      VehicleMake: null,// this.make !== null ?  this.make.filter(item => item.CodeId === Number(this.vehicleForm.value.make))[0].CodeValue : 0,
      VehicleModel: this.model !== null ? this.model.filter(item => item.CodeId === Number(this.vehicleForm.value.model))[0].CodeValue : 0,
      VehicleColor: this.color !== null ? this.color.filter(item => item.CodeId === Number(this.vehicleForm.value.color))[0].CodeValue : 0,
      Upcharge: null, //this.upcharge !== null ? this.upcharge.filter(item => item.CodeId === Number(this.vehicleForm.value.upcharge))[0].CodeValue : 0,
      Barcode: this.vehicleForm.value.barcode,
      CreatedDate: new Date()
    };
    sourceObj.push(formObj);
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
      this.vehicle.addVehicle.push(add);
      this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      this.toastr.success('Record Saved Successfully!!', 'Success!');
    }    
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved'});
  }
}

