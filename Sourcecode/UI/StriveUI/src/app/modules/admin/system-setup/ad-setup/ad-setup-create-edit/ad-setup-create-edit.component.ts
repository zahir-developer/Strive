import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private adSetup: AdSetupService,
     private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.Status = [{id : 0,Value :"Active"}, {id :1 , Value:"Inactive"}];
    this.formInitialize();
    this.submitted = false;
    this.employeeId = +localStorage.getItem('employeeId');

  }

  formInitialize() {
    this.adSetupForm = this.fb.group({
      description: ['', Validators.required],
      name: ['', Validators.required],
      image: ['', Validators.required],
      status: ['',],
    });
    this.adSetupForm.patchValue({status : 0});
  }

  get f() {
    return this.adSetupForm.controls;
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
    this.fileUploadformData = null;
  this.adSetupForm.controls['image'].setValue('');

  }
  // Get Service By Id
  getServiceById() {
    this.adSetup.getAdSetupById(this.selectedData.ServiceId).subscribe(data => {
      if (data.status === "Success") {
        const sType = JSON.parse(data.resultData);
        this.selectedService = sType.ServiceSetup;
        this.adSetupForm.patchValue({
          name: this.selectedService.ServiceName,
          description: this.selectedService.CommisionType,
          status: this.selectedService.IsActive ? 0 : 1
        });
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

 

  


 
  // Add/Update Service
  submit() {
    this.submitted = true;
    if (this.adSetupForm.invalid || this.fileName === null) {
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
        adSetup: {
          adSetupId: 0,
          documentId: 0,
          name: this.adSetupForm.value.name,
      description: this.adSetupForm.value.description,
      isActive: this.adSetupForm.value.status == 0 ? true : false,

          isDeleted: false,
          createdBy: +localStorage.getItem('empId'),
          createdDate: new Date(),
          updatedBy: +localStorage.getItem('empId'),
          updatedDate: new Date()
        }
      }
   const AdSetupAdd = {
    Document:obj,
    AdSetupAddDto: adSetupDto

   }  
 
     const formObj = {
      AdSetupAddDto : AdSetupAdd
     }
      
       
      
    
  
    
     
    if (this.isEdit === true) {
      this.adSetup.updateAdSetup(formObj).subscribe(data => {
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
