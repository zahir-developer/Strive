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
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-create-edit-employee-hand-book',
  templateUrl: './create-edit-employee-hand-book.component.html'
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
@Input() actionType: any;
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
  fileTypes: string;
  fileSize: number;
  localFileSize: any;
  subdocumentType: any;
  rollList: any;
  @Input() selectedData?: any;
  base64: any;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private toastr: ToastrService,
    private document: DocumentService,
    private spinner: NgxSpinnerService,
    private getCode: GetCodeService
  ) { }

  ngOnInit() {
    this.fileType = ApplicationConfig.UploadFileType.EmployeeHandbook;
    this.fileTypes = this.fileType.toString();
    this.fileSize = ApplicationConfig.UploadSize.EmployeeHandbook;
    if (localStorage.getItem('employeeName') !== undefined) {
      this.headerName = localStorage.getItem('employeeName');
      this.employeeId = +localStorage.getItem('empId');

    }
    this.getAllRoles()
    this.getDocumentSubType();
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;
    if (this.actionType === 'Edit') {
    debugger;
      this.handbookSetupForm.patchValue({
        name: this.selectedData?.DocumentName,
        roleId: this.selectedData?.RoleId,
        uploadBy:  this.selectedData?.FileName
      });
      this.fileName = this.selectedData?.FileName;
       this.base64 = this.selectedData?.Base64;
       this.fileUploadformData = this.selectedData?.Base64;
    }
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
    this.getCode.getCodeByCategory(ApplicationConfig.Category.documentSubType).subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
        this.subdocumentType = dType.Codes;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }





  get f() {
    return this.handbookSetupForm.controls;
  }

  fileNameChanged() {
    debugger;
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.localFileSize = fileToLoad.size
      this.fileName = fileToLoad.name;
      this.fileThumb = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);


      let lowercaseFileThumb = this.fileThumb.toLowerCase()
      if ((lowercaseFileThumb !== this.fileType[0].trim()) && (lowercaseFileThumb !== this.fileType[1].trim()) && (lowercaseFileThumb !== this.fileType[2].trim())) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.EmployeeHandBook.FileType + 'Allowed file types: ' + ApplicationConfig.UploadFileType.EmployeeHandbook.toString(), 'Warning!');
        this.clearDocument();
        return;
      }

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
        fileTosaveName = fileReader.result?.split(',')[1];

        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;

      }, 500);
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
        this.toastr.warning(MessageConfig.Admin.SystemSetup.EmployeeHandBook.nameValidation, 'Warning!');
        return;
      }
    }
    let localFileKbSize = this.localFileSize / Math.pow(1024, 1)
    let localFileKbRoundSize = +localFileKbSize.toFixed()
    if (this.fileSize < localFileKbRoundSize) {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.EmployeeHandBook.FileSize, 'Warning!');

      return;
    }
    
    const obj = {
      documentId: this.selectedData.DocumentId ? +this.selectedData.DocumentId  : 0,
      DocumentName: this.handbookSetupForm.controls['name'].value,
      DocumentSubType: null,
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
      roleId: +this.handbookSetupForm.controls['roleId'].value,
    };
    const finalObj = {
      document: obj,
      documentType: "EMPLOYEEHANDBOOK"
    };
    this.spinner.show();
    if(this.actionType == "Edit"){
      this.document.updateDocument(finalObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();
  
          this.toastr.success(MessageConfig.Admin.SystemSetup.EmployeeHandBook.Add, 'Success!');
  
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
          this.getDocumentType.emit();
          this.isLoading = false;
        } else {
          this.spinner.hide();
  
          this.isLoading = false;
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.submitted = false;
        }
      },
        (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      );
    }
  else{
    this.document.addDocument(finalObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.EmployeeHandBook.Add, 'Success!');

        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.getDocumentType.emit();
        this.isLoading = false;
      } else {
        this.spinner.hide();

        this.isLoading = false;
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.submitted = false;
      }
    },
      (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );
  }
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

