import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import * as moment from 'moment';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

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
  @Input() documentTypeId:any;
  
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
  employeeId: string;
  constructor(private fb: FormBuilder,
     private toastr: MessageServiceToastr, private document:DocumentService) { }

  ngOnInit() {
    if (localStorage.getItem('employeeName') !== undefined) {
      this.headerName = localStorage.getItem('employeeName');
      this.employeeId = localStorage.getItem('employeeId');

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
      uploadBy:['',Validators.required]
    });
    this.handbookSetupForm.patchValue({status : 0}); 
  }
  
  
 
  
  
  get f() {
    return this.handbookSetupForm.controls;
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;  
      const DateCreated = fileToLoad.lastModifiedDate;   
      this.createdDate = moment(DateCreated).format('l');
    this.handbookSetupForm.controls['createdDate'].setValue(this.createdDate);
    this.handbookSetupForm.controls['createdName'].setValue(this.headerName); 
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

 
  submit(){
    this.submitted = true;    
    if(this.fileName === null){   
      return;
    }
    const obj = {
      documentId: 0,
      DocumentName : this.handbookSetupForm.controls['name'].value,
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
      documentType:"EMPLOYEEHANDBOOK"
    };
    this.document.addDocument(finalObj).subscribe(data => {
      if (data.status === 'Success') {        
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Saved Successfully' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        this.getDocumentType.emit();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
        this.submitted = false;
      }
    });
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb= null;
    this.fileUploadformData = null;
  this.handbookSetupForm.controls['createdDate'].setValue('');
  this.handbookSetupForm.controls['createdName'].setValue('');

  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

