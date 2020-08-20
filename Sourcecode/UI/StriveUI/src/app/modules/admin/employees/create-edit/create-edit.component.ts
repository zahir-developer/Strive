import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
declare var $: any;
@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit {
  sampleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() employeeRoles?: any = [];
  @Input() gender?: any;
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeId?: any;
  @Input() location?: any;
  documentDailog: boolean;
  documentForm: FormGroup;
  fileName: any = '';
  fileUploadformData: any;
  mulitselected: any;
  Status: any;
  @Input() commissionType: any;
  @Input() actionType: string;
  employeeData: any;
  personalform: FormGroup;
  emplistform: FormGroup;
  dropdownSettings: IDropdownSettings = {};
  datePickerConfig = {
    format: 'DD-MM-YYYY',
    showTwentyFourHours: true
  };
  isPersonalCollapsed = false;
  isDetailCollapsed = false;
  isDocumentCollapsed = false;
  submitted: boolean;
  ctypeLabel: any;
  multipleFileUpload: any = [];
  fileType: any;
  isLoading: boolean;
  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private toastr: ToastrService
    ) { }

  ngOnInit() {
    this.isLoading = false;
    this.ctypeLabel = 'none';
    this.Status = ['Active', 'InActive'];
    this.documentDailog = false;
    this.submitted = false;
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      address: ['', Validators.required],
      mobile: ['', Validators.required],
      immigrationStatus: ['', Validators.required],
      ssn: ['', Validators.required]
    });
    this.emplistform = this.fb.group({
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      dateOfHire: ['', Validators.required],
      hourlyRateWash: ['', Validators.required],
      hourlyRateDetail: [''],
      commission: [''],
      status: ['Active'],
      exemptions: [''],
      roles: [[]],
      location: [[]]
    });
    this.documentForm = this.fb.group({
      password: ['', Validators.required]
    });
    this.employeRole();
    this.locationDropDown();
    // this.employeeDetail();
  }

  employeeDetail() {
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        console.log(employees, 'employeDeatil');
        if (employees.EmployeeDetail.length > 0) {
          this.employeeData = employees.EmployeeDetail[0];
        }
      }
    });
  }

  employeRole() {
    this.employeeRoles = this.employeeRoles.map(item => {
      return {
        item_id: item.CodeId,
        item_text: item.CodeValue
      };
    });
    console.log(this.employeeRoles, 'employeerolesmuliti');
    this.dropdownSettings = {
      singleSelection: false,
      defaultOpen: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: false
    };
  }

  locationDropDown() {
    this.location = this.location.map(item => {
      return {
        item_id: item.LocationId,
        item_text: item.LocationName
      };
    });
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
    });
  }

  upload() {
    this.documentDailog = true;
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;
      const fileExtension = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
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
        const fileObj = {
          fileName: this.fileName,
          fileUploadDate: this.fileUploadformData,
          fileType: fileExtension
        };
        this.multipleFileUpload.push(fileObj);
        this.isLoading = false;
        console.log(this.multipleFileUpload, 'fileupload');
      }, 5000);
    }
  }

  clearDocument(i) {
    this.multipleFileUpload = this.multipleFileUpload.filter((item, index) => index !== i);
  }

  closeDocumentPopup() {
    this.documentDailog = false;
  }

  onBlurMethod() {
    $('.custom-file-input').on('change', function () {
      const fileName = $(this).val().split('\\').pop();
      $(this).siblings('.custom-file-label').addClass('selected').html(fileName);
    });
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.emplistform.controls;
  }

  saveEmployee() {
    console.log(this.emplistform, 'empdorm');
    this.submitted = true;
    if (this.personalform.invalid || this.emplistform.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRoles = [];
    const employeeAddressObj = {
      employeeAddressId: 0,
      employeeId: 0,
      address1: this.personalform.value.address,
      address2: 'string',
      phoneNumber: this.personalform.value.mobile,
      phoneNumber2: '',
      email: this.emplistform.value.emailId,
      city: 303,
      state: 48,
      zip: 'string',
      country: 38
    };
    const employeeRoleObj = this.emplistform.value.roles.map(item => {
      return {
        employeeRoleId: 0,
        employeeId: 0,
        roleId: item.item_id
      };
    });
    const employeeDetailObj = {
      employeeDetailId: 0,
      employeeId: 0,
      employeeCode: 'string',
      hiredDate: moment(this.emplistform.value.dateOfHire).format('YYYY-MM-DD'),
      PayRate: this.emplistform.value.hourlyRateWash,
      ComRate: null,
      lrt: '2020 - 08 - 06T19: 24: 48.817Z',
      exemptions: +this.emplistform.value.exemptions
    };
    const locationObj = this.emplistform.value.location.map(item => {
      return {
        employeeLocationId: 0,
        employeeId: 0,
        locationId: item.item_id
      };
    });
    const employeeObj = {
      employeeId: 0,
      firstName: this.personalform.value.firstName,
      middleName: 'string',
      lastName: this.personalform.value.lastName,
      gender: +this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: 117,
      isCitizen: true,
      alienNo: 'string',
      birthDate: '2020 - 08 - 06T19: 24: 48.817Z',
      immigrationStatus: this.personalform.value.immigrationStatus,
      isActive: true,
      isDeleted: false,
    };
    const documentObj = this.multipleFileUpload.map(item => {
      return {
        employeeDocumentId: 0,
        employeeId: 0,
        filename: item.fileName,
        filepath: 'string',
        base64: item.fileUploadDate,
        fileType: item.fileType,
        isPasswordProtected: false,
        password: 'string',
        comments: 'string',
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: moment(new Date()).format('YYYY-MM-DD'),
        updatedBy: 0,
        updatedDate: moment(new Date()).format('YYYY-MM-DD')
      };
    });
    const finalObj = {
      employee: employeeObj,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeeAddressObj,
      employeeRole: employeeRoleObj,
      employeeLocation: locationObj,
      employeeDocument: documentObj
    };
    this.employeeService.saveEmployee(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Employee Saved Successfully!' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        if(res.status == 'Fail' && res.errorMesssage !== '')
        {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: res.ErrorMesssage });
        }
        else
        {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      }
    });
  }

  pesonalCollapsed() {
    this.isPersonalCollapsed = !this.isPersonalCollapsed;
  }

  detailCollapsed() {
    this.isDetailCollapsed = !this.isDetailCollapsed;
  }

  documentCollapsed() {
    this.isDocumentCollapsed = !this.isDocumentCollapsed;
  }

  navigatePage() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getCtype(data) {
    const label = this.commissionType.filter(item => item.CodeId === Number(data));
    if (label.length !== 0) {
      this.ctypeLabel = label[0].CodeValue;
    } else {
      this.ctypeLabel = 'none';
    }
  }
}
