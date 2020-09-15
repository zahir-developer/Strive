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

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;
      const fileExtension = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
      let fileReader: any;
      fileReader = new FileReader();
      fileReader.onload = function (fileLoadedEventTigger) {
        let textAreaFileContents: any;
        textAreaFileContents = document.getElementById('customFile');
        textAreaFileContents.innerHTML = fileLoadedEventTigger.target.result;
      };
      fileReader.readAsDataURL(fileToLoad);
      this.isLoading = true;
      setTimeout(() => {
        let fileTosaveName: any;
        fileTosaveName = fileReader.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
        const fileObj = {
          fileName: this.fileName,
          fileUploadDate: this.fileUploadformData,
          fileType: fileExtension
        };
        this.multipleFileUpload.push(fileObj);
        this.isLoading = false;
        console.log(this.multipleFileUpload, 'fileupload');
      }, 5000);
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
