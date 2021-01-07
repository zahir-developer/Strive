import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import * as moment from 'moment';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-create-edit-employee-hand-book',
  templateUrl: './create-edit-employee-hand-book.component.html',
  styleUrls: ['./create-edit-employee-hand-book.component.css']
})
export class CreateEditEmployeeHandBookComponent implements OnInit {

  handbookSetupForm: FormGroup;
  prodType: any;
  size: any;
  Status: any;
  Vendor: any;
  locationName: any;
  isChecked: boolean;
  @Input() documentTypeId: any;

  @Output() closeDialog = new EventEmitter();
  @Output() getDocumentType = new EventEmitter();

  submitted: boolean;
  selectedProduct: any;
  textDisplay: boolean;
  fileName: any = null;
  isLoading: boolean;
  fileUploadformData: any = null;
  fileThumb: any = null;
  createdDate: any;
  headerName: string;
  employeeId: any;
  constructor(
    private fb: FormBuilder,
    private toastr: MessageServiceToastr,
    private document: DocumentService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    if (localStorage.getItem('employeeName') !== undefined) {
      this.headerName = localStorage.getItem('employeeName');
      this.employeeId = +localStorage.getItem('empId');

    }
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;

  }

  formInitialize() {
    this.handbookSetupForm = this.fb.group({
      createdDate: [''],
      name: ['', Validators.required],
      createdName: [''],
      uploadBy: ['', Validators.required]
    });
    this.handbookSetupForm.patchValue({ status: 0 });
  }





  get f() {
    return this.handbookSetupForm.controls;
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


  submit() {
    this.submitted = true;
    if (this.fileName === null) {
      return;
    }
    const obj = {
      documentId: 0,
      DocumentName: this.handbookSetupForm.controls['name'].value,
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
      documentType: "EMPLOYEEHANDBOOK"
    };
    this.spinner.show();
    this.document.addDocument(finalObj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Saved Successfully' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.getDocumentType.emit();
        this.isLoading = false;
      } else {
        this.isLoading = false;
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
        this.submitted = false;
      }
    });
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb = null;
    this.fileUploadformData = null;
    this.handbookSetupForm.controls['createdDate'].setValue('');
    this.handbookSetupForm.controls['createdName'].setValue('');

  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

