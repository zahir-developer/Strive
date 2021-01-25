import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-create-edit-terms-and-conditions',
  templateUrl: './create-edit-terms-and-conditions.component.html',
  styleUrls: ['./create-edit-terms-and-conditions.component.css']
})
export class CreateEditTermsAndConditionsComponent implements OnInit {

  termsForm:FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() documentTypeId:any;
  fileName: any = null;
  isLoading: boolean;
  fileUploadformData: any = null;
  fileThumb: any = null;
  submitted: any;
  employeeId: any;
  subdocumentType: any;
  fileType: any;
  fileSize: number;
  localFileSize: any;
  constructor(private fb:FormBuilder, private toastr: MessageServiceToastr,
    private spinner: NgxSpinnerService,

     private document:DocumentService, private getCode: GetCodeService) { }

  ngOnInit() : void {
    this.fileType = ApplicationConfig.UploadFileType.TermsAndCondition;
    this.fileSize = ApplicationConfig.UploadSize.TermsAndCondition
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
    this.getCode.getCodeByCategory("DocumentSubType").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
        this.subdocumentType = dType.Codes;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }
  formInitialize() {
    this.termsForm = this.fb.group({
      createdDate: [''],
      createdName: [''],
      uploadBy:['',Validators.required],
      subDocumentId :['']
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
       
        if(this.fileThumb.toLowerCase() == this.fileType[0]){
          fileTosaveName = fileReader.result?.split(',')[1];
      }
      else{
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Upload Pdf Only' });
        this.clearDocument();
      }
        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;

      }, 500);
    }
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb= null;
    this.fileUploadformData = null;
   
  }

  submit(){
    this.submitted = true;    
    if(this.fileName === null){   
      return;
    }
let localFileKbSize =   this.localFileSize / Math.pow(1024,1)
let localFileKbRoundSize = +localFileKbSize.toFixed()
    if(this.fileSize < localFileKbRoundSize){
      this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Maximum File Size 5MB' });

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
      DocumentSubType : this.termsForm.controls['subDocumentId'].value
    };
    const finalObj = {
      document:obj,
      documentType:"TERMSANDCONDITION"
    };
this.spinner.show();
    this.document.addDocument(finalObj).subscribe(data => {
      this.spinner.hide();
      this.isLoading = false;

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
