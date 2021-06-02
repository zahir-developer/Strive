import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MustMatch } from 'src/app/shared/Validator/must-match.validator';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html'
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
  fileSize: number;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.isLoading = false;
    this.isPassword = false;
    this.submitted = false;
    this.fileSize = ApplicationConfig.UploadSize.EmployeeDocument;
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
      const localFileKbSize = fileSize / Math.pow(1024, 1);
      const localFileKbRoundSize = +localFileKbSize.toFixed();
      if (this.fileSize < localFileKbRoundSize) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.EmployeeHandBook.FileSize, 'Warning!');
        this.isLoading = false;
        return;
      }
      const sizeFixed = (fileSize / 1048576);
      const sizeFixedValue = +sizeFixed.toFixed(1);
      if (sizeFixedValue > 10) {
        this.toastr.warning(MessageConfig.Document.fileSize, 'Warning!');
        this.isLoading = false;
        return;
      }
      const fReader = new FileReader();
      fReader.readAsDataURL(file);
      fReader.onloadend = (event: any) => {
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
      };
    } catch (error) {
      this.fileName = null;
      this.fileUploadformData = null;
      this.isLoading = false;
    }
  }

  clearDocument(i) {
    this.multipleFileUpload = this.multipleFileUpload.filter((item, index) => index !== i);
  }

  get f() { return this.passwordForm.controls; }

  uploadDocument() {
    this.submitted = true;
    if (this.multipleFileUpload.length === 0) {
      this.messageService.showMessage({ severity: 'info', title: 'Info', body: MessageConfig.Document.fileRequired });
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
        filepath: '',
        base64: item.fileUploadDate,
        fileType: item.fileType,
        isPasswordProtected: this.isPassword,
        password: this.passwordForm.value.confirm,
        comments: '',
        isActive: true,
        isDeleted: false,
        createdBy: this.employeeId,
        createdDate: moment(new Date()),
        updatedBy: this.employeeId,
        updatedDate: moment(new Date())
      };
    });
    const finalObj = {
      employeeDocument: documentObj
    };
    this.spinner.show();
    this.employeeService.uploadDocument(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Document.upload, 'Success!');
        this.activeModal.close(true);
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
