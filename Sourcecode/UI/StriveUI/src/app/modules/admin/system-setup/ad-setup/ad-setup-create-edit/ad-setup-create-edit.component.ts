import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';

@Component({
  selector: 'app-ad-setup-create-edit',
  templateUrl: './ad-setup-create-edit.component.html',
  styleUrls: ['./ad-setup-create-edit.component.css']
})
export class AdSetupCreateEditComponent implements OnInit {

  adSetupForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  selectedService: any;
  Status: any;
  isChecked: boolean;
  today: Date = new Date();
  submitted: boolean;
  fileName: any = null;
  isLoading: boolean;
  fileUploadformData: any;
  fileThumb: any;
  @Input() documentTypeId:any;
  employeeId: number;
  documentClear: boolean = false;
  fileType: string[];
  fileSize: number;
  localFileSize: any;

  constructor(private adSetup: AdSetupService,
     private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.fileType = ApplicationConfig.UploadFileType.AdSetup;
    this.fileSize = ApplicationConfig.UploadSize.AdSetup
    this.Status = [{id : 0,Value :"Inactive"}, {id :1 , Value:"Active"}];
    this.formInitialize();
    this.submitted = false;
    this.employeeId = +localStorage.getItem('employeeId');
    this.adSetupForm.patchValue({
      name: this.selectedData.Name,
      description: this.selectedData.Description,
      status: this.selectedData.Status == false ? 0 : 1,
      image : this.selectedData.Image
    });
    this.fileName = this.selectedData.Image,
    this.fileUploadformData = this.selectedData.base64
  }

  formInitialize() {
    this.adSetupForm = this.fb.group({
      description: ['', Validators.required],
      name: ['', Validators.required],
      image: ['', Validators.required],
      status: ['',],
    });
    this.adSetupForm.patchValue({status : 1});
  }

  get f() {
    return this.adSetupForm.controls;
  }
  
  clearDocument() {
    this.fileName = null;
    this.fileUploadformData = null;
    this.documentClear = true

  }
  // Get Service By Id
  getServiceById() {
    this.adSetup.getAdSetupById(this.selectedData.ServiceId).subscribe(data => {
      if (data.status === "Success") {
        const sType = JSON.parse(data.resultData);
        this.selectedService = sType.AdSetup;
       
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
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
      let lowercaseFileThumb = this.fileThumb.toLowerCase()
        if( (lowercaseFileThumb == this.fileType[0]) ||(lowercaseFileThumb == this.fileType[1]) || (lowercaseFileThumb == this.fileType[2]) ){
          fileTosaveName = fileReader.result?.split(',')[1];
      }
      else{
        this.toastr.error( 'Upload Image Only' );
        this.clearDocument();
      }
        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;

      }, 5000);
    }
  }

 
  // Add/Update Service
  submit() {
    this.submitted = true;
    if (this.adSetupForm.invalid || this.fileName === null) {
      return;
    }
    let localFileKbSize =   this.localFileSize / Math.pow(1024,1)
    let localFileKbRoundSize = +localFileKbSize.toFixed()
        if(this.fileSize < localFileKbRoundSize){
          this.toastr.error('Maximum Image Size 5MB' );
    
          return;
        }
    const obj = { Document :{
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
     },
     documentType:"ADS",

    };
   const  adSetupDto= {
          adSetupId: this.selectedData.AdSetupId ? this.selectedData.AdSetupId: 0,
          documentId: this.selectedData.DocumentId ? this.selectedData.DocumentId : 0,
          name: this.adSetupForm.value.name,
      description: this.adSetupForm.value.description,
      isActive: this.adSetupForm.value.status == 1 ? true : false,
      DocumentSubType: null,
          isDeleted: false,
          createdBy: +localStorage.getItem('empId'),
          createdDate: new Date(),
          updatedBy: +localStorage.getItem('empId'),
          updatedDate: new Date()
        
      }
   
 
     const formObj = {
      AdSetupAddDto : {
        AdSetup: adSetupDto
      },
      Document:obj,

     }
      
     const formEditObj = {
      AdSetupAddDto : {
        AdSetup: adSetupDto
      },
      Document:obj,
      "removeDocument": {
        "document": {
          "documentId": this.selectedData?.DocumentId,
          "documentType": this.documentTypeId,
          "fileName": "",
          "originalFileName": "",
          "filePath": "",
          "base64": "",
          "documentName": "",
          "isActive": true,
          "isDeleted": true,
          "createdBy": this.employeeId,
          "createdDate": new Date(),
          "updatedBy": this.employeeId,
          "updatedDate": new Date()
        },
        "documentType": "ADS"
      }
      
     }
  let objList : any = [];
if (this.documentClear == false){
  objList = formObj
}   else{
  objList = formEditObj

} 
     
    if (this.isEdit === true) {
      this.adSetup.updateAdSetup(objList).subscribe(data => {
        if (data.status === 'Success') {   
          this.toastr.success('Record Updated Successfully!!', 'Success!');     
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.adSetupForm.reset();
          this.submitted = false;
        }
      });
    } else {
      this.adSetup.addAdSetup(formObj).subscribe(data => {
        if (data.status === 'Success') { 
          this.toastr.success('Record Saved Successfully!!', 'Success!');       
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.adSetupForm.reset();
          this.submitted = false;
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
