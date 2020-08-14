import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { WashService } from 'src/app/shared/services/data-service/wash.service';

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
  barcodeDetails: any;

  constructor(private fb: FormBuilder, private toastr: ToastrService, private wash: WashService) { }

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

  getByBarcode(barcode){
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.barcodeDetails = wash.ClientAndVehicleDetail;
        console.log(this.barcodeDetails);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  // Add/Update Wash
  submit() {  
    const job = {
      jobId: this.isEdit ? this.selectedData.JobId : 0,
      ticketNumber: "",
      locationId: 1,
      barCode: this.washForm.value.barcode,
      clientId: 1,
      vehicleId: 1,
      jobType: 15,
      jobDate: new Date(),
      timeIn: new Date(),
      estimatedTimeOut: new Date(),
      actualTimeOut: new Date(),
      jobStatus: "",
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const jobDetail = {
      jobDetailId: this.isEdit ? this.selectedData.JobDetailId : 0,
      jobId: this.isEdit ? this.selectedData.JobId : 0,
      bayId: 1,
      salesRep: 1,
      qaBy: 1,
      labour: 1,
      review: 1,
      reviewNote: "",
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const formObj = {
      job: job,
      jobDetail: jobDetail
    };
    if (this.isEdit === true) {
      this.wash.updateWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.washForm.reset();
        }
      });
    } else {
      this.wash.addWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.washForm.reset();
        }
      });
    }    
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved'});
  }
}

