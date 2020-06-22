import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit {
  sampleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private toastr: ToastrService) { }

  ngOnInit() {
    this.sampleForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      role: ['', Validators.required]
    });
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.sampleForm.reset();
      this.sampleForm.setValue({
        firstName: this.selectedData.FirstName,
        lastName: this.selectedData.LastName, role: this.selectedData.Role
      });
    }
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    // if (this.isEdit === true) {
    const formObj = {
      employeeId: this.isEdit === true ? this.selectedData?.EmployeeId : undefined,
      firstName: this.sampleForm.value.firstName,
      lastName: this.sampleForm.value.lastName,
      role: this.sampleForm.value.role
    };
    sourceObj.push(formObj);
    this.employeeService.updateEmployee(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
    // }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
