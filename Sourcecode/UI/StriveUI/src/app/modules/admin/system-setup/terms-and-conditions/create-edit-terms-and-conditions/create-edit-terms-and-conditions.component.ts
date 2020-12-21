import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';

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
  constructor(private fb:FormBuilder, private toastr: MessageServiceToastr, private document:DocumentService) { }

  ngOnInit() : void {
    if (localStorage.getItem('employeeName') !== undefined) {
      this.employeeId = localStorage.getItem('employeeId');
    }
    this.formInitialize();
  }

  formInitialize() {
    this.termsForm = this.fb.group({
      createdDate: [''],
      createdName: [''],
      uploadBy:['',Validators.required]
    }); 
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
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
        fileTosaveName = fileReader.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;
        console.log(this.fileName,this.fileUploadformData.length);
      }, 5000);
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
      document:obj,
      documentType:"TERMSANDCONDITION"
    };
    this.document.addDocument(finalObj).subscribe(data => {
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
