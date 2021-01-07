import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';

@Component({
  selector: 'app-create-edit-terms-and-conditions',
  templateUrl: './create-edit-terms-and-conditions.component.html',
  styleUrls: ['./create-edit-terms-and-conditions.component.css']
})
export class CreateEditTermsAndConditionsComponent implements OnInit {

  termsForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() documentTypeId: any;
  fileName: any = null;
  isLoading: boolean;
  fileUploadformData: any = null;
  fileThumb: any = null;
  submitted: any;
  employeeId: any;
  constructor(
    private fb: FormBuilder,
    private toastr: MessageServiceToastr,
    private document: DocumentService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    if (localStorage.getItem('employeeName') !== undefined) {
      this.employeeId = +localStorage.getItem('empId');
    }
    this.formInitialize();
  }

  formInitialize() {
    this.termsForm = this.fb.group({
      createdDate: [''],
      createdName: [''],
      uploadBy: ['', Validators.required]
    });
  }

  fileNameChanged(e: any) {
    this.isLoading = true;
    try {
      const file = e.target.files[0];
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
        this.isLoading = false;
      };
    } catch (error) {
      this.fileName = null;
      this.fileUploadformData = null;
      this.isLoading = false;
      console.log('no file was selected...');
    }
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb = null;
    this.fileUploadformData = null;
  }

  submit() {
    this.submitted = true;
    if (this.fileName === null) {
      return;
    }
    const obj = {
      documentId: 0,
      documentType: this.documentTypeId,
      fileName: this.fileName,
      originalFileName: null,
      filePath: null,
      base64: this.fileUploadformData,
      comments: null,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date()
    };
    const finalObj = {
      document: obj,
      documentType: "TERMSANDCONDITION"
    };
    this.spinner.show();
    this.document.addDocument(finalObj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Saved Successfully' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
        this.submitted = false;
      }
    });
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

}
