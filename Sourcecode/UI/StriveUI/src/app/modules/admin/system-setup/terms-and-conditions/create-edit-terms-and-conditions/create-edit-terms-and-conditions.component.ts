import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-create-edit-terms-and-conditions',
  templateUrl: './create-edit-terms-and-conditions.component.html'
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
  subdocumentType: any;
  fileType: string[];
  fileTypes: string;
  fileSize: number;
  localFileSize: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private document: DocumentService, private getCode: GetCodeService, private spinner: NgxSpinnerService,
    private codeService: CodeValueService) { }

  ngOnInit(): void {
    this.fileType = ApplicationConfig.UploadFileType.TermsAndCondition;
    this.fileSize = ApplicationConfig.UploadSize.TermsAndCondition;
    this.fileTypes = this.fileType.toString();

    if (localStorage.getItem('employeeName') !== undefined) {
      this.employeeId = +localStorage.getItem('empId');
    }
    this.formInitialize();
    this.getDocumentType();
  }
  get f() {
    return this.termsForm.controls;
  }

  getDocumentType() {
    const documentSubTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.documentSubType);
    if (documentSubTypeValue.length > 0) {
      this.subdocumentType = documentSubTypeValue.Codes;
    } else {
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
  }
  formInitialize() {
    this.termsForm = this.fb.group({
      createdDate: [''],
      createdName: [''],
      uploadBy: ['', Validators.required],
      name: ['', Validators.required],
      subDocumentId: ['']
    });
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.localFileSize = fileToLoad.size
      this.fileName = fileToLoad.name;
      this.fileThumb = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);

      if (this.fileThumb.toLowerCase() !== this.fileType[0]) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.TermsCondition.FileType + 'Allowed file types: ' + ApplicationConfig.UploadFileType.TermsAndCondition.toString(), 'Warning!');

        this.clearDocument();
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
    if (this.termsForm.invalid) {

      return;
    }
    let localFileKbSize = this.localFileSize / Math.pow(1024, 1)
    let localFileKbRoundSize = +localFileKbSize.toFixed()
    if (this.fileSize < localFileKbRoundSize) {
      this.toastr.error(MessageConfig.Admin.SystemSetup.TermsCondition.FileSize, 'Error!');

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
      updatedDate: new Date(),
      DocumentName: this.termsForm.controls['name'].value,
      DocumentSubType: this.termsForm.controls['subDocumentId'].value
    };
    const finalObj = {
      document: obj,
      documentType: "TERMSANDCONDITION"
    };
    this.spinner.show();
    this.document.addDocument(finalObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.TermsCondition.Add, 'Success!');
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.submitted = false;
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

}
