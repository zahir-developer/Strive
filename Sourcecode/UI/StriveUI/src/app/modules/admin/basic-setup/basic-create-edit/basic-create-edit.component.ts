import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';

@Component({
  selector: 'app-basic-create-edit',
  templateUrl: './basic-create-edit.component.html',
  styleUrls: ['./basic-create-edit.component.css']
})
export class BasicCreateEditComponent implements OnInit {
  basicSetupForm: FormGroup;
  State:any;
  Country:any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {
    this.basicSetupForm = this.fb.group({
      locationId: ['', Validators.required],
      locationName: ['', Validators.required],
      locationAddress: ['', Validators.required],
      zipcode: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      franchise: ['', Validators.required]
    });
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.basicSetupForm.reset();
      this.basicSetupForm.setValue({
        locationId: this.selectedData.LocationId,
        locationName: this.selectedData.LocationName,
        locationAddress: this.selectedData.LocationAddress,
        zipcode: this.selectedData.Zipcode,
        state: this.selectedData.State,
        country: this.selectedData.Country,
        phoneNumber: this.selectedData.PhoneNumber,
        email: this.selectedData.Email,
        franchise: this.selectedData.Franchise        
      });
    }
  }

  change(data){
    this.basicSetupForm.value.franchise = data;
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    const formObj = {
      locationId: this.basicSetupForm.value.locationId,
      locationName: this.basicSetupForm.value.locationName,
      locationAddress: this.basicSetupForm.value.locationAddress,
      zipcode: this.basicSetupForm.value.zipcode,
      state: this.basicSetupForm.value.state,
      country: this.basicSetupForm.value.country,
      phoneNumber: this.basicSetupForm.value.phoneNumber,
      email: this.basicSetupForm.value.email,
      franchise: this.basicSetupForm.value.franchise
    };
    sourceObj.push(formObj);
    this.crudService.basicsetupdetails.push(formObj);
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
