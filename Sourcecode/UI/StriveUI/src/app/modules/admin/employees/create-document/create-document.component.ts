import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MustMatch } from 'src/app/shared/Validator/must-match.validator';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {
  isPassword: boolean;
  fileName: any = '';
  fileUploadformData: any;
  passwordForm: FormGroup;
  @Input() employeeId?: any;
  multipleFileUpload: any = [];
  fileType: any;
  isLoading: boolean;
  submitted: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.isLoading = false;
    this.isPassword = false;
    this.submitted = false;
    this.formInitialize();
  }

  formInitialize() {
    this.passwordForm = this.fb.group({
      password: [''],
      confirm: ['']
    }, {
      validator: MustMatch('password', 'confirm')
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  protectPassword(event) {
    console.log(event, 'event');
    if (event.target.checked === true) {
      this.isPassword = true;
      this.passwordForm.get('password').setValidators([Validators.required]);
      this.passwordForm.get('confirm').setValidators([Validators.required]);
    } else {
      this.isPassword = false;
      this.passwordForm.get('password').clearValidators();
      this.passwordForm.get('password').clearValidators();
    }
  }

  fileNameChanged(e: any) {
    this.isLoading = true;
    try {
      const file = e.target.files[0];
      const fileSize = + file.size;
      const sizeFixed = (fileSize / 1048576);
      const sizeFixedValue = +sizeFixed.toFixed(1);
      if (sizeFixedValue > 1) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'File size cannot be more than 10MB' });
        this.isLoading = false;
        return;
      }
      const fReader = new FileReader();
      fReader.readAsDataURL(file);
      fReader.onloadend = (event: any) => {
        console.log(file.name);
        this.fileName = file.name;
        const fileExtension = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
        let fileTosaveName: any;
        fileTosaveName = event.target.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
        const fileObj = {
          fileName: this.fileName,
          fileUploadDate: this.fileUploadformData,
          fileType: fileExtension
        };
        this.multipleFileUpload.push(fileObj);
        this.isLoading = false;
        console.log(this.multipleFileUpload, 'fileupload');
      };
    } catch (error) {
      this.fileName = null;
      this.fileUploadformData = null;
      this.isLoading = false;
      console.log('no file was selected...');
    }
  }

  clearDocument(i) {
    this.multipleFileUpload = this.multipleFileUpload.filter((item, index) => index !== i);
  }

  get f() { return this.passwordForm.controls; }

  uploadDocument() {
    this.submitted = true;
    if (this.multipleFileUpload.length === 0) {
      this.messageService.showMessage({ severity: 'info', title: 'Info', body: 'Please Choose file to upload' });
      return;
    }
    if (this.passwordForm.invalid) {
      return;
    }
    const documentObj = this.multipleFileUpload.map(item => {
      return {
        employeeDocumentId: 0,
        employeeId: this.employeeId,
        filename: item.fileName,
        filepath: 'string',
        base64: item.fileUploadDate,
        fileType: item.fileType,
        isPasswordProtected: this.isPassword,
        password: this.passwordForm.value.confirm,
        comments: 'string',
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: moment(new Date()),
        updatedBy: 0,
        updatedDate: moment(new Date())
      };
    });
    const finalObj = {
      employeeDocument: documentObj
    };
    console.log(finalObj, 'obj');
    this.employeeService.uploadDocument(finalObj).subscribe(res => {
      console.log(res, 'uploadDcument');
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Document upload Successfully!' });
        this.activeModal.close(true);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      console.log(res, 'allDocument');
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
      }
    });
  }

}
