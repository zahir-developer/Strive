import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';

@Component({
  selector: 'app-create-edit-washes',
  templateUrl: './create-edit-washes.component.html',
  styleUrls: ['./create-edit-washes.component.css']
})
export class CreateEditWashesComponent implements OnInit {

  washForm : FormGroup;
  timeIn: any;
  timeOut: any;
  minutes: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  Score : any;
  ticketNumber : any;

  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
    this.Score = [{ CodeId: 1, CodeValue: "None" }, { CodeId: 2, CodeValue: "Option1" }, { CodeId: 3, CodeValue: "Option2" }];
    if (this.isView === true) {
      this.viewWash();
    }
    if (this.isEdit === true) {
      this.washForm.reset();
      this.getWashById();
    }
  }

  formInitialize() {
    this.washForm = this.fb.group({
      client: ['',],
      vehicle: ['',],
      type: ['',],
      barcode: ['',],
      washes: ['',],
      model: ['',],
      color: ['',],
      upcharges: ['',],
      upchargeType: ['',],
      airFreshners: ['',],
      notes: ['',],
      pastNotes: ['',]
    });
  }

  getWashById() {
    console.log(this.selectedData);
    this.washForm.patchValue({
      barcode: this.selectedData.BarCode,
      // make: this.selectedData.VehicleMake,
      // model: this.selectedData.VehicleModel,
      // color: this.selectedData.VehicleColor,
      // upcharge: this.selectedData.Upcharge
    });
    this.ticketNumber = this.selectedData.TicketNumber;
  }

  viewWash(){
    this.washForm.disable();
  }

  change(data) {
    this.washForm.value.franchise = data;
  }

  submit() {  
    const sourceObj=[]; 
    const formObj = {
      clientVehicleId: this.selectedData.ClientVehicleId,
      clientId: this.selectedData.ClientId,
      locationId: 1,
      vehicleNumber: this.selectedData.VehicleNumber,
      vehicleMfr: this.selectedData.VehicleMakeId,
      vehicleModel: this.selectedData.VehicleModelId,
      vehicleModelNo:0,
      vehicleYear:"",
      vehicleColor: this.selectedData.ColorId,
      upcharge: this.selectedData.Upcharge,
      barcode: this.selectedData.Barcode,
      notes: "",
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    };
    // sourceObj.push(formObj);
    // if (this.isEdit === true) {
    //   this.vehicle.updateVehicle(sourceObj).subscribe(data => {
    //     if (data.status === 'Success') {
    //       this.toastr.success('Record Updated Successfully!!', 'Success!');
    //       this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
    //     } else {
    //       this.toastr.error('Communication Error', 'Error!');
    //       this.washForm.reset();
    //     }
    //   });
    // } else {
    //   this.vehicle.updateVehicle(sourceObj).subscribe(data => {
    //     if (data.status === 'Success') {
    //       this.toastr.success('Record Updated Successfully!!', 'Success!');
    //       this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
    //     } else {
    //       this.toastr.error('Communication Error', 'Error!');
    //       this.washForm.reset();
    //     }
    //   });
    // }    
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved'});
  }
}

