import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import * as moment from 'moment';

@Component({
  selector: 'app-ad-setup-create-edit',
  templateUrl: './ad-setup-create-edit.component.html'
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
  @Input() documentTypeId: any;
  employeeId: number;
  documentClear: boolean = false;
  fileType: string[];
  fileTypes: string;
  fileSize: number;
  localFileSize: any;
  selectedDate: Date;
  constructor(private adSetup: AdSetupService, private spinner: NgxSpinnerService,
    private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {

    this.submitted = false;
    this.fileType = ApplicationConfig.UploadFileType.AdSetup;
    this.fileTypes = this.fileType.toString();
    this.fileSize = ApplicationConfig.UploadSize.AdSetup
    this.Status = [{ id: 0, Value: "Inactive" }, { id: 1, Value: "Active" }];
    this.formInitialize();
    this.employeeId = +localStorage.getItem('employeeId');
   if (this.selectedData){
    this.adSetupForm.patchValue({
      name: this.selectedData.Name,
      description: this.selectedData.Description,
      status: this.selectedData.IsActive === false ? 0 : 1,
      image: this.selectedData.OriginalFileName,
      daterangepickerModel : this.selectedData.LaunchDate ? moment(this.selectedData.LaunchDate).format('MM-DD-YYYY') : null
    });
  
    this.fileName = this.selectedData.OriginalFileName,
      this.fileUploadformData = this.selectedData.base64
   }
   
  }

  formInitialize() {
    this.adSetupForm = this.fb.group({
      description: ['', Validators.required],
      name: ['', Validators.required],
      image: ['', Validators.required],
      status: ['',],
      daterangepickerModel : ['',Validators.required]
    });
    this.adSetupForm.patchValue({ status: 1 });
  }

  get f() {
    return this.adSetupForm.controls;
  }

  clearDocument() {
    this.fileName = null;
    this.fileUploadformData = null;
    this.documentClear = true

  }
  

  onValueChange(event) {
   this.selectedDate = event;
    if (this.selectedDate !== null) {
      this.selectedDate = event
     
    }
    else{
      this.selectedDate = null;

    }
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

      let lowercaseFileThumb = this.fileThumb.toLowerCase()
      if ((lowercaseFileThumb !== this.fileType[0].trim()) && (lowercaseFileThumb !== this.fileType[1].trim()) && (lowercaseFileThumb !== this.fileType[2].trim())) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.AdSetup.FileType + 'Allowed file types: ' + ApplicationConfig.UploadFileType.AdSetup.toString(), 'Warning!');
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


  // Add/Update Service
  submit() {
    this.submitted = true;
    if (this.adSetupForm.invalid || this.fileName === null) {
      return;
    }
    let localFileKbSize = this.localFileSize / Math.pow(1024, 1)
    let localFileKbRoundSize = +localFileKbSize.toFixed()
    if (this.fileSize < localFileKbRoundSize) {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.AdSetup.FileSize, 'Warning!');
      return;
    }
    const obj = {
      Document: {
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
      
      },
      documentType: "ADS",

    };
    const adSetupDto = {
      adSetupId: this.selectedData.AdSetupId ? this.selectedData.AdSetupId : 0,
      documentId: this.selectedData.DocumentId ? this.selectedData.DocumentId : 0,
      name: this.adSetupForm.value.name,
      description: this.adSetupForm.value.description,
      isActive: this.adSetupForm.value.status == 1 ? true : false,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
      LaunchDate: this.selectedDate ? moment(this.selectedDate).format('MM-DD-YYYY') : null
    }


    const formObj = {
      AdSetupAddDto: {
        AdSetup: adSetupDto
      },
      Document: obj,

    }

    const formEditObj = {
      AdSetupAddDto: {
        AdSetup: adSetupDto
      },
      Document: obj,
      "removeDocument": {
        "document": {
          "documentId": this.selectedData?.DocumentId,
          "documentType": this.documentTypeId,
          "fileName": "string",
          "originalFileName": "string",
          "filePath": "string",
          "base64": "string",
          "documentName": "string",
          "isActive": true,
          "isDeleted": true,
          "createdBy": 0,
          "createdDate": "2021-01-05T14:28:23.915Z",
          "updatedBy": 0,
          "updatedDate": "2021-01-05T14:28:23.915Z"
        },
        "documentType": "ADS"
      }

    }
    let objList: any = [];
    if (this.documentClear == false) {
      objList = formObj
    } else {
      objList = formEditObj

    }

    if (this.isEdit === true) {
      this.spinner.show();
      this.adSetup.updateAdSetup(objList).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.AdSetup.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.adSetupForm.reset();
          this.submitted = false;
        }
      },  (err) => {
this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.adSetup.addAdSetup(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.AdSetup.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.adSetupForm.reset();
          this.submitted = false;
        }
      },  (err) => {
        this.spinner.hide();
                this.toastr.error(MessageConfig.CommunicationError, 'Error!');
              });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
