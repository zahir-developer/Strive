import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import * as moment from 'moment';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

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
  fileType: string[];
  fileSize: number;
  localFileSize: any;
  subdocumentType: any;
  rollList: any;
  constructor(
    private fb: FormBuilder,
    private employeeService : EmployeeService,
    private toastr: MessageServiceToastr,
    private document: DocumentService,
    private spinner: NgxSpinnerService,
    private getCode: GetCodeService
  ) { }

  ngOnInit() {
    this.fileType = ApplicationConfig.UploadFileType.EmployeeHandbook;
    this.fileSize = ApplicationConfig.UploadSize.EmployeeHandbook
    if (localStorage.getItem('employeeName') !== undefined) {
      this.headerName = localStorage.getItem('employeeName');
      this.employeeId = +localStorage.getItem('empId');

    }
    this.getAllRoles()
    this.getDocumentSubType();
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;

  }
  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.rollList = roles.EmployeeRoles
    
      }
    });
  }
  formInitialize() {
    this.handbookSetupForm = this.fb.group({
      createdDate: [''],
      name: ['', Validators.required, Validators.pattern['a-zA-Z~`\d!@#$%^&*()-_=+][a-zA-Z~`\d!@#$%^&*()-_=+\d\\s]*/']],
      createdName: [''],
      uploadBy: ['', Validators.required],
      subDocumentId: [''],
      roleId: ['', Validators.required]
    });
    this.handbookSetupForm.patchValue({ status: 0 });
  }

  getDocumentSubType() {
    this.getCode.getCodeByCategory("DocumentSubType").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
        this.subdocumentType = dType.Codes;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }





  get f() {
    return this.handbookSetupForm.controls;
  }

  // fileNameChanged(e: any) {
  //   this.isLoading = true;
  //   try {
  //     const file = e.target.files[0];
  //     const fReader = new FileReader();
  //     fReader.readAsDataURL(file);
  //     fReader.onloadend = (event: any) => {
  //       console.log(file.name);
  //       this.fileName = file.name;
  //       const fileExtension = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
  //       let fileTosaveName: any;
  //       fileTosaveName = event.target.result.split(',')[1];
  //       this.fileUploadformData = fileTosaveName;
  //       const fileObj = {
  //         fileName: this.fileName,
  //         fileUploadDate: this.fileUploadformData,
  //         fileType: fileExtension
  //       };
  //       this.isLoading = false;
  //     };
  //   } catch (error) {
  //     this.fileName = null;
  //     this.fileUploadformData = null;
  //     this.isLoading = false;
  //     console.log('no file was selected...');
  //   }
  // }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.localFileSize = fileToLoad.size
      this.fileName = fileToLoad.name;
      this.fileThumb = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
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
        let lowercaseFileThumb = this.fileThumb.toLowerCase()
        if ((lowercaseFileThumb == this.fileType[0]) || (lowercaseFileThumb == this.fileType[1]) || (lowercaseFileThumb == this.fileType[2])) {
          fileTosaveName = fileReader.result?.split(',')[1];
        }
        else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Upload DOC,DOCX,PDF Only' });
          this.clearDocument();
        }
        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;

      }, 5000);
    }
  }

  submit() {
    this.submitted = true;
    if (this.fileName === null) {
      return;
    }
    if (this.handbookSetupForm.invalid) {
      return;
    }
    const pattern = /[a-zA-Z~`\d!@#$%^&*()-_=+][a-zA-Z~`\d!@#$%^&*()-_=+\d\\s]*/;
    if (this.handbookSetupForm.controls['name'].value) {
      if (!pattern.test(this.handbookSetupForm.controls['name'].value)) {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Name is Required' });
        return;
      }
    }
    let localFileKbSize = this.localFileSize / Math.pow(1024, 1)
    let localFileKbRoundSize = +localFileKbSize.toFixed()
    if (this.fileSize < localFileKbRoundSize) {
      this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Maximum File Size 5MB' });

      return;
    }
    const obj = {
      documentId: 0,
      DocumentName: this.handbookSetupForm.controls['name'].value,
      DocumentSubType: this.handbookSetupForm.value.subDocumentId,
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
      updatedDate: new Date(),
      roleId : this.handbookSetupForm.controls['roleId'].value,
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
    }, (err) => {
      this.spinner.hide();
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

